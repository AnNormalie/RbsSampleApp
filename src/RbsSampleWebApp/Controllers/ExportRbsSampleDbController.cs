using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RbsSampleWebApp.Data;

namespace RbsSampleWebApp
{
    public partial class ExportRbsSampleDbController : ExportController
    {
        private readonly RbsSampleDbContext context;
        private readonly RbsSampleDbService service;
        public ExportRbsSampleDbController(RbsSampleDbContext context, RbsSampleDbService service)
        {
            this.service = service;
            this.context = context;
        }

        [HttpGet("/export/RbsSampleDb/languages/csv")]
        [HttpGet("/export/RbsSampleDb/languages/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLanguagesToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLanguages(), Request.Query), fileName);
        }

        [HttpGet("/export/RbsSampleDb/languages/excel")]
        [HttpGet("/export/RbsSampleDb/languages/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLanguagesToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLanguages(), Request.Query), fileName);
        }
        [HttpGet("/export/RbsSampleDb/locations/csv")]
        [HttpGet("/export/RbsSampleDb/locations/csv(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLocationsToCSV(string fileName = null)
        {
            return ToCSV(ApplyQuery(await service.GetLocations(), Request.Query), fileName);
        }

        [HttpGet("/export/RbsSampleDb/locations/excel")]
        [HttpGet("/export/RbsSampleDb/locations/excel(fileName='{fileName}')")]
        public async System.Threading.Tasks.Task<FileStreamResult> ExportLocationsToExcel(string fileName = null)
        {
            return ToExcel(ApplyQuery(await service.GetLocations(), Request.Query), fileName);
        }
    }
}
