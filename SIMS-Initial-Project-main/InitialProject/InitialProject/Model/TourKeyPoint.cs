using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InitialProject.Model;

[Table("TourKeyPoint")]
public class TourKeyPoint
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Name { get; set; }

    //Relationship between tour and keyPoints
    [ForeignKey("tourID")]
    public Tour Tour { get; set; }

    [Required]
    public TourKeyPointType Type { get; set; }
    public bool Reached;

    public TourKeyPoint()
    {
        Reached = false;
    }

    public TourKeyPoint(string name, TourKeyPointType type)
    {   
        this.Name = name;
        this.Type = type;
        Reached = false;
    }

}