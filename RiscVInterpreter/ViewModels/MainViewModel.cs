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
        
        RegisterList.Add(new("x0,  Zero", 30));
        RegisterList.Add(new("x1,  ra", -101));
        RegisterList.Add(new("x2,  sp", 300000000));
        RegisterList.Add(new("x3,  gp", 30));
        RegisterList.Add(new("x4,  tp", 30));
        RegisterList.Add(new("x5,  t0", 30));
        RegisterList.Add(new("x6,  t1", 30));
        RegisterList.Add(new("x7,  t2", 30));
        RegisterList.Add(new("x8,  s0", 30));
        RegisterList.Add(new("x9,  s1", 30));
        RegisterList.Add(new("x10, a0", 30));
        RegisterList.Add(new("x11, a1", 30));
        RegisterList.Add(new("x12, a2", 30));
        RegisterList.Add(new("x13, a3", 30));
        Task.Delay(5000).ContinueWith(t => Dispatcher.UIThread.Invoke(DocTest));
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
            string[] lineparams = line.Split([' ', '\t'], StringSplitOptions.RemoveEmptyEntries);
            
            foreach (var parameters in lineparams)
            {
                Console.Write($"[{parameters}]");
            }
            
            Console.WriteLine();
        }
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
    public string name { get; set;}
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
        this.name = key;
        this.visibleValue = value;
    }
}
