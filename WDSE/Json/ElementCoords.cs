using System;

namespace WDSE.Json
{
    [Serializable]
    public class ElementCoords
    {
        public int x { get; set; }
        public int y { get; set; }
        public int width { get; set; }
        public int height { get; set; }
        public int bottom { get; set; }
    }
}