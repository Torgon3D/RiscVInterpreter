

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
        registerValue = SignExtend(BitConverter.GetBytes(value), false);
    }
    
    public ushort GetAsUInt16()
    {
        return BitConverter.ToUInt16(registerValue);
    }
    
    public void SetFromUInt16(ushort value)
    {
        registerValue = SignExtend(BitConverter.GetBytes(value), true);
    }
    
    public sbyte GetAsInt8()
    {
        return unchecked((sbyte)registerValue[0]);
    }
    
    public void SetFromInt8(byte value)
    {
        registerValue = SignExtend([value], false);
    }
    
    public byte GetAsUInt8()
    {
        return registerValue[0];
    }
    
    public void SetFromUInt8(byte value)
    {
        registerValue = SignExtend([value], true);
    }
    
    private byte[] SignExtend(byte[] bytes, bool isUnsigned)
    {
        const byte negative = 0b1111_1111;
        const byte positive = 0b0000_0000;
        
        byte extentions;
        byte[] extendedBytes = new byte[4];
        
        bytes.CopyTo(extendedBytes, 0);
        
        if (isUnsigned || ((bytes[bytes.Length-1] & 0b1000_0000) == 0))
        {
            extentions = positive;
        }
        else
        {
            extentions = negative;
        }
        
        for (int i = bytes.Length; i < 4; i++)
        {
            extendedBytes[i] = extentions;
        }
        
        return extendedBytes;
    }
}