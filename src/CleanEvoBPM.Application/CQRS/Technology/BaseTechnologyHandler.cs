using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.CQRS.Technology
{
    public class BaseTechnologyHandler
    {
        public readonly ITechnologyDataService _technologyDataService;
        public BaseTechnologyHandler(ITechnologyDataService technologyDataService)
        {
            _technologyDataService = technologyDataService;
        }
    }
}
