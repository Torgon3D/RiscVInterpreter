using System;
using System.Collections.Generic;

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
    fR,
    fI,
    fS,
    fB,
    fU,
    fJ,
    pR,
    pI,
    pS,
    pB,
    pU,
    pJ,
}
public class Interpreter
{
    private Dictionary<string, int> constValues;
    private Dictionary<string, int> jumpPoints;
    //private Dictionary<string, RiscVInstruction> commands;
    private List<RunnableLine> commands;
    
    public void Start()
    {
        
    }
    
    public void End()
    {
        
    }
    
    private void ProgramLoop()
    {
        // Get info
        
        // Check validity
        
        // Execute command
        
        // Continue to next line
    }
    
    private void InterpretLine()
    {
        
    }
    
    private void ExtractConstants()
    {
        
    }
    
    private void RunInstruction()
    {
        
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
    private Action<RiscVArguments> _instructionFunction;
    private EInstructionFormat _instructionFormat;
    private string _instructionInfo;
    
    public RiscVInstruction(Action<RiscVArguments> instructionFunction,
                            EInstructionFormat instructionFormat, string instructionInfo)
    {
        _instructionFunction = instructionFunction;
        _instructionFormat = instructionFormat;
        _instructionInfo = instructionInfo;
    }
    
    public void RunFunction(RiscVArguments args)
    {
        
        
        _instructionFunction.Invoke(args);
    }
}
