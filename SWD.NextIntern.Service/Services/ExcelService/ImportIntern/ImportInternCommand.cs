﻿using MediatR;
using Microsoft.AspNetCore.Http;
using SWD.NextIntern.Service.Common.Interfaces;
using SWD.NextIntern.Service.DTOs.Responses;

namespace SWD.NextIntern.Service.Services.ExcelService.ImportIntern
{
    public class ImportInternCommand : IRequest<ResponseObject<string>>, IQuery
    {
        public IFormFile File { get; set; }

        public ImportInternCommand()
        {
        }
    }
}
