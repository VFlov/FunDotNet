using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    internal class Program
    {
        public delegate void SnakeHandler();
        public event SnakeHandler MoveHandler;
        static void Main(string[] args)
        {
            Snake_v2.Update();
            //Console.SetWindowSize(20,20);
            Console.CursorVisible = false;
            Dot.GenerateDot();
            Snake.Body head = new Snake.Body(Console.WindowWidth / 2, Console.WindowHeight / 2);
            head.Add(head);
            while (true)
            {
                Update(head);
                Thread.Sleep(20);
            }
        }
        static void Update(Snake.Body head)
        {

            if (Snake.Eating())
            {
                Snake.Body newBody = new Snake.Body(Snake.Tail.X, Snake.Tail.Y - 1);
                head.Add(newBody);
            }
            Snake.MoveTail();
            Snake.AutoMove();
            Console.Clear();
            Snake.Drawing();
            Dot.Drawing();
            DrawingScore();
        }
        static void DrawingScore()
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Score: {Snake.Score}");
        }
    }
    static class Dot
    {
        public static int X { get; set; }
        public static int Y { get; set; }

        public static void GenerateDot()
        {
            Random random = new Random();
            X = random.Next(0, Console.WindowWidth);
            Y = random.Next(0, Console.WindowHeight);
        }
        public static void Drawing()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("$");
        }
    }
    static class Snake
    {
        static Body Head;
        public static Body Tail;
        public static int Score;
        static Body SnakeBody;
        public class Body
        {
            public int X;
            public int Y;
            public Body Next;
            public Body(int x, int y)
            {
                X = x;
                Y = y;
            }
            public void Add(Body body)
            {
                if (Head == null)
                {
                    Head = body;
                    Next = null;
                    Tail = body;
                }
                else
                {
                    body.Next = Tail;
                    Tail = body;
                }
            }
        }
        public static void MoveTail()
        {
            if (Head == null)
                return;
            var temp = Tail;
            while (temp != Head)
            {
                temp.X = temp.Next.X;
                temp.Y = temp.Next.Y;
                temp = temp.Next;
            }

        }
        public static bool Eating()
        {
            if (Dot.X == Head.X && Dot.Y == Head.Y)
            {
                Score++;
                Dot.GenerateDot();
                return true;
            }
            return false;
        }
        public static void AutoMove()
        {
            if (Dot.X < Snake.Head.X)
                Head.X--;
            if (Dot.X > Snake.Head.X)
                Head.X++;
            if (Dot.Y < Snake.Head.Y)
                Head.Y--;
            if (Dot.Y > Snake.Head.Y)
                Head.Y++;
        }
        public static void PlayerMove()
        {
            switch (MoveSideChoose())
            {
                case MoveSide.Up:
                    Head.Y--;
                    break;
                case MoveSide.Down:
                    Head.Y++;
                    break;
                case MoveSide.Left:
                    Head.X--;
                    break;
                case MoveSide.Right:
                    Head.X++;
                    break;
            }
        }
        public static void Drawing()
        {
            var temp = Tail;
            while (temp != Head)
            {
                Console.SetCursorPosition(temp.X, temp.Y);
                Console.Write("#");
                temp = temp.Next;
            }

            Console.SetCursorPosition(Head.X, Head.Y);
            Console.Write("@");
        }
        static MoveSide MoveSideChoose()
        {
            switch (Console.ReadKey().Key)
            {
                case ConsoleKey.UpArrow:
                    return MoveSide.Up;
                case ConsoleKey.DownArrow:
                    return MoveSide.Down;
                case ConsoleKey.LeftArrow:
                    return MoveSide.Left;
                case ConsoleKey.RightArrow:
                    return MoveSide.Right;
                default:
                    return MoveSide.None;
            }
        }
        enum MoveSide
        {
            Up, Down, Left, Right, None
        }
    }

}
