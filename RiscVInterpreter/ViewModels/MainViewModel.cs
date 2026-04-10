using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Threading;
using AvaloniaEdit.Document;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RiscVInterpreter.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private TextDocument _editableTextDocument = new("Lol");
    
    [ObservableProperty]
    private int _selectedSpeed = 3;
    
    public ObservableCollection<MemoryUI> RegisterList {get;set;}
    public MainViewModel()
    {
        RegisterList = new();
        FillRegisterList();
        
        Task.Delay(5000).ContinueWith(t => Dispatcher.UIThread.Invoke(DocTest));
    }
    
    public void FillRegisterList()
    {
        RegisterList =
        [
            // Ints
            new("x0,  zero", 0),
            new("x1,  ra",   0),
            new("x2,  sp",   0),
            new("x3,  gp",   0),
            new("x4,  tp",   0),
            new("x5,  t0",   0),
            new("x6,  t1",   0),
            new("x7,  t2",   0),
            new("x8,  s0",   0),
            new("x9,  s1",   0),
            new("x10, a0",   0),
            new("x11, a1",   0),
            new("x12, a2",   0),
            new("x13, a3",   0),
            new("x14, a4",   0),
            new("x15, a5",   0),
            new("x16, a6",   0),
            new("x17, a7",   0),
            new("x18, s2",   0),
            new("x19, s3",   0),
            new("x20, s4",   0),
            new("x21, s5",   0),
            new("x22, s6",   0),
            new("x23, s7",   0),
            new("x24, s8",   0),
            new("x25, s9",   0),
            new("x26, s10",  0),
            new("x27, s11",  0),
            new("x28, t3",   0),
            new("x29, t4",   0),
            new("x30, t5",   0),
            new("x31, t6",   0),
            // Floats
            new("f0,  ft0",  0),
            new("f1,  ft1",  0),
            new("f2,  ft2",  0),
            new("f3,  ft3",  0),
            new("f4,  ft4",  0),
            new("f5,  ft5",  0),
            new("f6,  ft6",  0),
            new("f7,  ft7",  0),
            new("f8,  fs0",  0),
            new("f9,  fs1",  0),
            new("f10, fa0",  0),
            new("f11, fa1",  0),
            new("f12, fa2",  0),
            new("f13, fa3",  0),
            new("f14, fa4",  0),
            new("f15, fa5",  0),
            new("f16, fa6",  0),
            new("f17, fa7",  0),
            new("f18, fs2",  0),
            new("f19, fs3",  0),
            new("f20, fs4",  0),
            new("f21, fs5",  0),
            new("f22, fs6",  0),
            new("f23, fs7",  0),
            new("f24, fs8",  0),
            new("f25, fs9",  0),
            new("f26, fs10", 0),
            new("f27, fs11", 0),
            new("f28, ft8",  0),
            new("f29, ft9",  0),
            new("f30, ft10", 0),
            new("f31, ft11", 0),
        ];
    }
    
    public void PlayPressed(object? sender)
    {
        Console.WriteLine($"{SelectedSpeed}");
    }
    
    public void PausePressed(object? sender)
    {
        
    }
    
    public void StopPressed(object? sender)
    {
        string[] lines = EditableTextDocument.Text.Split('\n');
        
        foreach (var line in lines)
        {
            string[] lineparams = line.Split([','], /* StringSplitOptions.RemoveEmptyEntries | */ StringSplitOptions.TrimEntries);
            
            foreach (var parameters in lineparams)
            {
                Console.Write($"[{parameters}]");
            }
            
            Console.WriteLine();
        }
    }
    
    // From engine
    public void RecieveRegisterUpdate(int loc, int value)
    {
        RegisterList[loc].visibleValue = value;
    }
    
    public void RecieveMemoryUpdate(int loc, byte[] values)
    {
        
    }
    
    public void RecieveConsoleMessage(string message)
    {
        
    }
    
    // To engine
    public void RunConsoleCommand(string message)
    {
        
    }
    
    public void SendRegisterUpdate(int loc, int value)
    {
        
    }
    
    public void SendMemoryUpdate(int loc, byte[] values)
    {
        
    }
    
    async void DocTest()
    {
        Debug.Print(EditableTextDocument.Text);
        string[] lines = EditableTextDocument.Text.Split("\n");
        foreach (var line in lines)
        {
            Debug.Print(line);
        }
    }
}

public partial class MemoryUI : INotifyPropertyChanged
{
    public string id;
    public string name { get; set; }
    public int? visibleValue { get; set {
            field = (value != null) ? value : 0;
            
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("visibleValue"));
            ValueUpdated();
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    void ValueUpdated()
    {
        Debug.Print($"{name} has {visibleValue}");
        
    }
    
    public delegate void UpdateInternalValue(int newValue);
    void InternalValueUpdated(int newValue)
    {
        visibleValue = newValue;
    }
    
    public MemoryUI(string key, int value)
    {
        id = key;
        this.name = key;
        this.visibleValue = value;
    }
}
