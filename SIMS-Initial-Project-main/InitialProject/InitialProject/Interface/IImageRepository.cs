using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    public interface IImageRepository
    {
        public void Save(Image image);

        public void Update(Image image, Tour tour);

    }
}
