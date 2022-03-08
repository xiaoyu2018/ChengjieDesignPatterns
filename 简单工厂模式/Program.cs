using System;


//计算器实现加减乘除
namespace 简单工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            double n1, n2;
            string s;
            Operation o;
            while (true)
            {

                try
                {
                    n1 = double.Parse(Console.ReadLine());
                    n2 = double.Parse(Console.ReadLine());
                    s = Console.ReadLine();
                    o = OperatorFactory.CreateOperator(s);

                    o.Num1 = n1;
                    o.Num2 = n2;

                    Console.WriteLine(o.GetResult());
                }
                catch (Exception e)
                {

                    Console.WriteLine(e.Message);
                    continue;
                }
            }
            
            
        }
    }


    class OperatorFactory
    {
        static Operation Oper = null;
        public static Operation CreateOperator(string op)
        {
            switch (op)
            {
                case "+": Oper = new OpAdd();break;
                case "-": Oper = new OpSub(); break;
                case "*": Oper = new OpMulti(); break;
                case "/": Oper = new OpDiv(); break;

                default:Oper = null;break;
            }
            if (Oper == null)
                throw new Exception("运算符不存在");

            return Oper;
        }
    }

    abstract class Operation
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }

        abstract public double GetResult();

    }

    class OpAdd : Operation
    {
        public override double GetResult()
        {
            return Num1 + Num2;
        }
    }
    class OpSub : Operation
    {
        public override double GetResult()
        {
            return Num1 - Num2;
        }
    }
    class OpMulti : Operation
    {
        public override double GetResult()
        {
            return Num1 * Num2;
        }
    }
    class OpDiv : Operation
    {
        public override double GetResult()
        {
            return Num1 / Num2;
        }
    }
}
