using InitialProject.Interface;
using InitialProject.Model;

namespace InitialProject.Service;

public class SuperGuideService
{
    private  readonly ISuperGuideRepository _ISuperGuideRepository;

    public SuperGuideService(ISuperGuideRepository isuperGuideRepository)
    {
        this._ISuperGuideRepository = isuperGuideRepository;
    }

    
    public void Add(User guide, string language)
    {
        SuperGuide superGuide = new SuperGuide(language, guide);
        _ISuperGuideRepository.Add(superGuide);
    }
}