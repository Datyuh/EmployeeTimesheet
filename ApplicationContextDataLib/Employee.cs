using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationContextData
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Fio { get; set; }
        [Required]
        public string StatusUsers { get; set; }
        [Required]
        public int ServiceNumbers { get; set; }

        public int NameKbId { get; set; }
        [ForeignKey("NameKbId")]
        public NameKB NameKbs { get; set; }

        public ICollection<EmployeeTimesheet> EmployeeTimesheets { get; set; }


        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrDateInBase { get; set; } = DateTime.Now.Date;
    }
}