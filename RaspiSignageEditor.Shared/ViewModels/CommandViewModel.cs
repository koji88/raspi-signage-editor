using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ReactiveUI;

namespace RaspiSignageEditor.Shared.ViewModels
{
    public class CommandViewModel : ReactiveObject
    {
        #region Properties
        private ReactiveList<KeyValueSet<int,Data.FuncCommand>> _commands;
        public ReactiveList<KeyValueSet<int, Data.FuncCommand>> Commands
        {
            get { return _commands; }
            set { this.RaiseAndSetIfChanged(ref _commands, value); }
        }

        public Dictionary<int, Data.FuncCommand> CommandMap
        {
            get
            {
                var ret = new Dictionary<int, Data.FuncCommand>();
                foreach(var v in _commands)
                {
                    ret[v.Key] = v.Value;
                }

                return ret;
            }

            set
            {
                var ret = new ReactiveList<KeyValueSet<int, Data.FuncCommand>>(
                    value.Select(v => new KeyValueSet<int, Data.FuncCommand>(v.Key,v.Value))
                    );
                Commands = ret;                    
            }
        }
        
        #endregion

        #region Methods
        #endregion

        #region Commands
        public ReactiveCommand<object> AddCommand { get; private set; }
        public ReactiveCommand<object> RemoveCommand { get; private set; }
        #endregion

        #region Constructors
        public CommandViewModel()
        {
            AddCommand = ReactiveCommand.Create();
            AddCommand.Subscribe(_ => Commands.Add(new KeyValueSet<int, Data.FuncCommand>(-1, Data.FuncCommand.next)));

            RemoveCommand = ReactiveCommand.Create();
            RemoveCommand.Subscribe(p =>
            {
                if(p is KeyValueSet<int, Data.FuncCommand>)
                {
                    _commands.Remove((KeyValueSet<int, Data.FuncCommand>)p);
                    this.RaisePropertyChanged("Commands");
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
