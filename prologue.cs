using System;
using System.Threading.Tasks;
using static Sound2;
using static Gradation;

namespace TextBased_Dungeon_Game
{
    public class Prologue
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static async Task WaitForSkipAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo keyInfo = Console.ReadKey(intercept: true);
                    cts.Cancel();
                }
            }
            await Task.Delay(100);
        }

        public static void PlayPrologue()
        {
            Task.Run(async () => await WaitForSkipAsync(cts.Token));
            string _path = $"{DungeonGame.Instance.path}\\Type.mp3";
            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n                                             ");
            string S1 = "이곳은 초 국가적 비공식 연구집단, 아글라이아의 연구소  "; 


            for (int i = 0; i <= 255; i += 8)
            {
                Thread.Sleep(170);
                Console.Write($"\u001b[38;2;0;{i};255m{S1.Substring(i / 8, 1)}");
                if (S1.Substring(i / 8, 1) != " " || S1.Substring(i / 8, 1) == ",")
                {
                    Play(_path, false, true);
                }
                if (cts.Token.IsCancellationRequested)
                {
                    return;
                }

            }

            Thread.Sleep(350);
            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n                                             ");
            string S2 = "전 세계의 생명 공학과 물리학 분야의 천재들만 모인 곳에서 ";


            for (int i = 0; i <= 255; i += 8)
            {
                Thread.Sleep(170);
                Console.Write($"\u001b[38;2;0;{i};255m{S2.Substring(i / 8, 1)}");
                if (S2.Substring(i / 8, 1) != " " || S2.Substring(i / 8, 1) == "." || S2.Substring(i / 8, 1) == ",")
                {
                    Play(_path, false, true);

                }
                if (cts.Token.IsCancellationRequested)
                {
                    return;
                }

            }

            Thread.Sleep(350);
            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n                                          ");
            string S3 = "완전한 인간을 만든다는 명목으로 강행하는 인체 실험, 신인류 프로젝트...   ";

             
            for (int i = 0; i <= 255; i += 6)
            {
                Thread.Sleep(170);
                Console.Write($"\u001b[38;2;0;{i};255m{S3.Substring(i / 6, 1)}");
                if (S3.Substring(i / 6, 1) != " " || S3.Substring(i / 6, 1) == "." || S3.Substring(i / 6, 1) == ",")
                {
                    Play(_path, false, true);
                }
                if (cts.Token.IsCancellationRequested)
                {
                    return;
                }

            }

            Thread.Sleep(350);
            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n                                      ");
            string S4 = "이것은 이 끔찍한 실험이 진행되는걸 막기 위해 파견된 나의 이야기이다...    ";

            for (int i = 0; i <= 255; i += 6)
            {
                Thread.Sleep(170);
                Console.Write($"\u001b[38;2;0;{i};255m{S4.Substring(i / 6, 1)}");
                if (S4.Substring(i / 6, 1) != " " || S4.Substring(i / 6, 1) == "!" || S4.Substring(i / 6, 1) == ",")
                {
                    Play(_path, false, true);
                }
                if (cts.Token.IsCancellationRequested)
                {
                    return;
                }

            }

            Console.Clear();
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n                                                          ");
            string S5 = "절망의 끝까지 도달한 폰에게 모든 영광을... ";

            for (int i = 0; i <= 255; i += 10)
            {
                Thread.Sleep(170);
                Console.Write($"\u001b[38;2;0;{i};255m{S5.Substring(i / 10, 1)}");
                if (S5.Substring(i / 10, 1) != " " || S5.Substring(i / 10, 1) == "!" || S5.Substring(i / 10, 1) == ",")
                {
                    Play(_path, false, true);
                }
                if (cts.Token.IsCancellationRequested)
                {
                    return;
                }

            }

        }
    }
}