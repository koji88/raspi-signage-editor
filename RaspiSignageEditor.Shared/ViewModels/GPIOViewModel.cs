using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using ReactiveUI;

namespace RaspiSignageEditor.Shared.ViewModels
{
    public class KeyValueSet<T1,T2> : ReactiveObject
    {
        private T1 _key;
        public T1 Key
        {
            get { return _key; }
            set {
                this.RaiseAndSetIfChanged(ref _key, value);
            }
        }

        public T2 _value;
        public T2 Value
        {
            get { return _value; }
            set {
                this.RaiseAndSetIfChanged(ref _value, value);
            }
        }

        public KeyValueSet(T1 key, T2 value)
        {
            _key = key;
            _value = value;
        }
    }

    public class GPIOViewModel : ReactiveObject
    {
        #region Properties
        private int _ntrigger;
        public int NTrigger
        {
            get { return _ntrigger; }
            set { this.RaiseAndSetIfChanged(ref _ntrigger, value); }
        }

        private ReactiveList<KeyValueSet<int,int>> _nlist;
        public ReactiveList<KeyValueSet<int,int>> NList
        {
            get { return _nlist; }
            set { this.RaiseAndSetIfChanged(ref _nlist, value); }
        }

        private ReactiveList<KeyValueSet<int,int>> _ngpio;
        public ReactiveList<KeyValueSet<int,int>> NGPIO
        {
            get { return _ngpio; }
            set { this.RaiseAndSetIfChanged(ref _ngpio, value);  }
        }

        private ReactiveList<int> _funcNumbers;
        public ReactiveList<int> FuncNumbers
        {
            get { return _funcNumbers; }
            set { this.RaiseAndSetIfChanged(ref _funcNumbers, value); }
        }

        public Dictionary<string, object> GPIOMap
        {
            get
            {
                var ret = new Dictionary<string, object>();
                ret["ntri"] = _ntrigger;
                ret["n"] = _nlist.Select(x => x.Value).ToArray();
                
                foreach(var v in _ngpio)
                {
                    ret[v.Key.ToString()] = v.Value;
                }
                return ret;
            }

            set
            {
                // var nlist = new List<KeyValueSet<int,int>>();
                // var ngpio = new List<KeyValueSet<int,int>>();
                _nlist.Clear();
                _ngpio.Clear();

                int ivalue;
                foreach (var v in value)
                {
                    if (v.Value == null)
                        continue;

                    switch(v.Key)
                    {
                        case "ntri":
                            if(int.TryParse(v.Value.ToString(),out ivalue))
                            {
                                NTrigger = ivalue;
                            }
                            break;
                        case "n":
                            var ovs = v.Value as IList<object>;
                            if(ovs != null)
                            {
                                foreach(var o in ovs)
                                {
                                    if(int.TryParse(o.ToString(),out ivalue))
                                    {
                                        _nlist.Add(new KeyValueSet<int, int>(_nlist.Count, ivalue));
                                    }
                                }
                            }
                            break;
                        default:
                            int ikey;
                            if(int.TryParse(v.Value.ToString(),out ivalue) && int.TryParse(v.Key,out ikey))
                            {
                                _ngpio.Add(new KeyValueSet<int, int>(ikey, ivalue));
                            }
                            break;
                    }
                }
                updateFuncNumbers();
            }
        }

        #endregion

        #region Methods
        #endregion

        #region Commands
        public ReactiveCommand<object> AddGPIOCommand { get; private set; }
        public ReactiveCommand<object> AddNCommand { get; private set; }

        public ReactiveCommand<object> RemoveGPIOCommand { get; private set; }
        public ReactiveCommand<object> RemoveNCommand { get; private set; }
        #endregion

        #region Constructors
        public GPIOViewModel()
        {
            _ntrigger = -1;
            _nlist = new ReactiveList<KeyValueSet<int, int>>();
            _ngpio = new ReactiveList<KeyValueSet<int, int>>();
            _funcNumbers = new ReactiveList<int>();

            updateFuncNumbers();

            // _nlist.CollectionChanged += (s, e) => updateFuncNumbers();
            // _ngpio.CollectionChanged += (s, e) => updateFuncNumbers();

            AddGPIOCommand = ReactiveCommand.Create();
            AddGPIOCommand.Subscribe(_ => {
                _ngpio.Add(new KeyValueSet<int, int>(-1, -1));
                this.RaisePropertyChanged("NGPIO");
            });

            AddNCommand = ReactiveCommand.Create();
            AddNCommand.Subscribe(_ => {
                _nlist.Add(new KeyValueSet<int, int>(_nlist.Count, -1));
                this.RaisePropertyChanged("NList");
            });

            RemoveGPIOCommand = ReactiveCommand.Create();
            RemoveGPIOCommand.Subscribe(p =>
            {
                if (p is KeyValueSet<int,int>)
                {
                    _ngpio.Remove((KeyValueSet<int,int>)p);
                    this.RaisePropertyChanged("NGPIO");
                }
            });


            RemoveNCommand = ReactiveCommand.Create();
            RemoveNCommand.Subscribe(p =>
            {
                if (p is KeyValueSet<int, int>)
                {
                    _nlist.Remove((KeyValueSet<int,int>)p);
                    this.RaisePropertyChanged("NList");
                }
            });
        }
        #endregion

        #region fields
        #endregion

        #region private members
        private void updateFuncNumbers()
        {
            _funcNumbers.Clear();
            foreach(var v in enumFuncNumber().Distinct())
            {
                _funcNumbers.Add(v);
            }
            _funcNumbers.Sort();
        }

        private IEnumerable<int> enumFuncNumber()
        {
            yield return -1;

            for (int i = 0; i < (1 << _nlist.Count); i++)
            {
                yield return i;
            }

            foreach(var v in _ngpio)
            {
                yield return v.Key;
            }
        }
        #endregion

        #region private methods
        #endregion

    }
}
