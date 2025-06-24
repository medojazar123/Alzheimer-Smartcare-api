using System.ComponentModel.DataAnnotations.Schema;

namespace api.models
{
    public class FaceImage
    {
    public int Id { get; set; }
    public string Name { get; set; }
    public string Base64Image { get; set; }
    public string UserEmail { get; set; }
    }
} 