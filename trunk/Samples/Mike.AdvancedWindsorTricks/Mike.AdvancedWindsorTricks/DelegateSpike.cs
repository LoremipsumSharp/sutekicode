using System;
using System.Reflection;
using Mike.AdvancedWindsorTricks.Model;

namespace Mike.AdvancedWindsorTricks
{
    public class DelegateSpike
    {
        public void Reflecting_delegates()
        {
            Func<Func<int, int, int>, int, int, int> applier = (f, x, y) => f(x, y) * 2;
            Func<int, int, int> multiplier = (a, b) => a*b;

            var curried = Functional.Curry(applier);
            var curriedCombined = curried(multiplier);
            var combined = Functional.DeCurry(curriedCombined);

            Console.WriteLine("combined(4) = {0}", combined(4, 4));

            OutputDelegateDetails(applier);
        }

        public void OutputDelegateDetails(Delegate @delegate)
        {
            var type = @delegate.GetType();
            Console.Out.WriteLine("Is a delegate {0}", typeof(MulticastDelegate).IsAssignableFrom(type.BaseType));

            Console.Out.WriteLine("@delegate.GetType() = {0}", @delegate.GetType());
            Console.Out.WriteLine("@delegate.Method.Name = {0}", @delegate.Method.Name);

            var target = @delegate.Target;
            Console.Out.WriteLine("target.GetType() = {0}", target.GetType());

            foreach (var member in target.GetType().GetMembers())
            {
                Console.Out.WriteLine("\tmember.MemberType = {0}", member.MemberType);
                Console.Out.WriteLine("\tmember.Name = {0}", member.Name);
                if (member.MemberType == MemberTypes.Method)
                {
                    var method = member as MethodInfo;
                    foreach (var parameter in method.GetParameters())
                    {
                        Console.Out.WriteLine("\t\tparameter.ParameterType.Name = {0}", parameter.ParameterType.Name);
                        Console.Out.WriteLine("\t\tparameter.Name = {0}", parameter.Name);
                    }
                }
            }

            Console.Out.WriteLine("@delegate.Method.Name = {0}", @delegate.Method.Name);
        }

        

        public void How_do_you_know_if_a_type_is_a_delegate_type()
        {
            Console.WriteLine("string    {0}", IsDelegate(typeof(string)));
            Console.WriteLine("int       {0}", IsDelegate(typeof(int)));
            Console.WriteLine("Func<int> {0}", IsDelegate(typeof(Func<int>)));
            Console.WriteLine("Test      {0}", IsDelegate(typeof(Test)));
        }

        public bool IsDelegate(Type type)
        {
            return typeof (MulticastDelegate).IsAssignableFrom(type.BaseType);
        }

        public void PrintDelegate(Type type)
        {
            if (!IsDelegate(type))
            {
                Console.WriteLine("Not a delegate");
                return;
            }

            // how do you get the arguments and return type of the delegate?
        }

        public void bah()
        {
            Func<Func<int, int, int>, Func<int, Func<int, int>>> curried = f => x => y => f(x, y)*2;

            Func<int, Func<int, int>> curriedCombined = x => y => (x*y)*2;
        }
    }

    public delegate int Test(int a, int b);
}