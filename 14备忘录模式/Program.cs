using System;
using System.Collections.Generic;

//在不破坏一个对象的封装性的前提下，捕获一个对象的内部状态，并在该对象之外保存这个状态。从而实现以后恢复该对象为原先保存的状态。

//打BOSS之前，先存个档保存下游戏角色状态吧！

namespace _14备忘录模式
{
    class Program
    {
        static void Main(string[] args)
        {

            GameRole lixiaoyao = new GameRole();
            RoleStateCareTaker careTaker = new RoleStateCareTaker();
            careTaker.Add(lixiaoyao.CreateMem());

            lixiaoyao.Attack += 50;
            lixiaoyao.Attack += 10;
            careTaker.Add(lixiaoyao.CreateMem());

            lixiaoyao.Health = 0;
            lixiaoyao.ShowState();

            Console.WriteLine("==========存档一==========");
            lixiaoyao.LoadMem(careTaker.Provide(0));
            lixiaoyao.ShowState();
            Console.WriteLine("==========存档二==========");
            lixiaoyao.LoadMem(careTaker.Provide(1));
            lixiaoyao.ShowState();

        }
    }

    //发起人
    class GameRole
    {
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defense { get; set; }

        public GameRole()
        {
            Health = 100;
            Attack = 100;
            Defense = 100;
        }

        public void ShowState()
        {
            Console.WriteLine($"生命值：{Health}\n攻击力：{Attack}\n防御力：{Defense}");
        }

        public RoleStateMemento CreateMem()
        {

            return new RoleStateMemento(Health, Attack, Defense);
        }

        public void LoadMem(RoleStateMemento memento)
        {
            Health = memento.Health;
            Attack = memento.Attack;
            Defense = memento.Defense;
        }
    }

    //备忘录
    //要保存的细节在备忘录中体现，从发起人中挑选
    class RoleStateMemento 
    {
        

        //存档内容不允许随意修改
        public int Health { get;}
        public int Defense { get;}
        public int Attack { get; }

        public RoleStateMemento(int health,int attack,int defnese)
        {
            Health = health;
            Defense = defnese;
            Attack = attack;
        }
    }

    //管理者
    //管理多个存档
    class RoleStateCareTaker
    {
        private List<RoleStateMemento> _mementos=new List<RoleStateMemento>();

        public void Add(RoleStateMemento m)
        {
            _mementos.Add(m);
        }

        public RoleStateMemento Provide(int i)
        {
            return _mementos[i];
        }
    }
}
