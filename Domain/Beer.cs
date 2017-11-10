﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public class Beer
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Boolean IsGlutenFree { get; set; }

        public decimal Abv { get; set; }
    }
}
