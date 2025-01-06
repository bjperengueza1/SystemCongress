using Application.Common;
using Application.Congresses.DTOs;

namespace Application.Congresses.Interfaces;


public interface ICongressService : ICommonService<CongressDto, CongressInsertDto, CongressUpdateDto>
{
    Task<CongressDto> GetByGuidAsync(string guid);
    
}
