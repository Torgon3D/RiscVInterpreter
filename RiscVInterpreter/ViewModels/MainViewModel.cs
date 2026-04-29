using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using RiscVInterpreterEngine;

namespace RiscVInterpreter.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    static int[] Speeds = [
        512,
        256,
        128,
        64,
        32,
        16,
        8,
        4,
        2,
        1
    ];
    
    [ObservableProperty]
    private TextDocument _editableTextDocument = new("");
    
    [ObservableProperty]
    private int _consoleScrollbarLength;
    
    [ObservableProperty]
    private int _selectedSpeed = 3;
    
    [ObservableProperty]
    private string _memoryText = "";
    [ObservableProperty]
    private string _ConsoleText = "";
    
    private Interpreter _interpreterEngine;
    [ObservableProperty]
    private int _tempMemoryLocation = 0;
    public int MemoryLocation = 0;
    
    
    
    public ObservableCollection<MemoryUI> RegisterIntList {get;set;}
    public ObservableCollection<MemoryUI> RegisterFloatList {get;set;}
    public MainViewModel()
    {
        RegisterIntList = new();
        RegisterFloatList = new();
        FillRegisterList();
        
        
        byte[] mem = new byte[16 * 3];
        Array.Fill<byte>(mem, 2);
        UpdateMemory(mem);
        
        _interpreterEngine = new(PrintToConsole, UpdateRegister, UpdateMemory);
    }

    public void FillRegisterList()
    {
        for (int i = 0; i < 32; i++)
        {
            RegisterIntList.Add(new(Register.RegisterNamesKey[i], Register.RegisterNamesFull[i], 0, false));
        }
        for (int i = 32; i < 64; i++)
        {
            RegisterFloatList.Add(new(Register.RegisterNamesKey[i], Register.RegisterNamesFull[i], 0, true));
        }
    }
    
    public void UpdateRegister(string name, int value)
    {
        int location;
        if (name[0] == 'x' && Register.IntegerRegisterNames.TryGetValue(name, out location))
        {
            RegisterIntList[location].valueInt = value;
        }
        else if (name[0] == 'f' && Register.FloatRegisterNames.TryGetValue(name, out location))
        {
            RegisterFloatList[location].valueInt = value;
        }
    }
    
    private void UpdateMemory(byte[] values)
    {
        const int memLength = 16*3;
        StringBuilder stringout = new("Memory with hexadecimal values\nAdress     |    +0    |    +4    |    +8    |\n");
        int line = MemoryLocation;
        stringout = stringout.Append($"0x{line:X8} | ");
        for (int i = MemoryLocation; i < MemoryLocation + memLength; i+=4)
        {
            if (i % 12 == 0 &&  i != MemoryLocation && values.Length-1 != i)
            {
                line += 12;
                stringout = stringout.Append($"\n0x{line:X8} | ");
            }
            
            byte[] chosenBytes = [values[i], values[i+1], values[i+2], values[i+3]];
            int currentHex = BitConverter.ToInt32(chosenBytes);
            
            stringout = stringout.Append($"{currentHex:X8} | ");
        }
        
        MemoryText = stringout.ToString();
    }
    
    public void UpdateMemoryPressed(object? sender)
    {
        MemoryLocation = TempMemoryLocation;
        
        UpdateMemory(_interpreterEngine.RetriveMemory());
    }
    
    public void PlayPressed(object? sender)
    {
        if (_interpreterEngine.InterpreterState == EInterpreterState.STOPPED)
        {
            _interpreterEngine.Hertz = Speeds[SelectedSpeed];
            _interpreterEngine.Start(EditableTextDocument.Text.Split('\n'));
        }
        else if (_interpreterEngine.InterpreterState == EInterpreterState.PAUSED)
        {
            _interpreterEngine.Resume();
        }
    }
    
    public void PausePressed(object? sender)
    {
        if (_interpreterEngine.InterpreterState == EInterpreterState.RUNNING)
            _interpreterEngine.PausePressed = true;
    }
    
    public void StopPressed(object? sender)
    {
        if (_interpreterEngine.InterpreterState == EInterpreterState.RUNNING)
        {
            _interpreterEngine.StopPressed = true;
        }
        else if (_interpreterEngine.InterpreterState != EInterpreterState.STOPPED)
        {
            _interpreterEngine.End();
        }
    }
    
    public void PrintToConsole(string message)
    {
        ConsoleText = ConsoleText + "\n" + message;
        
        ConsoleScrollbarLength = 100;
    }
}

public partial class MemoryUI : INotifyPropertyChanged
{
    public string Id;
    public string Name { get; set; }
    public string? VisibleValues { get; set {
            field = (value != null) ? value : "";
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(VisibleValues)));
        }
    }
    
    public bool IsFloat = false;
    public int? valueInt { get; set {
            field = (value != null) ? value : 0;
            valueFloat = BitConverter.Int32BitsToSingle((value != null) ? (int)value : 0);
            ValueUpdated();
        }
    }
    
    public float? valueFloat;

    public event PropertyChangedEventHandler? PropertyChanged;

    void ValueUpdated()
    {
        if (IsFloat)
        {
            VisibleValues = $"Hex:{valueInt:X8} Dec:{valueFloat} \nBin:{valueInt:b32}";
        }
        else
        {
            VisibleValues = $"Hex:{valueInt:X8} Dec:{valueInt} \nBin:{valueInt:b32}";
        }
    }
    
    public delegate void UpdateInternalValue(int newValue);
    void InternalValueUpdated(int newValue)
    {
        valueInt = newValue;
    }
    
    public MemoryUI(string key, string name, int value, bool isFloat)
    {
        Id = key;
        this.Name = name;
        this.valueInt = value;
        IsFloat = isFloat;
        
        ValueUpdated();
    }
}
