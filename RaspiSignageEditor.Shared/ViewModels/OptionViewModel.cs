using System;
using System.Collections.Generic;
using System.Text;
using ReactiveUI;

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
        #endregion

        #region Constructors
        public OptionViewModel()
        {
            _option = new Data.Option();
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
