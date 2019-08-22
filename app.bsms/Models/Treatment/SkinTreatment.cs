 
// Type: app.bsms.Models.Treatment.SkinTreatment
 
 
 

namespace app.bsms.Models.Treatment
{
  public class SkinTreatment
  {
    public SkinTextureType skinTextureType { get; set; }

    public SkinProblem skinProblem { get; set; }

    public SkinTex skinTexture { get; set; }

    public SkinMoistureContent skinMoistureContent { get; set; }

    public SebumLevel sebumLevel { get; set; }

    public SkinPores skinPores { get; set; }

    public SkinBloodCirculation skinBloodCirculation { get; set; }

    public SkinTone skinTone { get; set; }

    public SkinSensitivity skinSensitivity { get; set; }

    public Elasticity elasticity { get; set; }

    public SuperficialLine superficialLine { get; set; }

    public Wrinkles wrinkles { get; set; }

    public ExpressionLines expressionLines { get; set; }

    public SuperficialHair superficialHair { get; set; }
  }
}
