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
    using System.ComponentModel.DataAnnotations;
    
    public partial class Offer
    {
        public Offer()
        {
            this.OfferPhotoDetails = new HashSet<OfferPhotoDetail>();
        }
    
        public int OfferId { get; set; }
        public string KeyWords { get; set; }
        public string Description { get; set; }
        public int OfferTypeId { get; set; }
        public int UtilityBuildingId { get; set; }
        public System.DateTime StartDate { get; set; }       
        public Nullable<System.DateTime> EndDate { get; set; }
        public Nullable<byte> Discount { get; set; }
    
        public virtual OfferType OfferType { get; set; }
        public virtual UtilityBuilding UtilityBuilding { get; set; }
        public virtual ICollection<OfferPhotoDetail> OfferPhotoDetails { get; set; }
    }
}
