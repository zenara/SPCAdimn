//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AdminSiteMVC.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class section
    {
        public int sec_id { get; set; }
        public int p_id { get; set; }
        public int v_id { get; set; }
        public string content { get; set; }
        public Nullable<int> sec_no { get; set; }
    
        public virtual policy policy { get; set; }
        public virtual version version { get; set; }
    }
}
