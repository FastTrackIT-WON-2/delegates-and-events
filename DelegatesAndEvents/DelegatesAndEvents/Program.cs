using System;

namespace DelegatesAndEvents
{
    public delegate int Sum3Numbers(int a1, int a2, int a3);

    public delegate T SumTwoValues<T>(T x, T y);

    public delegate void MessageBroadcast(string message);

    class Program
    {
        static void Main(string[] args)
        {
            // (.. lista params ...) => { ... cod .... }

            int factor = 10;
            Sum3Numbers func1 = (a, b, c) => a + b + c + factor;

            int sum = func1(1, 2, 3);

            Console.WriteLine($"Sum={sum}");
        }

        private static void Example_DelegatesPointingToStaticFunctions()
        {
            Sum3Numbers func1 = Sum;

            int sum = func1(1, 2, 3);

            Console.WriteLine($"Sum={sum}");
        }

        private static void Example_DelegatesPointingToInstanceFunctions()
        {
            MathHelper helper = new MathHelper();
            Sum3Numbers func1 = helper.Sum;

            int sum = func1(1, 2, 3);

            Console.WriteLine($"Sum={sum}");
        }

        private static void Example_DelegatesWithAnonimousFunctions()
        {
            Sum3Numbers func1 = delegate (int a, int b, int c)
            {
                return a + b + c;
            };

            int sum = func1(1, 2, 3);

            Console.WriteLine($"Sum={sum}");
        }

        private static void Example_MulticastDelegates()
        {
            MessageReceiver receiver = new MessageReceiver();

            MessageBroadcast broadcaster = null;
            broadcaster += ReceiveMessage;
            broadcaster += receiver.Receive;


            broadcaster("Test message");

            broadcaster -= ReceiveMessage;

            broadcaster("Test message after detach");
        }

        private static void Example_Events()
        {
            MessageBroadcaster broadcaster = new MessageBroadcaster();
            broadcaster.OnMessageReceived += ReceiveMessage;

            broadcaster.Broadcast("Test");
        }


        private static void Example_GenericDelegates()
        {
            SumTwoValues<decimal> func = SumDecimal;
            // or:
            // Func<decimal, decimal, decimal> func = SumDecimal;
            decimal result1 = func(2.5M, 3.1M);
            Console.WriteLine($"Result(1)={result1}");


            SumTwoValues<string> concat = ConcatStrings;
            string result2 = concat("str1", "str2");
            Console.WriteLine($"Result(2)={result2}");
        }

        private static int Sum(int a, int b, int c)
        {
            return a + b + c;
        }

        private static decimal SumDecimal(decimal a, decimal b)
        {
            return a + b;
        }

        private static string ConcatStrings(string a, string b)
        {
            return a + b;
        }

        private static void ReceiveMessage(string message)
        {
            Console.WriteLine($"Program received message: {message}");
        }

        private static void DoSomething(MessageBroadcast broadcaster)
        {
            Console.WriteLine("Doing some processing ...");
            broadcaster("Job finished");
        }
    }
}
