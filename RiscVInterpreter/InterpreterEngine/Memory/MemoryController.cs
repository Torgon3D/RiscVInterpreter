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
    
    public MemoryController(Action<string, int> registerUpdated, Action<byte[]> memoryUpdated)
    {
        // Create the x0 non writeable register
        IntegerRegisters[0] = new Register(4, Register.RegisterNamesKey[0], registerUpdated, false);
        
        for (int i = 1; i < 32; i++)
        {
            IntegerRegisters[i] = new Register(4, Register.RegisterNamesKey[i], registerUpdated);
        }
        for (int i = 32; i < 64; i++)
        {
            FloatRegisters[i - 32] = new Register(4, Register.RegisterNamesKey[i], registerUpdated);
        }
        MemoryStuff = new Memory(MemorySize, memoryUpdated);
    }
    
    public void ResetMemory()
    {
        PC.SetPCFromUInt32(0);
        MemoryStuff.ResetMemory();
        
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