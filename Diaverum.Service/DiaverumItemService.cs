using AutoMapper;
using Diaverum.Data;
using Diaverum.Domain;
using Diaverum.Service.CustomeException;
using Microsoft.EntityFrameworkCore;

namespace Diaverum.Service
{
    public interface IDiaverumItemService
    {
        Task<DiaverumItemDTO> AddDiaverumItemAsync(DiaverumItemDTO diaverumItemDto);
        Task<List<DiaverumItemDTO>?> GetDiaverumItemListAsync();
        Task<DiaverumItemDTO> GetDiaverumItemAsync(short diaverumItemId);
        Task<DiaverumItemDTO> UpdateDiaverumItemAsync(DiaverumItemDTO diaverumItemDto);
        Task DeleteDiaverumItemAsync(short diaverumItemId);
    }

    public class DiaverumItemService(
        DiaverumDbContext dbContext,
        IMapper mapper) : IDiaverumItemService
    {
        public async Task<DiaverumItemDTO> AddDiaverumItemAsync(DiaverumItemDTO diaverumItemDto)
        {
            if (diaverumItemDto.EvenNumber % 2 == 0)
            {
                if (diaverumItemDto.DateValue <= DateTime.UtcNow)
                {
                    var dbDiaverumItem = mapper.Map<DiaverumItem>(diaverumItemDto);
                    dbDiaverumItem.CreatedAt = DateTime.UtcNow;
                    dbDiaverumItem.CreatedBy = 1;
                    await dbContext.DiaverumItems.AddAsync(dbDiaverumItem);
                    await dbContext.SaveChangesAsync();

                    dbDiaverumItem = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == dbDiaverumItem.Id);
                    return mapper.Map<DiaverumItemDTO>(dbDiaverumItem);
                }
                else
                {
                    throw new ServiceException(ExceptionType.InvalidRequest, details: $"Given " +
                        $"{nameof(DiaverumItemDTO)} with " +
                        $"{nameof(DiaverumItemDTO.DateValue)} = '{diaverumItemDto.DateValue}' can not be set in the future.");
                }
            }
            else
            {
                throw new ServiceException(ExceptionType.InvalidRequest, details: $"Given " +
                    $"{nameof(DiaverumItemDTO)} with " +
                    $"{nameof(DiaverumItemDTO.EvenNumber)} = '{diaverumItemDto.EvenNumber}' is not an even number.");
            }
        }

        public async Task<List<DiaverumItemDTO>?> GetDiaverumItemListAsync()
        {
            var diaverumItems = await dbContext.DiaverumItems.ToListAsync();
            return diaverumItems.Count != 0 ? mapper.Map<List<DiaverumItemDTO>>(diaverumItems) : null;
        }

        public async Task<DiaverumItemDTO> GetDiaverumItemAsync(short diaverumItemId)
        {
            var dbDiaverumItem = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == diaverumItemId);
            if (dbDiaverumItem != null)
            {
                return mapper.Map<DiaverumItemDTO>(dbDiaverumItem);
            }
            else
            {
                throw new ServiceException(ExceptionType.ItemNotFound, details: $"{nameof(DiaverumItem)} with " +
                    $"{nameof(DiaverumItem.Id)} = '{diaverumItemId}' not found");
            }
        }

        public async Task<DiaverumItemDTO> UpdateDiaverumItemAsync(DiaverumItemDTO diaverumItemDto)
        {
            if (diaverumItemDto.EvenNumber % 2 == 0)
            {
                if (diaverumItemDto.DateValue <= DateTime.UtcNow)
                {
                    var dbDiaverumItem = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == diaverumItemDto.Id);
                    if (dbDiaverumItem != null)
                    {
                        mapper.Map(diaverumItemDto, dbDiaverumItem);
                        dbDiaverumItem.UpdatedAt = DateTime.UtcNow;
                        dbDiaverumItem.UpdatedBy = 1;
                        await dbContext.SaveChangesAsync();

                        dbDiaverumItem = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == diaverumItemDto.Id);
                        return mapper.Map<DiaverumItemDTO>(dbDiaverumItem);
                    }
                    else
                    {
                        throw new ServiceException(ExceptionType.ItemNotFound, details: $"Given " +
                            $"{nameof(DiaverumItemDTO)} with " +
                            $"{nameof(DiaverumItemDTO.Id)} = '{diaverumItemDto.Id}' can not be found.");
                    }
                }
                else
                {
                    throw new ServiceException(ExceptionType.InvalidRequest, details: $"Given " +
                        $"{nameof(DiaverumItemDTO)} with " +
                        $"{nameof(DiaverumItemDTO.DateValue)} = '{diaverumItemDto.DateValue}' can not be set in the future.");
                }
            }
            else
            {
                throw new ServiceException(ExceptionType.InvalidRequest, details: $"Given " +
                    $"{nameof(DiaverumItemDTO)} with " +
                    $"{nameof(DiaverumItemDTO.EvenNumber)} = '{diaverumItemDto.EvenNumber}' is not an even number.");
            }
        }

        public async Task DeleteDiaverumItemAsync(short diaverumItemId)
        {
            var dbDiaverumItem = await dbContext.DiaverumItems.FirstOrDefaultAsync(_ => _.Id == diaverumItemId);
            if (dbDiaverumItem != null)
            {
                dbContext.DiaverumItems.Remove(dbDiaverumItem);
                await dbContext.SaveChangesAsync();
            }
            else
            {
                throw new ServiceException(ExceptionType.ItemNotFound, details: $"Given " +
                    $"{nameof(DiaverumItemDTO)} with " +
                    $"{nameof(DiaverumItemDTO.Id)} = '{diaverumItemId}' can not be found.");
            }
        }
    }
}
