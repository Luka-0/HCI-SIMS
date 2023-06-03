using System.Runtime.CompilerServices;
using InitialProject.Repository;
using InitialProject.Service;

namespace InitialProject.Controller;

public class SuperGuideController
{
    private readonly SuperGuideService superGuideService = new SuperGuideService(new SuperGuideRepository());
}