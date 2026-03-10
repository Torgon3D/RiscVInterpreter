using System.Collections.ObjectModel;
using System.Diagnostics;
using Avalonia.Controls;
using Avalonia.Input;
using RiscVInterpreter.ViewModels;

namespace RiscVInterpreter.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }
    

    public void TextChangedTest(object? sender, TextChangedEventArgs args)
    {
        
    }
}