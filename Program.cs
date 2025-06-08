using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MiniVotingSystem
{
    class Program
    {
        // File paths
        static string usersFile = "users.txt";
        static string candidatesFile = "candidates.txt";
        static string votersFile = "voters.txt";

        // Lists and dictionaries to hold data
        static List<string> registeredUsers = new List<string>();
        static Dictionary<string, int> candidates = new Dictionary<string, int>();
        static HashSet<string> voters = new HashSet<string>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Mini Voting System\n");

            // Load saved data if available
            LoadData();

            // If no candidates loaded from file, create new
            if (candidates.Count == 0)
            {
                CreateCandidates();
                SaveData(); // Save initial candidates
            }

            while (true)
            {
                Console.WriteLine("\n1. Register New User");
                Console.WriteLine("2. Cast Vote");
                Console.WriteLine("3. Show Results");
                Console.WriteLine("4. Exit");
                Console.Write("Select an option: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        RegisterUser();
                        SaveData(); // Save users after registration
                        break;
                    case "2":
                        CastVote();
                        SaveData(); // Save votes and voters after voting
                        break;
                    case "3":
                        ShowResults();
                        break;
                    case "4":
                        Console.WriteLine("Thank you for using the Mini Voting System!");
                        SaveData(); // Save all data before exit
                        return;
                    default:
                        Console.WriteLine("Invalid option. Try again.");
                        break;
                }
            }
        }

        static void CreateCandidates()
        {
            Console.WriteLine("Enter candidate names (type 'done' to finish):");
            while (true)
            {
                Console.Write("Candidate: ");
                string name = Console.ReadLine()?.Trim();

                if (string.Equals(name, "done", StringComparison.OrdinalIgnoreCase))
                    break;

                if (!string.IsNullOrEmpty(name) && !candidates.ContainsKey(name))
                {
                    candidates[name] = 0;
                }
                else
                {
                    Console.WriteLine("Invalid or duplicate name.");
                }
            }
        }

        static void RegisterUser()
        {
            Console.Write("Enter new username: ");
            string username = Console.ReadLine()?.Trim().ToLower();

            if (string.IsNullOrEmpty(username))
            {
                Console.WriteLine("Username cannot be empty.");
                return;
            }

            if (registeredUsers.Contains(username))
            {
                Console.WriteLine("Username already exists.");
            }
            else
            {
                registeredUsers.Add(username);
                Console.WriteLine("User registered successfully.");
            }
        }

        static void CastVote()
        {
            Console.Write("Enter your username: ");
            string username = Console.ReadLine()?.Trim().ToLower();

            if (!registeredUsers.Contains(username))
            {
                Console.WriteLine("You are not a registered user.");
                return;
            }

            if (voters.Contains(username))
            {
                Console.WriteLine("You have already voted.");
                return;
            }

            Console.WriteLine("\nCandidates:");
            foreach (var candidate in candidates.Keys)
            {
                Console.WriteLine("- " + candidate);
            }

            Console.Write("Enter your vote: ");
            string vote = Console.ReadLine()?.Trim();

            if (candidates.ContainsKey(vote))
            {
                candidates[vote]++;
                voters.Add(username);
                Console.WriteLine("Vote cast successfully.");
            }
            else
            {
                Console.WriteLine("Invalid candidate. Vote not counted.");
            }
        }

        static void ShowResults()
        {
            Console.WriteLine("\nCurrent Results:");
            foreach (var kvp in candidates.OrderByDescending(c => c.Value))
            {
                Console.WriteLine($"{kvp.Key}: {kvp.Value} votes");
            }

            if (candidates.Count > 0 && candidates.Any(c => c.Value > 0))
            {
                var top = candidates.OrderByDescending(c => c.Value).First();
                Console.WriteLine($"\nLeading Candidate: {top.Key} with {top.Value} votes");
            }
        }

        static void LoadData()
        {
            if (File.Exists(usersFile))
            {
                registeredUsers = File.ReadAllLines(usersFile).ToList();
            }

            if (File.Exists(candidatesFile))
            {
                var lines = File.ReadAllLines(candidatesFile);
                candidates.Clear();
                foreach (var line in lines)
                {
                    var parts = line.Split('|');
                    if (parts.Length == 2 && int.TryParse(parts[1], out int votes))
                        candidates[parts[0]] = votes;
                }
            }

            if (File.Exists(votersFile))
            {
                voters = File.ReadAllLines(votersFile).ToHashSet();
            }
        }

        static void SaveData()
        {
            File.WriteAllLines(usersFile, registeredUsers);
            File.WriteAllLines(candidatesFile, candidates.Select(c => $"{c.Key}|{c.Value}"));
            File.WriteAllLines(votersFile, voters);
        }
    }
}
