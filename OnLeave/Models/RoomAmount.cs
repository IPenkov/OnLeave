//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnLeave.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class RoomAmount
    {
        public int RoomAmountId { get; set; }
        public int PeriodId { get; set; }
        public int RoomTypeId { get; set; }
        public decimal Amount { get; set; }
    
        public virtual Period Period { get; set; }
        public virtual RoomType RoomType { get; set; }
    }
}
