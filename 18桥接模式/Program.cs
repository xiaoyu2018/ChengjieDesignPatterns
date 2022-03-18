using System;

//继承是一种强耦合结构！父类变则子类必然变，只有确定两个事物是is-a的关系才考虑用继承
//其他关系要用组合、聚合来代替

//桥接模式：将抽象部分与他的实现部分分离，使他们都能独立变化。
//实现系统有多角度分类，每一种分类都有可能变化。那么就把多种变化情况（分类方法）分离出来，减少耦合

//有多种品牌的手机，不同品牌的手机可以具有不同的功能。手机分类可按品牌分，也可按功能分。
namespace _18桥接模式
{
    class Program
    {
        static void Main(string[] args)
        {
            MiPhone miPhone = new MiPhone(new GamePlaying());

            Phone Iphone = new IPhone(new MusicListening());

            miPhone.Run();
            Iphone.Run();
        }
    }

    abstract class PhoneFunctoin
    {
        public abstract void Run();
    }

    class GamePlaying : PhoneFunctoin
    {
        public override void Run()
        {
            Console.WriteLine("打游戏！");
        }
    }

    class MusicListening : PhoneFunctoin
    {
        public override void Run()
        {
            Console.WriteLine("听音乐！");
        }
    }

    class Phone
    {
        //手机类的手机功能具体实现可以作为一个字段
        //类似的还可以有手机配置类也可以这样
        //这样分离成多个类再组合/聚合的模式就是桥接模式。
        protected PhoneFunctoin functoin;

        public Phone(PhoneFunctoin f)
        {
            functoin = f;
        }

        public void Run()
        {
            functoin.Run();
        }
    }

    class MiPhone : Phone
    {
        public MiPhone(PhoneFunctoin f) : base(f)
        {

        }
    }

    class IPhone : Phone
    {
        public IPhone(PhoneFunctoin f) : base(f)
        {

        }
    }
}
