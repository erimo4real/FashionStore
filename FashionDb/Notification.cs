//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FashionDb
{
    using System;
    using System.Collections.Generic;
    
    public partial class Notification
    {
        public int ID { get; set; }
        public Nullable<int> productID { get; set; }
        public Nullable<int> userID { get; set; }
        public Nullable<int> categoryID { get; set; }
        public Nullable<bool> @checked { get; set; }
        public Nullable<int> BrandID { get; set; }
        public Nullable<System.DateTime> createdON { get; set; }
        public Nullable<int> createdBY { get; set; }
        public Nullable<System.DateTime> updatedON { get; set; }
        public string updatedBY { get; set; }
    }
}
