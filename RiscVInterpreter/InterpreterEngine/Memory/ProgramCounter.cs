

using System;

public class ProgramCounter
{
    private uint _PC;
    
    public ProgramCounter(uint startLoc)
    {
        _PC = startLoc;
    }
    
    public void IncrementPC()
    {
        _PC += 1;
    }
    
    public void AddPCFromBytes(byte[] addition)
    {
        _PC += BitConverter.ToUInt32(addition);
    }
    
    public void AddPCFromUInt32(uint addition)
    {
        _PC += addition;
    }
    
    public void SetPCFromBytes(byte[] newVal)
    {
        _PC = BitConverter.ToUInt32(newVal);
    }
    
    public void SetPCFromUInt32(uint newVal)
    {
        _PC = newVal;
    }
    
    public uint GetAsUInt32()
    {
        return _PC;
    }
    
    public byte[] GetAsBytes()
    {
        return BitConverter.GetBytes(_PC);
    }
}
