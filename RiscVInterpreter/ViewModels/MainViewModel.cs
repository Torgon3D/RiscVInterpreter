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
    public ObservableCollection<MemoryUI> RegisterList {get;set;}
    public MainViewModel()
    {
        RegisterList = new();
        
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello1", -101));
        RegisterList.Add(new("hello2", 300000000));
        RegisterList.Add(new("hello3", 30));
        RegisterList.Add(new("hello4", 30));
        RegisterList.Add(new("hello5", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        Task.Delay(5000).ContinueWith(t => Dispatcher.UIThread.Invoke(DocTest));
    }
    
    public void PlayPressed(object? sender)
    {
        
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
