using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeTimesheet.DAL.Entities
{
    public class EmployeeTimesheetEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public DateTime DateTimeAddData { get; set; }

        public int EmployeesId { get; set; }
        [ForeignKey("EmployeesId")]
        public EmployeeEntity Employees { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrDateInBase { get; set; } = DateTime.Now;
    }
}
