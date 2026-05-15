using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text.RegularExpressions;

namespace RiscVInterpreterEngine;

public struct RiscVCommand
{
    public RiscVCommand(RiscVInstruction instr, RiscVArguments args, int lineNumber, string lineText, int instructionNumber)
    {
        Instr = instr;
        Args = args;
        LineNumber = lineNumber;
        InstructionNumber = instructionNumber;
        
        CommandInfo = Regex.Replace(lineText.Trim(), @"\s+", " ");
    }
    
    public RiscVInstruction Instr;
    public RiscVArguments Args;
    public int LineNumber;
    public int InstructionNumber;
    public string CommandInfo;
    
    
    public void Run()
    {
        Instr.RunFunction(Args);
    }
    
}
