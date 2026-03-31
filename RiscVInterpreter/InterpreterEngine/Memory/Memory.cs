using System;

namespace RiscVInterpreterEngine;

public class Memory
{
    byte[] _internalMemory;
    
    public Memory(int memorySize)
    {
        _internalMemory = new byte[memorySize];
    }
    
    public void SaveToAdress(int adress, byte[] bytes)
    {
        CheckAdressSpace(adress, bytes.Length);
        
        Array.Copy(bytes, 0, _internalMemory, adress, bytes.Length);
    }
    
    public byte[] ReadFromAdress(int adress, int length)
    {
        CheckAdressSpace(adress, length);
        
        byte[] returnBytes = new byte[length];
        Array.Copy(_internalMemory, adress, returnBytes, 0, length);
        
        return returnBytes;
    }
    
    private void CheckAdressSpace(int adress, int length)
    {
        if (adress + (length-1) > (_internalMemory.Length-1) || adress < 0 || length < 0)
        {
            throw new MemoryOutOfRangeException($"Adress {adress}-{adress + length}");
        }
    }
    
    private class MemoryOutOfRangeException : Exception
    {
        public MemoryOutOfRangeException(string message)
        {
            message = "Memory out of range thrown in: " + message;
        }
    }
}