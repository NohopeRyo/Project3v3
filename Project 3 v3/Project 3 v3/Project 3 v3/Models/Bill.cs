//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Project_3_v3.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Bill
    {
        public int IDBill { get; set; }
        public Nullable<System.DateTime> DateBill { get; set; }
        public Nullable<int> ProID { get; set; }
        public Nullable<int> QuantityTotal { get; set; }
        public Nullable<decimal> PriceTotal { get; set; }
        public Nullable<int> CusID { get; set; }
    
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
    }
}