using ClientPatientManagement.Core.Interfaces;
using ClientPatientManagement.Core.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppMvc.Models
{
    public partial class Turno : IEntity
    {
        public int Id { get; set; }
        public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }
        public SharedKernel.Estado Estado { get; set; }
        public DateTime Fecha { get; set; }
        public int IdPaciente { get; set; }
        public int IdSala { get; set; }
        public int IdDoctor { get; set; }
        public int Hora { get; set; }

        public Patient Paciente { get; set; }
        public Room Sala { get; set; }

        public Doctor Doctor { get; set; }
    }

    [MetadataType(typeof(TurnoMetadata))]

    public partial class Turno
    {
        public class TurnoMetadata
        {
            [Key]
            [Column(Order = 1)]
            public int Id { get; set; }

            [ForeignKey("Paciente")]
            [Column(Order = 2)]
            [Required]
            public int IdPaciente { get; set; }

            [ForeignKey("Sala")]
            [Column(Order = 3)]
            [Required]
            public int IdSala { get; set; }

            [ForeignKey("Doctor")]
            [Column(Order = 4)]
            [Required]
            public int IdDoctor { get; set; }

            [Required]
            public DateTime Fecha { get; set; }

            [Required]
            public int Hora { get; set; }

            [Required]
            public SharedKernel.TipoEspecialidad TipoEspecialidad { get; set; }

            [Required]
            public SharedKernel.Estado Estado { get; set; }

        }
    }
}