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

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.AdministradorAgg
{
    /// <summary>
    /// Aplicação do padrão Factory.Seu propósito é encapsular o processo de criação de novos objetos
    /// A fábrica cria objetos transientes, e não está relacionada a persistência de objetos
    /// </summary>
    public static class AdministradorFactory
    {
      
        /// <summary>
        /// Cria um Administrador transiente
        /// </summary>
        /// <param name="nome">Nome do Administrador</param>
        /// <param name="senha">Senha do Administrador</param>
        /// <param name="email">Email do Administrador</param>
        /// <returns>Um Administrador válido</returns>
        public static Administrador Create(string nome, string senha, string email)
        {
            if(string.IsNullOrEmpty(nome))
                throw new  ArgumentNullException("Nome");
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException("senha");
            if (string.IsNullOrEmpty(nome))
                throw new ArgumentNullException("email");
            //if(senha.Length > 8)
            //    throw new ArgumentOutOfRangeException("senha: MAX(8)");

            //criar uma nova instância e setar a identidade
            var admin = new Administrador { Nome = nome, Senha = senha, Email =  email};
            admin.Enable();
            admin.GenerateNewIdentity();

            return admin;
        }
    }
}
