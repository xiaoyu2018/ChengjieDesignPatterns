using System;
using System.Collections.Generic;

//提供一种方法顺序访问一个聚合对象中的各个元素，而又不暴露该对象的内部表示。
//foreach和IEnumerable、IEnumerator都是迭代器模式的体现

namespace _16迭代器模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Two();
        }

        //.NET迭代器使用
        static void One()
        {
            //List是个聚类，可被迭代，其实现了IEnumerable
            IEnumerable<string> list = new List<string>() { "1asd" ,"asd","123"};

            //聚类下的GetEnumerator()将自己作为IEnumerator构造函数的参数传入
            IEnumerator<string> enumerator = list.GetEnumerator();

            while (enumerator.MoveNext())
            {
                Console.WriteLine(enumerator.Current);
            }

            //使用foreach，编译器自动产生了IEnumerator迭代器将聚类迭代
            //也即所有实现了IEnumerable的对象都可以使用foreach取出每一个元素
            foreach (string i in list)
            {
                Console.WriteLine(i);
            }
        }

        static void Two()
        {
            ConcreateAggregate ca = new ConcreateAggregate();
            ca[0] = "大鸟";
            ca[1] = "小菜";
            ca[2] = "老外";

            Iterator i = ca.CreateIterator();

            while (!(i.IsDone()))
            {
                Console.WriteLine($"{i.CurrentItem()} 请买票！");
                i.Next();
            }
        }
    }

    ////.NET中相当于IEnumerator
    abstract class Iterator
    {
        public abstract object First();
        public abstract object Next();
        public abstract bool IsDone();
        public abstract object CurrentItem();


    }

    class ConcreateIterator:Iterator
    {
        private ConcreateAggregate _aggreagte;
        private int _current;

        public ConcreateIterator(ConcreateAggregate aggregate)
        {
            _aggreagte = aggregate;
            _current = 0;
        }

        public override object CurrentItem()
        {
            return _aggreagte[_current];
        }

        public override object First()
        {
            return _aggreagte[0];
        }

        public override bool IsDone()
        {
            if (_current >= _aggreagte.Count)
                return true;
            return false;
        }

        public override object Next()
        {
            _current++;
            if (_current >= _aggreagte.Count)
                return null;
            return _aggreagte[_current];
            
        }
    }

    //.NET中相当于IEnumerable
    abstract class Aggregate
    {
        public abstract Iterator CreateIterator();
    }

    class ConcreateAggregate : Aggregate
    {
        private List<object> _list = new List<object>();
        public override Iterator CreateIterator()
        {
            return new ConcreateIterator(this);
        }

        public int Count { get { return _list.Count; } }

        //索引器
        public object this[int index]
        {
            get { return _list[index]; }
            set { _list.Insert(index,value); }
        }
    }
}
