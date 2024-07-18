using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Lifetime;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Snake
{
    public static class Snake_v2
    {
        public static void Update()
        {
            Console.SetWindowSize(50, 20);
            Console.CursorVisible = false;
            Snake.Head.AddHandler();
            Snake.DrawScore(new Snake.Body(0, 0, 0));
            while (true)
            {
                if (Snake.Head.DotEaten())
                    Dot.GenerateNewDot();
                Snake.Head.MoveBody();
                Snake.Head.Move();
                Snake.Head.Drawing();

                Thread.Sleep(17);
            }
        }
        static class Dot
        {
            public static int X;
            public static int Y;
            public static void GenerateNewDot()
            {
                Random random = new Random();
                int x = random.Next(0, Console.WindowWidth);
                int y = random.Next(0, Console.WindowHeight);
                Drawing(x, y);
            }
            static void Drawing(int x, int y)
            {
                Console.SetCursorPosition(X, Y);
                Console.Write(" ");
                Console.SetCursorPosition(x, y);
                Console.Write("#");
                X = x;
                Y = y;
            }
        }
        static class Snake
        {
            delegate void BodyHandler(Body body);
            static event BodyHandler RemoveTail;
            public static void DrawScore(Body notUse)
            {
                Console.SetCursorPosition(0, 0);
                Console.Write($"Score: {Head.Score}");
            }
            public static class Head
            {
                private static int X = Console.WindowWidth / 2;
                private static int Y = Console.WindowHeight / 2;
                public static int Score = 0;

                private static List<Body> BodyPieces = new List<Body>();
                public static void Move()
                {
                    BodyPieces.Add(new Body(X, Y, Score));
                    if (Dot.X > X)
                        X++;
                    else if (Dot.X < X)
                        X--;
                    else if (Dot.Y > Y)
                        Y++;
                    else if (Dot.Y < Y)
                        Y--;
                }
                public static void MoveBody()
                {
                    for (int i = 0; i < BodyPieces.Count; i++)
                    {
                        BodyPieces[i].LifeTimeLeft();
                    }
                }
                public static bool DotEaten()
                {
                    if (Dot.X == X && Dot.Y == Y)
                    {
                        Score++;
                        return true;
                    }
                    return false;
                }
                public static void Drawing()
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write("$");
                }
                public static void RemoveBody(Body body)
                {
                    BodyPieces.Remove(body);
                }
                public static void AddHandler()
                {
                    RemoveTail += RemoveBody;
                    RemoveTail += DrawScore;
                }

            }
            public class Body
            {
                private int X;
                private int Y;
                private int LifeTime;
                public Body(int x, int y, int lifeTime)
                {
                    X = x;
                    Y = y;
                    LifeTime = lifeTime;
                    Drawing();
                }
                public void Drawing()
                {
                    Console.SetCursorPosition(X, Y);
                    Console.Write("0");
                }
                public void LifeTimeLeft()
                {
                    LifeTime--;
                    if (LifeTime <= 0)
                    {
                        Console.SetCursorPosition(X, Y);
                        Console.Write(" ");
                        RemoveTail(this);
                    }

                }
            }
        }
    }

}
