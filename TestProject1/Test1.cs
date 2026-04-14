using System.Diagnostics;
using RiscVInterpreterEngine;

namespace TestProject1;

[TestClass]
public sealed class Test1
{
string tester = 
@"
hello: .byte -128
hello2: .byte 127 hei

    hi:addi x1, x0, 20
asdio
fgdsfgsdjkil
";
    
    [TestMethod]
    public void TestMethod1()
    {
        try
        {
            Interpreter testInt = new(PrintTest);
            testInt.Start(tester.Split("\n"));
            
            for (int i = 0; i < 7; i++)
            {
                RiscVArguments args;
                RiscVInstruction instr;
                RunnableLine r;
                switch (i)
                {
                case 0:
                    args = new();
                    instr = new(TestFunc, EInstructionFormat.R, "sub rd, r1, r2");
                    args.rd = 1;
                    args.rs1 = 10;
                    args.rs2 = 11;
                    instr.Opcode = 0b0110011;
                    instr.Funct3 = 0x0;
                    instr.Funct7 = 0x20;
                    r = new(instr, args, i, i+1);
                    break;
                case 1:
                    args = new();
                    instr = new(TestFunc, EInstructionFormat.I, "xori rd, r1, imm");
                    args.imm = -237;
                    args.rs1 = 10;
                    args.rd = 1;
                    instr.Opcode = 0b0010011;
                    instr.Funct3 = 0x4;
                    r = new(instr, args, i, i+1);
                    break;
                case 2:
                    args = new();
                    instr = new(TestFunc, EInstructionFormat.S, "sw rd, r1, r2");
                    args.imm = -200;
                    args.rs1 = 2;
                    args.rs2 = 1;
                    instr.Opcode = 0b0100011;
                    instr.Funct3 = 0x2;
                    r = new(instr, args, i, i+1);
                    break;
                case 3:
                    args = new();
                    instr = new(TestFunc, EInstructionFormat.B, "bge rd, r1, r2");
                    args.imm = -200;
                    args.rs1 = 2;
                    args.rs2 = 1;
                    instr.Opcode = 0b1100011;
                    instr.Funct3 = 0x5;
                    r = new(instr, args, i, i+1);
                    break;
                case 4:
                    args = new();
                    instr = new(TestFunc, EInstructionFormat.U, "lui rd, imm");
                    args.imm = 0x10020000;
                    args.rd = 1;
                    instr.Opcode = 0b0110111;
                    r = new(instr, args, i, i+1);
                    break;
                case 5:
                    args = new();
                    instr = new(TestFunc, EInstructionFormat.J, "jal rd, imm");
                    args.imm = -523610;
                    args.rd = 1;
                    instr.Opcode = 0b1101111;
                    r = new(instr, args, i, i+1);
                    break;
                case 6:
                    args = new();
                    instr = new(TestFunc, EInstructionFormat.R4, "fmadd rd, r1, r2, r3");
                    args.rd = 0;
                    args.rs1 = 2;
                    args.rs2 = 1;
                    instr.Opcode = 0b1000011;
                    instr.Funct3 = 0x0;
                    instr.Funct7 = 0x14;
                    r = new(instr, args, i, i+1);
                    break;
                default: continue;
                }
                Debug.Print(testInt.BuildInstructionInfo(r));
            }
            
        } catch (Exception e)
        {
            Debug.Print("IT FUCKING LOMMM: " + e.Message);
        }
    }
    
    public void PrintTest(string msg)
    {
        Debug.Print(msg);
    }
    
    public void TestFunc(RiscVArguments args){}
}
