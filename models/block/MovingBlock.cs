namespace Maze.models.block
{
    /// <summary>
    /// Represent a moving block.
    /// </summary>
    class MovingBlock : Block
    {
        /// <summary>
        /// MovingBlock's default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        public MovingBlock(Game game, int x, int y) : base(game, x, y, "box.single.png") { }

        public override bool AskMove(Direction direction)
        {
            var (dx, dy) = direction.GetVector();
            if (game[x + dx, y + dy].Solid == null)
            {
                Move(direction);
                return true;
            }

            return false;
        }
    }
}
