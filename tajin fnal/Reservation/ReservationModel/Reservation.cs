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
    
    public partial class Reservation
    {
        public int reservationId { get; set; }
        public int bUserId { get; set; }
        public int cUserId { get; set; }
        public System.DateTime rDay { get; set; }
        public System.TimeSpan rTime { get; set; }
        public int people { get; set; }
    
        public virtual Business Business { get; set; }
        public virtual Client Client { get; set; }
    }
}