using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ReactiveUI;
using System.IO;

namespace RaspiSignageEditor.Shared.ViewModels
{
    public class MainViewModel : ReactiveObject
    {
        #region Properties
        private GPIOViewModel _gpio;
        public GPIOViewModel GPIO
        {
            get { return _gpio; }
            set { this.RaiseAndSetIfChanged(ref _gpio, value); }
        }

        private OptionViewModel _option;
        public OptionViewModel Option
        {
            get { return _option; }
            set { this.RaiseAndSetIfChanged(ref _option, value); }
        }

        private PlayListViewModel _playlist;
        public PlayListViewModel PlayList
        {
            get { return _playlist; }
            set { this.RaiseAndSetIfChanged(ref _playlist, value); }
        }

        private CommandViewModel _commands;
        public CommandViewModel Commands
        {
            get { return _commands; }
            set { this.RaiseAndSetIfChanged(ref _commands, value); }
        }

        #endregion

        #region Methods
        #endregion

        #region Commands
        public ReactiveCommand<object> OpenFileCommand { get; private set; }
        public ReactiveCommand<object> SaveFileCommand { get; private set; }
        public ReactiveCommand<object> SaveFileAsCommand { get; private set; }
        public ReactiveCommand<object> ExitCommand { get; private set; }
        #endregion

        #region Constructors
        public MainViewModel(Interface.IFileChooserUi filechooser)
        {
            _gpio = new GPIOViewModel();
            _option = new OptionViewModel();
            _playlist = new PlayListViewModel();
            _commands = new CommandViewModel();

            _configData = new Data.RaspiSignageConfig();
            _configFilename = "raspi-signage.yml";

            OpenFileCommand = ReactiveCommand.Create();
            OpenFileCommand.Subscribe(_ =>
            {
                var filename = filechooser.ChooseOpenFile().Result;
                if (File.Exists(filename))
                {
                    var result = Data.RaspiSignageConfig.LoadData(filename);
                    if(result.Item1)
                    {
                        _configData = result.Item2;
                        loadFromData();
                        _configFilename = filename;
                    }
                }
                
            });

            SaveFileCommand = ReactiveCommand.Create();
            SaveFileCommand.Subscribe(_ =>
            {
                if(!string.IsNullOrEmpty(_configFilename))
                {
                    saveToData();
                    _configData.SaveData(_configFilename);
                }
            });

            SaveFileAsCommand = ReactiveCommand.Create();
            SaveFileAsCommand.Subscribe(_ =>
            {
                var filename = filechooser.ChooseSaveFile(_configFilename).Result;
                if(!string.IsNullOrEmpty(filename))
                {
                    _configFilename = filename;
                    saveToData();
                    _configData.SaveData(_configFilename);
                }

            });

            loadFromData();
        }
        #endregion

        #region fields
        #endregion

        #region private members
        private Data.RaspiSignageConfig _configData;
        private string _configFilename;
        #endregion

        #region private methods
        private void loadFromData()
        {
            _gpio.GPIOMap = _configData.GPIOMap;
            _option.Option = _configData.Option;
            _playlist.PlayList = new ReactiveList<PlaySetViewModel>(_configData.PlayList.Select(r => new PlaySetViewModel(r)));
            _commands.CommandMap = _configData.Command;
        }

        private void saveToData()
        {
            _configData.GPIOMap = _gpio.GPIOMap;
            _configData.Option = _option.Option;
            _configData.PlayList = new List<Data.PlaySet>(_playlist.PlayList.Select(r => r.PlaySet));
            _configData.Command = _commands.CommandMap;
        }

        #endregion
    }
}
