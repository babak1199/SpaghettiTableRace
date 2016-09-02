using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SpaghettiTableRace
{
    public class RoundTable
    {
        public readonly int NumberOfPersons;

        private List<Person> _persons;
        private List<Chopstick> _chopsticks;
        private List<Thread> _threads;
        private List<Result> _results;

        public RoundTable(int numberOfPersons)
        {
            NumberOfPersons = numberOfPersons;
            _threads = new List<Thread>();
            _results = new List<Result>();

            _persons = new List<Person>();
            _chopsticks = new List<Chopstick>();

            // Auto generate persons and chopsticks
            for (int i = 0; i < NumberOfPersons; i++)
            {
                _persons.Add(new Person("Person " + (i + 1).ToString()));
                _chopsticks.Add(new Chopstick("Chopstick " + (i + 1).ToString()));
            }

            ArrangeChopsticksOnTable();

            CreateThreads();
        }

        public RoundTable(int numberOfPersons, IEnumerable<Person> perons,
                                                IEnumerable<Chopstick> chopsticks)
        {
            NumberOfPersons = numberOfPersons;
            _threads = new List<Thread>();
            _results = new List<Result>();

            _persons = perons.ToList();
            _chopsticks = chopsticks.ToList();

            ArrangeChopsticksOnTable();

            CreateThreads();
        }


        /// <summary>
        /// Arranges two chopsticks on the table next to each person
        /// </summary>
        private void ArrangeChopsticksOnTable()
        {
            for (int i = 0; i < NumberOfPersons; i++)
            {
                _persons[i].RightChopstick = _chopsticks[i];
                if (i != _persons.Count - 1)
                    _persons[i].LeftChopstick = _chopsticks[i + 1];
                else
                    _persons[i].LeftChopstick = _chopsticks[0];
            }
        }

        /// <summary>
        /// Creates threads that are meant to race
        /// </summary>
        private void CreateThreads()
        {
            foreach (Person p in _persons)
                _threads.Add(new Thread(new ParameterizedThreadStart(p.EatSpaghetti)));
        }

        /// <summary>
        /// Starts the actual race
        /// </summary>
        /// <param name="wait">if true will wait until all threads finish</param>
        public void StartRace(bool wait = false)
        {
            foreach (Thread t in _threads)
            {
                Result result = new Result();
                _results.Add(result);

                t.Start(result);
            }

            if(wait)
            {
                foreach (Thread t in _threads)
                    t.Join();

                int failed = 0;
                foreach (Result res in _results)
                    if (!res.HasEaten)
                        failed++;

                Console.WriteLine("\n" + (failed == 0 ? "Done. All" : "Only") + " "
                                + (NumberOfPersons - failed) + " persons have eaten.");
            }
        }
    }
}
