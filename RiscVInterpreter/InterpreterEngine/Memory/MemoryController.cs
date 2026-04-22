using System;
using System.Collections.Generic;

namespace RiscVInterpreterEngine;

public class MemoryController
{
    public Register[] IntegerRegisters { get; private set; } = new Register[32];
    public Register[] FloatRegisters { get; private set; } = new Register[32];
    public ProgramCounter PC { get; private set; } = new ProgramCounter(0);
    public Memory MemoryStuff { get; private set; }
    public int MemorySize = 256;
    
    public MemoryController()
    {
        // Create the x0 non writeable register
        IntegerRegisters[0] = new Register(4, false);
        Array.Fill<Register>(IntegerRegisters, new Register(4), 1, 31);
        Array.Fill<Register>(FloatRegisters, new Register(4));
        MemoryStuff = new Memory(MemorySize);
    }
    
    public void ResetMemory()
    {
        PC.SetPCFromUInt32(0);
        MemoryStuff = new Memory(MemorySize);
        
        foreach (Register r in IntegerRegisters)
        {
            r.SetFromInt32(0);
        }
        
        foreach (Register r in FloatRegisters)
        {
            r.SetFromInt32(0);
        }
    }
}