﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DemoProject.Models
{
    public class MyAdvertiseModel
    {
        public int advertiseId { get; set; }
        public int productSubCategoryId { get; set; }
        public string productSubCategoryName { get; set; }
        public string advertiseTitle { get; set; }
        public string advertiseDescription { get; set; }
        public decimal advertisePrice { get; set; }
        public int areaId { get; set; }
        public string areaName { get; set; }
        public bool advertiseStatus { get; set; }
        public int UserId { get; set; }
        public string firstName { get; set; }
        public bool advertiseapproved { get; set; }
        public DateTime createdOn { get; set; }
        public DateTime updatedOn { get; set; }
        public byte[] imageData { get; set; }

    }
}