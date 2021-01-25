using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopBridgeAPI.Models
{
    public class ItemModel
    {
        public int ItemId { set; get; }
        public string Name { set; get; }
        public string Description { set; get; }
        public long? Price { set; get; }
        public string ItemImageBase64 { set; get; }
    }
}