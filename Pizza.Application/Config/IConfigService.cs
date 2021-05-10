using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Config
{
    public interface IConfigService
    {
        Task<List<TblDropList>> GetDropListByName(string name);
        Task<List<TblDropList>> GetAll();

        Task<bool> AddConfig(TblDropList dropList);

        Task<bool> RemoveConfig(Guid id);
    }
}
