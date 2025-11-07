using AutoMapper;
using Diaverum.Data;
using Diaverum.Domain;
using Diaverum.Service.CustomeException;
using Microsoft.EntityFrameworkCore;

namespace Diaverum.Service
{
    public interface ILabResultService
    {
        Task SubmitLabSersult(string content);
        Task<List<LabResultDTO>> GetLabResult(string? clinicNo, int? patientId);
    }

    public class LabResultService(
        DiaverumDbContext dbContext,
        IMapper mapper) : ILabResultService
    {
        public async Task SubmitLabSersult(string labResultsContent)
        {
            var contentRows = labResultsContent.Split('\n').ToList();
            var headResultRowIndex = contentRows.FindIndex(_ => _.Contains('|'));
            var headValues = headResultRowIndex != -1 ? (contentRows[headResultRowIndex] ?? "").Split('|') : [];

            if (headValues.Length > 0)
            {
                var results = contentRows.Count - (headResultRowIndex + 1);
                var resultRows = contentRows.TakeLast(results).ToList();
                if (resultRows.Count > 0)
                {
                    var labResults = new List<LabResult>();

                    foreach (var resultRow in resultRows)
                    {
                        var resultValues = resultRow.Split('|');
                        if (resultValues.Length == headValues.Length)
                        {
                            var labResult = mapper.Map<LabResult>(resultValues);
                            labResult.CreatedBy = 1;
                            labResults.Add(labResult);
                        }
                        else
                        {
                            throw new ServiceException(ExceptionType.InvalidLabResult, details: $"Lab result '{resultRow}' is invalid");
                        }
                    }

                    await dbContext.LabResults.AddRangeAsync(labResults);
                    await dbContext.SaveChangesAsync();
                }
                else
                {
                    throw new ServiceException(ExceptionType.InvalidLabResult, details: $"The given {nameof(labResultsContent)} has no results");
                }
            } 
            else
            {
                throw new ServiceException(ExceptionType.InvalidLabResult, details: $"The given {nameof(labResultsContent)} is invalid");
            }
        }

        public async Task<List<LabResultDTO>> GetLabResult(string? clinicNo, int? patientId)
        {
            var labResults = await dbContext.LabResults
                .Where(_ =>
                    (clinicNo == null || _.ClinicNo == clinicNo) &&
                    (patientId == null || _.PatientId == patientId))
                .ToListAsync();

            return mapper.Map<List<LabResultDTO>>(labResults);
        }
    }
}
