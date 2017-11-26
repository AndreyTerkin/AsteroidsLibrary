using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AsteroidsLibrary
{
    public interface IBoundary
    {
        Border Border { get; set; }
    }

    [System.Serializable]
    public struct Border
    {
        public float xMin;
        public float xMax;
        public float yMin;
        public float yMax;
    }
}
