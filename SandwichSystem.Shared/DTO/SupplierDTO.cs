﻿using System;
using System.Collections.Generic;
using System.Text;

namespace SandwichSystem.Shared.DTO
{
    public class SupplierDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsCurrentSupplier { get; set; }
    }
}