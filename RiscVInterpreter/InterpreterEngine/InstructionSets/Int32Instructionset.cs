using System;
using System.Diagnostics;

namespace RiscVInterpreterEngine;

public partial class InstructionsetImplementations : InstructionsetBase
{
    public void Int32Constructor()
    {
        // R type
        Instructions.Add("add", new RiscVInstruction(
            Add,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Add",
            0b0110011,
            0b000,
            0b0000000
        ));
        
        Instructions.Add("sub", new RiscVInstruction(
            Sub,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Subtract",
            0b0110011,
            0b000,
            0b0100000
        ));
        
        Instructions.Add("sll", new RiscVInstruction(
            Sll,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Shift left logical",
            0b0110011,
            0b001,
            0b0000000
        ));
        
        Instructions.Add("slt", new RiscVInstruction(
            Slt,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Set less than",
            0b0110011,
            0b010,
            0b0000000
        ));
        
        Instructions.Add("sltu", new RiscVInstruction(
            Sltu,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Add",
            0b0110011,
            0b011,
            0b0000000
        ));
        
        Instructions.Add("xor", new RiscVInstruction(
            Xor,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Add",
            0b0110011,
            0b100,
            0b0000000
        ));
        
        Instructions.Add("srl", new RiscVInstruction(
            Srl,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Shift right logical",
            0b0110011,
            0b101,
            0b0000000
        ));
        
        Instructions.Add("sra", new RiscVInstruction(
            Sra,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Shift right arithmetic",
            0b0110011,
            0b101,
            0b0100000
        ));
        
        Instructions.Add("or", new RiscVInstruction(
            Or,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "Or",
            0b0110011,
            0b110,
            0b0000000
        ));
        
        Instructions.Add("and", new RiscVInstruction(
            And,
            EInstructionFormat.R,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.RS2],
            "And",
            0b0110011,
            0b111,
            0b0000000
        ));
        
        // I type
        Instructions.Add("lb", new RiscVInstruction(
            Lb,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.MEMORY],
            "Load byte",
            0b0000011,
            0b000
        ));
        Instructions.Add("lh", new RiscVInstruction(
            Lh,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.MEMORY],
            "Load half",
            0b0000011,
            0b001
        ));
        Instructions.Add("lw", new RiscVInstruction(
            Lw,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.MEMORY],
            "Load word",
            0b0000011,
            0b010
        ));
        Instructions.Add("lbu", new RiscVInstruction(
            Lbu,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.MEMORY],
            "Load byte unsigned",
            0b0000011,
            0b100
        ));
        Instructions.Add("lhu", new RiscVInstruction(
            Lhu,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.MEMORY],
            "Load half unsigned",
            0b0000011,
            0b101
        ));
        Instructions.Add("addi", new RiscVInstruction(
            Addi,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Add immidiate",
            0b0010011,
            0b000
        ));
        Instructions.Add("slli", new RiscVInstruction(
            Slli,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Shift left logical immidiate",
            0b0010011,
            0b001
        ));
        Instructions.Add("slti", new RiscVInstruction(
            Slti,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Set less than immidiate",
            0b0010011,
            0b010
        ));
        Instructions.Add("sltiu", new RiscVInstruction(
            Sltiu,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Set less than immidiate unsigned",
            0b0010011,
            0b011
        ));
        Instructions.Add("xori", new RiscVInstruction(
            Xori,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Xor immidiate",
            0b0010011,
            0b100
        ));
        Instructions.Add("srli", new RiscVInstruction(
            Srli,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Shift right logical immidiate",
            0b0010011,
            0b101
        ));
        Instructions.Add("srai", new RiscVInstruction(
            Srai,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Shift right arithmetic immidiate",
            0b0010011,
            0b101
        ));
        Instructions.Add("ori", new RiscVInstruction(
            Ori,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Or immidiate",
            0b0010011,
            0b110
        ));
        Instructions.Add("andi", new RiscVInstruction(
            Andi,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "And immidiate",
            0b0010011,
            0b111
        ));
        Instructions.Add("jalr", new RiscVInstruction(
            Jalr,
            EInstructionFormat.I,
            [EArgumentTypes.RD, EArgumentTypes.RS1, EArgumentTypes.IMM],
            "Jump and link register",
            0b1100111,
            0b000
        ));
        
        // S type
        Instructions.Add("sb", new RiscVInstruction(
            Sb,
            EInstructionFormat.S,
            [EArgumentTypes.RS2, EArgumentTypes.MEMORY],
            "Store byte",
            0b0100011,
            0b000
        ));
        
        Instructions.Add("sh", new RiscVInstruction(
            Sh,
            EInstructionFormat.S,
            [EArgumentTypes.RS2, EArgumentTypes.MEMORY],
            "Store half",
            0b0100011,
            0b001
        ));
        
        Instructions.Add("sw", new RiscVInstruction(
            Sw,
            EInstructionFormat.S,
            [EArgumentTypes.RS2, EArgumentTypes.MEMORY],
            "Store word",
            0b0100011,
            0b010
        ));
        
        // B type
        Instructions.Add("beq", new RiscVInstruction(
            Beq,
            EInstructionFormat.B,
            [EArgumentTypes.RS1, EArgumentTypes.RS2, EArgumentTypes.IMM],
            "Branch if equal",
            0b1100011,
            0b000
        ));
        
        Instructions.Add("bne", new RiscVInstruction(
            Bne,
            EInstructionFormat.B,
            [EArgumentTypes.RS1, EArgumentTypes.RS2, EArgumentTypes.IMM],
            "Branch if not equal",
            0b1100011,
            0b001
        ));
        
        Instructions.Add("blt", new RiscVInstruction(
            Blt,
            EInstructionFormat.B,
            [EArgumentTypes.RS1, EArgumentTypes.RS2, EArgumentTypes.IMM],
            "Branch if less than",
            0b1100011,
            0b100
        ));
        
        Instructions.Add("bge", new RiscVInstruction(
            Bge,
            EInstructionFormat.B,
            [EArgumentTypes.RS1, EArgumentTypes.RS2, EArgumentTypes.IMM],
            "Branch if greater or equal",
            0b1100011,
            0b101
        ));
        
        Instructions.Add("bltu", new RiscVInstruction(
            Bltu,
            EInstructionFormat.B,
            [EArgumentTypes.RS1, EArgumentTypes.RS2, EArgumentTypes.IMM],
            "Branch if less than unsigned",
            0b1100011,
            0b110
        ));
        
        Instructions.Add("bgeu", new RiscVInstruction(
            Bgeu,
            EInstructionFormat.B,
            [EArgumentTypes.RS1, EArgumentTypes.RS2, EArgumentTypes.IMM],
            "Branch if greater or equal unsigned",
            0b1100011,
            0b111
        ));
        
        // U type
        Instructions.Add("auipc", new RiscVInstruction(
            Auipc,
            EInstructionFormat.U,
            [EArgumentTypes.RD, EArgumentTypes.IMM],
            "Add upper immidiate to PC",
            0b0010111,
            0b000
        ));
        
        Instructions.Add("lui", new RiscVInstruction(
            Lui,
            EInstructionFormat.U,
            [EArgumentTypes.RD, EArgumentTypes.IMM],
            "Load upper immidiate",
            0b0110111,
            0b111
        ));
        
        // J type
        Instructions.Add("jal", new RiscVInstruction(
            Jal,
            EInstructionFormat.J,
            [EArgumentTypes.RD, EArgumentTypes.IMM],
            "Jump and link",
            0b1101111,
            0b000
        ));
    }

    // R type
    private void Add(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Sub(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Sll(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Slt(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Sltu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Xor(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Srl(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Sra(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Or(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void And(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    // I type
    private void Lb(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        byte memoryValue = _memory.MemoryStuff.ReadFromAdress(location, 1)[0];
        _memory.IntegerRegisters[arguments.GetRD()].SetFromInt8(memoryValue);
        
        _memory.PC.IncrementPC();
    }

    private void Lh(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        short memoryValue = BitConverter.ToInt16(_memory.MemoryStuff.ReadFromAdress(location, 2));
        _memory.IntegerRegisters[arguments.GetRD()].SetFromInt16(memoryValue);
        
        _memory.PC.IncrementPC();
    }

    private void Lw(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        int memoryValue = BitConverter.ToInt32(_memory.MemoryStuff.ReadFromAdress(location, 4));
        _memory.IntegerRegisters[arguments.GetRD()].SetFromInt32(memoryValue);
        
        _memory.PC.IncrementPC();
    }

    private void Lbu(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        byte memoryValue = _memory.MemoryStuff.ReadFromAdress(location, 1)[0];
        _memory.IntegerRegisters[arguments.GetRD()].SetFromUInt8(memoryValue);
        
        _memory.PC.IncrementPC();
    }

    private void Lhu(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        ushort memoryValue = BitConverter.ToUInt16(_memory.MemoryStuff.ReadFromAdress(location, 2));
        _memory.IntegerRegisters[arguments.GetRD()].SetFromUInt16(memoryValue);
        
        _memory.PC.IncrementPC();
    }

    private void Addi(RiscVArguments arguments)
    {
        _memory.IntegerRegisters[arguments.GetRD()].SetFromInt32(arguments.GetIMM() + _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32());
        
        _memory.PC.IncrementPC();
    }

    private void Slli(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Slti(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Sltiu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Xori(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Srli(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Srai(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Ori(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Andi(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Jalr(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }
    
    // S type
    private void Sb(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        int saveValue = _memory.IntegerRegisters[arguments.GetRS2()].GetAsInt8();
        _memory.MemoryStuff.SaveToAdress(location, BitConverter.GetBytes(saveValue));
        
        _memory.PC.IncrementPC();
    }

    private void Sh(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        int saveValue = _memory.IntegerRegisters[arguments.GetRS2()].GetAsInt16();
        _memory.MemoryStuff.SaveToAdress(location, BitConverter.GetBytes(saveValue));
        
        _memory.PC.IncrementPC();
    }

    private void Sw(RiscVArguments arguments)
    {
        int location = _memory.IntegerRegisters[arguments.GetRS1()].GetAsInt32() + arguments.GetIMM();
        int saveValue = _memory.IntegerRegisters[arguments.GetRS2()].GetAsInt32();
        _memory.MemoryStuff.SaveToAdress(location, BitConverter.GetBytes(saveValue));
        
        _memory.PC.IncrementPC();
    }
    
    // B type
    private void Beq(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Bne(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Blt(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Bge(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Bltu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Bgeu(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }
    
    // U type
    private void Auipc(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    private void Lui(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }

    // J type
    private void Jal(RiscVArguments arguments)
    {
        throw new NotImplementedException();
        
        _memory.PC.IncrementPC();
    }
}