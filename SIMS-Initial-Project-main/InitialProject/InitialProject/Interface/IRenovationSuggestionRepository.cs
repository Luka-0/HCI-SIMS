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

    }
}
