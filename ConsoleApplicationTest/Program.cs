using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var service = new SmsAgileSoapApi.Service();
            service.Autenticacao("25D90121-7432-C3C6-2B39-08CFED0530B4", "teste");
            var mensagem = new SmsAgileSoapApi.Mensagem()
            {
                TextoDaMensagem = "Teste API",
                Destinatario = 552195389956,
                Remetente = "Levy"
            };

           var result = service.EnviarMensagem(mensagem);
        }
    }
}
