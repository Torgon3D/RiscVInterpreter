using System;

namespace RiscVInterpreterEngine;

public class Memory
{
    int _memorySize;
    byte[] _internalMemory;
    Action<byte[]> _memoryUpdated;
    public Memory(int memorySize, Action<byte[]> memoryUpdated)
    {
        _memorySize = memorySize;
        _internalMemory = new byte[_memorySize];
        _memoryUpdated = memoryUpdated;
        
        _memoryUpdated(_internalMemory);
    }
    
    public void ResetMemory()
    {
        _internalMemory = new byte[_memorySize];
        _memoryUpdated(_internalMemory);
    }
    
    public byte[] GetAllMemory()
    {
        return _internalMemory;
    }
    
    public void SaveToAdress(int adress, byte[] bytes)
    {
        CheckAdressSpace(adress, bytes.Length);
        
        Array.Copy(bytes, 0, _internalMemory, adress, bytes.Length);
        _memoryUpdated(_internalMemory);
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