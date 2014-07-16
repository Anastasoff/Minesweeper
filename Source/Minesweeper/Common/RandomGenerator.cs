namespace Minesweeper.Common
{
    using System;
    using System.Threading;

    public class RandomGenerator
    {
        private static Random instance;

        private RandomGenerator()
        {
        }

        public static Random GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Random(unchecked((Environment.TickCount * 31) + Thread.CurrentThread.ManagedThreadId));
                }

                return instance;
            }
        }
    }
}