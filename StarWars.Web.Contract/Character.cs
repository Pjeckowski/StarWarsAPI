﻿using System;
using System.Collections.Generic;

namespace StarWars.Web.Contract
{
    public class Character
    {
        public string Name { get; set; }
        public List<string> Episodes { get; set; }
        public List<string> Friends { get; set; }
    }
}
