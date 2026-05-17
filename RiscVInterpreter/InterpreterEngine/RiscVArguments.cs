using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Reflection.Emit;
using System.Text.RegularExpressions;

namespace RiscVInterpreterEngine;

public enum EArgumentTypes : short
{
    RD = 1,
    RS1,
    RS2,
    IMM,
    FD,
    FS1,
    FS2,
    FR3,
    MEMORY,
    LABEL,
    ROUNDING
}

public enum ERoundingModes : byte
{
    RNE = 0b000,
    RTZ = 0b001,
    RDN = 0b010,
    RUP = 0b011,
    RMM = 0b100
}

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm, fs3;
    public ERoundingModes rm;
    
    static Dictionary<string, ERoundingModes> _roundingModes = new()
    {
        { "RNE", ERoundingModes.RNE },
        { "RTZ", ERoundingModes.RTZ },
        { "RDN", ERoundingModes.RDN },
        { "RUP", ERoundingModes.RUP },
        { "RMM", ERoundingModes.RMM },
    };
    
    public RiscVArguments()
    {
        rm = ERoundingModes.RNE;
    }
    
    public void HasArgumentsFilled(EArgumentTypes[] arguments)
    {
        foreach (var arg in arguments)
        {
            switch (arg)
            {
            case EArgumentTypes.RD:
                if (rd != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.RS1:
                if (rs1 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.RS2:
                if (rs2 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.IMM:
                if (imm != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FD:
                if (rd != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FS1:
                if (rs1 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FS2:
                if (rs2 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FR3:
                if (fs3 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.MEMORY:
                if (rs1 != null && imm != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.LABEL:
                if (imm != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.ROUNDING:
                if (rm != null)
                {
                    continue;
                }
                break;
                
            default:
                break;
            }
            
            throw new ArgNotSetException();
        }
    }
    
    public Register GetRD(MemoryController mem)
    {
        if (rd != null)
        {
            return mem.IntegerRegisters[(int)rd];
        }
        else
        {
            throw new ArgNotSetException($" Argument: RD");
        }
    }
    
    public Register GetRS1(MemoryController mem)
    {
        if (rs1 != null)
        {
            return mem.IntegerRegisters[(int)rs1];
        }
        else
        {
            throw new ArgNotSetException($" Argument: RS1");
        }
    }
    
    public Register GetRS2(MemoryController mem)
    {
        if (rs2 != null)
        {
            return mem.IntegerRegisters[(int)rs2];
        }
        else
        {
            throw new ArgNotSetException($" Argument: RS2");
        }
    }
    
    public Register GetFD(MemoryController mem)
    {
        if (rd != null)
        {
            return mem.FloatRegisters[(int)rd];
        }
        else
        {
            throw new ArgNotSetException($" Argument: FD");
        }
    }
    
    public Register GetFS1(MemoryController mem)
    {
        if (rs1 != null)
        {
            return mem.FloatRegisters[(int)rs1];
        }
        else
        {
            throw new ArgNotSetException($" Argument: FS1");
        }
    }
    
    public Register GetFS2(MemoryController mem)
    {
        if (rs2 != null)
        {
            return mem.FloatRegisters[(int)rs2];
        }
        else
        {
            throw new ArgNotSetException($" Argument: FS2");
        }
    }
    
    public Register GetFS3(MemoryController mem)
    {
        if (fs3 != null)
        {
            return mem.FloatRegisters[(int)fs3];
        }
        else
        {
            throw new ArgNotSetException($" Argument: FS3");
        }
    }
    
    public int GetIMM()
    {
        if (imm != null)
        {
            return (int)imm;
        }
        else
        {
            throw new ArgNotSetException($" Argument: imm");
        }
    }
    
    public uint GetIMMU()
    {
        if (imm != null)
        {
            return unchecked((uint)imm);
        }
        else
        {
            throw new ArgNotSetException($" Argument: imm");
        }
    }
    
    public ERoundingModes GetRM()
    {
        return rm;
    }
    
    public int GetJumpAmount()
    {
        if (imm == null)
        {
            throw new ArgNotSetException($" Argument: imm");
        }
        
        if (imm % 4 != 0)
        {
            throw new WrongImmidiateFormatException();
        }
        
        return (int)imm / 4;
    }
    
    
    public void ParseArgument(string arg, RiscVInstruction instr, int instructionLocation, EArgumentTypes argType, Dictionary<string, int> consts, Dictionary<string, int> labels)
    {
        arg = arg.Trim();
        switch (argType)
        {
        case EArgumentTypes.RD:
            rd = ParseRegisterInt(arg, instr);
            break;
            
        case EArgumentTypes.RS1:
            rs1 = ParseRegisterInt(arg, instr);
            break;
            
        case EArgumentTypes.RS2:
            rs2 = ParseRegisterInt(arg, instr);
            break;
            
        case EArgumentTypes.IMM:
            ParseImmidiate(arg, consts, instr);
            break;
            
        case EArgumentTypes.FD:
            rd = ParseRegisterFloat(arg, instr);
            break;
            
        case EArgumentTypes.FS1:
            rs1 = ParseRegisterFloat(arg, instr);
            break;
            
        case EArgumentTypes.FS2:
            rs2 = ParseRegisterFloat(arg, instr);
            break;
            
        case EArgumentTypes.FR3:
            fs3 = ParseRegisterFloat(arg, instr);
            break;
            
        case EArgumentTypes.MEMORY:
            ParseMemory(arg, consts, instr);
            break;
            
        case EArgumentTypes.LABEL:
            ParseLabel(arg, labels, instr, instructionLocation);
            break;
            
        case EArgumentTypes.ROUNDING:
            ParseRounding(arg, instr);
            break;
            
        default:
            return;
        }
    }
    
    private int ParseRegisterInt(string regName, RiscVInstruction Instr)
    {
        int registerLoc;
        if (!Register.IntegerRegisterNames.TryGetValue(regName, out registerLoc))
        {
            throw new IncorrectRegisterException($" Int Register: {regName}");
        }
        
        return registerLoc;
    }
    
    private int ParseRegisterFloat(string regName, RiscVInstruction Instr)
    {
        int registerLoc;
        if (!Register.FloatRegisterNames.TryGetValue(regName, out registerLoc))
        {
            throw new IncorrectRegisterException($" Float Register: {regName}");
        }
        
        return registerLoc;
    }
    
    private void ParseImmidiate(string immidiate, Dictionary<string, int> consts, RiscVInstruction instr)
    {
        int immSize;
        int immStart;
        int immTemp;
        bool mustBePositive = false;
        bool lsbMustBeZero = false;
        bool Upper = false;
            
        if (instr.InstructionFormat == EInstructionFormat.I
            || instr.InstructionFormat == EInstructionFormat.S)
        {
            // Incase of logical shifts
            if (instr.Opcode == 0b0010011 && (instr.Funct3 == 0b001 || instr.Funct3 == 0b101))
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
        else if (instr.InstructionFormat == EInstructionFormat.U)
        {
            immSize = 32;//20;
            immStart = 0;
            Upper = true;
        }
        else if (instr.InstructionFormat == EInstructionFormat.B)
        {
            immSize = 13;
            immStart = 0;
            lsbMustBeZero = true;
        }
        else if (instr.InstructionFormat == EInstructionFormat.J)
        {
            immSize = 21;
            immStart = 0;
            lsbMustBeZero = true;
        }
        else
        {
            throw new WrongImmidiateFormatException();
        }
        
        bool TrimHigh = false;
        bool TrimLow = false;
        if (immidiate[0] == '%')
        {
            if (immidiate.Substring(1, 2) == "lo" && immSize == 12 && (immidiate[3] == ' ' || immidiate[3] == '\t' || immidiate[3] == '('))
            {
                TrimLow = true;
            }
            else if (immidiate.Substring(1, 2) == "hi" && immSize >= 20 && (immidiate[3] == ' ' || immidiate[3] == '\t' || immidiate[3] == '('))
            {
                TrimHigh = true;
            }
            else
            {
                throw new WrongImmidiateFormatException();
            }
            
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
            
            if (!consts.TryGetValue(labelToFind, out immTemp))
            {
                throw new CouldNotFindConstantLabelException($" Label: {labelToFind}");
            }
        }
        else
        {
            NumberStyles format = NumberStyles.Integer;
            string immSubstring;
            bool isOctal = false;
            if (immidiate[0] == '0' && immidiate.Length > 1)
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
                try { immTemp = Convert.ToInt32(immidiate, 8); }
                    catch { throw new CouldNotParseImmToIntException($" Immidiate: immSubstring"); }
            }
            else
            {
                if (!int.TryParse(immSubstring, format, CultureInfo.CurrentCulture, out immTemp))
                {
                    throw new CouldNotParseImmToIntException($" Immidiate: immSubstring");
                }
            }
        }
        
        if (TrimHigh)
        {
            immTemp = immTemp & (-1 << 12);
        }
        else if (TrimLow)
        {
            immTemp = RiscVBitTools.GetBitsAndSignWithinBounds(immTemp, 0, 12, 0);
        }
        
        if (!RiscVBitTools.IsBitsWithinBounds(immTemp, immSize) && !TrimLow)
        {
            throw new ImmidiateNotWithinBoundsException($" Immidiate: {immTemp}");
        }
        
        if (mustBePositive && immTemp < 0)
        {
            throw new ImmidateIsNotPositiveException($" Immidiate: {immTemp}");
        }
        
        if (lsbMustBeZero && (immTemp & 1) != 0)
        {
            throw new Immidiate13Exception($" Immidiate: {immTemp}");
        }
        
        if (Upper)
        {
            immTemp = immTemp & (-1 << 12);
        }
        
        imm = immTemp << immStart;
    }
    
    private void ParseMemory(string memoryAdress, Dictionary<string, int> consts, RiscVInstruction instr)
    {
        // Will have to split imm and register
        
        Match memorySplit = Regex.Match(memoryAdress, @"\([A-Za-z0-9]+\)$");
        
        if (memorySplit.Success)
        {
            rs1 = ParseRegisterInt(memoryAdress.Substring(memorySplit.Index + 1, memorySplit.Index + memorySplit.Length - 3).Trim(), instr);
            ParseImmidiate(memoryAdress.Substring(0, memorySplit.Index), consts, instr);
        }
        else
        {
            throw new IncorrectMemoryArgumentSyntaxException($" Memory adress: {memoryAdress}");
        }
    }
    
    private void ParseLabel(string labelName, Dictionary<string, int> labels, RiscVInstruction instr, int instructionLocation)
    {
        int bitlength = -1;
        if (instr.InstructionFormat == EInstructionFormat.B)
        {
            bitlength = 13;
        }
        else if (instr.InstructionFormat == EInstructionFormat.J)
        {
            bitlength = 21;
        }
        
        int offset, location;
        if (!labels.TryGetValue(labelName, out location))
        {
            throw new LabelNameNotFoundException($" Label: {labelName}");
        }
        
        offset = (location - instructionLocation) * 4;
        
        if (RiscVBitTools.IsBitsWithinBounds(offset, bitlength))
        
        imm = offset;
    }
    
    private void ParseRounding(string roundingMode, RiscVInstruction Instr)
    {
        ERoundingModes roundValue;
        if (!_roundingModes.TryGetValue(roundingMode, out roundValue))
        {
            throw new WrongRoundingModeException($" Rounding mode: {roundingMode}");
        }
        
        rm = roundValue;
    }
}


public class LabelNameNotFoundException : Exception
{
    const string msg = "Label name not found.";
    
    public LabelNameNotFoundException(string extraInfo) : base(msg + extraInfo){}
    public LabelNameNotFoundException() : base(msg){}
}

public class ImmidiateNotWithinBoundsException : Exception
{
    const string msg = "Bad bit length";
    
    public ImmidiateNotWithinBoundsException(string extraInfo) : base(msg + extraInfo){}
    public ImmidiateNotWithinBoundsException() : base(msg){}
}

public class WrongImmidiateFormatException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongImmidiateFormatException(string extraInfo) : base(msg + extraInfo){}
    public WrongImmidiateFormatException() : base(msg){}
}

public class Immidiate13Exception : Exception
{
    const string msg = "Least signifigant bit is set.";
    
    public Immidiate13Exception(string extraInfo) : base(msg + extraInfo){}
    public Immidiate13Exception() : base(msg){}
}

public class ImmidateIsNotPositiveException : Exception
{
    const string msg = "Immidiate is not a positive number.";
    
    public ImmidateIsNotPositiveException(string extraInfo) : base(msg + extraInfo){}
    public ImmidateIsNotPositiveException() : base(msg){}
}

public class WrongRoundingModeException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongRoundingModeException(string extraInfo) : base(msg + extraInfo){}
    public WrongRoundingModeException() : base(msg){}
}

public class CouldNotParseImmToIntException : Exception
{
    const string msg = "Could not parse argument to an integer.";
    
    public CouldNotParseImmToIntException(string extraInfo) : base(msg + extraInfo){}
    public CouldNotParseImmToIntException() : base(msg){}
}

public class CouldNotFindConstantLabelException : Exception
{
    const string msg = "Could not find a constant value from the label.";
    
    public CouldNotFindConstantLabelException(string extraInfo) : base(msg + extraInfo){}
    public CouldNotFindConstantLabelException() : base(msg){}
}

public class IncorrectMemoryArgumentSyntaxException : Exception
{
    const string msg = "Incorrect memory syntax.";
    
    public IncorrectMemoryArgumentSyntaxException(string extraInfo) : base(msg + extraInfo){}
    public IncorrectMemoryArgumentSyntaxException() : base(msg){}
}

public class IncorrectRegisterException : Exception
{
    const string msg = "Register not found or is not of right type.";
    
    public IncorrectRegisterException(string extraInfo) : base(msg + extraInfo){}
    public IncorrectRegisterException() : base(msg){}
}

public class ArgNotSetException : Exception
{
    const string msg = "Argument has not been set.";
    
    public ArgNotSetException(string extraInfo) : base(msg + extraInfo){}
    public ArgNotSetException() : base(msg){}
}
