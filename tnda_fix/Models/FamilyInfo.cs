//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace tnda_fix.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class FamilyInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public FamilyInfo()
        {
            this.FamilyLinkInfoes = new HashSet<FamilyLinkInfo>();
        }
    
        public int ID { get; set; }
        public Nullable<int> img_index { get; set; }
        public string src { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<FamilyLinkInfo> FamilyLinkInfoes { get; set; }
    }
}
