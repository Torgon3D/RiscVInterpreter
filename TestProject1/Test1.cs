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
<<<<<<< HEAD
      addi   x1, a5, 100            
=======
      addi   x1,x0, 0100            
>>>>>>> de587daea8fe575a2a910ce4f1c9003e532ed1d5
";
    
    [TestMethod]
    public void TestMethod1()
    {
        Interpreter testInt = new(PrintTest);
        testInt.Start(tester.Split("\n"));
    }
    
    public void PrintTest(string msg)
    {
        Debug.Print(msg + " It work :>");
    }
    
    public void TestFunc(RiscVArguments args){}
}
