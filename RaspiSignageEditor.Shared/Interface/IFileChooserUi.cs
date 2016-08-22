using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RaspiSignageEditor.Shared.Interface
{
    public interface IFileChooserUi
    {
        Task<string> ChooseOpenYamlFile();
        Task<string> ChooseSaveYamlFile(string filename);
        Task<string> ChooseOpenMediaFile();
        Task<string> ChooseOpenImageFile();
    }
}
