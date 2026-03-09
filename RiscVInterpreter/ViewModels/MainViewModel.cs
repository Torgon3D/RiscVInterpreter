using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace RiscVInterpreter.ViewModels;

public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _greeting = "Welcome to Avalonia!";
    
    public ObservableCollection<RegisterTest> RegisterList {get;set;}
    public MainViewModel()
    {
        RegisterList = new();
        
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
        RegisterList.Add(new("hello", 30));
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

public class RegisterTest
{
    public string key { get; set;}
    public int value { get; set;}
    
    public RegisterTest(string key, int value)
    {
        this.key = key;
        this.value = value;
    }
}
