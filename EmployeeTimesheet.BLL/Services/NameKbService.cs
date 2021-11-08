using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using EmployeeTimesheet.BLL.DTO;
using EmployeeTimesheet.BLL.Interfaces;
using EmployeeTimesheet.DAL.Entities;
using EmployeeTimesheet.DAL.Interfaces;

namespace EmployeeTimesheet.BLL.Services
{
    public class NameKbService : INameKbService
    {
        IUnitOfWork Database { get; set; }

        public NameKbService(IUnitOfWork uow)
        {
            Database = uow;
        }

        public IEnumerable<NameKBDTO> GetAllName()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NameKBEntity, NameKBDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<NameKBEntity>, List<NameKBDTO>>(Database.NameKBEntitys.GetAll());
        }

       public bool NameKbPasCheck(string nameKb)
        {
            var nameKbs = Database.NameKBEntitys.GetAll();
            return nameKbs.Select(p => p.PasswordsKb).Contains(nameKb);
        }

       public void Dispose()
       {
           Database.Dispose();
        }
    }
}
