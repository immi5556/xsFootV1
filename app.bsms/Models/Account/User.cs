 
// Type: app.bsms.Models.Account.User
 
 
 

using System;
using System.Collections.Generic;

namespace app.bsms.Models.Account
{
  public class User
  {
    public string companyName { get; set; }

    public string clientName { get; set; }

    public string userID { get; set; }

    public string email { get; set; }

    public string name { get; set; }

    public string fullName { get; set; }

    public string firstName { get; set; }

    public string lastName { get; set; }

    public string profilePic { get; set; }

    public DateTime? memberFrom { get; set; }

    public string role { get; set; }

    public string siteCode { get; set; }

    public string staffCode { get; set; }

    public string currency { get; set; }

    public string country { get; set; }

    public List<Settings> settings { get; set; }
  }
}
