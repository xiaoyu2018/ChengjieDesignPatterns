using System;

//原型模式就是从一个对象创建另一个可定制的对象，而不需要知道任何创建细节
//克隆很常用，在.NET中定义好了ICLONE接口，对原型类只需继承克隆接口，在实现具体克隆方法即可

//小菜准备求职简历，同一份简历要复制很多份。
namespace _6原型模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Two();
        }

        //浅复制
        //值类型的字段实现了真复制到新引用变量指向的新开辟内存中
        //引用类型的字段只是将原对象的地址复制给新引用变量，新旧引用变量指向同一块内存
        //string是具有值类型特性的引用类型
        static void One()
        {
            ResumeForLight r1 = new ResumeForLight();
            r1.SetAge(22);
            r1.SetName("大鸟");
            r1.SetSex("男");
            ResumeForLight r2 = (ResumeForLight)r1.Clone();

            r2.SetName("大狗");

            r1.Dispaly();
            r2.Dispaly();
        }

        //深复制
        //所有类型完全copy
        static void Two()
        {
            ResumeForHeavy r1 = new ResumeForHeavy();
            r1.SetAge(22);
            r1.SetName("大鸟");
            r1.SetSex("男");
            r1.SetWork("2018-2020", "ByteDance");
            ResumeForHeavy r2 = r1.Copy();

            r2.SetWork("2019-2020", "HuaWei");

            r1.Dispaly();
            r2.Dispaly();
        }
    }

    //浅复制
    class ResumeForLight : ICloneable
    {
        private string _name;
        private string _sex;
        private int _age;


        public object Clone()
        {
            return (object)this.MemberwiseClone();
        }

        public void SetName(string name)
        {
            _name = name;
        }

        public void SetSex(string sex)
        {
            _sex = sex;
        }

        public void SetAge(int age)
        {
            _age = age;
        }

        public void Dispaly()
        {
            Console.WriteLine($"{_name} {_sex} {_age}");
        }
    }

    //深复制
    class WorkExperiences:ICloneable
    {
        public string WorkDate { get; set; }
        public string WorkPlace { get; set; }

        //浅复制习惯起名为CLone
        public object Clone()
        {
            return MemberwiseClone();
        }
    }
    class ResumeForHeavy
    {
        private WorkExperiences _workExperiences=new WorkExperiences();
        private string _name;
        private string _sex;
        private int _age;

        //深复制习惯起名为Copy
        public ResumeForHeavy Copy()
        {
            //也可以考虑手动复制，即创建一个新的对象，然后将字段一个一个赋值
            WorkExperiences w = (WorkExperiences)_workExperiences.Clone();

            ResumeForHeavy o =(ResumeForHeavy) this.MemberwiseClone();

            o._workExperiences = w;

            return o;
        }

        public void SetWork(string s1,string s2)
        {
            _workExperiences.WorkDate = s1;
            _workExperiences.WorkPlace = s2;

        }
        public void SetName(string name)
        {
            _name = name;
        }

        public void SetSex(string sex)
        {
            _sex = sex;
        }

        public void SetAge(int age)
        {
            _age = age;
        }

        public void Dispaly()
        {
            Console.WriteLine($"{_name} {_sex} {_age}");
            Console.WriteLine($"{_workExperiences.WorkDate} {_workExperiences.WorkPlace}");
        }
    }
}
