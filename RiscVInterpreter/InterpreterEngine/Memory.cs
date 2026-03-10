

using System;

public class Memory
{
    byte[] internalMemory;
    
    public Memory(int memorySize)
    {
        internalMemory = new byte[memorySize];
    }
    
    public void SaveToAdress(int adress, byte[] bytes)
    {
        if (!IsAdressSpaceViable(adress, bytes.Length)) return;
        
        Array.Copy(bytes, 0, internalMemory, adress, bytes.Length);
    }
    
    public byte[]? ReadFromAdress(int adress, int length)
    {
        if (!IsAdressSpaceViable(adress, length)) return null;
        
        byte[] returnBytes = new byte[length];
        Array.Copy(internalMemory, adress, returnBytes, 0, length);
        
        return returnBytes;
    }
    
    private bool IsAdressSpaceViable(int adress, int length)
    {
        if (adress + (length-1) > (internalMemory.Length-1) || adress < 0 || length < 0)
        {
            return false;
        }
        
        return true;
    }
}