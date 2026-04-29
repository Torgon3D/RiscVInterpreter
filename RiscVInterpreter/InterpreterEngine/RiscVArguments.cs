using System;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RiscVInterpreterEngine;

public enum EArgumentTypes : short
{
    RD = 1,
    RS1,
    RS2,
    IMM,
    FD,
    FS1,
    FS2,
    FR3,
    MEMORY,
    LABEL,
    ROUNDING
}

public class RiscVArguments
{
    public int? rd, rs1, rs2, imm, frs3, rm;
    
    
    public RiscVArguments()
    {
        
    }
    
    public void HasArgumentsFilled(EArgumentTypes[] arguments)
    {
        foreach (var arg in arguments)
        {
            switch (arg)
            {
            case EArgumentTypes.RD:
                if (rd != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.RS1:
                if (rs1 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.RS2:
                if (rs2 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.IMM:
                if (imm != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FD:
                if (rd != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FS1:
                if (rs1 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FS2:
                if (rs2 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.FR3:
                if (frs3 != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.MEMORY:
                if (rs1 != null && imm != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.LABEL:
                if (imm != null)
                {
                    continue;
                }
                break;
                
            case EArgumentTypes.ROUNDING:
                if (rm != null)
                {
                    continue;
                }
                break;
                
            default:
                break;
            }
            
            throw new InvalidArgsException();
        }
    }
    
    public int GetRD()
    {
        if (rd != null)
        {
            return (int)rd;
        }
        else
        {
            throw new InvalidArgsException();
        }
    }
    public int GetRS1()
    {
        if (rs1 != null)
        {
            return (int)rs1;
        }
        else
        {
            throw new InvalidArgsException();
        }
    }
    public int GetRS2()
    {
        if (rs2 != null)
        {
            return (int)rs2;
        }
        else
        {
            throw new InvalidArgsException();
        }
    }
    public int GetFSR3()
    {
        if (frs3 != null)
        {
            return (int)frs3;
        }
        else
        {
            throw new InvalidArgsException();
        }
    }
    public int GetIMM()
    {
        if (imm != null)
        {
            return (int)imm;
        }
        else
        {
            throw new InvalidArgsException();
        }
    }
    public bool TryGetRM(out int roundMode)
    {
        if (rm != null)
        {
            roundMode = (int)rm;
            return true;
        }
        else
        {
            roundMode = 0;
            return false;
        }
    }
    
    public int GetJumpAmount()
    {
        if (imm == null)
        {
            throw new InvalidArgsException();
        }
        
        if (imm % 4 != 0)
        {
            throw new WrongImmidiateFormatException();
        }
        
        return (int)imm / 4;
    }
}
