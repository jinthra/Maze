namespace Maze.models
{
    /// <summary>
    /// Interface that handle the inputs.
    /// </summary>
    interface IInput
    {
        /// <returns>The direction of the move.</returns>
        Direction GetDirection();

        /// <returns>The file that contain the next level.</returns>
        string NextFile();
    }
}
