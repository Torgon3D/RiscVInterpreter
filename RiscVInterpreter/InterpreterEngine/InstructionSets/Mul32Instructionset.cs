using System;
using System.Diagnostics;

namespace RiscVInterpreterEngine;

public partial class InstructionsetImplementations : InstructionsetBase
{
    public void Mul32Constructor()
    {
        // R type
        Instructions.Add("mul", new RiscVInstruction(
            Mul,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Multiply",
            0b0110011,
            0b000,
            0b0000001
        ));
        
        Instructions.Add("mulh", new RiscVInstruction(
            Mulh,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Multiply high signed signed",
            0b0110011,
            0b001,
            0b0000001
        ));
        
        Instructions.Add("mulhsu", new RiscVInstruction(
            Mulhsu,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Multiply high signed unsigned",
            0b0110011,
            0b010,
            0b0000001
        ));
        
        Instructions.Add("mulhu", new RiscVInstruction(
            Mulhu,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Multiply high unsigned unsigned",
            0b0110011,
            0b011,
            0b0000001
        ));
        
        Instructions.Add("div", new RiscVInstruction(
            Div,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Divide",
            0b0110011,
            0b100,
            0b0000001
        ));
        
        Instructions.Add("divu", new RiscVInstruction(
            Divu,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Divide unsigned",
            0b0110011,
            0b101,
            0b0000001
        ));
        
        Instructions.Add("rem", new RiscVInstruction(
            Rem,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Remainder",
            0b0110011,
            0b110,
            0b0000001
        ));
        
        Instructions.Add("remu", new RiscVInstruction(
            Remu,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Remainder unsigned",
            0b0110011,
            0b111,
            0b0000001
        ));
    }

    // R type
    private void Mul(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Mulh(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Mulhsu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Mulhu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Div(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Divu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Rem(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Remu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }
}