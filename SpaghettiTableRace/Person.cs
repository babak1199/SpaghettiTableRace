using System;
using System.Threading;

namespace SpaghettiTableRace
{
    public class Person
    {
        public string Name { get; protected set; }

        public Chopstick LeftChopstick { get; set; }
        public Chopstick RightChopstick { get; set; }
        
        public Person(string name)
        {
            Name = name;
        }

        public void EatSpaghetti(object state)
        {
            Result result = state as Result;
            TimeSpan timeout = new TimeSpan(0, 0, 5);
            int numberOfRetries = 0;

            do
            {
                if (Monitor.TryEnter(LeftChopstick, timeout))
                {
                    try
                    {
                        Console.WriteLine(Name + " picked " + LeftChopstick.Name);

                        if (Monitor.TryEnter(RightChopstick, timeout))
                        {
                            try
                            {
                                Thread.Sleep(50);

                                Console.WriteLine(Name + " picked " + RightChopstick.Name);

                                Console.WriteLine(Name + " eats.\n");
                                result.HasEaten = true;
                            }
                            finally
                            {
                                Monitor.Exit(RightChopstick);
                                Console.WriteLine(Name + " released " + LeftChopstick.Name);
                            }
                        }
                        else
                        {
                            Console.WriteLine(Name + "'s attempt to pick up " + RightChopstick.Name
                                                                + " was failed due to timeout.");
                            result.HasEaten = false;
                        }
                    }
                    finally
                    {
                        Monitor.Exit(LeftChopstick);
                        Console.WriteLine(Name + " released " + RightChopstick.Name);
                    }
                }
                else
                {
                    Console.WriteLine(Name + "'s attempt to pick up " + LeftChopstick.Name
                                                        + " was failed due to timeout.");
                    result.HasEaten = false;
                }

                numberOfRetries++;

            } while (result.HasEaten == false || numberOfRetries > 5);
        }
    }
}
