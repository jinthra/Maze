using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Maze.models.block
{
    /// <summary>
    /// Represent a key.
    /// </summary>
    class Key : Block
    {
        /// <summary>
        /// Key's default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        /// <param name="inverse">if inverse is true then the door is open when there's no block on the key.</param>
        public Key(Game game, int x, int y, bool inverse = false) : base(game, x, y)
        {
            this.inverse = inverse;
            Solid = false;
            doors = new List<Door>();
            Panel.SetZIndex(Rect, -1);
        }

        public override void Update()
        {
            if ((game[x, y].Solid == null && inverse) || (game[x, y].Solid != null && !inverse))
                foreach (var door in doors)
                    door.Open();
            else
                foreach (var door in doors)
                    door.Close();
        }

        /// <summary>
        /// Add a door linked to this key.
        /// </summary>
        /// <param name="door">Door.</param>
        public void AddDoor(Door door)
        {
            doors.Add(door);

            // Update key's color
            string sprite = "key." + door.Color.ToString().ToLower() + ".png";
            image.ImageSource = new BitmapImage(new Uri(@"pack://application:,,,/res/" + sprite));
            Rect.Fill = image;
        }

        // Input
        private bool inverse;
        private List<Door> doors;
    }
}
