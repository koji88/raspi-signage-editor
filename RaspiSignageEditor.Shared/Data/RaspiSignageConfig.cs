using System;
using System.Collections.Generic;
using System.Text;
using YamlDotNet;
using YamlDotNet.Serialization;
using System.IO;

namespace RaspiSignageEditor.Shared.Data
{
    public enum AudioOutput
    {
        hdmi,
        local,
        both
    }

    public class Option
    {
        [YamlMember(Alias = "autostart")]
        public bool IsAutoStart { get; set; }

        [YamlMember(Alias = "autonext")]
        public bool IsAutoNext { get; set; }

        [YamlMember(Alias = "pullup")]
        public bool IsPullup { get; set; }

        [YamlMember(Alias = "bouncetime")]
        public int BounceTime { get; set; }

        [YamlMember(Alias = "ntritoggle")]
        public bool IsnTriToggle { get; set; }

        [YamlMember(Alias = "idlefile")]
        public string IdleFileOutput
        {
            get
            {
                return SignageTopDir + IdleFile;
            }
            set
            {
                IdleFile = value.StartsWith(SignageTopDir) ? value.Substring(SignageTopDir.Length) : value;
            }
        }

        [YamlIgnore]
        public string IdleFile { get; set; }

        [YamlMember(Alias = "clearimage")]
        public string ClearImageOutput
        {
            get
            {
                return SignageTopDir + ClearImage;
            }
            set
            {
                IdleFile = value.StartsWith(SignageTopDir) ? value.Substring(SignageTopDir.Length) : value;
            }
        }

        [YamlIgnore]
        public string ClearImage { get; set; }

        [YamlMember(Alias = "exit")]
        public int Exit { get; set; }

        [YamlMember(Alias = "remote")]
        public bool IsRemote { get; set; }

        [YamlMember(Alias = "port")]
        public int Port { get; set; }

        [YamlMember(Alias = "audio")]
        public AudioOutput Audio { get; set; }

        [YamlIgnore]
        public string SignageTopDir { get; set; }

        public Option()
        {
            IsAutoStart = true;
            IsAutoNext = false;
            IsPullup = false;
            BounceTime = 20;
            IsnTriToggle = false;
            Exit = 7;
            IsRemote = true;
            Port = 8888;
            Audio = AudioOutput.both;

            IdleFile = "top.jpg";
            ClearImage = "black.png";
            SignageTopDir = "/mnt/usbdisk/";
        }
    }

    public enum FuncCommand
    {
        play,
        stop,
        next,
        prev,
        reboot,
        shutdown,
    }

    public class PlaySet
    {
        [YamlMember(Alias = "func")]
        public int FuncNumber { get; set; }

        [YamlMember(Alias = "file")]
        public string FileNameOutput
        {
            get
            {
                return SignageTopDir + FileName;
            }
            set
            {
                FileName = value.StartsWith(SignageTopDir) ? value.Substring(SignageTopDir.Length) : value;
            }
        }

        [YamlIgnore]
        public string FileName { get; set; }

        [YamlMember(Alias = "loop")]
        public bool IsLoop { get; set; }

        [YamlMember(Alias = "timeout")]
        public int TimeOut { get; set; }


        [YamlIgnore]
        public string SignageTopDir { get; set; }

        public PlaySet()
        {
            FuncNumber = -1;
            FileName = string.Empty;
            IsLoop = false;
            TimeOut = 0;

            SignageTopDir = "/mnt/usbdisk/";
        }
    }

    public class RaspiSignageConfig
    {
        [YamlMember(Alias = "gpiomap")]
        public Dictionary<string, object> GPIOMap { get; set; }

        [YamlMember(Alias = "option")]
        public Option Option { get; set; }

        [YamlMember(Alias = "command")]
        public Dictionary<int, FuncCommand> Command { get; set; }

        [YamlMember(Alias = "playlist")]
        public List<PlaySet> PlayList { get; set; }

        public RaspiSignageConfig()
        {
            GPIOMap = new Dictionary<string, object>();
            Option = new Option();
            Command = new Dictionary<int, FuncCommand>();
            PlayList = new List<PlaySet>();

            GPIOMap["ntri"] = 14;
            GPIOMap["n"] = new object[] { 18, 15 };
            GPIOMap["4"] = 23;
            GPIOMap["5"] = 24;
            GPIOMap["6"] = 25;
            GPIOMap["7"] = 21;

            Command[0] = FuncCommand.play;
            Command[1] = FuncCommand.stop;
            Command[2] = FuncCommand.next;
            Command[3] = FuncCommand.prev;

            PlayList.Add(new PlaySet() { FuncNumber = 4, FileName = "test1.mp4", IsLoop = false });
            PlayList.Add(new PlaySet() { FuncNumber = 5, FileName = "sample.jpg", TimeOut = 10 });
        }

        public bool SaveData(string filename)
        {
            using (var writer = new StreamWriter(filename))
            {
                var serializer = new Serializer(SerializationOptions.EmitDefaults);
                serializer.Serialize(writer, this);
            }
            return true;
        }

        public static Tuple<bool, RaspiSignageConfig> LoadData(string filename)
        {
            using (var reader = new StreamReader(filename))
            {
                var desiralizer = new Deserializer();
                var data = desiralizer.Deserialize<RaspiSignageConfig>(reader);
                return new Tuple<bool, RaspiSignageConfig>(data != null, data);
            }
        }
    }
}
