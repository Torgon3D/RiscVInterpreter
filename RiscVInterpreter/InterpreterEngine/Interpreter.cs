using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

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
    private InstructionsetImplementations _instructions;
    private Dictionary<string, int> _constTypes = new()
    {
        { ".word", 32 },
        { ".half", 16 },
        { ".byte", 8  }
    };
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
        _instructions = new(_memController);
    }
    
    public void Start(string[] lines)
    {
        _memController.ResetMemory();
        
        ParseLineConstants(lines);
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

                if (!int.TryParse(nextLine.Substring(matchConstant.Index + matchConstant.Length).Trim(), out constValue)) throw new CouldNotParseLineException();
                
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
                RiscVInstruction instr;
                if (!_instructions.Instructions.TryGetValue(matchCommand.Value.Trim(), out instr))
                {
                    throw new NoInstructionFoundException();
                }
                RiscVArguments args = new();
                
                RunnableLine newLine = new(instr, args, i+1, instructionCounter);
                // And then fill in arguments
                
                string[] argsStrings = nextLine.Substring(matchCommand.Index + matchCommand.Length).Split(",");
                
                if (argsStrings.Length != instr.Arguments.Length)
                {
                    if (argsStrings.Length == instr.Arguments.Length -1
                        && instr.Arguments.Last<EArgumentTypes>() == EArgumentTypes.ROUNDING)
                    {
                        instr.Funct3 = 0;
                    }
                    else
                    {
                        throw new InvalidArgsCountException();
                    }
                }
                
                // Check and add all the args
                for (int j = 0; j < argsStrings.Length; j++)
                {
                    if (instr.Arguments[j] == EArgumentTypes.ROUNDING)
                    {
                        instr.Funct3 = (byte)args.GetRounding(argsStrings[j]);
                        continue;
                    }
                    
                    args.ParseArgument(argsStrings[j], instr.Arguments[j]);
                }
                
                _commands.Add(newLine);
                instructionCounter ++;
            }
        }
    }
    
    private void RunInstruction()
    {
        
    }
    
    public string BuildInstructionInfo(RunnableLine instruction)
    {
        StringBuilder output = new();
        
        int instructionMachineCode = 0;
        switch (instruction.Instr.InstructionFormat)
        {
        case EInstructionFormat.R:
            if (instruction.Args.rd == null
                || instruction.Args.rs1 == null
                || instruction.Args.rs2 == null
                || instruction.Instr.Funct3 == null
                || instruction.Instr.Funct7 == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 0, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
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
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
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
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0,  3, 12);
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
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
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
                || instruction.Instr.Funct3 == null
                || instruction.Instr.Funct7 == null) throw new InvalidArgsException();
            instructionMachineCode |= instruction.Instr.Opcode;
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rd, 0, 5, 7);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs1, 0, 5, 15);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Args.rs2, 0, 5, 20);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct3, 0, 3, 12);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct7, 0, 2, 25, false);
            instructionMachineCode |= RiscVBitTools.GetBitsAndSignWithinBounds((int)instruction.Instr.Funct7, 2, 5, 27);
            
            break;
        
        default:
            
            break;
        }
        output.Append($"[{instruction.LineNumber}] 0x{instructionMachineCode.ToString("x")} {instruction.InstructionInfo}");
        
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
    public EArgumentTypes[] Arguments;
    public string InstructionInfo;
    public byte Opcode;
    public byte? Funct3;
    public byte? Funct7;
    
    public RiscVInstruction(Action<RiscVArguments> instructionFunction,
                            EInstructionFormat instructionFormat,
                            EArgumentTypes[] arguments,
                            string instructionInfo)
    {
        InstructionFunction = instructionFunction;
        InstructionFormat = instructionFormat;
        InstructionInfo = instructionInfo;
        Arguments = arguments;
    }
    
    public void RunFunction(RiscVArguments args)
    {
        
        
        InstructionFunction.Invoke(args);
    }
}

public class InvalidArgsException : Exception
{
    const string msg = "";
    
    public InvalidArgsException(string extraInfo) : base(msg + extraInfo){}
    public InvalidArgsException() : base(msg){}
}

public class CouldNotParseLineException : Exception
{
    const string msg = "";
    
    public CouldNotParseLineException(string extraInfo) : base(msg + extraInfo){}
    public CouldNotParseLineException() : base(msg){}
}

public class NoLabelForConstantException : Exception
{
    const string msg = "";
    
    public NoLabelForConstantException(string extraInfo) : base(msg + extraInfo){}
    public NoLabelForConstantException() : base(msg){}
}

public class ConstantTypeDoesNotExistException : Exception
{
    const string msg = "";
    
    public ConstantTypeDoesNotExistException(string extraInfo) : base(msg + extraInfo){}
    public ConstantTypeDoesNotExistException() : base(msg){}
}

public class IncorrectBitLengthException : Exception
{
    const string msg = "Bad bit length";
    
    public IncorrectBitLengthException(string extraInfo) : base(msg + extraInfo){}
    public IncorrectBitLengthException() : base(msg){}
}



public class NoInstructionFoundException : Exception
{
    const string msg = "Bad bit length";
    
    public NoInstructionFoundException(string extraInfo) : base(msg + extraInfo){}
    public NoInstructionFoundException() : base(msg){}
}

public class InvalidArgsCountException : Exception
{
    const string msg = "Bad bit length";
    
    public InvalidArgsCountException(string extraInfo) : base(msg + extraInfo){}
    public InvalidArgsCountException() : base(msg){}
}
