﻿namespace MvcApplication1.Models
{
    public class CityStorage
    {
        public int Capacity { get; set; }

        public Product[] CurrentInventory { get; set; }
    }
}