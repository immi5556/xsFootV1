 
// Type: app.bsms.Models.Treatment.BodyTreatment
 
 
 

using System.ComponentModel.DataAnnotations;

namespace app.bsms.Models.Treatment
{
  public class BodyTreatment
  {
    [Key]
    public int Treatmentid { get; set; }

    public SkinTexture skinTextire { get; set; }

    public BodyProblem bodyProblem { get; set; }

    public Hair hair { get; set; }

    public string Others { get; set; }
  }
}
