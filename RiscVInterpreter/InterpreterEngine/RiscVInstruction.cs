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
                            string instructionInfo,
                            byte opcode,
                            byte? funct3 = null,
                            byte? funct7 = null)
    {
        InstructionFunction = instructionFunction;
        InstructionFormat = instructionFormat;
        InstructionInfo = instructionInfo;
        Arguments = arguments;
        Opcode = opcode;
        Funct3 = funct3;
        Funct7 = funct7;
    }
    
    public void RunFunction(RiscVArguments args)
    {
        
        
        InstructionFunction.Invoke(args);
    }
}
