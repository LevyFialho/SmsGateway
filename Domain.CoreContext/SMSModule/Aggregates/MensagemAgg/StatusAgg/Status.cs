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

using System.Data.SqlTypes;
using System.Text.RegularExpressions;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;


namespace SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using SmsGateway.Domain.Seedwork;
    using SmsGateway.Domain.CoreContext.Resources;


    /// <summary>
    /// Aggregate root para Status.
    /// Status são códigos retornados pelas operadoras para informar se a mensagem foi enviado com sucesso.
    /// As mensagens podem ter N códigos de status diferentes, a intenção é permitir aos administradores que cadastrem 
    /// dinamicamente no sistema os códigos retornados pelas operado.ras. basicamente um status é composto de
    /// Código retornado, Descrição, Mensagem a Exibir, Saldo a Debitar.
    /// </summary>
    public class Status
        : Entity, IValidatableObject
    {

        #region Members

        bool _ativo;

        #endregion

        #region Properties


        /// <summary>
        /// Codigo retornado pela operadora que identifica o Status
        /// </summary>
        public string Codigo { get; set; }

        /// <summary>
        /// Descrição do Status para os administradores do sistema
        /// </summary>
        public string Descricao { get; set; }

        /// <summary>
        /// Descrição do Status para os clientes do sistema
        /// </summary>
        public string MensagemAoCliente { get; set; }

        /// <summary>
        /// Valor  de créditos a serem debitados do contrato do cliente
        /// </summary>
        public int QuantoDebitarDoContratoDoCliente { get; private set; }
        /// <summary>
        /// Valor de créditos a serem debitados do contrato com a operadora
        /// </summary>
        public int QuantoDebitarDoContratoDaOperadora { get; private set; }

        /// <summary>
        /// Valor em $ da operação realizada.
        /// </summary>
        public double ValorDaOperacao { get; private set; }


        /// <summary>
        /// Tipo de API que a Operadora utiliza
        /// </summary>
        public OperadoraApi OperadoraApi { get; set; }

        /// <summary>
        /// Caso seja verdadeiro, o sistema precisa tentar enviar a mensagem novamente
        /// </summary>
        public bool PrecisaReenviar { get; set; }

        /// <summary>
        /// Caso seja verdadeiro, o sistema precisa enviar por outra Operadora
        /// </summary>
        public bool PrecisaReenviarPorOutraOperadora { get; set; }

        /// <summary>
        /// Caso seja verdadeiro, o sistema precisa atualizar o status da mensagem com a Operadora
        /// </summary>
        public bool PrecisaAtualizar { get; set; }
        
        /// <summary>
        /// True se o status está ativo
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

        /// <summary>
        /// Mensagens associadas a este status
        /// </summary>
        public ICollection<Mensagem> Mensagens { get; set; } 
        #endregion

        #region Public Methods

        /// <summary>
        /// Desativa o status
        /// </summary>
        public void Disable()
        {
            if (IsEnabled)
                this._ativo = false;
        }

        /// <summary>
        /// Ativa o status
        /// </summary>
        public void Enable()
        {
            if (!IsEnabled)
                this._ativo = true;
        }

       

        /// <summary>
        /// Seta quanto de saldo será debitado do contrato da operadora caso a mensagem esteja nesse status
        /// </summary>
        /// <param name="ammount"></param>
        public void SetQuantoDebitarDoContratoDaOperadora(int ammount)
        {
            this.QuantoDebitarDoContratoDaOperadora = ammount;
        }

        /// <summary>
        /// Seta quanto de saldo será debitado do contrato do cliente caso a mensagem esteja nesse status
        /// </summary>
        /// <param name="ammount"></param>
        public void SetQuantoDebitarDoContratoDoCliente(int ammount)
        {
            this.QuantoDebitarDoContratoDoCliente = ammount;
        }

        /// <summary>
        /// Seta o valor total da operação 
        /// </summary>
        /// <param name="ammount"></param>
        public void SetValordaOperacao(double ammount)
        {
            this.ValorDaOperacao = ammount;
        }
        #endregion

        #region IValidatableObject Members


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validationResults = new List<ValidationResult>();

            //-->Check Codigo
            if (String.IsNullOrWhiteSpace(this.Codigo))
            {
                validationResults.Add(new ValidationResult(Messages.validation_CodigoStatus,
                                                           new string[] { "Nome" }));
            }

            //-->Check Descricao
            if (String.IsNullOrWhiteSpace(this.Descricao))
            {
                validationResults.Add(new ValidationResult(Messages.validation_DescricaoStatus,
                                                           new string[] { "Senha" }));
            }

            return validationResults;
        }
      
        #endregion
    }
}
