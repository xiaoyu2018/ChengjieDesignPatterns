using System;

//将一个类的接口转换成客户希望的另一个接口，使原本由于接口不兼容而无法一起工作的类能够一起工作。
//适配器模式主要应用于希望复用那个一些现有的类，但是接口与复用环境不一致的情况。
//适配器模式用于后期维护代码和使用第三方开发组件时。在项目设计阶段，应保证接口一致，命名规范提前约定。
//由于C#不支持多继承，无法应用类适配器模式，本节只讲对象适配器模式。

//姚明刚去NBA不会英语，姚明直接学会英语、教练和其他队员学中文都不现实，只能先找个翻译（适配器）。
namespace _13适配器模式
{
    class Program
    {
        static void Main(string[] args)
        {
            NBAPlayer b = new Forward("巴蒂尔");

            NBAPlayer m = new Guard("麦克格雷迪");

            NBAPlayer yaoming = new Adaptor(new CBACenter("姚明"));

            b.Attack();
            m.Attack();
            yaoming.Defend();
        }
    }

    abstract class NBAPlayer
    {
        protected string name;

        public abstract void Attack();
        public abstract void Defend();

    }

    class Forward : NBAPlayer
    {
        public Forward(string name)
        {
            this.name = name;
        }
        public override void Attack()
        {
            Console.WriteLine($"前锋{name} 发起进攻！");
        }

        public override void Defend()
        {
            Console.WriteLine($"前锋{name} 防守！");

        }
    }

    class Center : NBAPlayer
    {
        public Center(string name)
        {
            this.name = name;
        }
        public override void Attack()
        {
            Console.WriteLine($"中锋{name} 发起进攻！");
        }

        public override void Defend()
        {
            Console.WriteLine($"中锋{name} 防守！");
        }
    }

    class Guard : NBAPlayer
    {
        public Guard(string name)
        {
            this.name = name;
        }
        public override void Attack()
        {
            Console.WriteLine($"后卫{name} 发起进攻！");
        }

        public override void Defend()
        {
            Console.WriteLine($"后卫{name} 防守！");
        }
    }

    //CBA的不会英文
    //未实现接口，导致接口不一致
    class CBACenter
    {
        private string _name;

        public CBACenter(string name)
        {
            _name = name;
        }

        //方法名便不见得一致，可能与本公司设计不符合
        public void 进攻()
        {
            Console.WriteLine($"中锋{_name} 发起进攻！");
        }

        public void 防守()
        {
            Console.WriteLine($"中锋{_name} 防守！");
        }
    }

    //翻译来做适配器
    //适配器实现接口，并拥有一个目标字段。
    class Adaptor : NBAPlayer
    {
        private CBACenter _cBACenter;
        public Adaptor(CBACenter cBACenter)
        {
            _cBACenter = cBACenter;
        }

        public override void Attack()
        {
            _cBACenter.进攻();
        }

        public override void Defend()
        {
            _cBACenter.防守();
        }
    }
}
