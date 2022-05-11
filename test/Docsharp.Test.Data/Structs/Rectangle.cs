﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docsharp.Test.Data.Structs
{
    public struct Rectangle
    {
        public int Top { get; set; }
        public int Left { get; set; }

        public int bottom;
        public int right;

        public int GetWidth() => right - Left;
        public int GetHeight() => bottom - Top;
    }
}
