using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspiSignageEditor.Shared.Interface;
using Microsoft.Win32;
using System.IO;

namespace RaspiSignageEditor.View.WPF
{
    public class FileChooserUi : IFileChooserUi
    {
        public Task<string> ChooseOpenYamlFile()
        {
            var dialog = new OpenFileDialog { Filter = "Yaml Files (*.yml)|*.yml|All files (*.*)|*.*", InitialDirectory=Directory.GetCurrentDirectory() };
            var filename = dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
            return Task.FromResult(filename);
        }

        public Task<string> ChooseSaveYamlFile(string filename)
        {
            var dialog = new SaveFileDialog { Filter = "Yaml Files (*.yml)|*.yml|All files (*.*)|*.*", FileName = filename, InitialDirectory = Directory.GetCurrentDirectory() };
            filename = dialog.ShowDialog() == true ? dialog.FileName : String.Empty;
            return Task.FromResult(filename);
        }

        public Task<string> ChooseOpenMediaFile()
        {            
            var dialog = new OpenFileDialog { Filter = "Movie Files (*.mp4;*.mov)|*.mp4;*.mov|Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*", InitialDirectory= Directory.GetCurrentDirectory() };
            var filename = dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
            return Task.FromResult(filename);
        }

        public Task<string> ChooseOpenImageFile()
        {
            var dialog = new OpenFileDialog { Filter = "Image Files (*.jpg;*.jpeg;*.png)|*.jpg;*.jpeg;*.png|All files (*.*)|*.*", InitialDirectory = Directory.GetCurrentDirectory() };
            var filename = dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
            return Task.FromResult(filename);
        }
    }
}
