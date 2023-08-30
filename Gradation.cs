public class Gradation
{
    // 1. 텍스트 2시작색깔코드 3엔드 색깔코드
    //Console.Write(T2G("Text", "FF0000", "0033FF"));

    private static string C2A(string s)
    {
        int r = Convert.ToInt32(s.Substring(0, 2), 16);
        int g = Convert.ToInt32(s.Substring(2, 2), 16);
        int b = Convert.ToInt32(s.Substring(4, 2), 16);
        return $"\u001b[38;2;{r};{g};{b}m";
    }

    public static string T2G(string Chat, string Start, string End)
    {
        int l = 1;
        int e = Chat.Length;
        string Value = "";
        string[] RGB = new string[4];
        int[] Color = new int[7];
        Color[1] = Convert.ToInt32(Start.Substring(0, 2), 16);
        Color[2] = Convert.ToInt32(Start.Substring(2, 2), 16);
        Color[3] = Convert.ToInt32(Start.Substring(4, 2), 16);
        Color[4] = (Convert.ToInt32(End.Substring(0, 2), 16) - Color[1]) / (e + 1);
        Color[5] = (Convert.ToInt32(End.Substring(2, 2), 16) - Color[2]) / (e + 1);
        Color[6] = (Convert.ToInt32(End.Substring(4, 2), 16) - Color[3]) / (e + 1);
        while (l <= e)
        {
            if (Chat.Substring(l - 1, 1) != " ")
            {
                RGB[1] = (Color[1] + Color[4] * l).ToString("X2");
                RGB[2] = (Color[2] + Color[5] * l).ToString("X2");
                RGB[3] = (Color[3] + Color[6] * l).ToString("X2");
                Value += C2A(RGB[1] + RGB[2] + RGB[3]) + Chat.Substring(l - 1, 1);
            }
            else
            {
                Value += " ";
            }
            l++;
        }
        return Value;
    }
}

