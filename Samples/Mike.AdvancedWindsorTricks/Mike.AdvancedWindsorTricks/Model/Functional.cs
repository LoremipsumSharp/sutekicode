using System;

namespace Mike.AdvancedWindsorTricks.Model
{
    public class Functional
    {
        public static Func<T1, Func<T2, T3>> Curry<T1, T2, T3>(Func<T1, T2, T3> function)
        {
            return a => b => function(a, b);
        }

        public static Func<T1, Func<T2, Func<T3, T4>>> Curry<T1, T2, T3, T4>(Func<T1, T2, T3, T4> function)
        {
            return a => b => c => function(a, b, c);
        }

        public static Func<T1, Func<T2, Func<T3, Func<T4, T5>>>> Curry<T1, T2, T3, T4, T5>(Func<T1, T2, T3, T4, T5> function)
        {
            return a => b => c => d => function(a, b, c, d);
        }

        public static Func<T1, T2, T3> DeCurry<T1, T2, T3>(Func<T1, Func<T2, T3>> function)
        {
            return (a, b) => function(a)(b);
        }

        public static Func<T1, T2, T3, T4> DeCurry<T1, T2, T3, T4>(Func<T1, Func<T2, Func<T3, T4>>> function)
        {
            return (a, b, c) => function(a)(b)(c);
        }

        public static Func<T1, T2, T3, T4, T5> DeCurry<T1, T2, T3, T4, T5>(Func<T1, Func<T2, Func<T3, Func<T4, T5>>>> function)
        {
            return (a, b, c, d) => function(a)(b)(c)(d);
        }
    }
}