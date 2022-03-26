using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Avalonia.Markup.Xaml;
using Avalonia.VisualTree;
using IS.Lab7.BL.Helpers;
using IS.Lab7.BL.Processing;
using IS.Lab7.BL.StatCounting;
using IS.Lab7.BL.SymbolReplacement;
using IS.Lab7.UIHelpers;

namespace IS.Lab7
{
    public partial class SampleTextForm : Window
    {
        public SampleTextForm()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
            Setup();
        }

        private void Setup()
        {
            var sampleText = ExampleTextGrabber.GrabSampleText();
            sampleText = TextPreprocessingHelper.PreprocessText(sampleText);
            this.FindControl<TextBox>("SampleTextBox").Text = sampleText;
            AvaloniaHelpers.FillStats(sampleText, this.FindControl<ListBox>("LetterStats"),
                this.FindControl<ListBox>("BigramStats"));

        }
        
    }
}