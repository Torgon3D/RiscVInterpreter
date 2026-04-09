using System;

namespace RiscVInterpreterEngine;

public enum EArgumentFlags : short
{
    RD = 1,
    RS1 = 1 << 1,
    RS2 = 1 << 2,
    IMM = 1 << 3,
    FRD = 1 << 4,
    FRS1 = 1 << 5,
    FRS2 = 1 << 6,
    STORE = 1 << 7,
    LOAD = 1 << 8,
    LABEL = 1 << 9
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
