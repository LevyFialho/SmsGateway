//=================================================================================== 
// INSTITUTO INFNET - GRADUAÇÃO EM ANÁLISE E DESENVOLVIMENTO DE SISTEMAS
// TRABALHO DE CONCLUSÃO DO CURSO
// AUTORES:
// JAIR MARTINS
// LEVY FIALHO
// MARCELO SÁ
//===================================================================================
// Este código foi desenvolvido com o objetivo de demonstrar a aplicação prática de 
// padrões de desenvolvimento de software adotados no mercado no ano de 2012.

// Mais especificamente, o código demonstra a aplicação prática de conceitos abordados
// em Domain driven Design e Patterns of Application Architechture na plataforma .Net
//===================================================================================

using System;

namespace SmsGateway.Application.CoreContext.DTO.SMSModule
{
    /// <summary>
    /// Data transfer object para uma entidade
    /// </summary>
    public class PacoteDTO
    {
      
        public Guid Id { get; set; }

        public string Nome { get; set; }

        public int QuantidadeDeMensagens { get; set; }

        public DateTime DataDeVencimento { get; set; }

        public double ValorCobradoPorMensagem { get; set; }

        public bool GratuitoAoNovoCliente { get; set; }

        public bool IsEnabled { get; set; }


        
    }
}
