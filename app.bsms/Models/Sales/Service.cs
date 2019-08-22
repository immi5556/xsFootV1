 
// Type: app.bsms.Models.Sales.Service
 
 
 

using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Sales
{
  public class Service
  {
    public int ServiceId { get; set; }

    [Required]
    public string ServiceName { get; set; }

    public string ServiceDesc { get; set; }
  }
}
