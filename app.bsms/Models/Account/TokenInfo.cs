 
// Type: app.bsms.Models.Account.TokenInfo
 
 
 

using System;

namespace app.bsms.Models.Account
{
  public class TokenInfo
  {
    public string access_token { get; set; }

    public string token_type { get; set; }

    public double expires_in { get; set; }

    public string error { get; set; }

    public string error_description { get; set; }

    public DateTime token_expires_datetime { get; set; }
  }
}
