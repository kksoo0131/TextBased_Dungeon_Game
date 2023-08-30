using NAudio.Wave;
using NAudio.Midi;

using NAudio.Wave;
using NAudio.Midi;

class SoundPlayer
{
    private static WaveOutEvent outputDevice;

    public static async Task Bgm(string audioFileName)
    {
        string audioFilePath = "BGM.mp3";
        string fullPath = Path.Combine(@"C:\Users\User\source\repos\kksoo0131\TextBased_Dungeon_Game\bin\Debug\net6.0", audioFilePath);

        // 사운드 반복재생
        while (true)
        {
            using (var audioFile = new AudioFileReader(audioFilePath))
            {
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.Play();
                outputDevice.Volume = 0.3f;

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(100);
                }
            }
        }
    }

    public static async Task SoundsDungeon(string audioFileName)
    {
        string audioFilePath = "Dungeon.mp3";
        string fullPath = Path.Combine(@"C:\Users\User\source\repos\kksoo0131\TextBased_Dungeon_Game\bin\Debug\net6.0", audioFilePath);

        while (true)
        {
            using (var audioFile = new AudioFileReader(audioFilePath))
            {
                outputDevice = new WaveOutEvent();
                outputDevice.Init(audioFile);
                outputDevice.Play();
                outputDevice.Volume = 0.3f;

                while (outputDevice.PlaybackState == PlaybackState.Playing)
                {
                    await Task.Delay(100);
                }
            }
        }
    }
    public static async Task SoundsClear(string audioFileName)
    {
        string audioFilePath = "Clear.mp3";
        string fullPath = Path.Combine(@"C:\Users\User\source\repos\kksoo0131\TextBased_Dungeon_Game\bin\Debug\net6.0", audioFilePath);

        using (var audioFile = new AudioFileReader(audioFilePath))
        {
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            outputDevice.Volume = 0.25f;


            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                await Task.Delay(100);
            }
        }
    }

    public static async Task SoundsAttack(string audioFileName)
    {
        string audioFilePath = "Attack.mp3";
        string fullPath = Path.Combine(@"C:\Users\User\source\repos\kksoo0131\TextBased_Dungeon_Game\bin\Debug\net6.0", audioFilePath);

        using (var audioFile = new AudioFileReader(audioFilePath))
        {
            outputDevice = new WaveOutEvent();
            outputDevice.Init(audioFile);
            outputDevice.Play();
            outputDevice.Volume = 0.25f;


            while (outputDevice.PlaybackState == PlaybackState.Playing)
            {
                await Task.Delay(100);
            }
        }
    }

    // SoundPlayer.PlaySoundAsync("");

    public static void StopSound()
    {
        if (outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
        {
            outputDevice.Stop();
            outputDevice.Dispose();
        }
    }
}



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
