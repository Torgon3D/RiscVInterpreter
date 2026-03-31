using System;

namespace RiscVInterpreterEngine;

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm;
    
    public RiscVArguments()
    {
        
    }
    
    public byte GetArgumentInfo(EInstructionFormat format)
    {
        byte returnval = 0;
        
        returnval += (byte)((rd != null ? 1 : 0) << 0);
        returnval += (byte)((rs1 != null ? 1 : 0) << 1);
        returnval += (byte)((rs2 != null ? 1 : 0) << 2);
        returnval += (byte)((imm != null ? 1 : 0) << 3);
        
        return returnval;
    }
}
