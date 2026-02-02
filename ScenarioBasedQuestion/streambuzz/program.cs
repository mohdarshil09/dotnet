using System;
using System.Collections.Generic;

/// <summary>
/// This class holds information about a single creator.
/// We store the creator's name and their weekly like counts (for 4 weeks).
/// </summary>
public class CreatorStats
{
    // Name of the creator (for example: "Nicky")
    public string CreatorName { get; set; }

    // Array that holds likes for each week (Week 1 to Week 4)
    public double[] WeeklyLikes { get; set; }

    // This is a common list shared by the whole application.
    // Every time we register a creator, we add one CreatorStats object into this list.
    public static List<CreatorStats> EngagementBoard = new List<CreatorStats>();
}

/// <summary>
/// This class contains the main logic for StreamBuzz.
/// It can register creators, find top posts, and calculate average likes.
/// </summary>
public class Program
{
    /// <summary>
    /// Adds a new creator record into the shared EngagementBoard list.
    /// </summary>
    public void RegisterCreator(CreatorStats record)
    {
        // Just add the given record to the global list
        CreatorStats.EngagementBoard.Add(record);
    }

    /// <summary>
    /// For each creator, count how many weeks have likes >= likeThreshold.
    /// Return a dictionary where:
    ///   key   = creator name
    ///   value = number of weeks meeting or exceeding the threshold
    /// If no creator has any week above or equal to the threshold, return an empty dictionary.
    /// </summary>
    public Dictionary<string, int> GetTopPostCounts(List<CreatorStats> records, double likeThreshold)
    {
        Dictionary<string, int> result = new Dictionary<string, int>();

        // Go through each creator
        foreach (CreatorStats creator in records)
        {
            int count = 0;

            // Go through each week's likes
            foreach (double likes in creator.WeeklyLikes)
            {
                if (likes >= likeThreshold)
                {
                    count++;
                }
            }

            // Only add to the dictionary if there is at least one matching week
            if (count > 0)
            {
                result[creator.CreatorName] = count;
            }
        }

        return result;
    }

    /// <summary>
    /// Calculate overall average likes across all creators and all weeks.
    /// Example:
    ///   If there are 2 creators and each has 4 weeks of likes, we average over 8 numbers.
    /// </summary>
    public double CalculateAverageLikes()
    {
        double totalLikes = 0;
        int totalWeeks = 0;

        // Sum all likes and count how many entries we have
        foreach (CreatorStats creator in CreatorStats.EngagementBoard)
        {
            foreach (double likes in creator.WeeklyLikes)
            {
                totalLikes += likes;
                totalWeeks++;
            }
        }

        // If there are no creators registered yet, avoid division by zero
        if (totalWeeks == 0)
        {
            return 0;
        }

        return totalLikes / totalWeeks;
    }

    public static void Main(string[] args)
    {
        Program app = new Program();

        while (true)
        {
            // Simple menu for the user
            Console.WriteLine("1. Register Creator");
            Console.WriteLine("2. Show Top Posts");
            Console.WriteLine("3. Calculate Average Likes");
            Console.WriteLine("4. Exit");
            Console.WriteLine("Enter your choice:");

            string choiceInput = Console.ReadLine();
            int choice;

            // Try to convert choice to a number.
            // If the user types something invalid, we show a message and continue.
            if (!int.TryParse(choiceInput, out choice))
            {
                Console.WriteLine("Please enter a valid number choice.");
                continue;
            }

            if (choice == 1)
            {
                // Register a new creator
                Console.WriteLine("Enter Creator Name:");
                string name = Console.ReadLine();

                // We expect 4 weeks of likes
                double[] weeks = new double[4];

                Console.WriteLine("Enter weekly likes (Week 1 to 4):");
                for (int i = 0; i < 4; i++)
                {
                    // Read each week's likes as double.
                    // We assume the user enters valid numbers as per the problem statement.
                    weeks[i] = Convert.ToDouble(Console.ReadLine());
                }

                CreatorStats creator = new CreatorStats
                {
                    CreatorName = name,
                    WeeklyLikes = weeks
                };

                app.RegisterCreator(creator);
                Console.WriteLine("Creator registered successfully");
                Console.WriteLine(); // blank line for readability
            }
            else if (choice == 2)
            {
                // Show top posts based on a like threshold
                Console.WriteLine("Enter like threshold:");
                double threshold = Convert.ToDouble(Console.ReadLine());

                Dictionary<string, int> topCounts =
                    app.GetTopPostCounts(CreatorStats.EngagementBoard, threshold);

                if (topCounts.Count == 0)
                {
                    Console.WriteLine("No top-performing posts this week");
                }
                else
                {
                    // Print each creator and their count, one per line
                    foreach (var item in topCounts)
                    {
                        Console.WriteLine(item.Key + " - " + item.Value);
                    }
                }

                Console.WriteLine(); // blank line for readability
            }
            else if (choice == 3)
            {
                // Calculate and show overall average likes
                double average = app.CalculateAverageLikes();
                Console.WriteLine("Overall average weekly likes: " + average);
                Console.WriteLine(); // blank line for readability
            }
            else if (choice == 4)
            {
                // Exit message, then break out of the loop to end the program
                Console.WriteLine("Logging off - Keep Creating with StreamBuzz!");
                break;
            }
            else
            {
                // Handle out-of-range menu choices
                Console.WriteLine("Please choose an option between 1 and 4.");
                Console.WriteLine();
            }
        }
    }
}
