using System.Windows.Controls;

namespace Maze.models.block
{
    /// <summary>
    /// Represent the exit.
    /// </summary>
    class Exit : Block
    {
        /// <summary>
        /// Exit's default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        public Exit(Game game, int x, int y) : base(game, x, y, "door.open.png")
        {
            Solid = false;
            Panel.SetZIndex(Rect, -1);
        }

        public override void Update()
        {
            if (game[x, y].Solid is Bot)
                game.End();
        }
    }
}
