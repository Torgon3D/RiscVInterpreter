using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Avalonia.Media;

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

public enum EInterpreterState
{
    STOPPED,
    RUNNING,
    PAUSED
}

public class Interpreter
{
    private MemoryController _memController;
    private InstructionsetImplementations _instructions;
    private Dictionary<string, int> _constTypes = new()
    {
        { ".word", 32 },
        { ".half", 16 },
        { ".byte", 8  }
    };
    private Dictionary<string, int> _constValues = new();
    private Dictionary<string, int> _jumpPoints = new();
    private List<RiscVCommand> _commands = new();
    public int Hertz = 0;
    public bool StopPressed = false;
    public bool PausePressed = false;
    public EInterpreterState InterpreterState = EInterpreterState.STOPPED;
    
    public Action<string> PrintToConsole;
    
    public Interpreter(Action<string> printToConsoleFunction, Action<string, int> registerUpdated, Action<byte[]> memoryUpdated)
    {
        _memController = new(registerUpdated, memoryUpdated);
        PrintToConsole = printToConsoleFunction;
        _instructions = new(_memController);
    }
    
    public async Task Start(string[] lines)
    {
        PrintToConsole("\nStarted new run");
        try
        {
            _memController.ResetMemory();
        
            ParseLineConstants(lines);
            ParseInstructions(lines);
            InterpreterState = EInterpreterState.RUNNING;
        } catch (Exception e)
        {
            PrintToConsole(e.Message);
            End();
            return;
        }
        
        while (InterpreterState == EInterpreterState.RUNNING)
            await Task.Run(ProgramLoop);
    }
    
    public void End()
    {
        if (InterpreterState == EInterpreterState.PAUSED)
        {
            PrintToConsole("Ended prematurely");
        }
        
        InterpreterState = EInterpreterState.STOPPED;
        _constValues.Clear();
        _commands.Clear();
        _jumpPoints.Clear();
    }
    
    private void ProgramLoop()
    {
        int nextPC = _memController.PC.GetForCommandArray();
        
        if (nextPC >= _commands.Count)
        {
            PrintToConsole("Run complete");
            End();
            return;
        }
        
        // Get info
        RiscVCommand currentLine = _commands[_memController.PC.GetForCommandArray()];
        
        // Execute command
        currentLine.Run();
        
        PrintInstructionInfoRun(currentLine);
        
        if (StopPressed)
        {
            PrintToConsole("Ended prematurely");
            PausePressed = false;
            StopPressed = false;
            End();
            return;
        }
        
        if (PausePressed)
        {
            // TODO REMOVE
            PrintAllInstructionInfos();
            
            InterpreterState = EInterpreterState.PAUSED;
            PausePressed = false;
            
            PrintToConsole("Paused");
            return;
        }
        else if (Hertz > 0)
        {
            Thread.Sleep(TimeSpan.FromSeconds(1.0/(double)Hertz));
        }
    }
    
    public async Task ResumeAsync()
    {
        InterpreterState = EInterpreterState.RUNNING;
        
        while (InterpreterState == EInterpreterState.RUNNING)
            await Task.Run(ProgramLoop);
    }
    
    public byte[] RetriveMemory()
    {
        return _memController.MemoryStuff.GetAllMemory();
    }
    
    private void ParseLineConstants(string[] lines)
    {
        int instructionCounter = 0;
        List<string> labels = new();
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IsWhiteSpace()) continue;
            
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
            
            string nextLine = trimmedLine.Substring(stringLoc).Trim();
            
            Match matchConstant = Regex.Match(nextLine, @"^\.[a-zA-Z.]+\b");
            Match matchCommand = Regex.Match(nextLine, @"^[^.][a-zA-Z.]+\b");
            
            if (matchConstant.Success)
            {
                if (labels.Count <= 0) throw new NoLabelForConstantException();
                
                // its an constant
                int constBits;
                int constValue;
                if (!_constTypes.TryGetValue(matchConstant.Value, out constBits)) throw new ConstantTypeDoesNotExistException();

                if (!int.TryParse(nextLine.Substring(matchConstant.Index + matchConstant.Length).Trim(), out constValue)) throw new CouldNotParseToIntException();
                
                if (!RiscVBitTools.IsBitsWithinBounds(constValue, constBits)) throw new IncorrectBitLengthException();
                
                foreach (string label in labels) _constValues.Add(label, constValue);
                labels.Clear();
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
    
    private void ParseInstructions(string[] lines)
    {
        int instructionCounter = 0;
        
        for (int i = 0; i < lines.Length; i++)
        {
            if (lines[i].IsWhiteSpace()) continue;
            
            string trimmedLine = lines[i].Trim();
            // Removes comments
            trimmedLine = Regex.Match(trimmedLine, @"^[^#]*").Value;
            if (trimmedLine.IsWhiteSpace()) return;
            
            int stringLoc = 0;
            
            Match matchLabel = Regex.Match(trimmedLine, @"^[^:][a-zA-Z\d_.]+:");
            // if its a label try to find out if its an command
            if (matchLabel.Success)
            {
                // its a label
                stringLoc = matchLabel.Index + matchLabel.Length;
            }
            
            string nextLine = trimmedLine.Substring(stringLoc).Trim();
            
            Match matchConstant = Regex.Match(nextLine, @"^\.[a-zA-Z.]+\b");
            Match matchCommand = Regex.Match(nextLine, @"^[^.][a-zA-Z.]+\b");
            
            if (matchConstant.Success)
            {
                continue;
            }
            else if (matchCommand.Success)
            {
                // its a command
                
                // Figure out setup
                RiscVInstruction? instr;
                if (!_instructions.Instructions.TryGetValue(matchCommand.Value.Trim(), out instr))
                {
                    throw new NoInstructionFoundException();
                }
                RiscVArguments args = new();
                
                RiscVCommand newLine = new(instr, args, i+1, trimmedLine, instructionCounter);
                // And then fill in arguments
                
                string[] argsStrings = nextLine.Substring(matchCommand.Index + matchCommand.Length).Split(",");
                
                if (argsStrings.Length != instr.Arguments.Length)
                {
                    if (instr.Arguments[instr.Arguments.Length-1] == EArgumentTypes.ROUNDING && argsStrings.Length == instr.Arguments.Length-1)
                    {
                        args.rm = 0;
                    }
                    else
                    {
                        throw new WrongRegisterException(); // TODO fix
                    }
                }
                
                for (int j = 0; j < argsStrings.Length; j++)
                {
                    newLine.ParseArgument(argsStrings[j], instr.Arguments[j], _constValues, _jumpPoints);
                }
                
                _commands.Add(newLine);
                instructionCounter ++;
            }
        }
    }
    
    public void PrintAllInstructionInfos()
    {
        PrintToConsole("Full instruction info");
        foreach (RiscVCommand c in _commands)
        {
            PrintToConsole(BuildInstructionInfo(c));
        }
    }
    
    public void PrintInstructionInfoRun(RiscVCommand instruction)
    {
        StringBuilder output = new();
        string machineCode = BuildInstructionInfo(instruction);
        
        output.Append($"[{instruction.LineNumber}] {machineCode.ToString()} {instruction.CommandInfo} {instruction.Instr.InstructionInfo}");
        
        PrintToConsole(output.ToString());
    }
    
    public string BuildInstructionInfo(RiscVCommand instruction)
    {
        StringBuilder output = new();
        
        int instructionMachineCode = 0;
        switch (instruction.Instr.InstructionFormat)
        {
        case EInstructionFormat.R:
            if (instruction.Args.rd == null
                || instruction.Args.rs1 == null
                || (instruction.Args.rs2 == null && instruction.Instr.rs2f == null)
                || instruction.Instr.Funct3 == null
                || instruction.Instr.Funct7 == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 0, 5, 15);
            
            if (instruction.Instr.rs2f != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.rs2f, 0, 5, 20);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 0, 5, 20);
            }
            if (instruction.Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct7, 0, 7, 25);
            break;
        
        case EInstructionFormat.I:
            if (instruction.Args.rd == null
                || instruction.Args.rs1 == null
                || instruction.Args.imm == null
                || instruction.Instr.Funct3 == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 0, 5, 15);
            if (instruction.Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 0, 12, 20);
            
            break;
        
        case EInstructionFormat.S:
            if (instruction.Args.rs1 == null
                || instruction.Args.rs2 == null
                || instruction.Args.imm == null
                || instruction.Instr.Funct3 == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 0, 5, 20);
            if (instruction.Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 0, 5, 7, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 5, 7, 25);
            
            break;
        
        case EInstructionFormat.B:
            if (instruction.Args.rs1 == null
                || instruction.Args.rs2 == null
                || instruction.Args.imm == null
                || instruction.Instr.Funct3 == null) throw new InvalidArgsException();
        
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 0, 5, 20);
            if (instruction.Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 11, 1, 7, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 1, 4, 8, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 5, 6, 25, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 12, 1, 31);
            
            break;
        
        case EInstructionFormat.U:
            if (instruction.Args.rd == null ||
                instruction.Args.imm == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 12, 20, 12);
            
            break;
        
        case EInstructionFormat.J:
            if (instruction.Args.rd == null
                || instruction.Args.imm == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 12, 8, 12, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 11, 1, 20, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 1, 10, 21, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.imm, 20, 1, 31);
            
            break;
        
        case EInstructionFormat.R4:
            if (instruction.Args.rd == null
                || instruction.Args.rs1 == null
                || instruction.Args.rs2 == null
                || instruction.Args.fs3 == null
                || instruction.Instr.Funct3 == null
                || instruction.Instr.Funct7 == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 0, 5, 20);
            if (instruction.Args.rm != null)
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rm, 0, 3, 12);
            }
            else
            {
                instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
            }
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct7, 0, 2, 25, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.fs3, 0, 5, 27);
            
            break;
        
        default:
            
            break;
        }
        
        output.Append($"0x{instructionMachineCode.ToString("x8")}");
        
        return output.ToString();
    }
}
