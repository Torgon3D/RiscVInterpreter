

using System;
using System.ComponentModel;

public class Register
{
    byte[] registerValue;
    
    public Register(int size)
    {
        registerValue = new byte[size];
    }
    
    public int GetAsInt32()
    {
        return BitConverter.ToInt32(registerValue);
    }
    
    public void SetFromInt32(int value)
    {
        registerValue = BitConverter.GetBytes(value);
    }
    
    public uint GetAsUInt32()
    {
        return BitConverter.ToUInt32(registerValue);
    }
    
    public void SetFromUInt32(uint value)
    {
        registerValue = BitConverter.GetBytes(value);
    }
    
    public short GetAsInt16()
    {
        return BitConverter.ToInt16(registerValue);
    }
    
    public void SetFromInt16(short value)
    {
        registerValue = BitConverter.GetBytes(value);
    }
    
    public ushort GetAsUInt16()
    {
        return BitConverter.ToUInt16(registerValue);
    }
    
    /* public void SetFromUInt16(ushort value)
    {
        registerValue = BitConverter.GetBytes(value);
    }
    
    public int GetAsInt8()
    {
        return BitConverter.To(registerValue);
    }
    
    public void SetFromInt8(int value)
    {
        registerValue = BitConverter.GetBytes(value);
    }
    
    public uint GetAsUInt8()
    {
        return BitConverter.ToUInt32(registerValue);
    }
    
    public void SetFromUInt8(byte value)
    {
        registerValue[0] = value;
    } */
}