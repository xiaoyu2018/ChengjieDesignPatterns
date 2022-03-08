using System;
using System.Collections.Generic;

//表示一个作用域某对象结构中的各元素的操作。它使你可以在不改变各元素的类的前提下定义作用域这些元素的新操作。
//访问者模式仅适用于数据结构相对稳定的系统，他把数据结构和作用于结构上的操作之间的耦合解脱开，使得操作集合可以相对自由地演化。
//访问者模式的优点就是增加新的操作（访问者）很容易，访问者模式将有关的行为集中到一个访问者对象中
//访问者模式的缺点就是使得增加新的数据结构变得更为困难
//GoF的作者之一曾说：大多数时候你并不需要访问者模式，但一旦你使用了访问者模式，你就真的很需要他了。


//人类只分男人和女人两类，这代表了一个稳定的数据结构，而人类的行为有成千上万种。
//这时使用访问者模式是合适的。

namespace _24访问者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectStruct objectStruct = new ObjectStruct();

            objectStruct.Add(new Man());
            objectStruct.Add(new Woman());
            objectStruct.Add(new Man());

            objectStruct.Display(new Run());
        }
    }


    class ObjectStruct
    {
        List<Human> humen = new List<Human>();

        public void Add(Human human)
        {
            humen.Add(human);
        }

        public void Remove(Human human)
        {
            humen.Remove(human);
        }

        public void Display(Action a)
        {
            foreach (var item in humen)
            {
                item.Act(a);
            }
        }
    }

    abstract class Human
    {
        public string Name { get; set; }

        public abstract void Act(Action action);
    }
    class Man : Human
    {
        public override void Act(Action action)
        {
            action.GetManConclusion(this);
        }
    }
    class Woman : Human
    {
        public override void Act(Action action)
        {
            action.GetWomanConclusion(this);
        }
    }


    abstract class Action
    {
        public abstract void GetManConclusion(Man man);
        public abstract void GetWomanConclusion(Woman woman);

    }

    //不同状态所做出的操作都在访问者中实现
    //因为男人女人的二分类稳定不变，所以访问者类不会出现修改的情况。新的行为只需要增加子类
    class Eat : Action
    {
        public override void GetManConclusion(Man man)
        {
            Console.WriteLine($"{man.Name}吃了5碗饭！");
        }

        public override void GetWomanConclusion(Woman woman)
        {
            Console.WriteLine($"{woman.Name}吃了1碗饭！");
        }
    }

    class Run : Action
    {
        public override void GetManConclusion(Man man)
        {
            Console.WriteLine($"{man.Name}跑得快！");
        }

        public override void GetWomanConclusion(Woman woman)
        {
            Console.WriteLine($"{woman.Name}跑得慢！");
        }
    }
}
