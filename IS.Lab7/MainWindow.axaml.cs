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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private TextCrypter crypter;
        private SymbolReplacementKeyDto ownKey;
        private FileInfo ownFileInfo;

        private IDictionary<char, char> GrabLetters()
        {
            var stackPanels = new[]
            {
                this.FindControl<StackPanel>("StackPanelA_M"),
                this.FindControl<StackPanel>("StackPanelN_Z")
            };
            var result = new Dictionary<char, char>();
            foreach(var panel in stackPanels)
                foreach (var control in panel.Children)
                {
                    var subChildren = (control as StackPanel)?.Children;
                    var textBlock = (subChildren?.First() as TextBlock);
                    var textBox = (subChildren?.Last() as TextBox);
                    var value = !string.IsNullOrWhiteSpace(textBox?.Text)?textBox.Text.ToUpper()[0]:'?';
                    var key = textBlock?.Text[0];
                    if (key == null)
                        throw new Exception("Null key in char table");
                    result.Add(key.Value,value);
                }

            return result;
        }


        private void FillStats(string text) => AvaloniaHelpers.FillStats(text, this.FindControl<ListBox>("LetterStats"),
            this.FindControl<ListBox>("BigramStats"));
        
        private async Task<FileInfo?> UpdateFileAsync()
        {
            var fd = new OpenFileDialog
            {
                AllowMultiple = false
            };
            var res = await fd.ShowAsync(this);
            if (res == null) return null;
            var path = res.FirstOrDefault();
            if (string.IsNullOrWhiteSpace(path)) return null;
            var fi = new FileInfo(path);
            return !fi.Exists ? null : fi;

        }
        
        private async void LoadText_OnClick(object? sender, RoutedEventArgs e)
        {
            var gotInfo = await UpdateFileAsync();
            if (gotInfo == null) return;
            ownFileInfo = gotInfo;
            crypter = new TextCrypter(ownFileInfo);
            Process(await File.ReadAllTextAsync(ownFileInfo.FullName));
            FillStats(crypter.Process());
            Unlock();
        }

        private void GrabExampleAndProcess_Click(object? sender, RoutedEventArgs e)
        {
            ClearKey();
            var text = ExampleTextGrabber.GrabFromExamples();
            TextStatCounter.GetBigramStats(text);
            TextStatCounter.GetLetterStats(text);
            crypter = new TextCrypter(text);
            Process(text);
            FillStats(crypter.Process());
            Unlock();
        }

        private string Process(string text)
        {
            this.FindControl<TextBox>("EncTextTxb").Text = crypter.Process();

            ownKey = new SymbolReplacementKeyDto
            {
                replacements = GrabLetters()
            };
            var process = crypter.Process(ownKey, text);
            this.FindControl<TextBox>("ProcessedBlock").Text = process;
            return process;
        }


        private void InputElement_OnKeyUp(object? sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(((sender as TextBox)!).Text)) return;
            ((sender as TextBox)!).Text = ((sender as TextBox)!).Text.ToUpper();
            var text = this.FindControl<TextBox>("EncTextTxb").Text;
            var ps=Process(text);
            if(crypter.Validate(this.FindControl<TextBox>("ProcessedBlock").Text))
            {
                Lock();
                this.Focus();
                Notify("Key Valid. Congratulations");
            }
        }

        private void ShowOrigBtn_OnClick(object? sender, RoutedEventArgs e)
        {
           Lock();
        }

        private void Notify(string text)
        {
            this.FindControl<TextBlock>("NotificationTb").Text = text;
        }

        private void Lock()
        {
            Notify("Original Text Shown. Key Locked. Encrypt Text Again to Unlock");
            this.FindControl<TextBlock>("EncTextHeaderTb").Text = "Original Text";
            this.FindControl<TextBox>("EncTextTxb").Text = crypter.OriginalText;
            this.FindControl<StackPanel>("KeySp").IsEnabled = false;
            this.FindControl<Button>("ShowOrigBtn").IsEnabled = false;
        }

        private void Unlock()
        {
            Notify("Try To Decrypt by Placing Letter-Replacement Key.");
            ClearKey();
            this.FindControl<TextBlock>("EncTextHeaderTb").Text = "Encrypted Text";
            this.FindControl<TextBox>("EncTextTxb").Text = crypter.Process();
            this.FindControl<StackPanel>("KeySp").IsEnabled = true;
            this.FindControl<Button>("ShowOrigBtn").IsEnabled = true;
        }

        private void ClearKey()
        {
            this.GetVisualDescendants().OfType<TextBox>().Where(t=>t.Classes.Contains("tb"))?
                .ToList().ForEach(t=>t.Text="");
        }

        private void ShowSampleBtn_OnClick(object? sender, RoutedEventArgs e)
        {
            var sampleForm = new SampleTextForm();
            sampleForm.ShowDialog(this);

        }
    }
}