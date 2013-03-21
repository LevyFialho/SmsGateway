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

using SmsGateway.Domain.Seedwork;
namespace Domain.Seedwork.Tests.Classes
{
    
    /// <summary>
    /// Classe exemplo de Objetos de valor, usada para testes
    /// </summary>
    public class Address
        :ValueObject<Address>
    {
        public string StreetLine1 { get; private set; }
        public string StreetLine2 { get; private set; }
        public string City { get; private set; }
        public string ZipCode { get; private set; }

        public Address(string streetLine1, string streetLine2, string city, string zipCode)
        {
            this.StreetLine1 = streetLine1;
            this.StreetLine2 = streetLine2;
            this.City = city;
            this.ZipCode = zipCode;
        }
    }
}
