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
    
    public partial class PVHistEmploee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public Nullable<System.DateTime> From { get; set; }
        public Nullable<System.DateTime> To { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LocalPhone { get; set; }
        public bool Status { get; set; }
        public string DepartmentId { get; set; }
        public string PartId { get; set; }
        public Nullable<int> ProfessionId { get; set; }
        public System.DateTime ChangeDate { get; set; }
        public int EmploeeID { get; set; }
    
        public virtual PVEmploee PVEmploee { get; set; }
        public virtual PVProfession PVProfession { get; set; }
        public virtual PVStructural Part { get; set; }
        public virtual PVStructural Department { get; set; }
    }
}
