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

            Console.WriteLine($"{data[i].Name} - [{ranks}]"); 
        }
        // take all of the students in each class up to the length of D class, which has 17 students
        // therefore we take the first 17 students from each class
        // to determine the winner we'll take the class with the highest average rank
        var winner = data
            .Select(g => new Student 
            { 
                Name = g.Name, 
                Ranks = g.Ranks.Take(17).ToList() 
            })
            .OrderBy(s => s.Ranks.Average())
            .Last();
        // now that the winner is determined, print out the winning class and average rank of the other classes in descending order
        Console.WriteLine($"\nThe winning class is {winner.Name} with an average rank of {winner.Ranks.Average():F2}.\n");
        Console.WriteLine("\nThis was determined by getting the average rank of the first 17 students. All 4 classes' ranks were determined this way.");
        Console.WriteLine("\nOther classes in order:");
        // get the other classes in order of average rank descending
        var otherClasses = data
            .Where(g => g.Name != winner.Name)
            .Select(g => new Student 
            { 
                Name = g.Name, 
                Ranks = g.Ranks.Take(17).ToList() 
            })
            .OrderByDescending(s => s.Ranks.Average());

        foreach (var s in otherClasses) 
        {             
            Console.WriteLine($"{s.Name} - Average Rank: {s.Ranks.Average():F2}");
        }


    }

    public class Student
    {
        public string Name { get; set; }
        public List<int> Ranks { get; set; }
    }

    
}
