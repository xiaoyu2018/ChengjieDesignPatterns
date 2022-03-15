using System;
using System.Configuration;
using System.Reflection;

//涉及到多个产品系列的工厂模式就是抽象工厂模式

//一个项目有多张数据库表，而且涉及到不同数据库的迁移

namespace _11抽象工厂模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Two();
        }

        //普通抽象工厂
        static void One()
        {
            //换数据库直接改此处
            IFactory factory1 = new SqlServerFactory();

            //生成数据库表
            User user = new User();
            Department department = new Department();

            //操作数据库
            IUser userOPer = factory1.CreateUserOper();
            IDepartment departmentOper = factory1.CreateDepOper();

            userOPer.Get(1);
            userOPer.Insert(user);

            departmentOper.Get(2);
            departmentOper.Insert(department);
        }

        //应用反射实现抽象工厂
        static void Two()
        {
            //生成数据库表
            User user = new User();
            Department department = new Department();

            IUser useroper = DataAccess.CreateUser();
            IDepartment departmentoper = DataAccess.CreateDepartment();

            useroper.Get(12);
            departmentoper.Get(33);

            useroper.Insert(user);
            departmentoper.Insert(department);


        }
    }

    //数据库表
    class User
    {
        public int ID { get; set; }
        public string UserName { get; set; }

    }

    class Department
    {
        public int ID { get; set; }
        public string DepName { get; set; }

    }

    //数据库表操作
    interface IDepartment
    {
        void Insert(Department dep);
        Department Get(int id);
    }
    interface IUser
    {
        void Insert(User user);
        User Get(int id);
    }

    //产品线1的产品

    class SqlServerDepartment : IDepartment
    {
        public void Insert(Department dep)
        {
            Console.WriteLine("SqlServer插入了一条部门数据到部门表");
        }

        Department IDepartment.Get(int id)
        {
            Console.WriteLine($"SqlServer查询ID为{id}的部门");
            return null;
        }
    }
    class AccessDepartment : IDepartment
    {
        public void Insert(Department dep)
        {
            Console.WriteLine("Access插入了一条部门数据到部门表");
        }

        Department IDepartment.Get(int id)
        {
            Console.WriteLine($"Access查询ID为{id}的部门");
            return null;
        }
    }

    //产品线2的产品
    class SqlServerUser : IUser
    {
        public User Get(int id)
        {
            Console.WriteLine($"SqlServer查询ID为{id}的用户");
            return null;
        }

        public void Insert(User user)
        {
            Console.WriteLine("SqlServer插入了一条用户数据到用户表");
        }
    }
    class AccessUser : IUser
    {
        public User Get(int id)
        {
            Console.WriteLine($"Access查询ID为{id}的用户");
            return null;
        }

        public void Insert(User user)
        {
            Console.WriteLine("Access插入了一条用户数据到用户表");
        }
    }

    //普通抽象工厂
    interface IFactory
    {
        IDepartment CreateDepOper();
        IUser CreateUserOper();

    }

    class SqlServerFactory : IFactory
    {
        //产品线1
        public IDepartment CreateDepOper()
        {
            return new SqlServerDepartment();
        }
        //产品线2
        public IUser CreateUserOper()
        {
            return new SqlServerUser();
        }
    }
    class AccessFactory : IFactory
    {
        //产品线1
        public IDepartment CreateDepOper()
        {
            return new AccessDepartment();
        }
        //产品线2
        public IUser CreateUserOper()
        {
            return new AccessUser();
        }
    }

    //应用反射实现抽象工厂
    //此处原本应该用简单工厂模式，使用反射则去除了简单工厂最具劣势的switch/if部分
    //一个类替代了普通抽象工厂的多个类，并用反射去除了分支语句
    class DataAccess
    {
        private static readonly string AssemlyName = "11抽象工厂模式";
        //更改数据库只需修改此处
        //private static readonly string db = "Access";
        //进一步可以使用配置文件设置数据库，连字符串db都不需要修改，实现真正的开放-封闭原则
        //配置文件名默认为App.config，自己加的文件要符合这个名字
        private static readonly string db = ConfigurationManager.AppSettings["db"];
        private static string className;
        public static IUser CreateUser()
        {
            className = "_" + AssemlyName + "." + db+"User";
            //使用元数据创建实例
            return (IUser)Assembly.Load(AssemlyName).CreateInstance(className);
        }

        public static IDepartment CreateDepartment()
        {
            className = "_" + AssemlyName + "." + db + "Department";
            return (IDepartment)Assembly.Load(AssemlyName).CreateInstance(className);
        }

        //若有新的数据库也不需要修改此类，只需在产品部分新增加新数据的类
    }

    

}
