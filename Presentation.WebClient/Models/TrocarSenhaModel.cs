using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SmsGateway.Presentation.WebClient.Models
{
    public class TrocarSenhaModel
    {
        
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string SenhaAtual { get; set; }
        
        
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        public string SenhaNova { get; set; }
        
        
        [Required(ErrorMessage = "*")]
        [DataType(DataType.Password)]
        [Compare("SenhaNova", ErrorMessage = "*")]
        public string SenhaConfirmada { get; set; }

         
    }
}