using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.CQRS.Status
{
    public class BaseStatusHandler
    {
        public readonly IStatusDataService _statusDataService;

        public BaseStatusHandler(IStatusDataService statusDataService)
        {
            _statusDataService = statusDataService;
        }
    }
}