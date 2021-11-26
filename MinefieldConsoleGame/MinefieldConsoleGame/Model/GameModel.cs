using System;
using System.Collections.Generic;
using System.Drawing;

namespace MinefieldConsoleGame
{
    public class GameModel : IGameModel
    {
        private readonly IIOHelper ioHelper;
        private readonly IMineFieldHelper mineFieldHelper;
        private List<Point> minePoints = new List<Point>();
        private int height;
        private int width;
        private Point playerLocation = new Point();
        private int lives = 0;
        private int moveCount = 0;

        public GameModel(IIOHelper ioHelper, IMineFieldHelper mineFieldHelper)
        {
            this.ioHelper = ioHelper;
            this.mineFieldHelper = mineFieldHelper;
        }

        public void InitModel()
        {
            ioHelper.Write("Setting up MineField");

            ConsoleKey useDefault = ioHelper.GetCharInput("Use Default Settings? (10 x 10 grid, 10 mines, 10 lives) (Y or N):", new List<ConsoleKey>() { ConsoleKey.Y, ConsoleKey.N });
            if (useDefault == ConsoleKey.Y)
            {
                height = 10;
                width = 10;
                minePoints = mineFieldHelper.CreateMineField();
                playerLocation = mineFieldHelper.GetStartingPoint();
                lives = 10;
            }
            else
            {
                height = ioHelper.GetNumericInput("Minefield Height:", 1, 20);
                width = ioHelper.GetNumericInput("Minefield Width:", 1, 20);
                int mineCount = ioHelper.GetNumericInput("Mine Count (Limited to max 50% of area):", 1, (height * width) / 2);

                minePoints = mineFieldHelper.CreateMineField(height, width, mineCount);
                playerLocation = mineFieldHelper.GetStartingPoint();
                lives = ioHelper.GetNumericInput("Live Count:", 1);
            }
            moveCount = 0;
            ioHelper.Write($"\nStarting Player Position: {playerLocation.X},{playerLocation.Y}");
        }

        public void MoveLeft()
        {
            Point newPlayerLocation = playerLocation;
            newPlayerLocation.X--;
            MoveMade(newPlayerLocation);
        }

        public void MoveUp()
        {
            Point newPlayerLocation = playerLocation;
            newPlayerLocation.Y++;
            MoveMade(newPlayerLocation);
        }

        public void MoveRight()
        {
            Point newPlayerLocation = playerLocation;
            newPlayerLocation.X++;
            MoveMade(newPlayerLocation);
        }

        public void MoveDown()
        {
            Point newPlayerLocation = playerLocation;
            newPlayerLocation.Y--;
            MoveMade(newPlayerLocation);
        }

        public bool IsGameOver()
        {
            return lives == 0 || playerLocation.X == width - 1;
        }

        public bool IsGameWon()
        {
            bool isWon = IsGameOver() && lives > 0;

            if (isWon)
            {
                ioHelper.Write($"Game Won! You reached the end in {moveCount} moves");
            }
            else if (IsGameOver() && lives == 0)
            {
                ioHelper.Write("Game Lost! No lives remaining");
            }

            return isWon;
        }

        public string GetGameStatus()
        {
            return $"Move {moveCount} - Player Position: {playerLocation.X},{playerLocation.Y}, Lives Remaining: {lives}, Mines Remaining: {minePoints.Count}";
        }

        private void MoveMade(Point newPlayerLocation)
        {
            moveCount++;

            if (newPlayerLocation.X < 0 || newPlayerLocation.X > width - 1 || newPlayerLocation.Y < 0 || newPlayerLocation.Y > height - 1)
            {
                ioHelper.Write("Invalid Move - Try Again!");
                return;
            }

            playerLocation = newPlayerLocation;

            if (minePoints.Contains(playerLocation))
            {
                minePoints.Remove(playerLocation);
                lives--;
                ioHelper.Write($"Player hit mine!");
            }
        }
    }
}
