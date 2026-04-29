using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;

namespace RiscVInterpreterEngine;

public class Register
{
    static public List<string> RegisterNamesKey { get; private set; } =
    [
        "x0", 
        "x1",
        "x2",
        "x3",
        "x4",
        "x5",
        "x6",
        "x7",
        "x8",
        "x9",
        "x10",
        "x11",
        "x12",
        "x13",
        "x14",
        "x15",
        "x16",
        "x17",
        "x18",
        "x19",
        "x20",
        "x21",
        "x22",
        "x23",
        "x24",
        "x25",
        "x26",
        "x27",
        "x28",
        "x29",
        "x30",
        "x31",
        "f0",
        "f1",
        "f2",
        "f3",
        "f4",
        "f5",
        "f6",
        "f7",
        "f8",
        "f9",
        "f10",
        "f11",
        "f12",
        "f13",
        "f14",
        "f15",
        "f16",
        "f17",
        "f18",
        "f19",
        "f20",
        "f21",
        "f22",
        "f23",
        "f24",
        "f25",
        "f26",
        "f27",
        "f28",
        "f29",
        "f30",
        "f31",
    ];
    static public List<string> RegisterNamesFull { get; private set; } =
    [
        "x0,  zero",
        "x1,  ra",
        "x2,  sp",
        "x3,  gp",
        "x4,  tp",
        "x5,  t0",
        "x6,  t1",
        "x7,  t2",
        "x8,  s0",
        "x9,  s1",
        "x10, a0",
        "x11, a1",
        "x12, a2",
        "x13, a3",
        "x14, a4",
        "x15, a5",
        "x16, a6",
        "x17, a7",
        "x18, s2",
        "x19, s3",
        "x20, s4",
        "x21, s5",
        "x22, s6",
        "x23, s7",
        "x24, s8",
        "x25, s9",
        "x26, s10",
        "x27, s11",
        "x28, t3",
        "x29, t4",
        "x30, t5",
        "x31, t6",
        "f0,  ft0",
        "f1,  ft1",
        "f2,  ft2",
        "f3,  ft3",
        "f4,  ft4",
        "f5,  ft5",
        "f6,  ft6",
        "f7,  ft7",
        "f8,  fs0",
        "f9,  fs1",
        "f10, fa0",
        "f11, fa1",
        "f12, fa2",
        "f13, fa3",
        "f14, fa4",
        "f15, fa5",
        "f16, fa6",
        "f17, fa7",
        "f18, fs2",
        "f19, fs3",
        "f20, fs4",
        "f21, fs5",
        "f22, fs6",
        "f23, fs7",
        "f24, fs8",
        "f25, fs9",
        "f26, fs10",
        "f27, fs11",
        "f28, ft8",
        "f29, ft9",
        "f30, ft10",
        "f31, ft11",
    ];
    
    static public Dictionary<string, int> IntegerRegisterNames { get; private set; } = new()
    {
        { "x0",   0  }, { "zero", 0  },
        { "x1",   1  }, { "ra",   1  },
        { "x2",   2  }, { "sp",   2  },
        { "x3",   3  }, { "gp",   3  },
        { "x4",   4  }, { "tp",   4  },
        { "x5",   5  }, { "t0",   5  },
        { "x6",   6  }, { "t1",   6  },
        { "x7",   7  }, { "t2",   7  },
        { "x8",   8  }, { "s0",   8  },
        { "x9",   9  }, { "s1",   9  },
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
        { "f0",   0  }, { "ft0",  0  },
        { "f1",   1  }, { "ft1",  1  },
        { "f2",   2  }, { "ft2",  2  },
        { "f3",   3  }, { "ft3",  3  },
        { "f4",   4  }, { "ft4",  4  },
        { "f5",   5  }, { "ft5",  5  },
        { "f6",   6  }, { "ft6",  6  },
        { "f7",   7  }, { "ft7",  7  },
        { "f8",   8  }, { "fs0",  8  },
        { "f9",   9  }, { "fs1",  9  },
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
    private bool _canWrite;
    private string _visualID;
    
    private Action<string, int> _registerUpdated;
    
    public Register(int size, string visualID, Action<string, int> registerUpdated, bool canWrite = true)
    {
        _registerValue = new byte[size];
        _visualID = visualID;
        _registerUpdated = registerUpdated;
        _canWrite = canWrite;
    }
    
    public int GetAsInt32()
    {
        return BitConverter.ToInt32(_registerValue);
    }
    
    public void SetFromInt32(int value)
    {
        if (!_canWrite) return;
        
        _registerValue = BitConverter.GetBytes(value);
        
        _registerUpdated.Invoke(_visualID, BitConverter.ToInt32(_registerValue));
    }
    
    public uint GetAsUInt32()
    {
        return BitConverter.ToUInt32(_registerValue);
    }
    
    public void SetFromUInt32(uint value)
    {
        if (!_canWrite) return;
        
        _registerValue = BitConverter.GetBytes(value);
        
        _registerUpdated.Invoke(_visualID, BitConverter.ToInt32(_registerValue));
    }
    
    public short GetAsInt16()
    {
        return BitConverter.ToInt16(_registerValue);
    }
    
    public void SetFromInt16(short value)
    {
        if (!_canWrite) return;
        
        _registerValue = SignExtend(BitConverter.GetBytes(value), false);
        
        _registerUpdated.Invoke(_visualID, BitConverter.ToInt32(_registerValue));
    }
    
    public ushort GetAsUInt16()
    {
        return BitConverter.ToUInt16(_registerValue);
    }
    
    public void SetFromUInt16(ushort value)
    {
        _registerValue = SignExtend(BitConverter.GetBytes(value), true);
        
        _registerUpdated.Invoke(_visualID, BitConverter.ToInt32(_registerValue));
    }
    
    public sbyte GetAsInt8()
    {
        return unchecked((sbyte)_registerValue[0]);
    }
    
    public void SetFromInt8(byte value)
    {
        if (!_canWrite) return;
        
        _registerValue = SignExtend([value], false);
        
        _registerUpdated.Invoke(_visualID, BitConverter.ToInt32(_registerValue));
    }
    
    public byte GetAsUInt8()
    {
        return _registerValue[0];
    }
    
    public void SetFromUInt8(byte value)
    {
        if (!_canWrite) return;
        
        _registerValue = SignExtend([value], true);
        
        _registerUpdated.Invoke(_visualID, BitConverter.ToInt32(_registerValue));
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