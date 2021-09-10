using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTimesheet.DAL.Entities
{
    public class NameKBEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string NameKbOgk { get; set; }
        [Required]
        public string PasswordsKb { get; set; }

        public List<EmployeeEntity> Employees { get; set; } = new();

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrDateInBase { get; set; } = DateTime.Now.Date;
    }
}