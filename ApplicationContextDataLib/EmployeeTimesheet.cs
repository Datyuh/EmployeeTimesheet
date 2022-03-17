﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationContextData
{
    public class EmployeeTimesheet
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
        public Employee Employees { get; set; }

        [MaxLength(20)]
        public string NumOrder { get; set; }
        [DataType(DataType.Date)]
        public DateTime? DateOrder { get; set; }
        public string StatusOrder { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd'/'MM'/'yyyy}", ApplyFormatInEditMode = true)]
        public DateTime RegistrDateInBase { get; set; } = DateTime.Now;
    }
}