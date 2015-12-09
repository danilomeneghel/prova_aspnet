using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Models
{
    public class UsuarioModel
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("Nome")]
        [StringLength(50, ErrorMessage = "O campo Nome permite no máximo 50 caracteres!")]	  
        public string Nome { get; set; }
        [StringLength(50)]
        [Required(ErrorMessage = "Informe o Email")]
        [RegularExpression(@"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Email inválido.")]
        public string Email { get; set; }
        [Required]
        public string Telefone { get; set; }
        [Required]
        public string Senha { get; set; }
    }
}