using System;
using System.Diagnostics;

namespace RiscVInterpreterEngine;

public partial class InstructionsetImplementations : InstructionsetBase
{
    
    
    public void Int32Constructor()
    {
        Instructions.Add("addi", new RiscVInstruction(
            Addi,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Its cool",
            0b0010011,
            0x00
        ));
    }
    
    private void Addi(RiscVArguments args)
    {
        Debug.Print($"It became {args.rd} {args.rs1} {args.imm}");
        if (args.rd == null) return;
        _memory.IntegerRegisters[(int)args.rd].SetFromInt32((int)(args.imm + args.rs1));
    }
}