namespace InitialProject.Model;

public class TourKeyPoint
{
    public int Id { get; set; }
    public string Name { get; set; }
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