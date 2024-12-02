namespace Application.Common;

public interface ICommonService<T,TI>
{
    Task<T> CreateAsync(TI ti); 
}