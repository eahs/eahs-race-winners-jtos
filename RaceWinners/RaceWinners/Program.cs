using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RaceWinners;

public class Program
{
    static async Task Main(string[] args)
    {
        DataService ds = new DataService();

        // Asynchronously retrieve the group (class) data
        var data = await ds.GetGroupRanksAsync();

        for (int i = 0; i < data.Count; i++)
        {
            // Combine the ranks to print as a list
            var ranks = String.Join(", ", data[i].Ranks);

            Console.WriteLine($"{data[i].Name} - [{ranks}]"); //take all of the students in each class up to the length of D class
        }
        // Orders by the number of first-place finishes (descending) and then by name (ascending)
        // take whichever class has the most high placing students
        var winner = data
            .OrderByDescending(x => x.Ranks.Count(r => r == 1))
            .ThenBy(x => x.Name)
            .First();

        Console.WriteLine($"\n Winner : {winner.Name}");

        public class Student
    {
        public string Name { get; set; }
        public List<int> Ranks { get; set; }
    }

    // Simulated "DataService" class
    public class DataService
    {
        public async Task<List<Student>> GetGroupRanksAsync()
        {
            // Simulate async operation
            await Task.Delay(100);

            // Return mock data
            return new List<Student>
            {
                new Student { Name = "Alice", Ranks = new List<int> { 1, 2, 1 } },
                new Student { Name = "Bob", Ranks = new List<int> { 2, 1, 3 } },
                new Student { Name = "Charlie", Ranks = new List<int> { 3, 3, 2 } }
            }
        }
    }
}
