using Maze.models.block;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Maze.models
{
    /// <summary>
    /// Represents a level.
    /// </summary>
    class Level
    {
        /// <summary>
        /// Level's default constructor.
        /// </summary>
        /// <param name="game">Game object.</param>
        /// <param name="fileName">File's name to load the level.</param>
        /// <param name="output">Output object.</param>
        public Level(Game game, string fileName, IOutput output)
        {
            this.game = game;
            this.output = output;
            links = new (List<Door> door, List<Key> key)[4];
            try
            {
                ParseFile(fileName);
            }
            catch (Exception)
            {
                throw;
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
            get { return tab[x, y]; }
        }

        /// <summary>
        /// Update the game.
        /// </summary>
        public void Update()
        {
            foreach (Slot slot in tab)
                slot.Update();
        }

        /// <summary>
        /// Parse the file to create the level.
        /// </summary>
        /// <param name="fileName">File's name to load the level.</param>
        private void ParseFile(string fileName)
        {
            y = -1;

            // Tool for linking keys and doors
            for (int i = 0; i < links.Length; i++)
                links[i] = (new List<Door>(), new List<Key>());

            using (StreamReader file = new StreamReader(MainWindow.FOLDER_NAME + fileName))
            {
                // Read file
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains("Name"))
                        ParseName(line);
                    else if (line.Contains("Width"))
                        int.TryParse(line.Split(' ')[1], out width);
                    else if (line.Contains("Height"))
                        int.TryParse(line.Split(' ')[1], out height);
                    else if (!line.Contains("#"))
                        ParseTab(line);
                }

                // Link keys and doors
                foreach (var (doors, keys) in links)
                    foreach (var key in keys)
                        foreach (var door in doors)
                            key.AddDoor(door);

                // Change canvas size
                output.SetDimension(width, height);
            }
        }

        /// <summary>
        /// Parse the string to find the level's name.
        /// </summary>
        /// <param name="line">String that contains the name.</param>
        private void ParseName(string line)
        {
            string remove = "Name ";
            int index = line.IndexOf(remove);
            Name = (index < 0) ? line : line.Remove(index, remove.Length);
        }

        /// <summary>
        /// Parse the string to build the level.
        /// </summary>
        /// <param name="line">String that contains a line of blocks.</param>
        private void ParseTab(string line)
        {
            int x = -1;

            // Create tab
            if (++y == 0)
                tab = new Slot[width, height];

            // Remove space in the string
            string cleared = Regex.Replace(line, @"\s+", "");

            // Split the slots
            foreach (var sub in cleared.Split('|'))
            {
                ++x;
                tab[x, y] = new Slot();

                // Split block in the slot
                foreach (string block in sub.Split(','))
                    ParseBlock(block, x, y);
            }
        }

        /// <summary>
        /// Parse the string to build block.
        /// </summary>
        /// <param name="block">String that contains the block.</param>
        /// <param name="x">X value of the position.</param>
        /// <param name="y">Y value of the position.</param>
        private void ParseBlock(string block, int x, int y)
        {
            if (block.Length > 0)
            {
                // Create the corresponding block
                Block b = null;
                int id;
                switch (block[0])
                {
                    case 'r':
                        b = new Bot(game, x, y);
                        Bot = (Bot)b;
                        break;
                    case 'b':
                        b = new Block(game, x, y);
                        break;
                    case 'm':
                        b = new MovingBlock(game, x, y);
                        break;
                    case 'e':
                        b = new Exit(game, x, y);
                        break;
                    case 'k':
                        int.TryParse(block[1].ToString(), out id);
                        id = id % colorNumber;
                        b = new Key(game, x, y, block.Length > 2);
                        links[id].keys.Add((Key)b);
                        break;
                    case 'd':
                        int.TryParse(block[1].ToString(), out id);
                        id = id % colorNumber;
                        b = new Door(game, x, y, (ImgColor)id);
                        links[id].doors.Add((Door)b);
                        break;
                }

                // Add block the the game
                if (b != null)
                {
                    tab[x, y].AddBlock(b);
                    output.Add(b.Rect);
                }
            }
        }

        // Input
        private Game game;
        private IOutput output;

        // Output
        private Slot[,] tab;
        public Bot Bot { get; private set; }
        public string Name { get; private set; }

        // Tools
        private int width;
        private int height;
        private int y;
        private (List<Door> doors, List<Key> keys)[] links;
        private readonly int colorNumber = Enum.GetNames(typeof(ImgColor)).Length;
    }
}
