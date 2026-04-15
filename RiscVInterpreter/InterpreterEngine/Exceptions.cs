using System;
namespace RiscVInterpreterEngine;

public class InvalidArgsException : Exception
{
    const string msg = "";
    
    public InvalidArgsException(string extraInfo) : base(msg + extraInfo){}
    public InvalidArgsException() : base(msg){}
}

public class CouldNotParseToIntException : Exception
{
    const string msg = "";
    
    public CouldNotParseToIntException(string extraInfo) : base(msg + extraInfo){}
    public CouldNotParseToIntException() : base(msg){}
}

public class NoLabelForConstantException : Exception
{
    const string msg = "";
    
    public NoLabelForConstantException(string extraInfo) : base(msg + extraInfo){}
    public NoLabelForConstantException() : base(msg){}
}

public class ConstantTypeDoesNotExistException : Exception
{
    const string msg = "";
    
    public ConstantTypeDoesNotExistException(string extraInfo) : base(msg + extraInfo){}
    public ConstantTypeDoesNotExistException() : base(msg){}
}

public class IncorrectBitLengthException : Exception
{
    const string msg = "Bad bit length";
    
    public IncorrectBitLengthException(string extraInfo) : base(msg + extraInfo){}
    public IncorrectBitLengthException() : base(msg){}
}



public class NoInstructionFoundException : Exception
{
    const string msg = "Bad bit length";
    
    public NoInstructionFoundException(string extraInfo) : base(msg + extraInfo){}
    public NoInstructionFoundException() : base(msg){}
}

public class InvalidArgsCountException : Exception
{
    const string msg = "Bad bit length";
    
    public InvalidArgsCountException(string extraInfo) : base(msg + extraInfo){}
    public InvalidArgsCountException() : base(msg){}
}

public class WrongRegisterException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongRegisterException(string extraInfo) : base(msg + extraInfo){}
    public WrongRegisterException() : base(msg){}
}

public class WrongLabelException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongLabelException(string extraInfo) : base(msg + extraInfo){}
    public WrongLabelException() : base(msg){}
}

public class WrongImmidiateSizeException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongImmidiateSizeException(string extraInfo) : base(msg + extraInfo){}
    public WrongImmidiateSizeException() : base(msg){}
}

public class WrongImmidiateFormatException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongImmidiateFormatException(string extraInfo) : base(msg + extraInfo){}
    public WrongImmidiateFormatException() : base(msg){}
}

public class WrongRoundingModeException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongRoundingModeException(string extraInfo) : base(msg + extraInfo){}
    public WrongRoundingModeException() : base(msg){}
}

public class WrongMemoryArgumentException : Exception
{
    const string msg = "Bad bit length";
    
    public WrongMemoryArgumentException(string extraInfo) : base(msg + extraInfo){}
    public WrongMemoryArgumentException() : base(msg){}
}
