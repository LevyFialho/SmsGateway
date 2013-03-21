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

using System.Linq;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContatoAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmsGateway.Domain.Seedwork;
    using SmsGateway.Domain.CoreContext.Resources;

    /// <summary>
    /// Raiz do Aggregate Contatos.
    /// </summary>
    public class ListaDeContatos
        : Entity, IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

        #region Properties


        /// <summary>da Listado Contato
        /// </summary>
        public string Nome { get; set; }

         
        /// <summary>
        /// Id do cliente  
        /// </summary>
        public virtual Guid ClienteId { get; set; }

        /// <summary>
        /// Cliente  
        /// </summary>
        public virtual Cliente Cliente { get; set; }
        
        public virtual ICollection<Contato> Contatos { get; set; }

        /// <summary>
        /// True se está ativo
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _ativo;
            }
            private set
            {
                _ativo = value;
            }
        }


        #endregion

        public ListaDeContatos()
        {
            Contatos = new List<Contato>();
        }

        #region Public Methods

        /// <summary>
        /// Desativa  
        /// </summary>
        public void Disable()
        {
          this._ativo = false;
        }

        /// <summary>
        /// Ativa 
        /// </summary>
        public void Enable()
        {
           this._ativo = true;
        }

        /// <summary>
        /// Associa um cliente ao Contato.
        /// </summary>
        /// <param name="contrato"></param>
        public void SetarCliente(Cliente cliente)
        {
            if (cliente == null
                ||
                cliente.IsTransient() || !cliente.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_SetarClienteDocontato);
            }

            //fix relation
            this.ClienteId = cliente.Id;

            this.Cliente = cliente;
        }

        /// <summary>
        /// Setar a referência do contrato atual deste Contato
        /// </summary>
        /// <param name="contractId"></param>
        public void SetTheCurrentClientReference(Guid clientId)
        {
            if (clientId != Guid.Empty)
            {
                //fix relation
                this.ClienteId = clientId;

                this.Cliente = null;
            }
        }

        #endregion

        #region IValidatableObject Members


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check Nome
            if (String.IsNullOrWhiteSpace(this.Nome))
            {
                validationResults.Add(new ValidationResult(Messages.validation_NomeContato,
                                                           new string[] { "Nome" }));
            }

            

            if (ClienteId == Guid.Empty)
            {
                validationResults.Add(new ValidationResult(Messages.validation_ClienteContato,
                                                           new string[] { "Senha" }));
            }

            return validationResults;
        }

        #endregion
    }


    
}
