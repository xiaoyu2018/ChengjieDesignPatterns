using System;

//状态模式解决了控制一个对象状态转化的分支语句过于复杂的情况。把状态判断逻辑转移到不同状态的一系列类中，简化了复杂的逻辑。
//当一个对象的行为取决于他的状态，并且在运行时时刻根据状态改变他的行为，考虑用状态模式。

//上班时，不同时间有不同的工作状态。

namespace _12状态模式
{
    class Program
    {
        static void Main(string[] args)
        {
            //有时间跨度也能到达正确的状态！
            Work work = new Work(new ForeNoonState());
            work.Time = 9;
            work.Handle();
            work.Time = 12;
            work.Handle();
            work.Time = 18;
            work.Handle();
            work.Time = 22;
            work.Handle();
            work.WorkFinished = true;
            work.Handle();
        }
    }


    interface IState
    {
        void WriteProgram(Work w);
    }

    class ForeNoonState : IState
    {
        public void WriteProgram(Work w)
        {
            if (w.Time < 12)
                Console.WriteLine($"当前时间：{w.Time}:00 上午工作，精神百倍！");
            else
            {
                //继续工作进入中午
                w.state = new NoonState();
                w.Handle();
            }
        }
    }

    class NoonState : IState
    {
        public void WriteProgram(Work w)
        {
            if (w.Time < 13)
                Console.WriteLine($"当前时间：{w.Time}:00 饿了，吃饭！");
            else
            {
                //继续工作进入下午
                w.state = new AfternoonState();
                w.Handle();
            }
        }
    }

    class AfternoonState : IState
    {
        public void WriteProgram(Work w)
        {
            if (w.Time < 17)
                Console.WriteLine($"当前时间：{w.Time}:00 下午状态不错，继续努力！");
            else
            {
                //继续工作进入晚上
                w.state = new NightState();
                w.Handle();
            }
        }
    }

    class NightState : IState
    {
        public void WriteProgram(Work w)
        {
            if (w.WorkFinished == true)
            {
                w.state = new RestState();
                w.Handle();
            }
            //加班
            else
            {
                if (w.Time > 23)
                {
                    w.state = new SleepState();
                    w.Handle();
                }
                else
                {
                    Console.WriteLine($"{w.Time}:00 我在加班！");
                }
            }
        }
    }

    class RestState : IState
    {
        public void WriteProgram(Work w)
        {
            Console.WriteLine($"{w.Time}:00 下班回家！");
        }
    }

    class SleepState : IState
    {
        public void WriteProgram(Work w)
        {
            Console.WriteLine($"不行了，睡着了。。。");

        }
    }
    class Work
    {
        public bool WorkFinished { get; set; }
        public int Time { get; set; }

        public IState state { get; set; }

        public Work(IState state)
        {
            this.state = state;
            WorkFinished = false;
        }

        public void Handle()
        {
            state.WriteProgram(this);
        }
    }
}


