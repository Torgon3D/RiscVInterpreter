using System;

namespace RiscVInterpreterEngine;

public class ProgramCounter
{
    private uint _PC;
    
    public ProgramCounter(uint startLoc)
    {
        _PC = startLoc;
    }
    
    public void IncrementPC()
    {
        _PC += 4;
    }
    
    public void AddPCFromBytes(byte[] addition)
    {
        _PC += BitConverter.ToUInt32(addition);
    }
    
    public void AddPCFromInt32(int addition)
    {
        _PC = (uint)(_PC + addition);
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
    
    public int GetAsInt32()
    {
        return (int)_PC;
    }
    
    public byte[] GetAsBytes()
    {
        return BitConverter.GetBytes(_PC);
    }
    
    public int GetForCommandArray()
    {
        if (_PC % 4 != 0) throw new PCSizeException($" PC: {_PC}");
        
        return (int)_PC / 4;
    }
}

public class PCSizeException : Exception
{
    const string msg = $"PC is is not divisible by 4.";
    
    public PCSizeException(string extraInfo) : base(msg + extraInfo){}
    public PCSizeException() : base(msg){}
}
