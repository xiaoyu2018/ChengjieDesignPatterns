using System;

//当我们要完成在某一细节层次一致的一个过程或一系列步骤，但个别步骤在更详细的层次上实现细节可能不同时，考虑使用模板方法模式


//一份试卷给多个学生做，只有答案不同。
namespace _7模板方法模式
{
    class Program
    {
        static void Main(string[] args)
        {
            TestPaper[] testPapers = new TestPaper[10];

            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine($"============================试卷{i}============================");
                testPapers[i] = new AnsweredPaper("00" + i.ToString(), "A", "B");
                testPapers[i].ShowPaper();
            }


        }
    }

    abstract class TestPaper
    {
        protected string ID="";
        protected string a1 = "";
        protected string a2 = "";

        public void TestOne()
        {
            Console.WriteLine("1 杨过得到，后来给了郭靖，炼成倚天剑、屠龙刀的玄铁可能是【 】\na.球墨铸铁 b.马口铁 c.高速合金钢 d.碳素纤维");

            Console.WriteLine(GetID()+Answer1());
        }
        public void TestTwo()
        {
            Console.WriteLine("2 杨过铲除情花，造成了【 】\na.这种植物不再害人 b.这种珍稀物种灭绝 c.破坏生态平衡 d.地区沙漠化");

            Console.WriteLine(GetID() + Answer2());

        }

        public void ShowPaper()
        {
            TestOne();
            TestTwo();

        }
        public string GetID()
        {
            return $"学生ID：{ID}\n作答如下：\n";
        }

        
        protected abstract string Answer1();
        protected abstract string Answer2();
        

    }

    class AnsweredPaper : TestPaper
    {
        public AnsweredPaper(string ID,string a1,string a2)
        {
            this.ID = ID;
            this.a1 = a1;
            this.a2 = a2;

        }
        protected override string Answer1()
        {
            return a1;
        }

        protected override string Answer2()
        {
            return a2;
        }

        
    }
}
