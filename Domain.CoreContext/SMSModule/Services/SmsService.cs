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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.ContratoAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.MensagemAgg.StatusAgg;
using SmsGateway.Domain.CoreContext.SMSModule.Aggregates.OperadoraAgg;

namespace SmsGateway.Domain.CoreContext.SMSModule.Services
{
    /// <summary>
    /// Serviço usado para envio de mensagens SMS
    /// </summary>
    public class SmsService : ISmsService
    {
        //Lista de contratos disponíveis com as operadoras. (Dependency Injection)
        List<Contrato> _contratosDisponiveisParaEnvio;
        //Contrato selecionado a partir da lista, utilizado para envioo.
        Contrato _contratoSelecionado;
        //Lista de status disponíveis  (Dependency Injection)
        List<Status> _statusList;
        //Factory de operadoras
        OperadoraFactory _operadoraFactory;


        public SmsService(List<Contrato> contratosDasOperadoras,
            List<Status> statusList,  OperadoraFactory operadoraFactory)
        {
            if (contratosDasOperadoras != null)
                _contratosDisponiveisParaEnvio = contratosDasOperadoras;
            else throw new ArgumentNullException("contratosDasOperadoras");
            if (contratosDasOperadoras.Count == 0)
                throw new Exception("Nenhum contrato disponivel com operadoras");

            if (statusList != null)
                _statusList = statusList;
            else throw new ArgumentNullException("statusList");
            if (statusList.Count == 0)
                throw new Exception("Nenhum status disponivel.");
            
            if (operadoraFactory != null)
                _operadoraFactory = operadoraFactory;
            else
                throw new ArgumentNullException("operadoraFactory");
             
            
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="contratoDoCliente">Contrato do cliente usado para autenticação e créditos</param>
        /// <param name="contratosDasOperadoras">Lista de Contratos que podem ser usados para envio de mensagem</param>
        /// <param name="statusList">Lista de status disponiveis no sistema</param>
        /// <param name="mensagem">mensagem a ser enviada</param>
        public Mensagem EnviarMensagem(Contrato contratoDoCliente, Mensagem mensagem)
        {
            
            
            //Validar contrato do ciente
            if (!contratoDoCliente.Validar())
                throw new Exception("Contrato do cliente inválido. Verifique o saldo.");
            //Validar mensagem
            if (!mensagem.Validar())
                throw new Exception("Mensagem inválida.");
            

           //percorrer lista e verificar melhor opção de contrato a ser usado para eniviar a mensagem
            SelecionarMelhorContrato(ref mensagem, false);
            //Atualizar referência do contrato usado apra envio na mensagem.
            mensagem.SetarContratoDaOperadora(_contratoSelecionado);
            //Pega a implementação da operadora de acordo com o contrato
            var operadora = Operadora(_contratoSelecionado.OperadoraApi);
            //Enviar mensagem a partir do contrato selecionado
            var status = operadora.EnviarMensagem(mensagem);
            //Checar status da mensagem, debitar ou reenviar se necessário
            ChecarEnvio(ref contratoDoCliente, ref mensagem, status);

            //retornar mensagem  (na aplicação, o metodo dee tratar o status para retornar para o cliente uma vez que o DTO nao conhece o status)

            return mensagem;


        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="contratoDoCliente">Contrato do cliente usado para autenticação e créditos</param>
        /// <param name="contratosDasOperadoras">Lista de Contratos que podem ser usados para envio de mensagem</param>
        /// <param name="statusList">Lista de status disponiveis no sistema</param>
        /// <param name="mensagens">mensagens a serem enviadas</param>
        public List<Mensagem> EnviarMensagens(Contrato contratoDoCliente, List<Contrato> contratosDasOperadoras, List<Status> statusList, List<Mensagem> mensagens)
        {
            var retorno = new List<Mensagem>();
            foreach (var thread in mensagens.Select(local => new Thread(t => retorno.Add(EnviarMensagem(contratoDoCliente,  local))) { IsBackground = true }))
            {
                thread.Start();
            }
            return retorno;
        }

        /// <summary>
        /// Atualiza os daddos da mensagem e do contrato do cliente de acordo com o status
        /// </summary>
        /// <param name="contratoDoCliente">Contrato do Cliente</param>
        /// <param name="mensagem">Mensagem</param>
        /// <param name="codigoStatus">Código do status</param>
        private void ChecarEnvio(ref Contrato contratoDoCliente, ref Mensagem mensagem, string codigoStatus)
        {

            DebitarCreditos(ref contratoDoCliente, ref mensagem, codigoStatus);

            if (mensagem.Status.PrecisaReenviarPorOutraOperadora)
            {
                SelecionarMelhorContrato(ref mensagem, true);
                mensagem.SetarContratoDaOperadora(_contratoSelecionado);
                //Pega a implementação da operadora de acordo com o contrato
                var operadora = Operadora(_contratoSelecionado.OperadoraApi);
                //Enviar mensagem a partir do contrato selecionado
                var status = operadora.EnviarMensagem(mensagem);

                DebitarCreditos(ref contratoDoCliente, ref mensagem, status);

            }
            else if (mensagem.Status.PrecisaReenviar)
            {
                //Pega a implementação da operadora de acordo com o contrato
                var operadora = Operadora(_contratoSelecionado.OperadoraApi);
                //Enviar mensagem a partir do contrato selecionado
                var status = operadora.EnviarMensagem(mensagem);
                DebitarCreditos(ref contratoDoCliente, ref mensagem, status);

            }
        }

        /// <summary>
        /// Atualiza o saldo do contrato do cliente e do contrato com a operadora que efetuou a operação
        /// </summary>
        /// <param name="contratoDoCliente"></param>
        /// <param name="mensagem"></param>
        private void DebitarCreditos(ref Contrato contratoDoCliente, ref Mensagem mensagem, string codigoStatus)
        {
            //Atualizar status
            bool encontrouStatus = false;

            foreach (var status in _statusList)
            {
                if (status.Codigo == codigoStatus)
                {
                    encontrouStatus = true;
                    mensagem.SetarStatus(status);
                }
            }
            if (!encontrouStatus)
                throw new Exception("Status code not found");
            //Se necessário, debitar creditos do contrato usado para envio
            _contratoSelecionado.DebitarSaldoDeMensagens(mensagem.Status.QuantoDebitarDoContratoDaOperadora);
            //Se necessário, debitar créditos do contrato do cliente
            contratoDoCliente.DebitarSaldoDeMensagens(mensagem.Status.QuantoDebitarDoContratoDoCliente);
        }

        /// <summary>
        /// Selecion na lista de contratos disponíveis o melhor contrato para enviar a mensagem
        /// </summary>
        /// <param name="mensagem"></param>
        /// <param name="usarOutraOperadoraApi"></param>
        private void SelecionarMelhorContrato(ref Mensagem mensagem, bool usarOutraOperadoraApi)
        {
            foreach (var contrato in _contratosDisponiveisParaEnvio)
            {
                //Caso seja a primeira tentativa, pega o primeiro contrato válido disponivel
                if ((_contratoSelecionado == null) & (contrato.Validar()))
                    _contratoSelecionado = contrato;
                else if (_contratoSelecionado != null)
                {
                    //Se for um contrato válido diferente do selecionado, analisar pra ver se vale a pena trocar
                    if ((contrato.Id != _contratoSelecionado.Id) & (contrato.Validar()))
                    {
                        //Se for necessário mudar de operadora..
                        if (usarOutraOperadoraApi)
                        {
                            if (contrato.OperadoraApi != _contratoSelecionado.OperadoraApi)
                            {
                                _contratoSelecionado = contrato;
                                return;
                            }
                        }
                        //Se o preço for menor
                        else if (contrato.ValorMensagem < _contratoSelecionado.ValorMensagem)
                            _contratoSelecionado = contrato;
                        //Se o saldo de mensagens for muito superior 
                        else if (contrato.SaldoDeMensagens >= (_contratoSelecionado.SaldoDeMensagens * 3))
                            _contratoSelecionado = contrato;


                    }
                }
            }

        }

        /// <summary>
        /// Retorna uma interface para envio de mensagens através do tipo de Api do contrato
        /// </summary>
        private IOperadora Operadora(OperadoraApi api)
        {
            return _operadoraFactory.Create(api);
        }
    }
}
