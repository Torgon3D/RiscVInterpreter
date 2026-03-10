using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Dynamic;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace RiscVInterpreter.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
    
    public ObservableCollection<MemoryUI> RegisterList {get;set;}
    public MainViewModel()
    {
        RegisterList = new();
        
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello1", -101));
        RegisterList.Add(new("hello2", 30));
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
        
    }
}

public partial class MemoryUI : INotifyPropertyChanged
{
    public string name { get; set;}
    public int visibleValue { get; set { field = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("visibleValue")); ValueUpdated();} }

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
