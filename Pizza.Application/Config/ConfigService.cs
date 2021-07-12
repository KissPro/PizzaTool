using Microsoft.EntityFrameworkCore;
using Pizza.Data.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pizza.Application.Config
{
    public class ConfigService : IConfigService
    {
        private readonly PizzaContext _context;

        public ConfigService(PizzaContext context)
  {
            _context = context;
        }

        public async Task<bool> AddConfig(TblDropList config)
        {
            var check = _context.TblDropList.FirstOrDefault(x => x.Id == config.Id);
            if (check == null)
            {
                _context.TblDropList.Add(config);
            }
            else
            {
                check.Name = config.Name;
                check.Value = config.Value;
                check.UpdatedBy = config.UpdatedBy;
                check.DropListRemark = config.DropListRemark;
                check.UpdateDate = DateTime.Now;
            }
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<TblDropList>> GetAll()
        {
            var res = await _context.TblDropList.ToListAsync();
            return res;
        }

        public async Task<List<TblDropList>> GetDropListByName(string name)
        {
            var res = await _context.TblDropList.Where(x => x.Name == name).ToListAsync();
            return res;
        }

        public async Task<bool> RemoveConfig(Guid id)
        {
            var check = _context.TblDropList.FirstOrDefault(x => x.Id == id);
            if (check != null)
            {
                _context.TblDropList.Remove(check);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
