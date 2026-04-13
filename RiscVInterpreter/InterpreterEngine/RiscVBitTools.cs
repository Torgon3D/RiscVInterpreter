using System;
using System.Diagnostics;

namespace RiscVInterpreterEngine;

public static class RiscVBitTools
{
    public static bool IsBitsWithinBounds(int inputBits, int bounds)
    {
        if (32 - int.LeadingZeroCount(Math.Abs(inputBits + (inputBits < 0 ? 1 : 0) )) < bounds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static int GetBitsAndSignWithinBounds(int inputBits, int start, int length, int offset, bool includeSign = true)
    {
        const int allBits = 0b1111111111111111111111111111111;
        int output = inputBits >> start;
        output &= allBits >> (31 - length);
        if (inputBits < 0 && includeSign)
        {
            output |= 1 << length;
        }
        
        return output << offset;
    }
}