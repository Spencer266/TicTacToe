using System;

namespace TicTacToe
{
    class Program
    {
        public static char[,] table = new char[3, 3];
        public static int playerTurn;


        public static void GameInput()
        {
            Retry:
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Player {0} choose your field: ", playerTurn);
            Console.ForegroundColor = ConsoleColor.Green;
            if (int.TryParse(Console.ReadLine(), out int field))
            {
                if (!(field >= 1 && field <= 9))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("---> Out of field!");
                    goto Retry;
                }
                else
                {
                    int Row = field % 3 == 0 ? (field / 3 - 1) : (field / 3);
                    int Col = field % 3 == 0 ? 2 : (field % 3 - 1);
                    if (!char.IsDigit(table[Row, Col]))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("---> This field is already marked!");
                        goto Retry;
                    }
                    else table[Row, Col] = playerTurn == 1 ? 'X' : 'O';
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("---> Please enter a proper field number!");
                goto Retry;
            }
        }

        public static void GameOutput()
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Clear();
            Console.WriteLine("__________________");
            Console.WriteLine("|     |     |     |");
            Console.WriteLine("|  {0}  |  {1}  |  {2}  |", char.IsDigit(table[0, 0]) ? ' ' : table[0, 0], char.IsDigit(table[0, 1]) ? ' ' : table[0, 1], char.IsDigit(table[0, 2]) ? ' ' : table[0, 2]);
            Console.WriteLine("|_____|_____|_____|");
            Console.WriteLine("|     |     |     |");
            Console.WriteLine("|  {0}  |  {1}  |  {2}  |", char.IsDigit(table[1, 0]) ? ' ' : table[1, 0], char.IsDigit(table[1, 1]) ? ' ' : table[1, 1], char.IsDigit(table[1, 2]) ? ' ' : table[1, 2]);
            Console.WriteLine("|_____|_____|_____|");
            Console.WriteLine("|     |     |     |");
            Console.WriteLine("|  {0}  |  {1}  |  {2}  |", char.IsDigit(table[2, 0]) ? ' ' : table[2, 0], char.IsDigit(table[2, 1]) ? ' ' : table[2, 1], char.IsDigit(table[2, 2]) ? ' ' : table[2, 2]);
            Console.WriteLine("|_____|_____|_____|");
            Console.WriteLine("");
        }

        public static int FindWinner()
        {
            //Rows check
            for (int i = 0; i < 3; i++)
            {
                bool check = true;
                for (int j = 1; j < 3; j++)
                {
                    if (table[i, j - 1] != table[i, j])
                    {
                        check = false;
                        break;
                    }
                }
                if (check) return playerTurn;
            }

            //Column check
            for (int i = 0; i < 3; i++)
            {
                bool check = true;
                for (int j = 1; j < 3; j++)
                {
                    if (table[j - 1, i] != table[j, i])
                    {
                        check = false;
                        break;
                    }
                }
                if (check) return playerTurn;
            }

            //Left diagonal check
            for (int i = 1; i < 3; i++)
            {
                if (table[i, i] != table[i - 1, i - 1])
                {
                    break;
                }
                if (i == 2) return playerTurn;
            }

            //Right diagonal check
            for (int i = 0; i < 2; i++)
            {
                if (table[2 - i, i] != table[2 - i - 1, i + 1])
                {
                    break;
                }
                if (i == 1) return playerTurn;
            }

            //check for a draw
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++) if (char.IsDigit(table[i, j])) return 0;
            }
            return 3;
        }

        public static void Initialize()
        {
            table = new char[3, 3]
            {
                {'1', '2', '3' },
                {'4', '5', '6' },
                {'7', '8', '9' }
            };
            playerTurn = 1;
        }

        public static void DeclareWinner(int winnerIdentifier)
        {
            if (winnerIdentifier != 3)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("Player {0} won the game!", playerTurn == 1 ? 2 : 1);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Game Draw!!! No winner");
            }
        }

        //The method that mostly controls the whole game: the game loop
        public static void GameLoop() 
        {
            int check;
            do
            {
                GameOutput();
                GameInput();
                playerTurn = playerTurn == 1 ? 2 : 1;
                check = FindWinner();
            } while (check == 0);
            GameOutput();

            DeclareWinner(check);

        }
        static void Main(string[] args)
        {
            StartPoint://flag to reset the game process

            //Game process
            Initialize();
            GameLoop();

            //Decide to keep playing or quit the game
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write("Enter 1 to restart the game, 2 to end the game: ");
            switch (Console.ReadLine())
            {
                case "1":
                    goto StartPoint;                    
                case "2":
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Unknown option! End the game by default.");
                    break;
            }
        }
    }
}
