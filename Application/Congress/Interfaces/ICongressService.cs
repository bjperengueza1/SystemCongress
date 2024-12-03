using Application.Common;
using Application.Congress.DTOs;

namespace Application.Congress.Interfaces;


public interface ICongressService : ICommonService<CongressDto, CongressInsertDto, CongressUpdateDto>
{
    
}
