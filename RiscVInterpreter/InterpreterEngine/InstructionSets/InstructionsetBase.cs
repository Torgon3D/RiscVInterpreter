using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace RiscVInterpreterEngine;
// Include all the base information and checker functions that are needed
public abstract class InstructionsetBase
{
    public Dictionary<string, RiscVInstruction> Instructions = new();
    protected MemoryController _memory;
    
    public InstructionsetBase(MemoryController memory)
    {
        _memory = memory;
    }
}
// Do this for every implementation in their own file

public partial class InstructionsetImplementations : InstructionsetBase
{
    public InstructionsetImplementations(MemoryController memory) : base(memory)
    {
        Int32Constructor();
        Mul32Constructor();
        Float32Constructor();
        
        Debug.Print(Instructions.Count + "Yay");
    }
}
