//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace OnLeave.Database
{
    using System;
    using System.Collections.Generic;
    
    public partial class UtilityBuildingType
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public UtilityBuildingType()
        {
            this.UtilityBuildings = new HashSet<UtilityBuilding>();
        }
    
        public int UtilityBuildingTypeId { get; set; }
        public string KeyWords { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<UtilityBuilding> UtilityBuildings { get; set; }
    }
}
