using System.Diagnostics;
using RiscVInterpreterEngine;

namespace TestProject1;

[TestClass]
public sealed class Test1
{
string tester = 
@"hello: .byte -128
hello2: .byte 127

    hi:
      addi        x1,a5,    100            
";
    
    [TestMethod]
    public void TestMethod1()
    {
        Interpreter testInt = new(PrintTest, null, null);
        testInt.Start(tester.Split("\n"));
    }
    [TestMethod]
    public void TestMethod2()
    {
        int a = -123128832;
        
        Debug.Print("" + (a >>> 2).ToString("b32"));
        Debug.Print("" + (a >> 2).ToString("b32"));
    }
    
    public void PrintTest(string msg)
    {
        Debug.Print(msg + " It work :>");
    }
    
    public void TestFunc(RiscVArguments args){}
}
