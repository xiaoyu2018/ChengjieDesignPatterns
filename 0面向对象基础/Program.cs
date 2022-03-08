using System;
using System.Collections.Generic;

//本章讲了学习设计模式之前所需的基础面向对象知识
//各类编程实体配合得当，编程体验酣畅淋漓！
namespace _0面向对象基础
{
    class Program
    {
        static void Main(string[] args)
        {
            Three();
        }

        static void One()
        {
            Aniamal cat = new Cat("咪咪");
            Aniamal dog = new Dog("旺财");
            Aniamal pig = new Pig("佩奇");

            cat.Shout();
            dog.Shout();
            pig.ShoutNum = 1;
            pig.Shout();
        }

        static void Two()
        {
            BaJie baJie = new BaJie();
            baJie.Change("机器猫");

            List<IChange> changes = new List<IChange>();

            changes.Add(new DingDangCat());
            changes.Add(baJie);

            foreach (var i in changes)
            {
                i.Change("everything");
            }
        }

        static void Three()
        {
            Cat cat1 = new Cat("Tom");
            Cat cat2 = new Cat("Tony");

            Mouse mouse1 = new Mouse("Jerry");
            Mouse mouse2 = new Mouse("Jenny");

            cat1.CatRush += mouse1.Action;
            cat1.CatRush += mouse2.Action;

            cat2.CatRush += mouse1.Action;
            cat2.CatRush += mouse2.Action;

            Console.WriteLine("按下回车，观看老猫和鼠！");
            Console.Read();
            cat1.LetsGo();
            cat2.LetsGo();
        }
    }

    interface IChange
    {
        void Change(string item);
    }


    abstract class Aniamal
    {
        public string Name;
        private int _num = 3;

        

        public int ShoutNum { get { return _num; } 
            set
            {
                if (value <= 10)
                    _num = value;
                else
                    _num = 10;
            } 
        }

        public Aniamal(string name)
        {
            Name = name;
        }

        public Aniamal()
        {
            Name = "无名";
        }

        protected abstract string GetShout();

        public void Shout()
        {
            string result = "";
            result += "我是 " + Name+" ";
            for (int i = 0; i < _num; i++)
            {
                result += GetShout()+" ";
            }

            Console.WriteLine(result);
        }
    }

    class Cat : Aniamal
    {

        public event EventHandler CatRush;

        public void LetsGo()
        {
            OnCatRush();
        }

        protected void OnCatRush()
        {
            if (CatRush == null)
                return;
            Console.WriteLine($"老{Name}冲锋!");
            CatRush.Invoke(this,new EventArgs());
        }

        public Cat() : base() { }


        public Cat(string name) : base(name) { }


        protected override string GetShout()
        {
            return "喵";
        }
    }

    class Mouse : Aniamal
    {
        public Mouse() : base() { }

        public Mouse(string name) : base(name) { }


        protected override string GetShout()
        {
            return "吱";
        }

        internal void Action(object sender, EventArgs e)
        {
            if((sender as Cat).Name=="Tom")
                Console.WriteLine($"是老汤姆！我{Name}要溜溜球辣！");
            else
                Console.WriteLine("小事小事，xdm继续嗨皮！");
        }
    }

    class Dog : Aniamal
    {
        public Dog() : base() { }


        public Dog(string name) : base(name) { }


        protected override string GetShout()
        {
            return "汪";
        }
    }

    class Pig : Aniamal
    {
        public Pig() : base(){}

        public Pig(string name) : base(name) {}
       

        protected override string GetShout()
        {
            return "哼";
        }
    }

    class DingDangCat : Cat, IChange
    {
        public DingDangCat()
        {
            Name = "叮当猫";
        }
        public void Change(string item)
        {
            Console.WriteLine($"我有万能口袋，变出{item}");
        }
    }

    class BaJie : Pig, IChange
    {
        public void Change(string item)
        {
            Console.WriteLine($"我有36变，变出{item}");
        }
    }


}
