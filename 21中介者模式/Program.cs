using System;

//用一个中介对象来封装一系列的对象交互，中介者使个对象不需要显示地互相引用，从而使其耦合松散，而且可独立地改变他们之间的交互
//优点：减少了各个Colleague的耦合，使得可以更独立地改变和复用Colleague和Mediator类
//缺点：ConcreateMediator控制了集中化，把各个Colleague地交互复杂性变成了中介者地复杂性，这就是得中介者变得越来越复杂。
//中介者模式一般应用于一组对象以定义良好但是复杂的方式进行通信的场合（如图形界面计算器）,以及像定制一个分布在多个类中的行为，但又不想生成太多子类的场合。

//美国和伊拉克之间的对话都是通过联合国安理会作为中介来完成的。
namespace _21中介者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            UN un = new UN();
            Iraq iraq = new Iraq(un);
            USA usa= new USA(un);

            un.USA = usa;
            un.Iraq = iraq;
            iraq.Send("草拟大坝！");
            usa.Send("牛！");
            
        }
    }

    abstract class InternationalOrgnazition
    {
        public abstract void Declare(string content, Country source);
    }


    //所有实体只与中介耦合
    abstract class Country
    {
        protected InternationalOrgnazition mediator;

        public Country(InternationalOrgnazition m)
        {
            mediator = m;
        }

        public abstract void ShowMessage(string receievedMessage);

        public void Send(string msg)
        {
            mediator.Declare(msg, this);
        }
    }

    class USA : Country
    {
        public USA(InternationalOrgnazition m) : base(m)
        {
        }

        public override void ShowMessage(string receievedMessage)
        {
            Console.WriteLine($"美军收到伊拉克消息：{receievedMessage}");
        }
    }
    class Iraq : Country
    {
        public Iraq(InternationalOrgnazition m) : base(m)
        {
        }

        public override void ShowMessage(string receievedMessage)
        {
            Console.WriteLine($"伊拉克收到美军消息：{receievedMessage}");
        }
    }

    //ConcreateMediator类必须要知道以他为中介的所有对象
    
    class UN : InternationalOrgnazition
    {
        
        public Country USA { get; set; }
        public Country Iraq { get; set; }

        public override void Declare(string content, Country source)
        {
            if (source == USA)
                Iraq.ShowMessage(content);
            else
                USA.ShowMessage(content);
        }
    }
}
