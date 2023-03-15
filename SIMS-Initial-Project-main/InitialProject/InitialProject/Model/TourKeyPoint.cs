using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace InitialProject.Model;

[Table("TourKeyPoint")]
public class TourKeyPoint
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }

    //Relationship between tour and keyPoints
    [ForeignKey("tourID")]
    [AllowNull]
    public Tour Tour { get; set; }

    [Required]
    public TourKeyPointType Type { get; set; }
    public bool Reached { get; set; }

    public TourKeyPoint(int Id, string name, TourKeyPointType tourKeyPointType)
    {
        this.Name = "default";
        this.Type = TourKeyPointType.Mid;
        this.Reached = false;
    }

    public TourKeyPoint(string name, TourKeyPointType type)
    {
        this.Tour = null;
        this.Name = name;
        this.Type = type;
        Reached = false;
    }

}