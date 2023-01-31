using System;
using System.Collections.Generic;
using MasterMindLibrary;


namespace MasterMind
{
    class Program
    {
        static List<Peg> pegList = new List<Peg>()
        {
            new Peg(ConsoleColor.White, ConsoleColor.Red),
            new Peg(ConsoleColor.White, ConsoleColor.Blue),
            new Peg(ConsoleColor.Black, ConsoleColor.Green),
            new Peg(ConsoleColor.Black, ConsoleColor.Yellow),
            new Peg(ConsoleColor.Black, ConsoleColor.Cyan),
            new Peg(ConsoleColor.White, ConsoleColor.Magenta),
            new Peg(ConsoleColor.White, ConsoleColor.DarkGray),
            new Peg(ConsoleColor.White, ConsoleColor.DarkRed)
        };

        static void Main(string[] args)
        {
            List<Attempt> allAttempts = new List<Attempt>();

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Welcome to Mastermind!");
            Console.ResetColor();

            //ask for difficulty
            Console.WriteLine("Choose difficulty: ");
            int difficulty = MMLib.GetConsoleMenu(new List<string> { "Easy: 4 Colors", "Medium: 6 Colors", "Hard: 8 Colors" });


            //ask for maxTurns of turns to guess it
            int maxTurns = MMLib.GetConsoleInt("How many attempts (1-50)? ", 1, 50);
            //store the maxPegs based on difficulty
            int maxPegs = (difficulty + 1) * 2;
            Attempt attempt = new Attempt();

            //Generate an answer
            List<int> answer = MMLib.GenerateAnswer(maxPegs);

            //show cheat? 
            //for testing
            MMLib.Cheat(answer, pegList);
         

            //loop while !gameWon && maxTurns != 0
            //  get user attempt
            //  Check the attempt for a correct guess
            bool gameWon = false;
            while(!gameWon && maxTurns !=0) {        
              
            //  add the attempt to the attempt list
                Attempt attempt2 = GetAttemptFromUser(maxPegs, allAttempts, maxTurns);
                CheckAttempt(attempt2,answer);
                allAttempts.Add(attempt2);

                //  determin if the game has been won or not
                //  reduce the maxTurns
                if (attempt2.CorrectAnswerCount == maxPegs)
                {
                    gameWon= true;  

                }
                maxTurns--;

            }
            //If won, display Game Won!
            //If lost, show game loss
            //show the correct answer
            if (gameWon)
            {
                Console.WriteLine("congratulations, you win");
            }
            else
            {

                Console.WriteLine("you lose! answer was: ");
                MMLib.ShowAnswer(answer, pegList, "a");
            }         
        }


        static Attempt GetAttemptFromUser(int maxPegs, List<Attempt> allAttempts, int maxTurns)
        {
            //Create a new Attempt
            Attempt attempt = new Attempt();
            //Get color options based on maxPegs
          
            //Loop of # of pegs they need to choose
            for (int i = 0; i < maxPegs; i++)
            {
                //clear console
                Console.Clear();
                //Display # of attempts left
                Console.WriteLine("Atempts left: " + (maxTurns));
                //Show all previous attempts
                MMLib.ShowAttempts(allAttempts,pegList, "a");
                //Show pegs they have chosen already in this attempt
                MMLib.ShowChosenPegs(attempt, pegList);
                //Ask them to pick a peg color from a menu of options

                int userChoice = MMLib.GetConsoleMenu(MMLib.GetColorOptions(maxPegs, pegList))-1;
                //Add the chosen peg to the Attempt.AttemptList
                attempt.AttemptList.Add(userChoice);
                 
            }
            //Return the attempt when done
            return attempt;
        }


        static void CheckAttempt(Attempt attempt, List<int> answer)
        {
            //Check the attempt.AttemptList to see if they got a match to the answer
            //If a peg is correct, increment the attempt.CorrectAnswerCount
            for (int i = 0; i < attempt.AttemptList.Count; i++)
            {
                if (attempt.AttemptList[i] == answer[i])
                {
                    attempt.CorrectAnswerCount++;
                }
            }
        }
    }
}
