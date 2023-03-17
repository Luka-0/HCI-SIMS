using System.Collections.Generic;
using InitialProject.Model;
using InitialProject.Repository;

namespace InitialProject.Service;

public class TourKeyPointService
{
    public static List<TourKeyPoint> getAll()
    {
        return TourKeyPointRepository.getAll();
    }
    public static List<TourKeyPoint> getBy(List<string> names)
    {

        return TourKeyPointRepository.getBy(names);
    }

    public  List<TourKeyPoint> save(List<string> keyPointNames)
    {
        return TourKeyPointRepository.save(keyPointNames);
    }
}