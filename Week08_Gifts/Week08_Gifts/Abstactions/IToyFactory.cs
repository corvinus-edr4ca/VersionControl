﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Week08_Gifts.Abstractions;

namespace Week08_Gifts.Abstactions
{
    public interface IToyFactory
    {
        Toy CreateNew();
    }
}
