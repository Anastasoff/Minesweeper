namespace Minesweeper.Common
{
    using System;
    using System.Threading;

    /// <summary>
    /// The class returns an instance of the Random class
    /// </summary>
    public class RandomGenerator
    {
        private static Random instance;

        /// <summary>
        /// Prevents a default instance of the RandomGenerator class from being created. -> parameterless constructor
        /// </summary>
        private RandomGenerator()
        {
        }

        /// <summary>
        /// Gets an instance of the Random class, implementation of the Singleton Design Pattern
        /// </summary>
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