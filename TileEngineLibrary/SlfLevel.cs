using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Xna.Framework;
using TileEngineLibrary.Tiles;

namespace TileEngineLibrary
{
    [Serializable]
    public class SlfLevel : Level
    {
        string path;

        public SlfLevel(string path)
            : base(0, 0, 1, 1)
        {
            this.path = path;

            if (this.path != null)
                LoadLevelFromFile(path);
        }

        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }
    }
}
