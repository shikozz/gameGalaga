using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gg
{
    public class Enemy
    {
        public int width;
        public int height;


        public int x;
        public int y;

        public int x1
        {
            get { return x; }
        }

        public int x2
        {
            get { return x + width; }
        }

        public int y1
        {
            get { return y; }
        }

        public int y2
        {
            get { return y + height; }
        }



    }
}
