﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ColorMixERP.Server.Entities.DTO
{
    public class ProductStockDTO
    {
        public int Id { get; set; }
        public int WorkPlaceId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductCurrencId { get; set; }
        public decimal CurrencyRate { get; set; }
        public decimal Quantity { get; set; }
        public string MeasurementUnit { get; set; }
        public string ProductCode { get; set; }
        public decimal BoxedNumber { get; set; }
        public Category Category { get; set; }
    }
}
