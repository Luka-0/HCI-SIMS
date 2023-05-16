using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Interface
{
    interface IRenovationSuggestionRepository
    {
        public void Save(RenovationSuggestion renovationSuggestion);
        public int GetCountBy(int year, Accommodation accommodation);
        public int GetCountBy(int year, int month, Accommodation accommodation);

    }
}
