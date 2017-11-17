/*
 November 17, 2017
   _____             _        
  / ____|           | |       
 | (___  _ __   __ _| | _____ 
  \___ \| '_ \ / _` | |/ / _ \
  ____) | | | | (_| |   <  __/
 |_____/|_| |_|\__,_|_|\_\___|
                              
          Made by Paskal Paskalev
       -------------------------------
       https://github.com/paskalev2001
 */

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SnakeGame
{
    struct Position
    {
        public int row;
        public int col;
        public Position(int x, int y)
        {
            this.row = x;
            this.col = y;
        }
    }

    class Program
    {
        public static bool IsSoundOn = true;
        public static SystemSound Beep { get; }
        static void Main(string[] args)
        {

            Console.Write("Enter snake speed (x25 ms) or leave blank for (4):");
            int speed;
            string SpeedInput = Console.ReadLine();
            if (SpeedInput != null)
            {
                speed = 100;
            }
            else
            {
                speed = int.Parse(SpeedInput);
            }
            string sinp = "";
            while (sinp != "y" && sinp != "n") {
                Console.Clear();
                Console.Write("Enable sounds? (y/n) _");
                sinp = Console.ReadLine().ToLower();
                if (sinp == "y")
                {
                    IsSoundOn = true;
                }
                if (sinp == "n")
                {
                    IsSoundOn = false;
                }
            }
            Position[] directions = new Position[]
            {
                new Position(0,1), //right
                new Position(0,-1), //left
                new Position(1,0), //down
                new Position(-1,0) //up
            };
            int direction = 0; //0
            Random randomNumGen = new Random();
            Console.BufferHeight = Console.WindowHeight;
            Position food = new Position(randomNumGen.Next(1,Console.WindowHeight), randomNumGen.Next(0, Console.WindowWidth));
            Console.SetCursorPosition(food.col,food.row);
            Console.Write("@");

            Queue<Position> snakeElements = new Queue<Position>();
            for (int i = 0; i < 5; i++)
            {
                snakeElements.Enqueue(new Position(Console.WindowHeight/2, i));
            }
            foreach (Position position in snakeElements)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    Console.Write("█");
                }
            while (true)
            {
                // Key Controller:
                if (Console.KeyAvailable)
                {
                    ConsoleKeyInfo userInput = Console.ReadKey();
                    if ((userInput.Key == ConsoleKey.LeftArrow || userInput.Key == ConsoleKey.A)&&direction!=0)
                    {
                        direction = 1;
                    }
                    if ((userInput.Key == ConsoleKey.RightArrow || userInput.Key == ConsoleKey.D) && direction != 1)
                    {
                        direction = 0;
                    }
                    if ((userInput.Key == ConsoleKey.UpArrow || userInput.Key == ConsoleKey.W) && direction != 2)
                    {
                        direction = 3;
                    }
                    if ((userInput.Key == ConsoleKey.DownArrow || userInput.Key == ConsoleKey.S) && direction != 3)
                    {
                        direction = 2;
                    }
                    
                }
                Position snakeHead = snakeElements.Last();
                Position nextDirection = directions[direction];
                Position snakeNewHead = new Position(snakeHead.row + nextDirection.row, snakeHead.col + nextDirection.col);
                Position newHead = snakeElements.Last();
                if(snakeNewHead.row < 1 || snakeNewHead.col < 0
                    || snakeNewHead.row>= Console.WindowHeight || snakeNewHead.col>=Console.WindowWidth||
                    snakeElements.Contains(snakeNewHead))
                {
                    if (IsSoundOn == true)
                    {
                        SystemSounds.Hand.Play();
                    }
                    
                    Console.SetCursorPosition(Console.WindowWidth/2-7,Console.WindowHeight/2-2);
                    Console.WriteLine("/████████████\\");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2-1);
                    Console.WriteLine("█ GAME OVER! █");
                    Console.SetCursorPosition(Console.WindowWidth / 2 - 7, Console.WindowHeight / 2);
                    Console.WriteLine("\\████████████/");
                    Console.ReadKey();
                    return;
                }
                snakeElements.Enqueue(snakeNewHead);
                if (newHead.col == food.col && newHead.row == food.row)
                {
                    if (IsSoundOn == true)
                    {
                        SystemSounds.Beep.Play();
                    }
                    food = new Position(randomNumGen.Next(1, Console.WindowHeight), randomNumGen.Next(0, Console.WindowWidth));
                    Console.SetCursorPosition(food.col, food.row);
                }
                else
                {
                    snakeElements.Dequeue();
                }
                
                Console.Clear();
                Console.Write("SCORE: {0} ",snakeElements.Count-5);
                while (Console.CursorLeft < Console.WindowWidth-2)
                {
                    Console.Write(" █ ");
                }
                Console.WriteLine();
                foreach (Position position in snakeElements)
                {
                    Console.SetCursorPosition(position.col, position.row);
                    
                    Console.Write("█");
                    //Console.Write("*");
                }
                
                
                
                Console.SetCursorPosition(food.col, food.row);
                Console.Write("@");
                Thread.Sleep(speed);
            }
        }
    }
}
