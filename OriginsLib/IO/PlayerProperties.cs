using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace OriginsLib.IO
{
    public class EntityProperties
    {
        public string Name { get; set; }
        public Vector2 Position { get; set; }
        public int Direction { get; set; }
        public bool NoClip { get; set; }
        public bool GodMode { get; set; }
        public bool SuperSpeed { get; set; }
        public DateTime TimeOfSave { get; set; }
    }
}
