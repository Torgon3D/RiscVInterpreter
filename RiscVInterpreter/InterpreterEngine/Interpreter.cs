using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
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
    private MemoryController _memController = new();
    
    private Dictionary<string, int> _constValues = new();
    private Dictionary<string, int> _jumpPoints = new();
    private List<RunnableLine> _commands = new();
    public int Hertz = 0;
    public bool StopPressed = false;
    public bool PausePressed = false;
    
    public Action<string> PrintToConsole;
    
    public Interpreter(Action<string> printToConsoleFunction)
    {
        PrintToConsole = printToConsoleFunction;
    }
    
    public void Start(string[] lines)
    {
        _memController.ResetMemory();
        
        InterpretLineConstants(lines);
    }
    
    public void End()
    {
        _constValues.Clear();
        _commands.Clear();
        _jumpPoints.Clear();
    }
    
    private void ProgramLoop()
    {
        // Get info
        RunnableLine currentLine = _commands[_memController.PC.GetAsInt32()];
        
        // Check validity
        
        // Execute command
        
        // Continue to next line
        PrintToConsole(BuildInstructionInfo(currentLine));
        
        if (PausePressed)
        {
            
        }
        
        if (Hertz > 0)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1.0/(double)Hertz));
        }
    }
    
    private void Pause()
    {
        
    }
    
    private void Resume()
    {
        
    }
    
    private void InterpretLineConstants(string[] lines)
    {
        int instructionCounter = 0;
        List<string> labels = new();
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IsWhiteSpace()) return;
            
            string trimmedLine = lines[i].Trim();
            // Removes comments
            trimmedLine = Regex.Match(trimmedLine, @"^[^#]*").Value;
            if (trimmedLine.IsWhiteSpace()) return;
            
            int stringLoc = 0;
            
            Match match = Regex.Match(trimmedLine, @"^[^:][a-zA-Z\d_.]+:");
            // if its a label try to find out if its an command
            if (match.Success)
            {
                // its a label
                labels.Add(match.Value.Replace(":", ""));
                stringLoc = match.Index + match.Length;
            }
            
            Match matchConstant = Regex.Match(trimmedLine.Substring(stringLoc).Trim(), @"^\.[a-zA-Z.]+\b");
            Match matchCommand = Regex.Match(trimmedLine.Substring(stringLoc).Trim(), @"^[^.][a-zA-Z.]+");
            if (matchConstant.Success)
            {
                // its an constant
                int constValue;
                
                if (int.TryParse(trimmedLine.Substring(matchConstant.Index + matchConstant.Length).Trim(), out constValue));
                
                if (labels.Count > 0)
                {
                    foreach (string label in labels) _constValues.Add(label, constValue);
                    labels.Clear();
                }
            }
            else if (matchCommand.Success)
            {
                // its a command
                
                
                
                if (labels.Count > 0)
                {
                    foreach (string label in labels) _jumpPoints.Add(label, instructionCounter);
                    labels.Clear();
                }
                instructionCounter += 1;
            }
        }
    }
    
    private void RunInstruction()
    {
        
    }
    
    private string BuildInstructionInfo(RunnableLine instruction)
    {
        StringBuilder output = new();
        
        int instructionMachineCode = 0;
        switch (instruction.Instr.InstructionFormat)
        {
        case EInstructionFormat.R:
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct7, 7, 25);
            break;
        
        case EInstructionFormat.I:
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 12, 20);
            
            break;
        
        case EInstructionFormat.S:
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm >> 5, 7, 25);
            
            break;
        
        case EInstructionFormat.B:
        // TODO better error handling
            if (instruction.Args.rd == null || instruction.Args.rs1 == null || instruction.Args.rs2 == null || instruction.Args.imm == null || instruction.Instr.Funct3 == null || instruction.Instr.Funct7 == null) return null;
        
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm >> 5, 7, 25);
            
            break;
        
        case EInstructionFormat.U:
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 12, 20);
            
            break;
        
        case EInstructionFormat.J:
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 12, 20);
            
            break;
        
        case EInstructionFormat.R4:
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct7, 7, 25);
            
            break;
        
        default:
            
            break;
        }
        output.Append($"[{instruction.LineNumber}] {instructionMachineCode}. {instruction.InstructionInfo}");
        
        return output.ToString();
    }
}

public struct RunnableLine
{
    public RunnableLine(RiscVInstruction instr, RiscVArguments args, int lineNumber, int instructionNumber)
    {
        Instr = instr;
        Args = args;
        LineNumber = lineNumber;
        InstructionNumber = instructionNumber;
        
        InstructionInfo = instr.InstructionInfo;
        
        if (args.rd != null)
        {
            InstructionInfo += $", rd={args.rd}";
        }
        if (args.rs1 != null)
        {
            InstructionInfo += $", rs1={args.rs1}";
        }
        if (args.rs2 != null)
        {
            InstructionInfo += $", rs2={args.rs2}";
        }
        if (args.imm != null)
        {
            InstructionInfo += $", imm={args.imm}";
        }
    }
    
    public RiscVInstruction Instr;
    public RiscVArguments Args;
    public int LineNumber;
    public int InstructionNumber;
    public string InstructionInfo;
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
