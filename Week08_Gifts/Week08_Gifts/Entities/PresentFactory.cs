using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week08_Gifts.Abstactions;
using Week08_Gifts.Abstractions;

namespace Week08_Gifts.Entities
{
    class PresentFactory : IToyFactory
    {
        public Color RibbonColor { get; set; }
        public Color BoxColor { get; set; }
        public Toy CreateNew()
        {
            return new Present(RibbonColor, BoxColor);
        }
    }
}
