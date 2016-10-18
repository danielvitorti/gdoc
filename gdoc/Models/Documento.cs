namespace gdoc.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Documento")]
    public partial class Documento
    {
        [Key]
        public long idDocumento { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name ="Nome")]
        public string nomeDocumento { get; set; }

        [Required]
        [StringLength(1)]
        [Display(Name = "Tipo")]
        public string tipoDocumento { get; set; }

        [Required]
        [Display(Name = "Observação")]
        public string observacaoDocumento { get; set; }
        
        public string nomeArquivo { get; set; }

        [Display(Name = "Data de Cadastro")]
        public DateTime dataCadastro { get; set; }

        public Documento()
        {
            this.dataCadastro = System.DateTime.Now;
        }
    }
}
