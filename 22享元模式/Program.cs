using System;
using System.Collections;

//运用共享技术有效地支持大量细粒度的对象
//享元模式避免了大量相似类的开销，在程序设计中，如果发现用于表示数据的实例除了几个参数外基本都相同，则考虑使用享元模式

//大型博客网站、电子商务网站里面的每一个博客和一个商家都可以理解为一个个小的网站，核心代码和数据库都是共享的。节省了大量资源。
namespace _22享元模式
{
    class Program
    {
        static void Main(string[] args)
        {
            User user1 = new User();
            User user2 = new User();

            user1.Name = "小菜";
            user2.Name = "大鸟";

            Factory factory = new Factory();

            Website w1 = factory.GetWeb("博客");
            Website w2 = factory.GetWeb("电商");
            Website w3 = factory.GetWeb("博客");
            Website w4 = factory.GetWeb("电商");
            Website w5 = factory.GetWeb("电商");
            Website w6 = factory.GetWeb("电商");

            factory.ShowWebAmount();

            w2.Use(user1);
            w6.Use(user2);

            //两个变量保存的引用是否相同
            Console.WriteLine(object.ReferenceEquals(w2,w6));
            //两个变量保存的引用指向的值是否相等
            Console.WriteLine(object.Equals(w2,w6));

            string a1 = new string("asd");
            string a2 = "asd";

            Console.WriteLine(object.Equals(a1,a2)); //True
            Console.WriteLine(object.ReferenceEquals(a1, a2)); //False


        }
    }

    //享元对象的外部状态
    class User
    {
        public string Name { get; set; }

    }

    //抽象享元类
    abstract class Website
    {
        protected string name;

        public abstract void Use(User user);
    }

    //具体享元类
    class ConcreateWebsite : Website
    {
        public ConcreateWebsite(string name)
        {
            this.name = name;
        }
        public override void Use(User user)
        {
            Console.WriteLine($"网站名称：{name} 登录用户：{user.Name}");
        }
    }

    //享元工厂
    class Factory
    {
        private Hashtable websits = new Hashtable();
        

        //相同的网站名对应相同对象
        public Website GetWeb(string webName)
        {
            if (websits[webName] == null)
                websits.Add(webName, new ConcreateWebsite(webName));

            //强制转换到父类
            return (Website)websits[webName];
        }

        public void ShowWebAmount()
        {
            Console.WriteLine(websits.Count);
        }
    }
}
