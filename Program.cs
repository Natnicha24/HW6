using System;

namespace HW6
{
    class Program
    {
        enum Difficulty
        {
            Easy,
            Normal,
            Hard
        }

        struct Problem
        {
            public string Message;
            public int Answer;

            public Problem(string message, int answer)
            {
                Message = message;
                Answer = answer;
            }
        }

        static Problem[] GenerateRandomProblems(int numProblem)
        {
            Problem[] randomProblems = new Problem[numProblem];

            Random rnd = new Random();
            int x, y;

            for (int i = 0; i < numProblem; i++)
            {
                x = rnd.Next(50);
                y = rnd.Next(50);
                if (rnd.NextDouble() >= 0.5) 
                    randomProblems[i] =
                    new Problem(String.Format("{0} + {1} = ?", x, y), x + y);
                  
                else 
                    randomProblems[i] =
                    new Problem(String.Format("{0} - {1} = ?", x, y), x - y);
            }
            
            return randomProblems;
        }

        static void Menu(ref double score, int Level)//หน้าเมนู
        {
            double collectscore = score;
            Console.WriteLine("Score:{0}, Difficulty: {1}", score, (Difficulty)Level);
        }

        static void Playgame(int Level,ref double score)//หน้าเล่นเกม
        {
            int numProblems=0;
            double Qc = 0;
            double time=0;

            if(Level==0)
            {
                numProblems = 3;
            }
            else if(Level==1)
            {
                numProblems = 5;
            }
            else if (Level==2)
            {
                numProblems = 7;
            }

            Problem[] randomProblem;
            randomProblem = GenerateRandomProblems(numProblems);
            long starttime = DateTimeOffset.Now.ToUnixTimeSeconds();
            int answer;
           
            for (int i = 0; i < numProblems; i++)
            {
                Console.WriteLine("{0}",randomProblem[i].Message);
                answer = int.Parse(Console.ReadLine());
                if (answer == randomProblem[i].Answer)
                {
                    Qc++;
                }
            }

            long endtime = DateTimeOffset.Now.ToUnixTimeSeconds();
            time = endtime - starttime;//คิดเวลา
            score =score+((Qc / numProblems) * ((25 - (Math.Pow(Level, 2)))/Math.Max(time, 25 - (Math.Pow(Level, 2)))) * Math.Pow((2 * Level+1), 2));//คิดคะแนน
            Menu(ref score, Level);
        }
        static int setting(int Level)//หน้าตั้งค่า
        {
            int Levelsetting;

            do
            {
                Levelsetting = int.Parse(Console.ReadLine());
                if (Levelsetting != 0 && Levelsetting != 1 && Levelsetting != 2)
                {
                    Console.WriteLine("Please input 0 - 2.");
                }
            } while (Levelsetting != 0 && Levelsetting != 1 && Levelsetting != 2);

            return Levelsetting;
        }
        
        static void Main(string[] args)
        {
          
            double score=0;
            int Level=0, page;
            Menu(ref score, Level);
            do
            {
                do
                {
                page = int.Parse(Console.ReadLine());
               
                    if (page == 0)
                    {
                        Playgame(Level,ref score);
                    }
                    else if (page == 1)
                    {
                        Menu(ref score, Level);
                        Level = setting(Level);
                        Menu(ref score, Level);

                    }
                    else if (page == 2)
                    {

                    }
                    else
                    {
                        Console.WriteLine("Please input 0 - 2.");
                    }
                } while (page != 0 && page != 1 && page != 2);
            } while (page != 2);
        }
    }
}
