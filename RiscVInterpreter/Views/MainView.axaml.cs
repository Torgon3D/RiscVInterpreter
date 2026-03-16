using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Xml;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Platform;
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
}
