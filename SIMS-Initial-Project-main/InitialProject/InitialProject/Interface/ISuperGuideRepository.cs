using InitialProject.Model;

namespace InitialProject.Interface;

public interface ISuperGuideRepository
{
    public void Add(SuperGuide superGuide);
    public bool IsActive(User guide, string language);
}