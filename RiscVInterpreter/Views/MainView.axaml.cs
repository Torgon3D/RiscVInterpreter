using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Platform;
using AvaloniaEdit.Editing;
using AvaloniaEdit.Highlighting;
using AvaloniaEdit.Highlighting.Xshd;
using RiscVInterpreter.ViewModels;

namespace RiscVInterpreter.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();

        Stream steam = AssetLoader.Open(new Uri("avares://RiscVInterpreter/Assets/RiscVSyntax.xshd"));
        TextEdit.SyntaxHighlighting = HighlightingLoader.Load(XmlReader.Create(steam), HighlightingManager.Instance);
    }

    protected override void OnDataContextChanged(EventArgs e)
    {
        if (DataContext is MainViewModel m)
        {
            m.SetLineUpdates(StartLine, UpdateLine, EndLine);
        }
    }

    private void ConsoleBox_SizeChanged(object? sender, SizeChangedEventArgs e)
    {
        ConsoleScroll.ScrollToEnd();
    }
    
    private void StartLine()
    {
        TextEdit.Options.HighlightCurrentLine = true;
        TextEdit.ScrollTo(0, 1);
    }
    
    private void UpdateLine(int lineNumber)
    {
        TextEdit.ScrollTo(lineNumber, 0);
        int line = TextEdit.Document.GetLineByNumber(lineNumber).Offset;
        
        TextEdit.Select(line, 0);
        
    }
    
    private void EndLine()
    {
        TextEdit.Options.HighlightCurrentLine = false;
    }
}
