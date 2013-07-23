using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LayerLevelEngine
{
    public class Layer
    {
        private int layerId;
        private Level parent;

        public Layer(Level parent)
        {
            this.parent = parent;
            this.layerId = parent.LayerCount;
        }

        public void Update(GameTime gameTime)
        {

        }

        public void Draw(SpriteBatch batch, GameTime gameTime)
        {

        }
    }
}
