using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using Microsoft.VisualBasic;

namespace RiscVInterpreterEngine;

// Kansje endre til class som holder alle instruksjonformater. Kansje som partial
public enum EInstructionFormat
{
    R,
    I,
    S,
    B,
    U,
    J,
    R4
}
public class Interpreter
{
    private Dictionary<string, int> constValues;
    private Dictionary<string, int> jumpPoints;
    //private Dictionary<string, RiscVInstruction> commands;
    private List<RunnableLine> commands;
    
    private string? heldLabel;
    
    public Action<string> PrintToConsole;
    
    public void Start()
    {
        
    }
    
    public void End()
    {
        
    }
    
    private void ProgramLoop()
    {
        RunnableLine line;
        // Get info
        
        // Check validity
        
        // Execute command
        
        // Continue to next line
        PrintToConsole(BuildInstructionInfo(line));
    }
    
    private void InterpretLineConstants(string line)
    {
        if (line.IsWhiteSpace()) return;
        
        string trimmedLine = line.Trim();
        // Removes comments
        trimmedLine = Regex.Match(trimmedLine, @"^[^#]*").Value;
        if (trimmedLine.IsWhiteSpace()) return;
        
        int stringLoc = 0;
        
        Match match = Regex.Match(trimmedLine, @"^[^:][a-zA-Z\d_.]+:");
        // if its a label try to find out if its an command
        if (match.Success)
        {
            // its a label
            stringLoc = match.Index + match.Length;
        }
        
        Match matchCommand = Regex.Match(trimmedLine.Substring(stringLoc), @"^[a-zA-Z.]+");
        if (matchCommand.Success)
        {
            // its a command
        }
        
        Match matchConstant = Regex.Match(trimmedLine.Substring(stringLoc), @"^\.[a-zA-Z.]+");
        if (matchConstant.Success)
        {
            // its an constant
        }
    }
    
    private void ExtractConstants()
    {
        
    }
    
    private void RunInstruction()
    {
        
    }
    
    private string BuildInstructionInfo(RunnableLine instruction)
    {
        StringBuilder output = new();
        
        int instructionMachineCode = 0;
        switch (instruction.instr.InstructionFormat)
        {
        case EInstructionFormat.R:
            instructionMachineCode |= instruction.instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.instr.Funct7, 7, 25);
            break;
        
        case EInstructionFormat.I:
            instructionMachineCode |= instruction.instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.imm, 12, 20);
            
            break;
        
        case EInstructionFormat.S:
            instructionMachineCode |= instruction.instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.imm, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.imm >> 5, 7, 25);
            
            break;
        
        case EInstructionFormat.B:
        // TODO better error handling
            if (instruction.args.rd == null || instruction.args.rs1 == null || instruction.args.rs2 == null || instruction.args.imm == null || instruction.instr.Funct3 == null || instruction.instr.Funct7 == null) return null;
        
            instructionMachineCode |= instruction.instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.imm, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.imm >> 5, 7, 25);
            
            break;
        
        case EInstructionFormat.U:
            instructionMachineCode |= instruction.instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.imm, 12, 20);
            
            break;
        
        case EInstructionFormat.J:
            instructionMachineCode |= instruction.instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.imm, 12, 20);
            
            break;
        
        case EInstructionFormat.R4:
            instructionMachineCode |= instruction.instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.instr.Funct7, 7, 25);
            
            break;
        
        default:
            
            break;
        }
        output.Append(instructionMachineCode);
        
        return output.ToString();
    }
}

public struct RunnableLine
{
    public RunnableLine(RiscVInstruction instr, RiscVArguments args, int lineNumber)
    {
        this.instr = instr;
        this.args = args;
        this.lineNumber = lineNumber;
    }
    
    public RiscVInstruction instr;
    public RiscVArguments args;
    public int lineNumber;
}

public class RiscVInstruction
{
    private Action<RiscVArguments> InstructionFunction;
    public EInstructionFormat InstructionFormat;
    public EArgumentFlags ArgumentFlags;
    public string InstructionInfo;
    public byte Opcode;
    public byte? Funct3;
    public byte? Funct7;
    
    public RiscVInstruction(Action<RiscVArguments> instructionFunction,
                            EInstructionFormat instructionFormat, string instructionInfo)
    {
        InstructionFunction = instructionFunction;
        InstructionFormat = instructionFormat;
        InstructionInfo = instructionInfo;
    }
    
    public void RunFunction(RiscVArguments args)
    {
        
        
        InstructionFunction.Invoke(args);
    }
}
