using System;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;

namespace Maze.models.block
{
    /// <summary>
    /// Represent a base block.
    /// </summary>
    class Block
    {
        /// <summary>
        /// Block's default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        /// <param name="sprite">Image of the block.</param>
        public Block(Game game, int x, int y, string sprite = "castle.center.png")
        {
            this.x = x;
            this.y = y;
            this.game = game;
            ready = true;
            Solid = true;

            image = new ImageBrush
            {
                ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/res/" + sprite))
            };

            Rect = new Rectangle
            {
                Height = 1,
                Width = 1,
                Fill = image,
            };

            Canvas.SetTop(Rect, y);
            Canvas.SetLeft(Rect, x);
        }

        /// <summary>
        /// Update the block.
        /// </summary>
        public virtual void Update() { }

        /// <summary>
        /// Move the block if it's possible.
        /// </summary>
        /// <param name="direction">Direction of the move.</param>
        /// <returns></returns>
        public virtual bool AskMove(Direction direction)
        {
            return false;
        }

        /// <summary>
        /// Move the block.
        /// </summary>
        /// <param name="direction">Direction of the move.</param>
        protected virtual void Move(Direction direction)
        {
            if (ready)
            {
                ready = false;

                // Update coordinates
                var (dx, dy) = direction.GetVector();
                x += dx;
                y += dy;
                game[x, y].Solid = this;

                // Update Draw
                Canvas.SetTop(Rect, y);
                Canvas.SetLeft(Rect, x);

                // Animation
                TranslateTransform trans = new TranslateTransform();
                Rect.RenderTransform = trans;

                DoubleAnimation anim1 = new DoubleAnimation(-dy, 0, TimeSpan.FromSeconds(TIME));
                trans.BeginAnimation(TranslateTransform.YProperty, anim1);

                DoubleAnimation anim2 = new DoubleAnimation(-dx, 0, TimeSpan.FromSeconds(TIME));
                anim2.Completed += (sender, e) => EndAction(direction);
                trans.BeginAnimation(TranslateTransform.XProperty, anim2);
            }
        }

        /// <summary>
        /// Action to do at the end of the move's animation.
        /// </summary>
        /// <param name="direction">Direction of the move.</param>
        protected virtual void EndAction(Direction direction)
        {
            var (dx, dy) = direction.GetVector();
            if (game[x - dx, y - dy].Solid == this)
                game[x - dx, y - dy].Solid = null;

            ready = true;
        }

        // Input
        protected int x;
        protected int y;
        protected Game game;
        protected ImageBrush image;

        // Output
        public Rectangle Rect { get; }
        public bool Solid { get; protected set; }

        // Tools
        protected bool ready;
        private const double TIME = 0.2;
    }

    /// <summary>
    /// Enumeration for image's color.
    /// </summary>
    public enum ImgColor { Blue = 0, Green = 1, Red = 2, Yellow = 3 };
}
