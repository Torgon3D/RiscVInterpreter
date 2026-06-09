using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace RiscVInterpreterEngine;

public struct RiscVCommand
{
    public RiscVInstruction Instr;
    public RiscVArguments Args;
    public int LineNumber;
    public int InstructionNumber;
    public string CommandInfo;
    
    public RiscVCommand(RiscVInstruction instr, RiscVArguments args, int lineNumber, string lineText, int instructionNumber)
    {
        Instr = instr;
        Args = args;
        LineNumber = lineNumber;
        InstructionNumber = instructionNumber;
        
        CommandInfo = Regex.Replace(lineText.Trim(), @"\s+", " ");
    }
    
    public void Run()
    {
        Instr.RunFunction(Args);
    }
    
    public string BuildInstructionMachineCodeHex()
    {
        StringBuilder output = new();
        
        int instructionMachineCode = 0;
        switch (Instr.InstructionFormat)
        {
        case EInstructionFormat.R:
            if (Args.rd == null
                || Args.rs1 == null
                || (Args.rs2 == null && Instr.rs2f == null)
                || Instr.Funct3 == null
                || Instr.Funct7 == null) throw new InvalidArgsForInstructionTypeException($" Instruction type: R");
            instructionMachineCode |= Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs1, 0, 5, 15);
            
            if (Instr.rs2f != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.rs2f, 0, 5, 20);
            }
            else if (Args.rs2 != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs2, 0, 5, 20);
            }
            if (Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.Funct7, 0, 7, 25);
            break;
        
        case EInstructionFormat.I:
            if (Args.rd == null
                || Args.rs1 == null
                || Args.imm == null
                || Instr.Funct3 == null) throw new InvalidArgsForInstructionTypeException($" Instruction type: I");
            instructionMachineCode |= Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs1, 0, 5, 15);
            if (Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 0, 12, 20);
            
            break;
        
        case EInstructionFormat.S:
            if (Args.rs1 == null
                || Args.rs2 == null
                || Args.imm == null
                || Instr.Funct3 == null) throw new InvalidArgsForInstructionTypeException($" Instruction type: S");
            instructionMachineCode |= Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs2, 0, 5, 20);
            if (Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 0, 5, 7, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 5, 7, 25);
            
            break;
        
        case EInstructionFormat.B:
            if (Args.rs1 == null
                || Args.rs2 == null
                || Args.imm == null
                || Instr.Funct3 == null) throw new InvalidArgsForInstructionTypeException($" Instruction type: B");
        
            instructionMachineCode |= Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs2, 0, 5, 20);
            if (Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 11, 1, 7, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 1, 4, 8, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 5, 6, 25, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 12, 1, 31);
            
            break;
        
        case EInstructionFormat.U:
            if (Args.rd == null ||
                Args.imm == null) throw new InvalidArgsForInstructionTypeException($" Instruction type: U");
            instructionMachineCode |= Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 12, 20, 12);
            
            break;
        
        case EInstructionFormat.J:
            if (Args.rd == null
                || Args.imm == null) throw new InvalidArgsForInstructionTypeException($" Instruction type: J");
            instructionMachineCode |= Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 12, 8, 12, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 11, 1, 20, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 1, 10, 21, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.imm, 20, 1, 31);
            
            break;
        
        case EInstructionFormat.R4:
            if (Args.rd == null
                || Args.rs1 == null
                || Args.rs2 == null
                || Args.fs3 == null
                || Instr.Funct3 == null
                || Instr.Funct7 == null) throw new InvalidArgsForInstructionTypeException($" Instruction type: R4");
            instructionMachineCode |= Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rs2, 0, 5, 20);
            if (Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Instr.Funct7, 0, 2, 25, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)Args.fs3, 0, 5, 27);
            
            break;
        
        default:
            
            break;
        }
        
        output.Append($"0x{instructionMachineCode.ToString("x8")}");
        
        return output.ToString();
    }
}

public class InvalidArgsForInstructionTypeException : Exception
{
    const string msg = "Arg(s) for the instruction type has not been set.";
    
    public InvalidArgsForInstructionTypeException(string extraInfo) : base(msg + extraInfo){}
    public InvalidArgsForInstructionTypeException() : base(msg){}
}
