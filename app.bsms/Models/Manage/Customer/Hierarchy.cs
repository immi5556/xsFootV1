 
// Type: app.bsms.Models.Manage.Customer.Hierarchy
 
 
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Manage.Customer
{
  public class Hierarchy
  {
    public string level { get; set; }

    public string referredBy { get; set; }

    public string customerName { get; set; }

    public string customerCode { get; set; }

    public string customerCardNumber { get; set; }

    public DateTime customerJoinedDate { get; set; }

    public int customerPoints { get; set; }

    public List<Hierarchy> children { get; set; }
  }
}
