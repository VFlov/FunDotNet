using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FunCSharp
{
    public class PingPong
    {
        static void Main(string[] args)
        {
            Platform player1 = new Platform(1, 1, 1, 3);
            Platform player2 = new Platform(Console.WindowWidth - 2, 1, 1, 3);
            while (true)
            {
                Update(player1, player2);
                Thread.Sleep(16);
            }
        }
        static void Update(Platform player1, Platform player2)
        {
            Console.Clear();
            player1.Update();
            player2.Update();
            Ball.Update();
            ScoreUpdate(player1, player2);

        }
        static void ScoreUpdate(Platform player1, Platform player2)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write($"Score: {player1.Score} / {player2.Score}");
        }
    }
    class Platform
    {
        int X { get; set; }
        int Y { get; set; }
        int Speed { get; set; }
        int Height { get; set; }
        public int Score { get; set; }
        public Platform(int x, int y, int speed, int height)
        {
            X = x;
            Y = y;
            Speed = speed;
            Height = height;
        }
        public void Update()
        {
            Move();
            Collision();
            Drawing();
        }
        void Move()
        {
            if (Ball.Y < Y)
                Y -= Speed;
            if (Ball.Y > Y)
                Y += Speed;
        }
        void Collision()
        {
            if (X - Ball.X == 1 || X - Ball.X == -1)
                if (Y >= Ball.Y && Y <= Ball.Y + Height)
                {
                    Ball.SpeedX *= -1;
                    Score++;
                }
        }
        void Drawing()
        {
            for (int i = Y; i < Y + Height; i++)
            {
                Console.SetCursorPosition(X, i);
                Console.Write("|");
            }
        }
    }
    static class Ball
    {
        public static int X { get; private set; } = Console.WindowWidth / 2;
        public static int Y { get; private set; } = Console.WindowHeight / 2;
        public static int SpeedX { get; set; } = 1;
        public static int SpeedY { get; private set; } = 1;
        public static void Update()
        {
            Move();
            Collision();
            Drawing();
        }
        static void Move()
        {
            X += SpeedX;
            Y += SpeedY;
        }
        static void Collision()
        {
            if (Y == 0 || Y == Console.WindowHeight)
                SpeedY *= -1;
            if (X == 0 || X == Console.WindowWidth)
                SpeedX *= -1;
        }
        static void Drawing()
        {
            Console.SetCursorPosition(X, Y);
            Console.Write("@");
        }
    }
}
