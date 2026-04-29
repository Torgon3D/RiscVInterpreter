using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RiscVInterpreterEngine;

public struct RiscVCommand
{
    public RiscVCommand(RiscVInstruction instr, RiscVArguments args, int lineNumber, string lineText, int instructionNumber)
    {
        Instr = instr;
        Args = args;
        LineNumber = lineNumber;
        InstructionNumber = instructionNumber;
        
        CommandInfo = Regex.Replace(lineText.Trim(), @"\s+", " ");
    }
    
    public RiscVInstruction Instr;
    public RiscVArguments Args;
    public int LineNumber;
    public int InstructionNumber;
    public string CommandInfo;
    
    static Dictionary<string, byte> _roundingModes = new()
    {
        { "RNE", 0b000 },
        { "RTZ", 0b001 },
        { "RDN", 0b010 },
        { "RUP", 0b011 },
        { "RMM", 0b100 },
    };
    
    public void Run()
    {
        Instr.RunFunction(Args);
    }
    
    public void ParseArgument(string arg, EArgumentTypes argType, Dictionary<string, int> consts, Dictionary<string, int> labels)
    {
        arg = arg.Trim();
        switch (argType)
        {
        case EArgumentTypes.RD:
            Args.rd = ParseRegisterInt(arg);
            break;
            
        case EArgumentTypes.RS1:
            Args.rs1 = ParseRegisterInt(arg);
            break;
            
        case EArgumentTypes.RS2:
            Args.rs2 = ParseRegisterInt(arg);
            break;
            
        case EArgumentTypes.IMM:
            ParseImmidiate(arg, consts);
            break;
            
        case EArgumentTypes.FD:
            Args.rd = ParseRegisterFloat(arg);
            break;
            
        case EArgumentTypes.FS1:
            Args.rs1 = ParseRegisterFloat(arg);
            break;
            
        case EArgumentTypes.FS2:
            Args.rs2 = ParseRegisterFloat(arg);
            break;
            
        case EArgumentTypes.FR3:
            Args.frs3 = ParseRegisterFloat(arg);
            break;
            
        case EArgumentTypes.MEMORY:
            ParseMemory(arg, consts);
            break;
            
        case EArgumentTypes.LABEL:
            ParseLabel(arg, labels);
            break;
            
        case EArgumentTypes.ROUNDING:
            ParseRounding(arg);
            break;
            
        default:
            return;
        }
    }
    
    private int ParseRegisterInt(string regName)
    {
        int registerLoc;
        if (!Register.IntegerRegisterNames.TryGetValue(regName, out registerLoc))
        {
            throw new WrongRegisterException();
        }
        
        return registerLoc;
    }
    
    private int ParseRegisterFloat(string regName)
    {
        int registerLoc;
        if (!Register.FloatRegisterNames.TryGetValue(regName, out registerLoc))
        {
            throw new WrongRegisterException();
        }
        
        return registerLoc;
    }
    
    private void ParseImmidiate(string immidiate, Dictionary<string, int> consts)
    {
        int immSize;
        int immStart;
        int imm;
        bool mustBePositive = false;
        bool lsbMustBeZero = false;
            
        if (Instr.InstructionFormat == EInstructionFormat.I
            || Instr.InstructionFormat == EInstructionFormat.S)
        {
            // Incase of logical shifts
            if (Instr.Opcode == 0b0010011 && (Instr.Funct3 == 0b001 || Instr.Funct3 == 0b101))
            {
                immSize = 5;
                immStart = 0;
                mustBePositive = true;
            }
            else
            {
                immSize = 12;
                immStart = 0;
            }
        }
        else if (Instr.InstructionFormat == EInstructionFormat.U)
        {
            immSize = 20;
            immStart = 12;
        }
        else if (Instr.InstructionFormat == EInstructionFormat.B)
        {
            immSize = 13;
            immStart = 0;
            lsbMustBeZero = true;
        }
        else if (Instr.InstructionFormat == EInstructionFormat.J)
        {
            immSize = 21;
            immStart = 0;
            lsbMustBeZero = true;
        }
        else
        {
            throw new WrongImmidiateFormatException();
        }
        
        if (immidiate[0] == '%')
        {
            bool high;
            
            if (immidiate.Substring(1, 2) == "lo" && immSize == 12 && (immidiate[3] == ' ' || immidiate[3] == '\t' || immidiate[3] == '('))
            {
                high = false;
            }
            else if (immidiate.Substring(1, 2) == "hi" && immSize == 20 && (immidiate[3] == ' ' || immidiate[3] == '\t' || immidiate[3] == '('))
            {
                high = true;
            }
            else
            {
                throw new WrongImmidiateFormatException();
            }
            
            if (high) {Debug.Print("High");} else {Debug.Print("low");}
            
            string valueString = immidiate.Substring(3, immidiate.Length - 3).Trim();
            string labelToFind;
            
            if (valueString[0] == '(' && valueString[valueString.Length-1] == ')')
            {
                labelToFind = valueString.Substring(1, valueString.Length-2).Trim();
            }
            else
            {
                labelToFind = valueString;
            }
            
            if (!consts.TryGetValue(labelToFind, out imm))
            {
                throw new NoLabelForConstantException();
            }
        }
        else
        {
            NumberStyles format = NumberStyles.Integer;
            string immSubstring;
            bool isOctal = false;
            if (immidiate[0] == '0')
            {
                if (immidiate[1] == 'x' || immidiate[1] == 'X')
                {
                    format = NumberStyles.HexNumber;
                    immSubstring = immidiate.Substring(2);
                }
                else if (immidiate[1] == 'b' || immidiate[1] == 'B')
                {
                    format = NumberStyles.BinaryNumber;
                    immSubstring = immidiate.Substring(2);
                }
                else
                {
                    isOctal = true;
                    immSubstring = immidiate;
                }
            }
            else
            {
                immSubstring = immidiate;
            }
            
            if (isOctal)
            {
                try { imm = Convert.ToInt32(immidiate, 8); }
                    catch { throw new CouldNotParseToIntException(); }
            }
            else
            {
                if (!int.TryParse(immSubstring, format, CultureInfo.CurrentCulture, out imm))
                {
                    throw new CouldNotParseToIntException();
                }
            }
        }
        
        if (!RiscVBitTools.IsBitsWithinBounds(imm, immSize))
        {
            throw new WrongImmidiateSizeException();
        }
        
        if (mustBePositive && imm < 0)
        {
            throw new WrongImmidiateFormatException();
        }
        
        if (lsbMustBeZero && (imm & 1) != 0)
        {
            throw new WrongImmidiateFormatException();
        }
        
        Args.imm = imm << immStart;
    }
    
    private void ParseMemory(string memoryAdress, Dictionary<string, int> consts)
    {
        // Will have to split imm and register
        
        Match memorySplit = Regex.Match(memoryAdress, @"\([A-Za-z0-9]+\)$");
        
        if (memorySplit.Success)
        {
            Args.rs1 = ParseRegisterInt(memoryAdress.Substring(memorySplit.Index + 1, memorySplit.Index + memorySplit.Length - 3).Trim());
            ParseImmidiate(memoryAdress.Substring(0, memorySplit.Index), consts);
        }
        else
        {
            throw new WrongMemoryArgumentException();
        }
    }
    
    private void ParseLabel(string labelName, Dictionary<string, int> labels)
    {
        int bitlength = -1;
        if (Instr.InstructionFormat == EInstructionFormat.B)
        {
            bitlength = 13;
        }
        else if (Instr.InstructionFormat == EInstructionFormat.J)
        {
            bitlength = 21;
        }
        
        int offset, location;
        if (!labels.TryGetValue(labelName, out location))
        {
            throw new WrongLabelException();
        }
        
        offset = (location - InstructionNumber) * 4;
        
        if (RiscVBitTools.IsBitsWithinBounds(offset, bitlength))
        
        Args.imm = offset;
    }
    
    private void ParseRounding(string roundingMode)
    {
        byte roundValue;
        if (!_roundingModes.TryGetValue(roundingMode, out roundValue))
        {
            throw new WrongRoundingModeException();
        }
        
        Args.rm = roundValue;
    }
}
