using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RaspiSignageEditor.Shared.Interface
{
    public interface IFileChooserUi
    {
        Task<string> ChooseOpenFile();
        Task<string> ChooseSaveFile(string filename);
    }
}
