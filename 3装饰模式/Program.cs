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

            Decorator[] decorators = { new Underwear(), new Socks(), new ShortTrouser(), new TShirt(), new Tie() };

            for (int i = 0; i < 5; i++)
            {
                if (i == 0)
                    decorators[i].Decorate(cai);
                else
                    decorators[i].Decorate(decorators[i - 1]);
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
            Console.WriteLine("装扮的男生--{0}", Name);
        }
    }

    class Female : Person
    {
        public Female(string name) : base(name) { }

        public override void Show()
        {
            Console.WriteLine("装扮的女生--{0}", Name);
        }
    }

    //继承Person类保证了装饰器只是一个装饰
    //给人加上装饰，人还是人,可以继续被装饰。
    //此为抽象装饰，不能直接用
    abstract class Decorator : Person
    {
        private Person _person;
        protected string _itemName;
        
        //获取要装饰的对象
        public void Decorate(Person person)
        {
            _person = person;
        }


        public override void Show()
        {
            Console.Write(_itemName);
            //在此之前设置装饰操作
            if (_person != null)
                _person.Show();
            //在此之后也可以设置装饰操作
        }
    }

    class TShirt : Decorator
    {
        public TShirt()
        {
            _itemName = "衬衫";
        }
    }

    class Tie : Decorator
    {
        public Tie()
        {
            _itemName = "领带";
        }
    }

    class Underwear : Decorator
    {
        public Underwear()
        {
            _itemName = "内裤";
        }
    }

    class ShortTrouser : Decorator
    {
        public ShortTrouser()
        {
            _itemName = "短裤";
        }
    }

    class Socks : Decorator
    {
        public Socks()
        {
            _itemName = "袜子";
        }
    }


}
