using System;

//给定一个语言，定义它的文法的一种表示，并定义一个解释器，这个解释器使用该表示来解释语言中的句子。
//如果一种特定类型的问题发生的频率足够高，那么就值得将该问题的各个实例表述为一个简单语言中的句子，并构建解释器来解决问题（如正则表达式、HTML、python等）。
//解释器模式可以容易地改变和扩展文法，因为其用类来表示文法规则，你可以使用继承来改变或扩展文法。
//也比较容易实现文法，因为定义抽象语法树中各个节点的类的实现大体类似，这些类易于直接编写。

//做一个小音乐解释器吧！
//文法规则：
//1 O表示音阶，O 1为低音阶；O 2为中音阶；O 3为高音阶。
//2 P表示休止符
//3 C D E F G A B分别表示DO RE MI FA SO LA XI；1表示一拍、2表示两拍、0.5表示半拍 等
//如上海滩中浪奔可表示为：O 2 E 0.5 G 0.5 A 3

//解释器模式一般按规则条目划分出具体类
//音乐解释器可从音符、音阶两个规则入手编写

namespace _23解释器模式
{
    class Program
    {
        static void Main(string[] args)
        {
            PlayContent content = new PlayContent();
            content.Text = "O 2 E 0.5 G 0.5 D 3 E 0.5 G 0.5 A 0.5 O 3 C 1 O 2 A 0.5 G 1 C 0.5 E 0.5 D 3 ";
            Expression expression = null;
            while (content.Text.Length > 0)
            {
                //使用switch是为了以后有更多命令时好加
                switch (content.Text[0])
                {
                    case 'O':
                        expression = new Scale();
                        break;
                    case 'C':
                    case 'D':
                    case 'E':
                    case 'F':
                    case 'G':
                    case 'A':
                    case 'B':
                    case 'P':
                        expression = new Note();
                        break;
                    default:
                        break;
                }
                expression.Interpret(content);
            }
            Console.WriteLine("止");
        }
    }

    //演奏内容
    class PlayContent
    {
        public string Text { get; set; }

    }

    //抽象表达式类
    abstract class Expression
    {
        public void Interpret(PlayContent content)
        {
            if (content.Text.Length == 0)
                return;
            else
            {
                //获得子串首位字符，即命令字母
                string playKey = content.Text.Substring(0, 1);
                //去除首位命令字母和空格
                content.Text = content.Text.Substring(2);
                //获取首位命令值，即到第一个空格为止的小数值。
                double playValue = double.Parse(content.Text.Substring(0, content.Text.IndexOf(" ")));
                //去除该命令值
                content.Text = content.Text.Substring(content.Text.IndexOf(" ")+1);

                //执行命令语句及命令值
                Excute(playKey, playValue);
            }
        }

        public abstract void Excute(string key, double value);
    }

    //音符类
    class Note : Expression
    {
        public override void Excute(string key, double value)
        {
            string result = "";

            switch (key)
            {
                case "C":
                    result = "1";
                    break;
                case "D":
                    result = "2";
                    break;
                case "E":
                    result = "3";
                    break;
                case "F":
                    result = "4";
                    break;
                case "G":
                    result = "5";
                    break;
                case "A":
                    result = "6";
                    break;
                case "B":
                    result = "7";
                    break;
                default:
                    result = "?";
                    break;
            }
            Console.Write(result + new string(' ', Convert.ToInt32(2 * value)));
        }
    }

    //音阶类
    class Scale : Expression
    {
        public override void Excute(string key, double value)
        {
            string result = "";
            switch (Convert.ToInt32(value))
            {
                case 1:
                    result = "低音";
                    break;
                case 2:
                    result = "中音";
                    break;
                case 3:
                    result = "高音";
                    break;
                default:
                    result = "?";
                    break;
            }

            Console.Write(result+" ");
        }
    }
}
