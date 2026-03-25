

using System;
using System.Collections.Generic;
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
        if (!args.GetArgumentInfo(_instructionFormat)) return; // TODO
        
        _instructionFunction.Invoke(args);
    }
}

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm;
    
    public RiscVArguments()
    {
        
    }
    
    public byte GetArgumentInfo(EInstructionFormat format)
    {
        byte returnval = 0;
        
        returnval += (byte)((rd != null ? 1 : 0) << 0);
        returnval += (byte)((rs1 != null ? 1 : 0) << 1);
        returnval += (byte)((rs2 != null ? 1 : 0) << 2);
        returnval += (byte)((imm != null ? 1 : 0) << 3);
        
        return returnval;
    }
}
// Include all the base information and checker functions that are needed
public abstract class InstructionsetBase
{
    public Dictionary<string, RiscVInstruction> Instructions = new();
}
// Do this for every implementation in their own file
public partial class InstructionsetImplementations : InstructionsetBase
{
    InstructionsetImplementations()
    {
        Instructions.
    }
}
