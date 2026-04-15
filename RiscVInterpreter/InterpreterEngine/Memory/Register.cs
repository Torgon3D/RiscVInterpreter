using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace RiscVInterpreterEngine;

public class Register
{
    static public Dictionary<string, int> IntegerRegisterNames { get; private set; } = new()
    {
        { "x0 ",  0  }, { "zero", 0  },
        { "x1 ",  1  }, { "ra",   1  },
        { "x2 ",  2  }, { "sp",   2  },
        { "x3 ",  3  }, { "gp",   3  },
        { "x4 ",  4  }, { "tp",   4  },
        { "x5 ",  5  }, { "t0",   5  },
        { "x6 ",  6  }, { "t1",   6  },
        { "x7 ",  7  }, { "t2",   7  },
        { "x8 ",  8  }, { "s0",   8  },
        { "x9 ",  9  }, { "s1",   9  },
        { "x10",  10 }, { "a0",   10 },
        { "x11",  11 }, { "a1",   11 },
        { "x12",  12 }, { "a2",   12 },
        { "x13",  13 }, { "a3",   13 },
        { "x14",  14 }, { "a4",   14 },
        { "x15",  15 }, { "a5",   15 },
        { "x16",  16 }, { "a6",   16 },
        { "x17",  17 }, { "a7",   17 },
        { "x18",  18 }, { "s2",   18 },
        { "x19",  19 }, { "s3",   19 },
        { "x20",  20 }, { "s4",   20 },
        { "x21",  21 }, { "s5",   21 },
        { "x22",  22 }, { "s6",   22 },
        { "x23",  23 }, { "s7",   23 },
        { "x24",  24 }, { "s8",   24 },
        { "x25",  25 }, { "s9",   25 },
        { "x26",  26 }, { "s10",  26 },
        { "x27",  27 }, { "s11",  27 },
        { "x28",  28 }, { "t3",   28 },
        { "x29",  29 }, { "t4",   29 },
        { "x30",  30 }, { "t5",   30 },
        { "x31",  31 }, { "t6",   31 },
    };
    static public Dictionary<string, int> FloatRegisterNames { get; private set; } = new()
    {
        { "f0 ",  0  }, { "ft0",  0  },
        { "f1 ",  1  }, { "ft1",  1  },
        { "f2 ",  2  }, { "ft2",  2  },
        { "f3 ",  3  }, { "ft3",  3  },
        { "f4 ",  4  }, { "ft4",  4  },
        { "f5 ",  5  }, { "ft5",  5  },
        { "f6 ",  6  }, { "ft6",  6  },
        { "f7 ",  7  }, { "ft7",  7  },
        { "f8 ",  8  }, { "fs0",  8  },
        { "f9 ",  9  }, { "fs1",  9  },
        { "f10",  10 }, { "fa0",  10 },
        { "f11",  11 }, { "fa1",  11 },
        { "f12",  12 }, { "fa2",  12 },
        { "f13",  13 }, { "fa3",  13 },
        { "f14",  14 }, { "fa4",  14 },
        { "f15",  15 }, { "fa5",  15 },
        { "f16",  16 }, { "fa6",  16 },
        { "f17",  17 }, { "fa7",  17 },
        { "f18",  18 }, { "fs2",  18 },
        { "f19",  19 }, { "fs3",  19 },
        { "f20",  20 }, { "fs4",  20 },
        { "f21",  21 }, { "fs5",  21 },
        { "f22",  22 }, { "fs6",  22 },
        { "f23",  23 }, { "fs7",  23 },
        { "f24",  24 }, { "fs8",  24 },
        { "f25",  25 }, { "fs9",  25 },
        { "f26",  26 }, { "fs10", 26 },
        { "f27",  27 }, { "fs11", 27 },
        { "f28",  28 }, { "ft8",  28 },
        { "f29",  29 }, { "ft9",  29 },
        { "f30",  30 }, { "ft10", 30 },
        { "f31",  31 }, { "ft11", 31 },
    };
    
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