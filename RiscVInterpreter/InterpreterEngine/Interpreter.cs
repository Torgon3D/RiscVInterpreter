

using System;
using System.Collections.Generic;
    public enum EInstructionFormat
    {
        R,
        I,
        S,
        B,
        U,
        J
    }
public class Interpreter
{

    
    private Dictionary<string, int> constValues;
    private Dictionary<string, int> jumpPoints;
    private Dictionary<string, RiscVInstruction> commands;
    
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
        if (!args.IsInstructionformatCorrect(_instructionFormat)) return; // TODO
        
        _instructionFunction.Invoke(args);
    }
}

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm;
    
    public RiscVArguments()
    {
        
    }
    
    public bool IsInstructionformatCorrect(EInstructionFormat format)
    {
        switch (format)
        {
        case EInstructionFormat.R:
            return rd != null && rs1 != null && rs2 != null;
            
        case EInstructionFormat.I:
            return rd != null && rs1 != null && imm != null;
            
        case EInstructionFormat.S:
            return rs1 != null && rs2 != null && imm != null;
            
        case EInstructionFormat.B:
            return rs1 != null && rs2 != null && imm != null;
            
        case EInstructionFormat.U:
            return rd != null && imm != null;
            
        case EInstructionFormat.J:
            return rd != null && imm != null;
            
        default:
            return false;
        }
    }
}
