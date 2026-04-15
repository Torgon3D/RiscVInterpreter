using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RiscVInterpreterEngine;

/* public enum EArgumentFlags : short
{
    NONE = 0,
    RD = 1,
    RS1 = 1 << 1,
    RS2 = 1 << 2,
    IMM = 1 << 3,
    FRD = 1 << 4,
    FRS1 = 1 << 5,
    FRS2 = 1 << 6,
    FRS3 = 1 << 7,
    STORE = 1 << 8,
    LOAD = 1 << 9,
    LABEL = 1 << 10,
    ROUNDING = 1 << 11
} */

public enum EArgumentTypes : short
{
    RD = 1,
    RS1,
    RS2,
    IMM,
    FRD,
    FR1,
    FR2,
    FR3,
    MEMORY,
    LABEL,
    ROUNDING
}

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm, frs3;
    
    static Dictionary<string, byte> _roundingModes = new()
    {
        { "RNE", 0b000 },
        { "RTZ", 0b001 },
        { "RDN", 0b010 },
        { "RUP", 0b011 },
        { "RMM", 0b100 },
    };
    
    public RiscVArguments()
    {
        
    }
    
    public bool HasArgument(EArgumentTypes argument)
    {
        switch (argument)
        {
        case EArgumentTypes.RD:
            
            break;
            
        case EArgumentTypes.RS1:
            
            break;
            
        case EArgumentTypes.RS2:
            
            break;
            
        case EArgumentTypes.IMM:
            
            break;
            
        case EArgumentTypes.FRD:
            
            break;
            
        case EArgumentTypes.FR1:
            
            break;
            
        case EArgumentTypes.FR2:
            
            break;
            
        case EArgumentTypes.MEMORY:
            
            break;
            
        case EArgumentTypes.LABEL:
            
            break;
            
        case EArgumentTypes.ROUNDING:
            
            break;
            
        default:
            return false;
        }
        
        return false;
    }
    
    public void ParseArgument(string arg, EArgumentTypes argType, int line)
    {
        switch (argType)
        {
        case EArgumentTypes.RD:
            rd = ParseRegisterInt(arg, line);
            break;
            
        case EArgumentTypes.RS1:
            rs1 = ParseRegisterInt(arg, line);
            break;
            
        case EArgumentTypes.RS2:
            rs2 = ParseRegisterInt(arg, line);
            break;
            
        case EArgumentTypes.IMM:
            imm = ParseImmidiate(arg, line);
            break;
            
        case EArgumentTypes.FRD:
            rd = ParseRegisterFloat(arg, line);
            break;
            
        case EArgumentTypes.FR1:
            rs1 = ParseRegisterFloat(arg, line);
            break;
            
        case EArgumentTypes.FR2:
            rs2 = ParseRegisterFloat(arg, line);
            break;
            
        case EArgumentTypes.FR3:
            frs3 = ParseRegisterFloat(arg, line);
            break;
            
        case EArgumentTypes.MEMORY:
            ParseMemory(arg, line);
            break;
            
        case EArgumentTypes.LABEL:
            imm = ParseLabel(arg, line);
            break;
            
        case EArgumentTypes.ROUNDING:
            // Should not happen
            break;
            
        default:
            return;
        }
    }
    
    private int ParseRegisterInt(string regName, int line)
    {
        
    }
    
    private int ParseRegisterFloat(string regName, int line)
    {
        
    }
    
    private int ParseImmidiate(string immidiate, int line)
    {
        
    }
    
    private void ParseMemory(string memoryAdress, int line)
    {
        
    }
    
    private int ParseLabel(string labelName, int line)
    {
        byte roundValue;
        if (!_roundingModes.TryGetValue(labelName, out roundValue))
        {
            throw new ;
        }
    }
    
    public byte GetRounding(string roundingMode, int line)
    {
        byte roundValue;
        if (!_roundingModes.TryGetValue(roundingMode, out roundValue))
        {
            throw new ;
        }
        
        return roundValue;
    }
}
