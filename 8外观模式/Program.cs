using System;

//为子系统提供给一个一致的外观界面，定义出一个高层接口，这个高层结构使子系统更加容易使用
//当子系统很复杂时，使用外观模式可以让外界专注于使用子系统而不关心子系统内部实现

//小白买股票不如买基金，基金经理去面对复杂的股票子系统，小白只需要去使用基金经理提供的高层接口

//外观模式提供了一个再次封装的接口，对基本的方法再次组合、修改
//工厂模式和策略模式直接返回，不会组合、修改
namespace _8外观模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Manager manager = new Manager();
            manager.BuyWithPlan1();
            manager.BuyWithPlan2();

            manager.Sell();
        }
    }
    abstract class Base
    {
        public string Name { get; set; }

        public abstract void Buy();
        public abstract void Sell();

    }

    class Stock: Base
    {

        public override void Buy()
        {
            Console.WriteLine($"买入股票{Name}");
        }

        public override void Sell()
        {
            Console.WriteLine($"卖出股票{Name}");
        }
    }

    class NationalDebt : Base
    {
        public override void Buy()
        {
            Console.WriteLine($"买入国债{Name}");
        }

        public override void Sell()
        {
            Console.WriteLine($"卖出国债{Name}");
        }
    }

    class Realty : Base
    {
        public override void Buy()
        {
            Console.WriteLine($"买入实体{Name}");
        }

        public override void Sell()
        {
            Console.WriteLine($"卖出实体{Name}");
        }
    }

    class Manager
    {
        //该经理看好这几支
        Base Base0 = new Stock() { Name = "小米科技" };
        Base Base1 = new Stock() { Name = "北京青枫" };
        Base Base2 = new Realty() { Name = "万科" };
        Base Base3 = new Realty() { Name = "万达" };
        Base Base4 = new NationalDebt() { Name = "美国国债" };
        Base Base5 = new NationalDebt() { Name = "中国国债" };

        public void BuyWithPlan1()
        {
            Base1.Buy();
            Base5.Buy();
        }

        public void BuyWithPlan2()
        {
            Base3.Buy();
            Base2.Buy();
            Base4.Buy();
        }

        public void Sell()
        {
            Console.WriteLine("Sell All");
        }

        //还有可以有很多其他操作，对外隐藏了操作细节
    }
}
