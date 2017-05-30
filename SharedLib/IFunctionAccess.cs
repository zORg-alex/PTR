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
    
    public partial class IFunctionAccess
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public IFunctionAccess()
        {
            this.IQuestAccesses = new HashSet<IQuestAccess>();
    		OnCreated();
        }
    	
    	partial void OnCreated();
    
        public int Id { get; set; }
        public int UserID { get; set; }
        public int FunctionId { get; set; }
        public Nullable<System.DateTime> From { get; set; }
        public Nullable<System.DateTime> To { get; set; }
    
        public virtual IFunction IFunction { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<IQuestAccess> IQuestAccesses { get; set; }
        public virtual ISDAVSUser ISDAVSUser { get; set; }
    }
}