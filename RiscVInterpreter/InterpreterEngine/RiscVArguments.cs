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
    public int? rd, rs1, rs2, imm, frs3, rm;
    
    
    public RiscVArguments()
    {
        
    }
    
    public bool HasArgument(EArgumentTypes argument)
    {
        switch (argument)
        {
        case EArgumentTypes.RD:
            return rd != null && IsIntRegister(rd);
            
        case EArgumentTypes.RS1:
            return rs1 != null && IsIntRegister(rs1);
            
        case EArgumentTypes.RS2:
            return rs2 != null && IsIntRegister(rs2);
            
        case EArgumentTypes.IMM:
            return imm != null;
            
        case EArgumentTypes.FRD:
            return rd != null && IsFloatRegister(rd);
            
        case EArgumentTypes.FR1:
            return rs1 != null && IsFloatRegister(rs1);
            
        case EArgumentTypes.FR2:
            return rs2 != null && IsFloatRegister(rs2);
            
        case EArgumentTypes.FR3:
            return frs3 != null && IsFloatRegister(frs3);
            
        case EArgumentTypes.MEMORY:
            return rs1 != null && imm != null && IsIntRegister(rs1);
            
        case EArgumentTypes.LABEL:
            return imm != null;
            
        case EArgumentTypes.ROUNDING:
            return rm != null;
            
        default:
            return false;
        }
    }
    
    private bool IsIntRegister(int? loc)
    {
        return loc >= 0 && loc <= 31;
    }
    
    private bool IsFloatRegister(int? loc)
    {
        return loc >= 32 && loc <= 63;
    }
}
