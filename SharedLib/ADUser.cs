//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PTR.PTRLib
{
    using System;
    using System.Collections.Generic;
    
    public partial class ADUser
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ADUser()
        {
            this.ADFolderAccesses = new HashSet<ADFolderAccess>();
    		OnCreated();
        }
    	
    	partial void OnCreated();
    
        public int Id { get; set; }
        public string Login { get; set; }
        public bool Status { get; set; }
        public string DN { get; set; }
        public Nullable<int> UUserId { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ADFolderAccess> ADFolderAccesses { get; set; }
        public virtual UUser UUser { get; set; }
    }
}
