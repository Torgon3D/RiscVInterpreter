using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace RiscVInterpreterEngine;

public struct RiscVCommand
{
    public RiscVCommand(RiscVInstruction instr, RiscVArguments args, int lineNumber, int instructionNumber)
    {
        Instr = instr;
        Args = args;
        LineNumber = lineNumber;
        InstructionNumber = instructionNumber;
        
        InstructionInfo = instr.InstructionInfo;
        
        if (args.rd != null)
        {
            InstructionInfo += $", rd={args.rd}";
        }
        if (args.rs1 != null)
        {
            InstructionInfo += $", rs1={args.rs1}";
        }
        if (args.rs2 != null)
        {
            InstructionInfo += $", rs2={args.rs2}";
        }
        if (args.imm != null)
        {
            InstructionInfo += $", imm={args.imm}";
        }
    }
    
    public RiscVInstruction Instr;
    public RiscVArguments Args;
    public int LineNumber;
    public int InstructionNumber;
    public string InstructionInfo;
    
    static Dictionary<string, byte> _roundingModes = new()
    {
        { "RNE", 0b000 },
        { "RTZ", 0b001 },
        { "RDN", 0b010 },
        { "RUP", 0b011 },
        { "RMM", 0b100 },
    };
    
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
            
        case EArgumentTypes.FRD:
            Args.rd = ParseRegisterFloat(arg);
            break;
            
        case EArgumentTypes.FR1:
            Args.rs1 = ParseRegisterFloat(arg);
            break;
            
        case EArgumentTypes.FR2:
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
            
        if (Instr.InstructionFormat == EInstructionFormat.I
            || Instr.InstructionFormat == EInstructionFormat.S
            || Instr.InstructionFormat == EInstructionFormat.B)
        {
            immSize = 12;
            immStart = 0;
        }
        else if (Instr.InstructionFormat == EInstructionFormat.U
            || Instr.InstructionFormat == EInstructionFormat.J)
        {
            immSize = 20;
            immStart = 12;
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
            
            if (!RiscVBitTools.IsBitsWithinBounds(imm, immSize))
            {
                throw new WrongImmidiateSizeException();
            }
            else ; // TODO add check for upper immidiate
            
            Debug.Print("imm became " + imm);
            
            Args.imm = imm;
        }
        else
        {
            if (!int.TryParse(immidiate, out imm))
            {
                throw new CouldNotParseToIntException();
            }
            
            if (!RiscVBitTools.IsBitsWithinBounds(imm, immSize))
            {
                throw new WrongImmidiateSizeException();
            }
            
            Args.imm = imm;
        }
    }
    
    private void ParseMemory(string memoryAdress, Dictionary<string, int> consts)
    {
        // Will have to split imm and register
        
        Match memorySplit = Regex.Match(memoryAdress, @"\([A-Za-z0-9]+\)$");
        
        if (memorySplit.Success)
        {
            Args.rs1 = ParseRegisterInt(memoryAdress.Substring(memorySplit.Index + 1, memorySplit.Index + memorySplit.Length - 2));
            ParseImmidiate(memoryAdress.Substring(0, memorySplit.Index-1), consts);
        }
        else
        {
            throw new WrongMemoryArgumentException();
        }
    }
    
    private void ParseLabel(string labelName, Dictionary<string, int> labels)
    {
        int offset, location;
        if (!labels.TryGetValue(labelName, out location))
        {
            throw new WrongLabelException();
        }
        
        offset = location - InstructionNumber;
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
    
    public void PrintParse()
    {
        
    }
}
