using System;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace RiscVInterpreterEngine;

public class Register
{
    private byte[] _registerValue;
    
    public Register(int size)
    {
        _registerValue = new byte[size];
    }
    
    public int GetAsInt32()
    {
        return BitConverter.ToInt32(_registerValue);
    }
    
    public void SetFromInt32(int value)
    {
        _registerValue = BitConverter.GetBytes(value);
    }
    
    public uint GetAsUInt32()
    {
        return BitConverter.ToUInt32(_registerValue);
    }
    
    public void SetFromUInt32(uint value)
    {
        _registerValue = BitConverter.GetBytes(value);
    }
    
    public short GetAsInt16()
    {
        return BitConverter.ToInt16(_registerValue);
    }
    
    public void SetFromInt16(short value)
    {
        _registerValue = SignExtend(BitConverter.GetBytes(value), false);
    }
    
    public ushort GetAsUInt16()
    {
        return BitConverter.ToUInt16(_registerValue);
    }
    
    public void SetFromUInt16(ushort value)
    {
        _registerValue = SignExtend(BitConverter.GetBytes(value), true);
    }
    
    public sbyte GetAsInt8()
    {
        return unchecked((sbyte)_registerValue[0]);
    }
    
    public void SetFromInt8(byte value)
    {
        _registerValue = SignExtend([value], false);
    }
    
    public byte GetAsUInt8()
    {
        return _registerValue[0];
    }
    
    public void SetFromUInt8(byte value)
    {
        _registerValue = SignExtend([value], true);
    }
    
    private byte[] SignExtend(byte[] bytes, bool isUnsigned)
    {
        const byte negative = 0b1111_1111;
        const byte positive = 0b0000_0000;
        
        byte extentions;
        byte[] extendedBytes = new byte[4];
        
        bytes.CopyTo(extendedBytes, 0);
        
        if (isUnsigned || IsBytePositive(bytes[bytes.Length-1]))
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
    
    private bool IsBytePositive(byte check)
    {
        return (check & 0b1000_0000) == 0;
    }
}