using Maze.models.block;

namespace Maze.models
{
    /// <summary>
    /// Represent the playable block.
    /// </summary>
    class Bot : Block
    {
        /// <summary>
        /// Bot's default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        public Bot(Game game, int x, int y) : base(game, x, y, "blocker.happy.png") { }

        public override bool AskMove(Direction direction)
        {
            var (dx, dy) = direction.GetVector();
            Block next = game[x + dx, y + dy].Solid;
            if (next == null || next.AskMove(direction))
            {
                Move(direction);
                return true;
            }
                
            return false;
        }
        
        protected override void Move(Direction direction)
        {
            if (ready)
                game.Lock();
            base.Move(direction);
        }

        protected override void EndAction(Direction direction)
        {
            base.EndAction(direction);
            game.Unlock();
        }
    }
}
