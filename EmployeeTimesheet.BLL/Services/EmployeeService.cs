using System.Collections.Generic;
using AutoMapper;
using EmployeeTimesheet.BLL.DTO;
using EmployeeTimesheet.BLL.Infrastructure;
using EmployeeTimesheet.BLL.Interfaces;
using EmployeeTimesheet.DAL.Entities;
using EmployeeTimesheet.DAL.Interfaces;

namespace EmployeeTimesheet.BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        IUnitOfWork Database { get; set; }

        public EmployeeService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public void MakeEmployee(EmployeeDTO employeeDto)
        {
            NameKBEntity nameKbEntity = Database.NameKBEntitys.Get(employeeDto.NameKbId);

            // валидация
            if (nameKbEntity == null)
                throw new ValidationException("Телефон не найден", "");

        }

        public IEnumerable<NameKBDTO> GetNameKBs()
        {
            // применяем автомаппер для проекции одной коллекции на другую
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<NameKBEntity, NameKBDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<NameKBEntity>, List<NameKBDTO>>(Database.NameKBEntitys.GetAll());
        }

        public NameKBDTO GetNameKB(int? id)
        {
            if (id == null)
                throw new ValidationException("Не установлено id телефона", "");
            var nameKbEntity = Database.NameKBEntitys.Get(id.Value);
            if (nameKbEntity == null)
                throw new ValidationException("Телефон не найден", "");

            return new NameKBDTO
            {
                NameKbOgk = nameKbEntity.NameKbOgk,
                Id = nameKbEntity.Id,
                PasswordsKb = nameKbEntity.PasswordsKb,
                RegistrDateInBase = nameKbEntity.RegistrDateInBase
            };
        }

        public void Dispose()
        {
            Database.Dispose();
        }
    }
}
