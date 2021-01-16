using System.Windows;

namespace Maze.models
{
    /// <summary>
    /// Interface that handle output.
    /// </summary>
    interface IOutput
    {
        /// <summary>
        /// Return the UIElement that represents the black transition.
        /// </summary>
        UIElement Transition { get; }

        /// <summary>
        /// Add an UIElement to the output.
        /// </summary>
        /// <param name="element">UIElement object.</param>
        void Add(UIElement element);

        /// <summary>
        /// Set the dimension of the output.
        /// </summary>
        /// <param name="width">Horizontal size.</param>
        /// <param name="height">Vertical size.</param>
        void SetDimension(double width, double height);

        /// <summary>
        /// Set the level's name that is shown.
        /// </summary>
        /// <param name="name">Name of the level.</param>
        void SetLevelName(string name);

        /// <summary>
        /// Clear the output.
        /// </summary>
        void Clear();

        /// <summary>
        /// Close the output.
        /// </summary>
        void Close();
    }
}
