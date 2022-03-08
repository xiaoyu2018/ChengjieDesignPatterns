using System;

//戴砺替他朋友卓易给娇娇送礼物。

namespace _4代理模式
{
    class Program
    {
        static void Main(string[] args)
        {
            SchoolGirl schoolGirl = new SchoolGirl();
            schoolGirl.Name = "娇娇";

            Proxy proxy = new Proxy(schoolGirl);

            proxy.GiveAway();
        }
    }


    class SchoolGirl
    {
        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        internal void Action(object sender, EventArgs e)
            
        {
            Console.WriteLine($"替我谢谢{(sender as Persuit).Name}");
        }
    }

    interface IGiveAway
    {

        void GiveAway();
    }

    class Persuit : IGiveAway
    {
        private SchoolGirl _schoolGirl;
        public string Name { get; set; }
        public string Present { get; set; }

        public event EventHandler SendPresent;

        public Persuit(SchoolGirl schoolGirl)
        {
            Present = "无";
            
            _schoolGirl = schoolGirl;  
        }

        private void OnSendPresent()
        {
            SendPresent.Invoke(this, new EventArgs());
        }

        public void GiveAway()
        {
            Console.WriteLine($"{_schoolGirl.Name},送你{Present}");
            OnSendPresent();
        }
    }

    class Proxy : IGiveAway
    {
        Persuit Persuit;
        public Proxy(SchoolGirl schoolGirl)
        {
            Persuit = new Persuit(schoolGirl);
            Persuit.SendPresent += schoolGirl.Action;
            Persuit.Present = "电影票";
            Persuit.Name = "卓易";
        }
        public void GiveAway()
        {
            Persuit.GiveAway();
        }
    }
}
