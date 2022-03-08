using System;

//为小菜搭配衣服
//要考虑穿衣顺序！

namespace _3装饰模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Male cai = new Male("小菜");

            Decorator[] decorators = { new Underwear(), new Socks(), new ShortTrouser(), new TShirt(), new Tie()};

            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                    decorators[i].Decorate(cai);
                else
                {
                    decorators[i].Decorate(decorators[i - 1]);
                }
            }
            decorators[4].Show();
        }
    }

    abstract class Person
    {
        public Person()
        {
            Name = "无名";
        }
        public string Name { get; set; }

        public Person(string name)
        {
            Name = name;
        }
        public abstract void Show();
    }

    class Male : Person
    {
        public Male(string name) : base(name) { }
        
        public override void Show()
        {
            Console.WriteLine("装扮的男生--{0}",Name);
        }
    }

    class FeMale : Person
    {
        public FeMale(string name) : base(name) { }

        public override void Show()
        {
            Console.WriteLine("装扮的女生--{0}", Name);
        }
    }

    //继承Person类保证了装饰器只是一个装饰
    //给人加上装饰，人还是人可以继续被装饰。
    //此为抽象装饰，不能直接用
    abstract class Decorator:Person
    {
        private Person _person;

        public void Decorate(Person person)
        {
            _person = person;
        }

        public override void Show()
        {
            if (_person != null)
                _person.Show();
        }
    }

    class TShirt:Decorator
    {
        private string _itemName = "衬衫 ";
        public override void Show()
        {
            Console.Write(_itemName);
            base.Show();

        }
    }

    class Tie : Decorator
    {
        private string _itemName = "领带 ";
        public override void Show()
        {
            Console.Write(_itemName);
            base.Show();

        }
    }

    class Underwear : Decorator
    {
        private string _itemName = "内裤 ";
        public override void Show()
        {
            Console.Write(_itemName);
            base.Show();

        }
    }

    class ShortTrouser : Decorator
    {
        private string _itemName = "短裤 ";
        public override void Show()
        {
            Console.Write(_itemName);
            base.Show();

        }
    }

    class Socks : Decorator
    {
        private string _itemName = "袜子 ";
        public override void Show()
        {
            Console.Write(_itemName);
            base.Show();
        }
    }
}
