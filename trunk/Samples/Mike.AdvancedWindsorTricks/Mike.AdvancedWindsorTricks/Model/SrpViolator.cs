using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class SrpViolator : IThing, IWidget, IWonder
    {
        public string SayHello(string name)
        {
            return string.Format("Hello {0} from SrpViolator", name);
        }

        public double Calculate(double a, double b)
        {
            return Math.Pow(a, b);
        }

        public void DoesNothing()
        {
            Console.WriteLine("Doing nothing");
        }
    }
}