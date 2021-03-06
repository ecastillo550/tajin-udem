//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ReservationModel
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    
    public partial class Account
    {
        [Required(ErrorMessage = "password is required")]
        [DataType(DataType.Password)]
        public string password { get; set; }
        [Required(ErrorMessage = "user name is required")]
        public string userName { get; set; }
        public int userTypeId { get; set; }
        public int userId { get; set; }
        public string mail { get; set; }
        public virtual Business Business { get; set; }
        public virtual Client Client { get; set; }
    }
}
