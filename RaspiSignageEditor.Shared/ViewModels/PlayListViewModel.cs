using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

namespace RaspiSignageEditor.Shared.ViewModels
{
    public class PlaySetViewModel : ReactiveObject
    {
        #region Properties

        public string FileName
        {
            get { return _playset.FileName; }
            set
            {
                if(value != _playset.FileName)
                {
                    _playset.FileName = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public int FuncNumber
        {
            get { return _playset.FuncNumber; }
            set
            {
                if(value != _playset.FuncNumber)
                {
                    _playset.FuncNumber = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public bool IsLoop
        {
            get { return _playset.IsLoop; }
            set
            {
                if(value != _playset.IsLoop)
                {
                    _playset.IsLoop = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public int TimeOut
        {
            get { return _playset.TimeOut; }
            set
            {
                if(value != _playset.TimeOut)
                {
                    _playset.TimeOut = value;
                    this.RaisePropertyChanged();
                }
            }
        }

        public Data.PlaySet PlaySet
        {
            get { return _playset; }
        }

        #endregion

        private Data.PlaySet _playset;
        public PlaySetViewModel()
        {
            _playset = new Data.PlaySet();
        }

        public PlaySetViewModel(Data.PlaySet ps)
        {
            _playset = ps;
        }
    }

    public class PlayListViewModel : ReactiveObject
    {
        #region Properties
        private ReactiveList<PlaySetViewModel> _playList;
        public ReactiveList<PlaySetViewModel> PlayList
        {
            get { return _playList; }
            set { this.RaiseAndSetIfChanged(ref _playList, value); }
        }



        #endregion

        #region Methods
        #endregion

        #region Commands
        public ReactiveCommand<object> RemoveCommand { get; private set; }
        #endregion

        #region Constructors
        public PlayListViewModel()
        {
            _playList = new ReactiveList<PlaySetViewModel>();

            RemoveCommand = ReactiveCommand.Create();
            RemoveCommand.Subscribe(p =>
            {
                if (p is PlaySetViewModel)
                {
                    _playList.Remove((PlaySetViewModel)p);
                    this.RaisePropertyChanged("PlayList");
                }
            });
        }
        #endregion

        #region fields
        #endregion

        #region private members

        #endregion

        #region private methods
        #endregion
    }
}
