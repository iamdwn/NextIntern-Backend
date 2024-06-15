﻿using MediatR;
using SWD.NextIntern.Repository.Entities;
using SWD.NextIntern.Repository.Repositories;
using SWD.NextIntern.Repository.Repositories.IRepositories;
using SWD.NextIntern.Service.DTOs.Responses;
using System.Net;

namespace SWD.NextIntern.Service.Services.InvernEvaluationService.Create
{
    public class CreateInternEvaluationCommandHandler : IRequestHandler<CreateInternEvaluationCommand, ResponseObject<string>>
    {
        private readonly IInternEvaluationRepository _internEvaluationRepository;
        private readonly IUniversityRepository _universityRepository;
        private readonly ICampaignEvaluationRepository _campaignEvaluationRepository;

        public CreateInternEvaluationCommandHandler(IInternEvaluationRepository internEvaluationRepository, IUniversityRepository universityRepository, ICampaignEvaluationRepository campaignEvaluationRepository)
        {
            _internEvaluationRepository = internEvaluationRepository;
            _universityRepository = universityRepository;
            _campaignEvaluationRepository = campaignEvaluationRepository;
        }

        public async Task<ResponseObject<string>> Handle(CreateInternEvaluationCommand request, CancellationToken cancellationToken)
        {
            var intern = await _universityRepository.FindAsync(u => u.UniversityId.ToString().Equals(request.InternId) && u.DeletedDate == null, cancellationToken);

            if (intern == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Intern with id {request.InternId} does not exist!");
            }

            var campaignEvaluation = await _campaignEvaluationRepository.FindAsync(ce => ce.CampaignEvaluationId.ToString().Equals(request.CampaignEvaluationId) && ce.DeletedDate == null, cancellationToken);

            if (campaignEvaluation == null)
            {
                return new ResponseObject<string>(System.Net.HttpStatusCode.NotFound, $"Campaign Evaluation with id {request.CampaignEvaluationId} does not exist!");
            }

            var internEvaluation = new InternEvaluation
            {
                CampaignEvaluationId = Guid.Parse(request.CampaignEvaluationId),
                InternId = Guid.Parse(request.InternId),
                Feedback = request.Feedback,
            };

            _internEvaluationRepository.Add(internEvaluation);
            return await _internEvaluationRepository.UnitOfWork.SaveChangesAsync(cancellationToken) > 0 ? new ResponseObject<string>(HttpStatusCode.Created, "Success!") : new ResponseObject<string>(HttpStatusCode.BadRequest, "Fail!");
        }
    }
}
