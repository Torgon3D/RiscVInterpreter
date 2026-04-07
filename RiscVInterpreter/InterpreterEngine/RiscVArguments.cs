using System;

namespace RiscVInterpreterEngine;

enum EArgumentFlags : byte
{
    RD = 1,
    RS1 = 1 << 1,
    RS2 = 1 << 2,
    IMM = 1 << 3,
    STORE = 1 << 4,
    LOAD = 1 << 5,
    JUMP = 1 << 6
}

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm;
    
    public RiscVArguments()
    {
        
    }
    
    public byte GetArgumentInfo(EInstructionFormat format)
    {
        byte returnval = 0;
        
        returnval += (byte)(rd != null ? EArgumentFlags.RD : 0);
        returnval += (byte)(rs1 != null ? EArgumentFlags.RS1 : 0);
        returnval += (byte)(rs2 != null ? EArgumentFlags.RS2 : 0);
        returnval += (byte)(imm != null ? EArgumentFlags.IMM : 0);
        
        return returnval;
    }
}
