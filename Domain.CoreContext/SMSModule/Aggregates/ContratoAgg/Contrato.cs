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


using System.Collections;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ClienteAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Seedwork;
    using Resources;
    
    /// <summary>
    /// Entidade raiz do aggregate Contratos
    /// </summary>
    public class Contrato
        :Entity,IValidatableObject
    {

        #region Members

        bool _Ativo;
        IOperadora _operadora;
        #endregion

        #region Properties

        
        /// <summary>
        /// Data em que o contrato entra em vigor
        /// </summary>
        public DateTime DataInicial { get; set; }

        /// <summary>
        /// Data em que o contrato termina
        /// </summary>
        public DateTime DataFinal { get; set; }

        /// <summary>
        /// Quantidade total de mensagens que o contrato permite enviar
        /// </summary>
        public int SaldoDeMensagens { get; private set; }

        /// <summary>
        /// Valor cobrado por mesnsagem enviada a aprtir deste contrato
        /// </summary>
        public double ValorMensagem { get; private set; }

        /// <summary>
        /// Id do contrato associado a este contrato quando este é renovado
        /// </summary>
        public Guid? ContratoRenovadoId { get; private set; }

        /// <summary>
        /// Caso o contrato tenha sido renovado, retorna a instancia do novo contrato
        /// </summary>
        public virtual Contrato ContratoRenovado { get; private set; }

        /// <summary>
        /// Id do cliente associado a este contrato
        /// </summary>
        public Guid? ClienteId { get; private set; }

        /// <summary>
        /// Cliente ao qual o contrato pertence
        /// </summary>
        public virtual Cliente Cliente { get; private set; }
        
        /// <summary>
        /// Tipo de API da Operadora 
        /// </summary>
        public OperadoraApi OperadoraApi { get; set; }
      
        /// <summary>
        /// Define se é um contrato de cliente ou operadoras
        /// </summary>
        public TipoDeContrato Tipo { get; set; }

        /// <summary>
        /// Get or set if this customer is enabled
        /// </summary>
        public bool IsEnabled
        {
            get
            {
                return _Ativo;
            }
            private set
            {
                _Ativo = value;
            }
        }

       

        /// <summary>
        /// Mensagens associadas a este contrato
        /// </summary>
        public virtual ICollection<Mensagem> MensagensEnviadas { get; set; }

        /// <summary>
        /// Mensagens associadas a este contrato
        /// </summary>
        public virtual ICollection<Mensagem> MensagensDoCliente { get; set; }
        #endregion

        #region Public Methods

        /// <summary>
        /// Disable customer
        /// </summary>
        public void Disable()
        {
            if ( IsEnabled)
                this._Ativo = false;
        }

        /// <summary>
        /// Enable customer
        /// </summary>
        public void Enable()
        {
            if( !IsEnabled)
                this._Ativo = true;
        }

        /// <summary>
        /// Verifica se a mensagem está em um status válido para envio
        /// </summary>
        /// <returns></returns>
        public bool Validar()
        {
            if (!IsEnabled)
                return false;
            else if (DataFinal < System.DateTime.Now)
                return false;
            else if (DataInicial > System.DateTime.Now)
                return false;
            else if (SaldoDeMensagens <= 0)
                return false;
            else return true;

        }

        /// <summary>
        /// Associar o contrato a um cliente
        /// </summary>
        /// <param name="cliente"></param>
        public void SetarCliente(Cliente cliente)
        {
            if (cliente == null
                ||
                cliente.IsTransient())
            {
                throw new ArgumentException(Messages.exception_CannotAssociateTransientOrNullEntity);
            }

            //fix relation
            this.ClienteId = cliente.Id;

            this.Cliente = cliente;
        }

        /// <summary>
        /// Seta a referencia do contrato a um cliente
        /// </summary>
        /// <param name="clienteId"></param>
        public void SetTheClientReference(Guid clienteId)
        {
            if (clienteId != Guid.Empty)
            {
                //fix relation
                this.ClienteId = clienteId;

                this.Cliente = null;
            }
        }
        
        /// <summary>
        /// Associar o contrato a um novo contrato (Renovação)
        /// </summary>
        /// <param name="novoContrato"></param>
        public void RenovarContrato(Contrato novoContrato)
        {
            if (novoContrato == null
                ||
                novoContrato.IsTransient() || !novoContrato.IsEnabled)
            {
                throw new ArgumentException(Messages.exception_RenovacaoContrato);
            }

            //fix relation
            this.ContratoRenovadoId = novoContrato.Id;

            this.ContratoRenovado = novoContrato;

            this.Disable();
        }

        /// <summary>
        /// Seta a quantidade total de mensagens que podem ser enviadas a partir deste contrato
        /// </summary>
        /// <param name="ammount">Novo limite de mensagens</param>
        public void SetarSaldoDeMensagens(int ammount)
        {
            this.SaldoDeMensagens = ammount;
        }

        /// <summary>
        /// Altera a quantidade total de mensagens que podem ser enviadas a partir deste contrato
        /// </summary>
        /// <param name="ammount">Valor a ser debitado ou somado</param>
        public void AlterarSaldoDeMensagens(int ammount)
        {
           this.SaldoDeMensagens += ammount;
        }

        /// <summary>
        /// Altera a quantidade total de mensagens que podem ser enviadas a partir deste contrato
        /// </summary>
        /// <param name="ammount">Valor a ser debitado</param>
        public void DebitarSaldoDeMensagens(int ammount)
        {
            if (ammount < 0) ammount = ammount*-1;
            this.SaldoDeMensagens -= ammount;
        }
        
        /// <summary>
        /// Seta o valor cobrado por mensagem enviada a partir deste contrato
        /// </summary>
        /// <param name="ammount">Novo valor por mensagem</param>
        public void SetarValorCobradoPorMensagem(double ammount)
        {
            this.ValorMensagem = ammount;
        }

        /// <summary>
        /// Altera o valor cobrado por mensagem enviada a partir deste contrato
        /// </summary>
        /// <param name="ammount">Novo valor por mensagem</param>
        public void AlterarValorCobradoPorMensagem(double ammount)
        {
            this.ValorMensagem += ammount;
        }

        #endregion

        #region IValidatableObject Members

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            
            return validationResults;
        }

        #endregion
    }

    public enum TipoDeContrato
    {
        Cliente,
        Operadora
    }
}
