//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace _10023767_P2
{
    using System;
    using System.Collections.Generic;
    
    public partial class Module
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Module()
        {
            this.SelfStudyHours = new HashSet<SelfStudyHour>();
            this.Studies = new HashSet<Study>();
        }
    
        public int ModuleID { get; set; }
        public string Name { get; set; }
        public Nullable<int> ClassHrsPerWeek_Hours { get; set; }
        public Nullable<int> ClassHrsPerWeek_Minutes { get; set; }
        public string Code { get; set; }
        public Nullable<int> NumOfCredits { get; set; }
        public string FormattedSelfStudyTime { get; set; }
        public string FormattedClassHours { get; set; }
        public Nullable<int> SemesterID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SelfStudyHour> SelfStudyHours { get; set; }
        public virtual Semester Semester { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Study> Studies { get; set; }
    }
}
