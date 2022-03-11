using System;

//相比于简单工厂增加了额外的开发量，
//但实现了工厂类的封闭，工厂类只对扩展开放，克服了简单工厂在增加产品时需要修改工厂类的缺点
//在客户端集中了对象的创建，使产品众多时更好修改。

//有志愿者、大学生、街溜子。。。他们都是学雷锋预备队！
//建立雷锋工厂，助人为乐大本营。
namespace _5工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //想更换同一工厂生产出的对象，只需改这一个地方
            IFactory undergraduateFactory = new UndergraduateFactory();
            IFactory volunteerFactory=new VolunteerFactory();

            LeiFeng stu1 = undergraduateFactory.CreateLeiFeng();
            LeiFeng stu2 = undergraduateFactory.CreateLeiFeng();
            LeiFeng vlt1 = volunteerFactory.CreateLeiFeng();
            LeiFeng vlt2 = volunteerFactory.CreateLeiFeng();

            stu1.Cook();
            stu2.Sweep();
            vlt1.Wash();
        }
    }




    abstract class LeiFeng
    {
        protected string Id = "";

        private void Report()
        {
            Console.Write($"我是{Id} ");
        }

        public void Cook()
        {
            Report();
            Console.WriteLine("帮您做做饭");
        }

        public void Sweep()
        {
            Report();
            Console.WriteLine("帮您扫扫地");
        }

        public void Wash()
        {
            Report();
            Console.WriteLine("帮您洗洗碗");
        }
    }

    class Undergraduate : LeiFeng
    {
        public Undergraduate()
        {
            Id = "大学生";
        }

    }

    class Volunteer : LeiFeng
    {
        public Volunteer()
        {
            Id = "志愿者";
        }
    }

    class StreetWander : LeiFeng
    {
        public StreetWander()
        {
            Id = "街溜子";
        }
    }

    interface IFactory
    {
        LeiFeng CreateLeiFeng();
    }

    class UndergraduateFactory : IFactory
    {
        public LeiFeng CreateLeiFeng()
        {
            return new Undergraduate();
        }
    }

    class VolunteerFactory : IFactory
    {
        public LeiFeng CreateLeiFeng()
        {
            return new Volunteer();
        }
    }

    class StreetWanderFactory : IFactory
    {
        public LeiFeng CreateLeiFeng()
        {
            return new StreetWander();
        }
    }
}

