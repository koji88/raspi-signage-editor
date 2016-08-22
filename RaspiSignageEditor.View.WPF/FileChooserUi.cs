using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RaspiSignageEditor.Shared.Interface;
using Microsoft.Win32;

namespace RaspiSignageEditor.View.WPF
{
    public class FileChooserUi : IFileChooserUi
    {
        public Task<string> ChooseOpenFile()
        {
            var dialog = new OpenFileDialog { Filter = "Yaml Files (*.yml)|*.yml|All files (*.*)|*.*" };
            var filename = dialog.ShowDialog() == true ? dialog.FileName : string.Empty;
            return Task.FromResult(filename);
        }

        public Task<string> ChooseSaveFile(string filename)
        {
            var dialog = new SaveFileDialog { Filter = "Yaml Files (*.yml)|*.yml|All files (*.*)|*.*", FileName = filename };
            filename = dialog.ShowDialog() == true ? dialog.FileName : String.Empty;
            return Task.FromResult(filename);
        }
    }
}
