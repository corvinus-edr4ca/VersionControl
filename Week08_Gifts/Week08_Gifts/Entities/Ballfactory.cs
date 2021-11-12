using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Week08_Gifts.Entities
{
    public class Ballfactory
    {
        public Toy CreateNew()
        {
            return new Ball();
        }
    }
}
