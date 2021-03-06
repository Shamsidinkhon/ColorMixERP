﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace ColorMixERP.Server.Entities
{
    [Table(Name = "Sale")]
    public class Sale
    {
        public Sale()
        {

        }
        public Sale(int productId)
        {
            ProductId = productId;
        }

        [Column(Name = "Id", IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name="ProductId")]
        private int ProductId { get; set; }
        private EntityRef<Product> _Product;
        [Association(Storage = "_Product", ThisKey = "ProductId")]
        public Product Product
        {
            get { return _Product.Entity; }
            set { this._Product.Entity = value; }
        }

        [Column(Name = "ProductName")]
        public string ProductName { get; set; }

        [Column(Name = "Quantity")]
        public decimal Quantity { get; set; }

        [Column(Name = "ProductPrice")]
        public decimal ProductPrice { get; set; }

        [Column(Name = "SalesPrice")]
        public decimal SalesPrice { get; set; }

        [Column(Name = "CurrencyRate")]
        public decimal? CurrencyRate { get; set; }

        [Column(Name="OrderId")]
        public int OrderId { get; set; }

        // =================================================

        [Column(Name = "IsDeleted")]
        public bool IsDeleted { get; set; }

        [Column(Name = "DeletedDate")]
        public DateTime? DeletedDate { get; set; }

        [Column(Name = "UpdatedDate")]
        public DateTime? UpdatedDate { get; set; }
    }
}
