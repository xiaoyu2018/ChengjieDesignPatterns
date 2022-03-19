using System;
using System.Collections.Generic;
using System.Threading;

//将一个请求封装为一个对象，从而使你可用不同的请求对客户进行参数化；对请求排队、记录请求日志、撤销请求等操作。
//命令模式能简便地设计一个命令队列；在需要的情况下可以容易地将命令记入日志；允许接收命令的一方否决请求；容易实现命令撤销；增加新具体命令很容易
//最重要的一点：命令模式把请求操作的对象和执行操作的对象分割开


//小菜和大鸟去门店吃烧烤，门店与烧烤摊不同，是命令模式的体现。
namespace _19命令模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Babecuer babecuer = new Babecuer();

            Waiter waiter = new Waiter();

            waiter.Order(new KaoYangRouChuan(babecuer, 10));
            waiter.Order(new KaoJiChi(babecuer, 10));
            waiter.Order(new KaoYangRouChuan(babecuer, 30));

            BakeCommand c = new KaoYangRouChuan(babecuer, 50);

            waiter.Remove(c);

            waiter.Notify();
        }
    }

    abstract class BakeCommand
    {
        protected Babecuer reciever;
        protected int _amount;

        public BakeCommand(Babecuer reciever,int amount)
        {
            this.reciever = reciever;
            _amount = amount;
        }

        public abstract void Excute();
    }

    class KaoYangRouChuan : BakeCommand
    {
        
        public KaoYangRouChuan(Babecuer reciever,int a):base(reciever,a)
        {

        }
        public override void Excute()
        {
            reciever.BakeMutton(_amount);
        }
    }

    class KaoJiChi : BakeCommand
    {
        public KaoJiChi(Babecuer reciever, int a) : base(reciever, a)
        {

        }
        public override void Excute()
        {
            reciever.BakeChikenWing(_amount);
        }
    }

    class Babecuer
    {
        public void BakeMutton(int num)
        {
            Thread.Sleep(100 * num);
            Console.WriteLine($"烤了{num}串羊肉串！时间：{DateTime.Now}");
        }

        public void BakeChikenWing(int num)
        {
            Console.WriteLine($"烤了{num}串鸡翅！时间：{DateTime.Now}");
        }
    }

    class Waiter
    {
        List<BakeCommand> commands = new List<BakeCommand>();

        public void Order(BakeCommand command)
        {
            if (command.ToString() == "_19命令模式.KaoJiChi")
                Console.WriteLine("鸡翅没了！");
            else
            {
                commands.Add(command);
            }
        }

        public void Remove(BakeCommand command)
        {
            commands.Remove(command);
        }


        public void Notify()
        {
            foreach (var i in commands)
            {
                i.Excute();
            }
        }

    }
}
