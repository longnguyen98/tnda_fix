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
    
    public partial class ACC
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ACC()
        {
            this.PersonLogs = new HashSet<PersonLog>();
        }
    
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public Nullable<int> ID_Person { get; set; }
        public Nullable<int> accLevel { get; set; }
    
        public virtual Person Person { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<PersonLog> PersonLogs { get; set; }
    }
}
