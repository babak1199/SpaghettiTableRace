using System;

namespace SpaghettiTableRace
{
    public class Program
    {
        public const int NUMBER_OF_PERSONS = 2;

        public static void Main(string[] args)
        {
            // Creates a table with the specified number of persons
            // plus create the persons and chopsticks
            RoundTable table = new RoundTable(NUMBER_OF_PERSONS);


            // Starts threads and waif for all of them to exit
            table.StartRace(true);

            Console.ReadLine();
        }
    }
}
