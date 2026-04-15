using System;

namespace RiscVInterpreterEngine;

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
