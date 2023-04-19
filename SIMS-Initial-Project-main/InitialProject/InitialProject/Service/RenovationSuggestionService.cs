using InitialProject.Interface;
using InitialProject.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InitialProject.Service
{
    class RenovationSuggestionService
    {
        private readonly IRenovationSuggestionRepository IRenovationSuggestionRepository;

        public RenovationSuggestionService(IRenovationSuggestionRepository iRenovationSuggestionRepository)
        {
            IRenovationSuggestionRepository = iRenovationSuggestionRepository;
        }

        public void Save(RenovationSuggestion renovationSuggestion)
        {
            IRenovationSuggestionRepository.Save(renovationSuggestion);
        }

    }
}
