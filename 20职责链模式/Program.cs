using System;

//使多个对象被都有机会处理请求，避免请求的发送者和接收者之间的耦合关系。将多个对象连成一条链，沿着该链传递请求，知道有一个对象处理请求为止。
//像加强版链表！将大量分支语句分散到不同的类中，利用开放-封闭原则将分支语句消除！

//员工的加薪、请假等请求可以传给不同管理层人员，管理层根据自己的权限判断处理请求或者向上报告。

namespace _20职责链模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Request r1 = new Request() { Number = 2, RequestType = "请假" };
            Request r2 = new Request() { Number = 5, RequestType = "请假" };
            Request r3 = new Request() { Number = 222, RequestType = "涨薪" };
            Request r4 = new Request() { Number = 1234, RequestType = "涨薪" };
            Request r5 = new Request() { Number = 222, RequestType = "请假" };

            Manager m1 = new CommonManager();
            Manager m2 = new Majordomo();
            Manager m3 = new GeneralManager();

            m1.SetSuccessor(m2);
            m2.SetSuccessor(m3);

            Request[] requests = new Request[] { r1, r2, r3, r4, r5 };

            foreach (var i in requests)
            {
                m1.Handle(i);
            }
        }
    }

    struct Request
    {
        public string RequestType { get; set; }

        public int Number { get; set; }

    }

    abstract class Manager
    {
        protected Manager successor;

        public void SetSuccessor(Manager manager)
        {
            successor = manager;
        }

        public abstract void Handle(Request request);
    }

    class CommonManager : Manager
    {
        public override void Handle(Request request)
        {
            if(request.RequestType=="请假"&&request.Number<=2)
            {
                Console.WriteLine($"请假{request.Number}天，我小组长就能给你批了！");
                Console.WriteLine("=========================================================");
            }

            else
            {
                Console.WriteLine("你这个要求，俺这个小组长权限不够啊！帮你问问带总监");
                if (successor != null)
                    successor.Handle(request);
            }
        }
    }

    class Majordomo : Manager
    {
        public override void Handle(Request request)
        {
            if (request.RequestType == "请假" && request.Number <= 5)
            {
                Console.WriteLine($"请假{request.Number}天，我带总监就能给你批了！");
                Console.WriteLine("=========================================================");
            }
            else
            {
                Console.WriteLine("你这个要求，俺这个带总监权限不够啊！帮你问问总经理");
                if (successor != null)
                    successor.Handle(request);
            }
        }
    }

    class GeneralManager : Manager
    {
        public override void Handle(Request request)
        {
            if(request.RequestType == "请假")
            {
                if(request.Number<=10)
                    Console.WriteLine($"{request.Number}天假期，快去快回，回来加班！");
                else
                    Console.WriteLine($"{request.Number}天？不想干直说。");
            }

            else
            {
                if(request.Number<=500)
                    Console.WriteLine($"看你表现好，一个月涨{request.Number}！");
                else
                    Console.WriteLine($"想钱想疯了？一个月涨{request.Number}？不如你来剥削我吧！");
            }
            Console.WriteLine("=========================================================");

        }
    }
}
