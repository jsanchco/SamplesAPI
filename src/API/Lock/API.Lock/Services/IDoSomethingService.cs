using API.Lock.Models;
using Shared.Model.Response;
using System.Threading.Tasks;

namespace API.Lock.Services
{
    public interface IDoSomethingService
    {
        ResponseResult<DoSomethingDTO> DoSomething();
        Task<ResponseResult<DoSomethingDTO>> DoSomethingAsync();
    }
}
