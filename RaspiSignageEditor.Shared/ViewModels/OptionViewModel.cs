using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;
using System.IO;

namespace RaspiSignageEditor.Shared.ViewModels
{
    public class OptionViewModel : ReactiveObject
    {
        #region Properties
        public Data.Option Option
        {
            get { return _option; }
            set
            {
                this.RaiseAndSetIfChanged(ref _option, value);
            }
        }
        #endregion

        #region Methods
        #endregion

        #region Commands
        public ReactiveCommand<object> OpenIdlefileCommand { get; private set; }
        public ReactiveCommand<object> OpenClearImageCommand { get; private set; }
        #endregion

        #region Constructors
        public OptionViewModel(Interface.IFileChooserUi fileChooser)
        {
            _option = new Data.Option();

            OpenIdlefileCommand = ReactiveCommand.Create();
            OpenIdlefileCommand.Subscribe(_ =>
            {
                var filename = fileChooser.ChooseOpenMediaFile().Result;
                if(File.Exists(filename) && Directory.GetCurrentDirectory() == Path.GetDirectoryName(filename))
                {
                    _option.IdleFile = Path.GetFileName(filename);
                    this.RaisePropertyChanged("Option");
                }
            });

            OpenClearImageCommand = ReactiveCommand.Create();
            OpenClearImageCommand.Subscribe(_ =>
            {
                var filename = fileChooser.ChooseOpenImageFile().Result;
                if (File.Exists(filename) && Directory.GetCurrentDirectory() == Path.GetDirectoryName(filename))
                {
                    _option.ClearImage = Path.GetFileName(filename);
                    this.RaisePropertyChanged("Option");
                }
            });
        }
        #endregion

        #region fields
        #endregion

        #region private members
        private Data.Option _option;
        #endregion

        #region private methods
        #endregion



    }
}
