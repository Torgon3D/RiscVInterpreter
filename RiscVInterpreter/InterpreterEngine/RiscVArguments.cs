using System;

namespace RiscVInterpreterEngine;

public enum EArgumentFlags : short
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
}

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm;
    
    public RiscVArguments()
    {
        
    }
    
    public short GetArgumentInfo(EInstructionFormat format)
    {
        short returnval = 0;
        
        returnval += (short)(rd != null ? EArgumentFlags.RD : 0);
        returnval += (short)(rs2 != null ? EArgumentFlags.RS2 : 0);
        returnval += (short)(imm != null ? EArgumentFlags.IMM : 0);
        returnval += (short)(rs1 != null ? EArgumentFlags.RS1 : 0);
        
        return returnval;
    }
}
