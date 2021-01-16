using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Maze.models
{
    /// <summary>
    /// Represent the game.
    /// </summary>
    class Game
    {
        /// <summary>
        /// Game's default constructor.
        /// </summary>
        /// <param name="fileName">File's name to load.</param>
        /// <param name="input">Input object.</param>
        /// <param name="output">Output object.</param>
        public Game(string fileName, IInput input, IOutput output)
        {
            this.fileName = fileName;
            this.input = input;
            this.output = output;

            Init();
        }

        /// <summary>
        /// Initialize the game and load the level.
        /// </summary>
        private void Init()
        {
            output.Clear();
            try
            {
                // Load the level
                level = new Level(this, fileName, output);

                // Set the transition
                output.SetLevelName(level.Name);
                output.Transition.Opacity = 1;
                var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                timer.Start();
                timer.Tick += (sender, args) =>
                {
                    timer.Stop();
                    DoubleAnimation anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(TIME), FillBehavior.Stop);

                    anim.Completed += (s, a) => output.Transition.Opacity = 0;
                    output.Transition.BeginAnimation(UIElement.OpacityProperty, anim);
                };

                // Update the level
                level.Update();
                isReady = true;
            }
            catch (Exception)
            {
                End();
            }
        }

        /// <summary>
        /// Indexer to access the slots.
        /// </summary>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        /// <returns>Slot at this position.</returns>
        public Slot this[int x, int y]
        {
            get { return level[x, y]; }
        }

        /// <summary>
        /// Advance the game.
        /// </summary>
        public void MoveEvent()
        {
            Direction direction = input.GetDirection();

            if (isReady && direction != Direction.None)
                level.Bot.AskMove(direction);
        }

        /// <summary>
        /// Lock the game.
        /// </summary>
        public void Lock()
        {
            isReady = false;
        }

        /// <summary>
        /// Unlock the game.
        /// </summary>
        public void Unlock()
        {
            isReady = true;
            level.Update();
            MoveEvent();
        }

        /// <summary>
        /// Restart the game.
        /// </summary>
        public void Restart()
        {
            Init();
        }

        /// <summary>
        /// End the game.
        /// </summary>
        public void End()
        {
            fileName = input.NextFile();
            if (string.IsNullOrEmpty(fileName))
                output.Close();
            else
                Init();
        }

        // Input
        private string fileName;
        private IInput input;
        private IOutput output;

        // Tools
        private Level level;
        private bool isReady;
        private const double TIME = 1.0;
    }

    /// <summary>
    /// Enumeration for directions.
    /// </summary>
    public enum Direction {
        None,
        Right = Key.Right,
        Left = Key.Left,
        Up = Key.Up,
        Down = Key.Down
    };

    public static class Extensions
    {
        /// <summary>
        /// Return the vector associate to the direction.
        /// </summary>
        /// <param name="direction">Direction object.</param>
        /// <returns>Tuple that contains the vector.</returns>
        public static (int dx, int dy) GetVector(this Direction direction)
        {
            switch (direction)
            {
                case Direction.Right:
                    return (1, 0);
                case Direction.Left:
                    return (-1, 0);
                case Direction.Up:
                    return (0, -1);
                case Direction.Down:
                    return (0, 1);
                case Direction.None:
                default:
                    return (0, 0);
            }
        }
    }
}
