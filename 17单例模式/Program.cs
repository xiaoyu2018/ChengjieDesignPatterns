using System;

//保证一个类仅有一个实例，并提供一个访问它的全局访问点
//让类本身保证自己只有一个实例能被创建！

namespace _17单例模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton1 s1 = Singleton1.GetInstance();

            var s2 = Singleton1.GetInstance();

            Console.WriteLine((s1==s2));
        }
    }

    //单例模式1：在第一次被引用时才实例化出单例。
    class Singleton1
    {
        //必须是静态变量，保证此单例附属于类
        private static Singleton1 _instance;

        //加锁资源,相当于互斥信号量
        //只能在类第一次加载时实例化
        private static readonly object _syncRoot = new object();

        //构造函数访问级别设为private，防止外部new实例
        private Singleton1()
        {

        }

        //使用一个函数来控制单例
        //多线程时可能出现创建出多个实例的情况，要加个线程锁
        public static Singleton1 GetInstance()
        {
            //为提高程序效率，只有需要创建单例时才去争抢资源
            if(_instance==null)
            {
                lock (_syncRoot)
                {
                    //由于多线程，这里也要判断，自己想想吧
                    if (_instance == null)
                        _instance = new Singleton1();
                }
            }

            return _instance;
        }
    }


    //直接在类加载时就实例化单例，单例必然只有一个
    sealed class Singleton2
    {
        private static readonly Singleton2 _instance = new Singleton2();

        private Singleton2()
        {

        }

        public Singleton2 GetINstance()
        {
            return _instance;
        }
    }
}
