using System;
using System.Collections.Generic;

//将对象组合成树形结构以表示‘部分-整体’，的层次结构。组合模式是的用户对单个对象和组合对象的使用具有一致性。
//需求中体现部分和整体的层次结构时，或希望用户可一忽略组合对象与单个对象的不同，统一的使用组合结构的所有对象时，考虑使用组合模式。
//组合模式使用面向对象思想指导建立树形结构！


//小菜接到了一个需求，要求实现总公司和各地分公司的管理系统，分公司和总公司的需求都一致，但理应体现出层次结构

namespace _15组合模式
{
    class Program
    {
        static void Main(string[] args)
        {

            ConcreateCompany root = new ConcreateCompany("中国建设银行总行");
            root.Add(new Department("总行人力资源部", "员工招聘及培训"));
            root.Add(new Department("总行财务部", "公司财务管理"));
            root.Add(new Department("总行安保部", "公司安全保障"));

            ConcreateCompany comp1 = new ConcreateCompany("建设银行北京东方广场支行");
            comp1.Add(new Department("东方财务部", "公司财务管理"));
            comp1.Add(new Department("东方安保部", "公司安全保障"));

            ConcreateCompany comp2 = new ConcreateCompany("建设银行盘锦鹤乡支行");
            comp2.Add(new Department("鹤乡财务部", "公司财务管理"));

            root.Add(comp1);
            root.Add(comp2);
            root.ShowStructure(1);
            root.ShowDuty();
            comp2.ShowDuty();
        }
    }

    abstract class Company
    {
        protected string name;

        public Company(string name)
        {
            this.name = name;
        }

        public abstract void Add(Company company);
        public abstract void Remove(Company company);
        public abstract void ShowStructure(int depth);
        public abstract void ShowDuty();

    }

    class ConcreateCompany : Company
    {

        private List<Company> children = new List<Company>();
       

        public ConcreateCompany(string name):base(name)
        {

        }
        public override void Add(Company company)
        {
            children.Add(company);
        }

        public override void Remove(Company company)
        {
            children.Remove(company);
        }

        public override void ShowDuty()
        {
            Console.WriteLine($"{name}具有职能：\n");
            foreach (var i in children)
            {
                i.ShowDuty();
            }
        }

        public override void ShowStructure(int depth)
        {
            Console.WriteLine($"{new String('-',depth)}{name}");

            foreach (var i in children)
            {
                i.ShowStructure(depth+2);
            }
        }
    }

    class Department : Company
    {
        private string _duty;
        public Department(string name,string duty) : base(name)
        {
            _duty = duty;
        }
        public override void Add(Company company)
        {
            Console.WriteLine("无法为部门添加子公司");
        }

        public override void Remove(Company company)
        {
            Console.WriteLine("无法为部门移除子公司");
        }

        public override void ShowDuty()
        {
            Console.WriteLine($"{name}：{_duty}");
        }

        public override void ShowStructure(int depth)
        {
            Console.WriteLine($"{new String('-', depth)}{name}");
        }
    }
}
