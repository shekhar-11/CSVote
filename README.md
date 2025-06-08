# Mini Voting System

A simple console-based voting system built in C# that allows users to register, cast votes for candidates, and view results. The system persists data across sessions by saving user registrations, candidates, votes, and voters to text files.

---

## Features

- Register new users with unique usernames.
- Create and manage candidates.
- Allow registered users to cast a single vote.
- Prevent multiple voting by the same user.
- Display current voting results with the leading candidate.
- Data persistence using text files (`users.txt`, `candidates.txt`, `voters.txt`).

---

## How to Use

1. **Run the Program**

   Compile and run the `MiniVotingSystem` console application.

2. **Main Menu**

   - **Register New User:** Add a new username to participate in voting.
   - **Cast Vote:** Registered users can vote for their preferred candidate.
   - **Show Results:** Display the current vote count and leading candidate.
   - **Exit:** Save all data and close the application.

3. **Candidates Setup**

   On first run, you will be prompted to enter candidate names (type `done` to finish).

---

## Data Files

The program uses three text files to store data persistently:

- `users.txt` — Stores registered usernames.
- `candidates.txt` — Stores candidates and their vote counts (`CandidateName|Votes` format).
- `voters.txt` — Stores usernames of users who have voted.

---

## Code Structure

- **Program.cs** — Contains the main logic for registration, voting, results display, and file operations.
- Uses collections like `List<string>`, `Dictionary<string, int>`, and `HashSet<string>` for managing users, candidates, and voters respectively.

---

## Requirements

- .NET Core SDK or .NET Framework to compile and run the C# console application.

---

## How to Run

1. Clone the repository:

   ```bash
   git clone https://github.com/yourusername/MiniVotingSystem.git
   cd MiniVotingSystem
   ```

2. Run the project:
   ```bash
   dotnet run
   ```
   
