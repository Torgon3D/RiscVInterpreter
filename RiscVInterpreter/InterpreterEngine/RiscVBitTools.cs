using System;

namespace RiscVInterpreterEngine;

public static class RiscVBitTools
{
    public static bool IsBitsWithinBounds(int inputBits, int bounds)
    {
        if (32 - int.LeadingZeroCount(Math.Abs(inputBits)) < bounds)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public static int GetBitsAndSignWithinBounds(int inputBits, int bounds, int offset = 0)
    {
        const int allBits = ~0;
        
        int output = inputBits;
        
        if (inputBits < 0)
        {
            output &= (allBits >> (32-bounds));
        }
        
        return output << offset;
    }
}