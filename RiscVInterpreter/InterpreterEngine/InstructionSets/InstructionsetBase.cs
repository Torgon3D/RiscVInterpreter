using System.Collections.Generic;

namespace RiscVInterpreterEngine;
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
    }
}
