using System.Windows.Media;

namespace Maze.models.block
{
    /// <summary>
    /// Represent a door.
    /// </summary>
    class Door : Block
    {
        /// <summary>
        /// Door's default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        /// <param name="color">Color of the door.</param>
        public Door(Game game, int x, int y, ImgColor color) : base(game, x, y, "lock." + color.ToString().ToLower() + ".png")
        {
            Color = color;
        }

        /// <summary>
        /// Open the door.
        /// </summary>
        public void Open()
        {
            Solid = false;
            if (game[x, y].Solid == this)
                game[x, y].Solid = null;
            Rect.Fill = Brushes.Transparent;
        }

        /// <summary>
        /// Close the door.
        /// </summary>
        public void Close()
        {
            Solid = true;
            if (game[x, y].Solid == null)
                game[x, y].Solid = this;
            Rect.Fill = image;
        }

        // Input
        public ImgColor Color { get; }
    }
}
