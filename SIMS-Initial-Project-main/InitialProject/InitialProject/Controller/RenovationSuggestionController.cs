using InitialProject.Interface;
using InitialProject.Model;
using InitialProject.Repository;
using InitialProject.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Controller
{
    public class RenovationSuggestionController
    {
        private readonly RenovationSuggestionService RenovationSuggestionService = new (new RenovationSuggestionRepository());

        public void Save(RenovationSuggestion renovationSuggestion)
        {
            RenovationSuggestionService.Save(renovationSuggestion);
        }

    }
}
