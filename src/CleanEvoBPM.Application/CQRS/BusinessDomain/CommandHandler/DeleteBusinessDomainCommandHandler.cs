using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CleanEvoBPM.Application.Common;
using CleanEvoBPM.Application.CQRS.BusinessDomain.Command;
using CleanEvoBPM.Application.DatabaseServices.Interfaces;
using CleanEvoBPM.Application.Models.ProjectBusinessDomain;
using CleanEvoBPM.Domain;

namespace CleanEvoBPM.Application.CQRS.BusinessDomain.CommandHandler
{
    public class DeleteBusinessDomainCommandHandler : BaseBusinessDomainHandler, MediatR.IRequestHandler<DeleteBusinessDomainCommand, GenericResponse>
    {
        public readonly IGenericDataService<ProjectBusinessDomainModel> _genericDataService;
        private readonly INotificationDispatcher _dispatcher;
        public DeleteBusinessDomainCommandHandler(IBusinessDomainDataService businessDomainDataService,
            IGenericDataService<ProjectBusinessDomainModel> genericDataService,
            INotificationDispatcher dispatcher) : base(businessDomainDataService)
        {
            _genericDataService = genericDataService;
            _dispatcher = dispatcher;
        }

        public async Task<GenericResponse> Handle(DeleteBusinessDomainCommand request, CancellationToken cancellationToken)
        {

            var checkList = await _genericDataService.GetAll(TableName.ProjectBusinessDomain);
            if (checkList.Any(x => x.BusinessDomainId == request.Id))
                return new GenericResponse
                {
                    Code = 400,
                    Success = false,
                    Message = ValidateMessage.DeleteMasterDataFailed
                };
            await _dispatcher.Push(new Event.DeleteBusinessDomainLog(request.Id));
            var deleteResult = await _businessDomainDataService.DeleteBusinessDomain(request);
            if (!deleteResult)
                return new GenericResponse
                {
                    Code = 404,
                    Success = false,
                    Message = ValidateMessage.NotFound
                };
            return new GenericResponse
            {
                Code = 200,
                Success = true,
                Message = ValidateMessage.DeleteSucess
            };
        }
    }
}