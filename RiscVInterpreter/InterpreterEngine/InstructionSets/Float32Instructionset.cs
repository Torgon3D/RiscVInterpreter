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
        
        // R type with s
        Instructions.Add("fadd.s", new RiscVInstruction(
            Fadd,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float add",
            0b1010011,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fsub.s", new RiscVInstruction(
            Fsub,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float subtract",
            0b1010011,
            0b000,
            0b0000100
        ));
        
        Instructions.Add("fmul.s", new RiscVInstruction(
            Fmul,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float multiply",
            0b1010011,
            0b000,
            0b0001000
        ));
        
        Instructions.Add("fdiv.s", new RiscVInstruction(
            Fdiv,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.ROUNDING],
            "Float divide",
            0b1010011,
            0b000,
            0b0001100
        ));
        
        Instructions.Add("fsqrt.s", new RiscVInstruction(
            Fsqrt,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.ROUNDING],
            "Float square root",
            0b1010011,
            0b000,
            0b0101100,
            0b00000
        ));
        
        Instructions.Add("fsgnj.s", new RiscVInstruction(
            Fsgnj,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float sign injection",
            0b1010011,
            0b000,
            0b0010000
        ));
        
        Instructions.Add("fsgnjn.s", new RiscVInstruction(
            Fsgnjn,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float negate sign injection",
            0b1010011,
            0b001,
            0b0010000
        ));
        
        Instructions.Add("fsgnjx.s", new RiscVInstruction(
            Fsgnjx,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float xor sign injection",
            0b1010011,
            0b010,
            0b0010000
        ));
        
        Instructions.Add("fmin.s", new RiscVInstruction(
            Fmin,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float add",
            0b1010011,
            0b000,
            0b0010100
        ));
        
        Instructions.Add("fmax.s", new RiscVInstruction(
            Fmax,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float add",
            0b1010011,
            0b001,
            0b0010100
        ));
        
        Instructions.Add("feq.s", new RiscVInstruction(
            Feq,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float equal",
            0b1010011,
            0b010,
            0b1010000
        ));
        
        Instructions.Add("flt.s", new RiscVInstruction(
            Flt,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float less than",
            0b1010011,
            0b001,
            0b1010000
        ));
        
        Instructions.Add("fle.s", new RiscVInstruction(
            Fle,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2],
            "Float less or equal",
            0b1010011,
            0b000,
            0b1010000
        ));
        
        Instructions.Add("fclass.s", new RiscVInstruction(
            Fclass,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1],
            "Float classify",
            0b1010011,
            0b001,
            0b1110000,
            0b00000
        ));
        
        // R type converters
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
            0b010
        ));
        
        // S type
        Instructions.Add("fsw", new RiscVInstruction(
            Fsw,
            EInstructionFormat.S,
            [EArgumentTypes.FS2, EArgumentTypes.MEMORY],
            "Store float",
            0b0100111,
            0b010
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
        
        // R4 type with s
        Instructions.Add("fmadd.s", new RiscVInstruction(
            Fmadd,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.FR3, EArgumentTypes.ROUNDING],
            "Multiply add",
            0b1000011,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fmsub.s", new RiscVInstruction(
            Fmsub,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.FR3, EArgumentTypes.ROUNDING],
            "Multiply subtract",
            0b1000111,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fnmsub.s", new RiscVInstruction(
            Fnmsub,
            EInstructionFormat.R,
            [EArgumentTypes.FD, EArgumentTypes.FS1, EArgumentTypes.FS2, EArgumentTypes.FR3, EArgumentTypes.ROUNDING],
            "Negate multiply subtract",
            0b1001011,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("fnmadd.s", new RiscVInstruction(
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
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 + val2;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fsub(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 - val2;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fmul(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 * val2;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fdiv(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 / val2;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fsqrt(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float newVal = MathF.Sqrt(val1);

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fsgnj(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 * (val2 >= 0f ? 1f : -1f);
        newVal *= val1 >= 0f ? 1f : -1f;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fsgnjn(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 * -(val2 >= 0f ? 1f : -1f);
        newVal *= val1 >= 0f ? 1f : -1f;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fsgnjx(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 * ((val1 >= 0f) == (val2 >= 0f) ? 1f : -1f);
        newVal *= val1 >= 0f ? 1f : -1f;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fmin(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 < val2 ? val1 : val2;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fmax(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float newVal = val1 > val2 ? val1 : val2;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Feq(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        int newVal = val1 == val2 ? 1 : 0;

        arguments.GetRD(_memory).SetFromInt32(newVal);
        _memory.PC.IncrementPC();
    }

    private void Flt(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        int newVal = val1 < val2 ? 1 : 0;

        arguments.GetRD(_memory).SetFromInt32(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fle(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        int newVal = val1 <= val2 ? 1 : 0;

        arguments.GetRD(_memory).SetFromInt32(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fclass(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        int calssifyMaskVal = 0;
        if (val1 == 0f)
        {
            if (float.IsNegative(val1)) calssifyMaskVal = 1 << 3;
            else if (float.IsPositive(val1)) calssifyMaskVal = 1 << 4;
        }
        else if (float.IsNegative(val1))
        {
            if (float.IsNegativeInfinity(val1)) calssifyMaskVal = 1 << 0;
            else if (float.IsNormal(val1)) calssifyMaskVal = 1 << 1;
            else if (float.IsSubnormal(val1)) calssifyMaskVal = 1 << 2;
        }
        else if (float.IsPositive(val1))
        {
            if (float.IsPositiveInfinity(val1)) calssifyMaskVal = 1 << 7;
            else if (float.IsNormal(val1)) calssifyMaskVal = 1 << 6;
            else if (float.IsSubnormal(val1)) calssifyMaskVal = 1 << 5;
        }
        else if (float.IsNaN(val1))
        {
            if ((unchecked((int)val1) & 1) == 1) calssifyMaskVal = 1 << 8;
            else calssifyMaskVal = 1 << 9;
        }
        
        arguments.GetRD(_memory).SetFromInt32(calssifyMaskVal);
        _memory.PC.IncrementPC();
    }

    private void FcvtWS(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        int newVal = (int)val1;

        arguments.GetRD(_memory).SetFromInt32(newVal);
        _memory.PC.IncrementPC();
    }

    private void FcvtWuS(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        uint newVal = (uint)val1;

        arguments.GetRD(_memory).SetFromUInt32(newVal);
        _memory.PC.IncrementPC();
    }

    private void FcvtSW(RiscVArguments arguments)
    {
        int val1 = arguments.GetRS1(_memory).GetAsInt32();
        float newVal = (float)val1;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void FcvtSWu(RiscVArguments arguments)
    {
        uint val1 = arguments.GetRS1(_memory).GetAsUInt32();
        float newVal = (float)val1;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void FmvXW(RiscVArguments arguments)
    {
        int val1 = arguments.GetFS1(_memory).GetAsInt32();

        arguments.GetRD(_memory).SetFromInt32(val1);
        _memory.PC.IncrementPC();
    }

    private void FmvWX(RiscVArguments arguments)
    {
        int val1 = arguments.GetRS1(_memory).GetAsInt32();

        arguments.GetFD(_memory).SetFromInt32(val1);
        _memory.PC.IncrementPC();
    }
    
    // I type
    private void Flw(RiscVArguments arguments)
    {
        int location = arguments.GetRS1(_memory).GetAsInt32() + arguments.GetIMM();
        int memoryValue = BitConverter.ToInt32(_memory.MemoryStuff.ReadFromAdress(location, 4));
        
        arguments.GetFD(_memory).SetFromInt32(memoryValue);
        _memory.PC.IncrementPC();
    }

    // S type
    private void Fsw(RiscVArguments arguments)
    {
        int location = arguments.GetRS1(_memory).GetAsInt32() + arguments.GetIMM();
        int saveValue = arguments.GetFS2(_memory).GetAsInt32();
        
        _memory.MemoryStuff.SaveToAdress(location, BitConverter.GetBytes(saveValue));
        _memory.PC.IncrementPC();
    }

    // R4 type
    private void Fmadd(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float val3 = arguments.GetFS3(_memory).GetAsFloat();
        float newVal = val1 * val2 + val3;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fmsub(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float val3 = arguments.GetFS3(_memory).GetAsFloat();
        float newVal = val1 * val2 - val3;

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fnmsub(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float val3 = arguments.GetFS3(_memory).GetAsFloat();
        float newVal = -(val1 * val2 + val3);

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }

    private void Fnmadd(RiscVArguments arguments)
    {
        float val1 = arguments.GetFS1(_memory).GetAsFloat();
        float val2 = arguments.GetFS2(_memory).GetAsFloat();
        float val3 = arguments.GetFS3(_memory).GetAsFloat();
        float newVal = -(val1 * val2 - val3);

        arguments.GetFD(_memory).SetFromFloat(newVal);
        _memory.PC.IncrementPC();
    }
}