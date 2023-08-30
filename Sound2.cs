using NAudio.Wave;
using NAudio.Midi;
public class Sound2
{
    static Dictionary<string, AudioFileReader> Files = new Dictionary<string, AudioFileReader>();
    static Dictionary<string, WaveOutEvent> Devices = new Dictionary<string, WaveOutEvent>();
    static Dictionary<string, bool> Plays = new Dictionary<string, bool>();

    public static void Play(string Path, bool Repeat, bool Play)
    {
        if (Repeat)
        {
            if (!Files.ContainsKey(Path))
            {
                Files[Path] = new AudioFileReader(Path);
                Devices[Path] = new WaveOutEvent();
                Devices[Path].Init(Files[Path]);
            }
            if (Play)
            {
                if (Devices[Path].PlaybackState == PlaybackState.Stopped)
                {
                    Plays[Path] = true;
                    Files[Path].Position = 0;
                    Devices[Path].Play();
                    Devices[Path].PlaybackStopped += (sender, e) =>
                    {
                        if (Plays[Path])
                        {
                            Files[Path].Position = 0;
                            Devices[Path].Play();
                        }
                    };
                }
            }
            else
            {
                Plays[Path] = false;
                Devices[Path].Stop();
            }
        }
        else
        {
            var File = new AudioFileReader(Path);
            var Device = new WaveOutEvent();
            Device.Init(File);
            Device.Play();
            Device.PlaybackStopped += (sender, e) =>
            {
                File.Dispose();
                Device.Dispose();
            };
        }
    }
}

//class SoundPlayer
//{
//    private static WaveOutEvent outputDevice;

//    public static async Task Type(string audioFileName)
//    {
//        string audioFilePath = "Type.mp3";
//        string fullPath = Path.Combine(@"C:\Users\User\source\repos\kksoo0131\TextBased_Dungeon_Game\.vscode", audioFilePath);

//        // 사운드 반복재생
//        while (true)
//        {
//            using (var audioFile = new AudioFileReader(audioFilePath))
//            {
//                outputDevice = new WaveOutEvent();
//                outputDevice.Init(audioFile);
//                outputDevice.Play();
//                outputDevice.Volume = 0.3f;

//                while (outputDevice.PlaybackState == PlaybackState.Playing)
//                {
//                    await Task.Delay(100);
//                }
//            }
//        }
//    }

//    // SoundPlayer.PlaySoundAsync("");

//    public static void StopSound()
//    {
//        if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
//        {
//            outputDevice.Stop();
//            outputDevice.Dispose();
//        }
//    }
//}



// 사운드 플레이어


//public class Audio
//{
//    private List<WaveOutEvent> outputDevices;
//    private List<AudioFileReader> audioFiles;

//    public Audio()
//    {
//        outputDevices = new List<WaveOutEvent>();
//        audioFiles = new List<AudioFileReader>();
//    }

//    public void PlayAudio(string audioFilePath, bool shouldRepeat, bool shouldPlay)
//    {
//        if (shouldPlay)
//        {
//            var audioFile = new AudioFileReader(audioFilePath);
//            var outputDevice = new WaveOutEvent();

//            outputDevice.PlaybackStopped += (sender, args) =>
//            {
//                audioFile.Position = 0;

//                if (shouldRepeat)
//                {
//                    PlayAudio(audioFilePath, shouldRepeat, true);
//                }
//                else
//                {
//                    outputDevice.Dispose();
//                    audioFile.Dispose();
//                }
//            };

//            outputDevice.Init(audioFile);
//            outputDevice.Play();

//            outputDevices.Add(outputDevice);
//            audioFiles.Add(audioFile);
//        }
//        else
//        {
//            foreach (var outputDevice in outputDevices)
//            {
//                outputDevice.Stop();
//                outputDevice.Dispose();
//            }

//            foreach (var audioFile in audioFiles)
//            {
//                audioFile.Dispose();
//            }

//            outputDevices.Clear();
//            audioFiles.Clear();
//        }
//    }
//}

//class SoundPlayer
//{

//    private static WaveOutEvent outputDevice;

//    public static async Task PlaySoundAsync(string audioFileName)
//    {
//        string audioFilePath = "911.mp3";
//        string fullPath = Path.Combine(@"C:\Users\User\Desktop\Coding\", audioFilePath);

//        using (var audioFile = new AudioFileReader(audioFilePath))
//        {
//            outputDevice = new WaveOutEvent();
//            outputDevice.Init(audioFile);
//            outputDevice.Play();


//            while (outputDevice.PlaybackState == PlaybackState.Playing)
//            {
//                await Task.Delay(100);
//            }
//        }
//    }


//    public static void StopSound()
//    {
//        if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
//        {
//            outputDevice.Stop();
//            outputDevice.Dispose();
//        }
//    }
//}

//class Programs
//{
//    static void Main(string[] args)
//    {
//        string audioFilePath = "C:\Users\User\Desktop\Coding\";

//        Audio.Play(audioFilePath, repeat: true);



//    }

//}
//public class Audio
//{
//    private static Dictionary<string, AudioFileReader> Files = new Dictionary<string, AudioFileReader>();
//    private static Dictionary<string, WaveOutEvent> Devices = new Dictionary<string, WaveOutEvent>();

//    public static void Play(string path, bool repeat = false)
//    {
//        if (!Files.ContainsKey(path))
//        {
//            var fileReader = new AudioFileReader(path);
//            Files.Add(path, fileReader);

//            var outputDevice = new WaveOutEvent();
//            Devices.Add(path, outputDevice);

//            outputDevice.PlaybackStopped += (sender, args) =>
//            {
//                fileReader.Position = 0;

//                if (!repeat)
//                {
//                    outputDevice.Stop();
//                    outputDevice.Dispose();
//                    fileReader.Dispose();

//                    Files.Remove(path);
//                    Devices.Remove(path);
//                }
//            };

//            outputDevice.Init(fileReader);
//            outputDevice.Play();
//        }
//        else
//        {
//            Devices[path].Play();
//        }
//    }

//    public static void Stop(string path)
//    {
//        if (Files.ContainsKey(path))
//        {
//            Devices[path].Stop();
//        }
//    }

//    public static void StopAll()
//    {
//        foreach (var outputDevice in Devices.Values)
//        {
//            outputDevice.Stop();
//        }
//    }
//}
