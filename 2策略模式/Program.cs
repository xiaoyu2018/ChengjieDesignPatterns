using System;

//输入单价和数量计算价格
//记录总价，选择重置总结归零
//价格计算有不同策略
//策略模式不同于简单工厂模式
//策略模式将各类算法完全封装在内部，使用Context类作为数据的交换窗口。界面逻辑只接收和发送数据，不需出现任何算法（包括简单工厂模式抽象算法）
//在COntxt类中使用简单工厂模式，可以进一步使界面从分支逻辑中解放，switch或if分支被封装在Context类中
//其实就是让简单工厂不直接返回算法对象，算完把数据传回去
namespace _2策略模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("=================价格计算器=================");
            while (true)
            {
                Console.WriteLine("请选择优惠策略：");
                Console.WriteLine("1 无优惠");
                Console.WriteLine("2 八折");
                Console.WriteLine("3 五折");
                Console.WriteLine("4 满100减20");
                Console.WriteLine("5 满500减120");

                int mode;
                StrategyContext context;
                double total = 0;

                try
                {
                    mode = int.Parse(Console.ReadLine());
                    context = new StrategyContext(mode);                    
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    continue;
                }

                while (true)
                {
                    try
                    {
                        Console.WriteLine("请输入商品价格：");
                        double price = double.Parse(Console.ReadLine());

                        if (price == -1)
                        {
                            Console.WriteLine($"本策略计算结束，最终总价额为{total}");
                            break;
                        }

                        Console.WriteLine("请输入商品数量：");
                        int amout = int.Parse(Console.ReadLine());

                        Console.WriteLine($"单次消费额为：{context.GetResult(price, amout)}");
                        total += context.GetResult(price, amout);
                        Console.WriteLine($"目前总价为：{total}");
                    }
                    catch (Exception e)
                    {

                        Console.WriteLine(e.Message);
                        continue;
                    }
                    
                    
                }
            }
            

        }
    }

    class StrategyContext
    {
        private Strategy _strategy;
        public StrategyContext(int mode)
        {
            switch (mode)
            {
                case 1: _strategy = new NoStrategy(); break;
                case 2: _strategy = new Discount(0.8);break;
                case 3:_strategy = new Discount(0.5);break;
                case 4:_strategy = new ReturnMoney(100, 20);break;
                case 5: _strategy = new ReturnMoney(500, 120); break;

                default:
                    _strategy = null;
                    break;
            }
            if (_strategy == null)
                throw new Exception("无此策略！");
            
        }

        //用这个方法来做数据的交换
        public double GetResult(double price,int amount)
        {
            _strategy.Num = amount;
            _strategy.Price = price;
            return _strategy.GetPrice();
        }
    }

    abstract class Strategy
    {
        public int Num { get; set; }
        public double Price { get; set; }

        abstract public double GetPrice();
    }

    class NoStrategy : Strategy
    {
        public override double GetPrice()
        {
            return Num * Price;
        }
    }

    class Discount : Strategy
    {
        private double _f = 1;
        public Discount(double factor)
        {
            _f = factor;
        }
        public override double GetPrice()
        {
            return Num * Price * _f;
        }
    }

    class ReturnMoney : Strategy
    {
        private int _aim;
        private int _bonus;
        public ReturnMoney(int aim,int bonus)
        {
            _aim = aim;
            _bonus = bonus;
        }
        public override double GetPrice()
        {
            double result = 0;
            result = Num * Price;
            if (result > _aim)
                return result - (int)(result / _aim) * _bonus;
            return result;
        }
    }
}
