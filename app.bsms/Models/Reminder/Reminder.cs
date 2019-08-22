 
// Type: app.bsms.Models.Reminder.Reminder
 
 
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Reminder
{
  public class Reminder
  {
    public DateTime appointmentDateTime { get; set; }

    public List<Details> details { get; set; }

    public string remarks { get; set; }

    public int detailsId { get; set; }
  }
}
