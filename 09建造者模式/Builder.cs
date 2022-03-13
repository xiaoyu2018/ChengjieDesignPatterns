

namespace _09建造者模式
{
    //将一个复杂对象的构建与它的表示分离，使得同样的构建过程可以创建不同的表示
    //定义好一个建造规范，可以源源不断地补充具体建造细节

    //创建一个小人，这个小人肯定有一个脑袋一个身体两手两脚，但其胖瘦可能不同


    //抽象建造类
    abstract class PersonBUilder
    {
        protected Graphics graphics;
        protected Pen pen;

        public PersonBUilder(Graphics graphics,Pen pen)
        {
            this.pen = pen;
            this.graphics = graphics;

        }

        public abstract void BuildHead();
        public abstract void BuilBody();
        public abstract void BuildArmLeft();
        public abstract void BuildArmRight();
        public abstract void BuildLegLeft();
        public abstract void BuildLegRight();
    }

    //具体建造类
    class ThinPersonBuilder : PersonBUilder
    {

        public ThinPersonBuilder(Graphics graphics, Pen pen) : base(graphics, pen)
        {
            graphics.DrawEllipse(pen, 50, 20, 30, 30);
        }
        public override void BuilBody()
        {

            graphics.DrawRectangle(pen, 60, 50, 10, 50);
        }

        public override void BuildArmLeft()
        {
            graphics.DrawLine(pen, 60, 50, 40, 100);
        }

        public override void BuildArmRight()
        {
            graphics.DrawLine(pen, 60, 50, 40, 100);
        }

        public override void BuildHead()
        {
            graphics.DrawEllipse(pen, 50, 20, 30, 30);
        }

        public override void BuildLegLeft()
        {
            graphics.DrawLine(pen, 60, 1000, 450, 150);
        }

        public override void BuildLegRight()
        {
            graphics.DrawLine(pen, 60, 1000, 450, 150);
        }

    }
    class FatPersonBuilder : PersonBUilder
    {

        public FatPersonBuilder(Graphics graphics, Pen pen) : base(graphics, pen)
        {
            graphics.DrawEllipse(pen, 50, 20, 30, 30);
        }
        public override void BuilBody()
        {

            graphics.DrawEllipse(pen, 45, 50, 40, 50);
        }

        public override void BuildArmLeft()
        {
            graphics.DrawLine(pen, 60, 50, 40, 100);
        }

        public override void BuildArmRight()
        {
            graphics.DrawLine(pen, 60, 50, 40, 100);
        }

        public override void BuildHead()
        {
            graphics.DrawEllipse(pen, 50, 20, 30, 30);
        }

        public override void BuildLegLeft()
        {
            graphics.DrawLine(pen, 60, 1000, 450, 150);
        }

        public override void BuildLegRight()
        {
            graphics.DrawLine(pen, 60, 1000, 450, 150);
        }
    }

    //指挥者类
    //用于控制建造过程，隔离用户和建造的关联
    class BuildDirector
    {
        PersonBUilder _personBUilder;
        public BuildDirector(PersonBUilder PersonBUilder)
        {
            _personBUilder = PersonBUilder;
        }

        public void Build()
        {
            _personBUilder.BuildHead();
            _personBUilder.BuilBody();
            _personBUilder.BuildArmLeft();
            _personBUilder.BuildArmRight();
            _personBUilder.BuildLegLeft();
            _personBUilder.BuildArmRight();
        }
    }
}
