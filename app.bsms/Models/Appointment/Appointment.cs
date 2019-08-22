 
// Type: app.bsms.Models.Appointment.Appointment
 
 
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Appointment
{
  public class Appointment
  {
    public DateTime appointmentDateTime { get; set; }

    public int duration { get; set; }

    public List<Staff> staffs { get; set; }

    public int staffId { get; set; }
  }
}
