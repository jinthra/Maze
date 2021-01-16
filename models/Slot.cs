using Maze.models.block;
using System.Collections.Generic;

namespace Maze.models
{
    /// <summary>
    /// Represents a slot that can contain blocks.
    /// </summary>
    class Slot
    {
        /// <summary>
        /// Slot's default constructor.
        /// </summary>
        public Slot()
        {
            Solid = null;
            others = new List<Block>();
        }

        /// <summary>
        /// Add a block to this slot.
        /// </summary>
        /// <param name="block">Block object</param>
        public void AddBlock(Block block)
        {
            if (block.Solid)
                Solid = block;
            else
                others.Add(block);
        }

        /// <summary>
        /// Update the blocks contained in this slot.
        /// </summary>
        public void Update()
        {
            if (Solid != null)
                Solid.Update();
            foreach (Block block in others)
                block.Update();
        }

        // Output
        public Block Solid { get; set; }

        // Tools
        private List<Block> others;
    }
}
