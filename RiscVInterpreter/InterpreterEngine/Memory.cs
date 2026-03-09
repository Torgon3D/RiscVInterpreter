

using System;

public class Memory
{
    byte[] internalMemory;
    
    public Memory(int memorySize)
    {
        internalMemory = new byte[memorySize];
    }
}