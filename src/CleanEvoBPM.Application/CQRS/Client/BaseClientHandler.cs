using CleanEvoBPM.Application.DatabaseServices.Interfaces;

namespace CleanEvoBPM.Application.CQRS.Client
{
    public class BaseClientHandler
    {
        public readonly IClientDataService _clientDataService;
        public BaseClientHandler(IClientDataService clientDataService)
        {
            _clientDataService = clientDataService;
        }

    }
}