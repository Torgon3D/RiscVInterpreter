using System;
using System.Diagnostics;

namespace RiscVInterpreterEngine;

public partial class InstructionsetImplementations : InstructionsetBase
{
    public void Float32Constructor()
    {
        // R type
        Instructions.Add("fadd", new RiscVInstruction(
            Fadd,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float add",
            0b1010011,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fsub", new RiscVInstruction(
            Fsub,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float subtract",
            0b1010011,
            0b000,
            0b0000100
        ));
        
        Instructions.Add("fmul", new RiscVInstruction(
            Fmul,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float multiply",
            0b1010011,
            0b000,
            0b0001000
        ));
        
        Instructions.Add("fdiv", new RiscVInstruction(
            Fdiv,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float divide",
            0b1010011,
            0b000,
            0b0001100
        ));
        
        Instructions.Add("fsqrt", new RiscVInstruction(
            Fsqrt,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.ROUNDING],
            "Float square root",
            0b1010011,
            0b000,
            0b0101100,
            0b00000
        ));
        
        Instructions.Add("fsgnj", new RiscVInstruction(
            Fsgnj,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float sign injection",
            0b1010011,
            0b000,
            0b0010000
        ));
        
        Instructions.Add("fsgnjn", new RiscVInstruction(
            Fsgnjn,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float negate sign injection",
            0b1010011,
            0b001,
            0b0010000
        ));
        
        Instructions.Add("fsgnjx", new RiscVInstruction(
            Fsgnjx,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float xor sign injection",
            0b1010011,
            0b010,
            0b0010000
        ));
        
        Instructions.Add("fmin", new RiscVInstruction(
            Fmin,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float add",
            0b1010011,
            0b000,
            0b0010100
        ));
        
        Instructions.Add("fmax", new RiscVInstruction(
            Fmax,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float add",
            0b1010011,
            0b001,
            0b0010100
        ));
        
        Instructions.Add("feq", new RiscVInstruction(
            Feq,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float equal",
            0b1010011,
            0b010,
            0b1010000
        ));
        
        Instructions.Add("flt", new RiscVInstruction(
            Flt,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float less than",
            0b1010011,
            0b001,
            0b1010000
        ));
        
        Instructions.Add("fle", new RiscVInstruction(
            Fle,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float less or equal",
            0b1010011,
            0b000,
            0b1010000
        ));
        
        Instructions.Add("fclass", new RiscVInstruction(
            Fclass,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1],
            "Float classify",
            0b1010011,
            0b001,
            0b1110000,
            0b00000
        ));
        
        Instructions.Add("fcvt.w.s", new RiscVInstruction(
            FcvtWS,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.FS1, EArgumentTypes.ROUNDING],
            "Convert float to int",
            0b1010011,
            0b000,
            0b1100000,
            0b00000
        ));
        
        Instructions.Add("fcvt.wu.s", new RiscVInstruction(
            FcvtWuS,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.FS1, EArgumentTypes.ROUNDING],
            "Convert float to unsigned int",
            0b1010011,
            0b000,
            0b1100000,
            0b00001
        ));
        
        Instructions.Add("fcvt.s.w", new RiscVInstruction(
            FcvtSW,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.RS1, EArgumentTypes.ROUNDING],
            "Convert int to float",
            0b1010011,
            0b000,
            0b1101000,
            0b00000
        ));
        
        Instructions.Add("fcvt.s.wu", new RiscVInstruction(
            FcvtSWu,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.RS1, EArgumentTypes.ROUNDING],
            "Convert unsigned int to float",
            0b1010011,
            0b000,
            0b1101000,
            0b00001
        ));
        
        Instructions.Add("fmv.x.w", new RiscVInstruction(
            FmvXW,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.FS1],
            "Int move to float register",
            0b1010011,
            0b000,
            0b1110000,
            0b00000
        ));
        
        Instructions.Add("fmv.w.x", new RiscVInstruction(
            FmvWX,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.RS1],
            "Float move to int register",
            0b1010011,
            0b000,
            0b1111000,
            0b00000
        ));
        
        // I type
        Instructions.Add("flw", new RiscVInstruction(
            Flw,
            EInstructionFormat.I,
            [EArgumentTypes.FD, EArgumentTypes.MEMORY],
            "Load float",
            0b0000111,
            0b000
        ));
        
        // S type
        Instructions.Add("fsw", new RiscVInstruction(
            Fsw,
            EInstructionFormat.S,
            [EArgumentTypes.FS2, EArgumentTypes.MEMORY],
            "Store float",
            0b0100111,
            0b000
        ));
        
        // R4 type
        Instructions.Add("fmadd", new RiscVInstruction(
            Fmadd,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.FR3, EArgumentTypes.ROUNDING],
            "Multiply add",
            0b1000011,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fmsub", new RiscVInstruction(
            Fmsub,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.FR3, EArgumentTypes.ROUNDING],
            "Multiply subtract",
            0b1000111,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fnmsub", new RiscVInstruction(
            Fnmsub,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.FR3, EArgumentTypes.ROUNDING],
            "Negate multiply subtract",
            0b1001011,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fnmadd", new RiscVInstruction(
            Fnmadd,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.FR3, EArgumentTypes.ROUNDING],
            "Negate multiply add",
            0b1001111,
            0b000,
            0b0000000
        ));
    }

    // R type
    private void Fadd(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fsub(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fmul(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fdiv(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fsqrt(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fsgnj(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fsgnjn(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fsgnjx(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fmin(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fmax(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Feq(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Flt(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fle(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fclass(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void FcvtWS(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void FcvtWuS(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void FcvtSW(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void FcvtSWu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void FmvXW(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void FmvWX(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }
    
    // I type
    private void Flw(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    // S type
    private void Fsw(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    // R4 type
    private void Fmadd(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fmsub(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fnmsub(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Fnmadd(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }
}