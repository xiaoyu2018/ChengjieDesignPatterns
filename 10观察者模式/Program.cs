using System;
using System.Collections.Generic;

//观察者模式定义了一种一对多的依赖关系，多个观察者监听一个通知者。
//当通知者在状态发生变化时通知所有监听自己的观察者，使他们能及时改变自身状态

//公司同事买通了前台，老板一回来，前台就会通知摸鱼的员工！

namespace _10观察者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Two();
        }
        //经典观察者模式
        static void One()
        {
            Boss huhansan = new Boss();
            NBAObserver laozhang = new NBAObserver("张伟",huhansan);
            StockObserver laozhao = new StockObserver("赵华琼", huhansan);

            huhansan.Attach(laozhao);
            huhansan.Attach(laozhang);

            huhansan.Action = "我胡汉三回来了";
            huhansan.Notify();

        }
        //事件实现观察者模式
        static void Two()
        {
            LiveShowObserver liveShowObserver = new LiveShowObserver();
            MovieObserver movieObserver = new MovieObserver();

            Secretary secretary = new Secretary();

            secretary.Notified += liveShowObserver.StopLive;
            secretary.Notified += movieObserver.StopMovie;

            secretary.Notify();
        }
    }

    //经典观察者模式
    //通知者
    interface ISubject
    {
        void Attach(Observer observer);
        void Detach(Observer observer);
        void Notify();

        string Action { get; set; }
    }

    class Boss : ISubject
    {
        public string Action { get; set; }
        List<Observer> observers = new List<Observer>();

        public void Attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void Detach(Observer observer)
        {
            observers.Remove(observer);
        }

        public void Notify()
        {
            foreach (var i in observers)
            {
                i.Upadate();
            }
        }
    }
    
    //观察者
    abstract class Observer
    {
        private string _name;

        protected string _state="普通摸鱼";

        private ISubject _subject;

        public Observer(string name,ISubject subject)
        {
            _name = name;
            _subject = subject;
        }

        public void Upadate()
        {
            if(_subject==null)
                Console.WriteLine($"{_state}被抓到了");
            else
                Console.WriteLine($"{_subject.Action} 我{_name}结束{_state}");
        }
    }

    class NBAObserver:Observer
    {
        public NBAObserver(string name, ISubject subject):base(name,subject)
        {
            _state = "看NBA";
        }
    }

    class StockObserver : Observer
    {
        public StockObserver(string name, ISubject subject) : base(name, subject)
        {
            _state = "看股票";
        }
    }
    //事件实现观察者模式
    interface Subject
    {
        void Notify();
    }
    class Secretary : Subject
    {
        public event EventHandler Notified;

        public void Notify()
        {
            if(Notified!=null)
            Notified(this, new EventArgs());
        }
    }


    class LiveShowObserver
    {
        internal void StopLive(object sender, EventArgs e)
        {
            Console.WriteLine("不看直播了");
        }
    }

    class MovieObserver
    {
        internal void StopMovie(object sender, EventArgs e)
        {
            Console.WriteLine("不看电影了");
        }
    }
}
