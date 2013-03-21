﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmsGateway.Infrastructure.Operadoras.OperadoraTwwServiceReference {
    using System.Data;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Name="ReluzCap Web ServiceSoap", Namespace="https://www.twwwireless.com.br/reluzcap/wsreluzcap", ConfigurationName="OperadoraTwwServiceReference.ReluzCapWebServiceSoap")]
    public interface ReluzCapWebServiceSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMS(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS2SN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMS2SN(string NumUsu, string Senha, string SeuNum1, string SeuNum2, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMS2SN", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSQuebra", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSQuebra(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSQuebra", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAlt", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSAlt(string user, string pwd, string msgid, string phone, string msgtext);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAlt", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSAltAsync(string user, string pwd, string msgid, string phone, string msgtext);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAge", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSAge(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAge", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSAgeAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAgeQuebra", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSAgeQuebra(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSAgeQuebra", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSAgeQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSDataSet(string NumUsu, string Senha, System.Data.DataSet DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSDataSetAsync(string NumUsu, string Senha, System.Data.DataSet DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSXML", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSXML(string NumUsu, string Senha, string StrXML);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSXML", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSXMLAsync(string NumUsu, string Senha, string StrXML);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTIM", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Xml.XmlNode EnviaSMSTIM(string XMLString);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTIM", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Xml.XmlNode> EnviaSMSTIMAsync(string XMLString);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet StatusSMS(string NumUsu, string Senha, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> StatusSMSAsync(string NumUsu, string Senha, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS2SN", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet StatusSMS2SN(string NumUsu, string Senha, string SeuNum1, string SeuNum2);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMS2SN", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> StatusSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet StatusSMSDataSet(string NumUsu, string Senha, System.Data.DataSet DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> StatusSMSDataSetAsync(string NumUsu, string Senha, System.Data.DataSet DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMS", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet BuscaSMS(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMS", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMO", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet BuscaSMSMO(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMO", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSMOAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgenda", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet BuscaSMSAgenda(string NumUsu, string Senha, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgenda", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSAgendaAsync(string NumUsu, string Senha, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgendaDataSet", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet BuscaSMSAgendaDataSet(string NumUsu, string Senha, System.Data.DataSet DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSAgendaDataSet", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSAgendaDataSetAsync(string NumUsu, string Senha, System.Data.DataSet DS);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgenda", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string DelSMSAgenda(string NumUsu, string Senha, System.DateTime Agendamento, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/DelSMSAgenda", ReplyAction="*")]
        System.Threading.Tasks.Task<string> DelSMSAgendaAsync(string NumUsu, string Senha, System.DateTime Agendamento, string SeuNum);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/AlteraSenha", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        bool AlteraSenha(string NumUsu, string SenhaAntiga, string SenhaNova);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/AlteraSenha", ReplyAction="*")]
        System.Threading.Tasks.Task<bool> AlteraSenhaAsync(string NumUsu, string SenhaAntiga, string SenhaNova);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerCredito", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        int VerCredito(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerCredito", ReplyAction="*")]
        System.Threading.Tasks.Task<int> VerCreditoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerValidade", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.DateTime VerValidade(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/VerValidade", ReplyAction="*")]
        System.Threading.Tasks.Task<System.DateTime> VerValidadeAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSOTA8Bit", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSOTA8Bit(string NumUsu, string Senha, string SeuNum, string Celular, string Header, string Data);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSOTA8Bit", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSOTA8BitAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Header, string Data);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSEnhanced", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSEnhanced(string NumUsu, string Senha, string SeuNum, string Celular, string bmp, string texto);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSEnhanced", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSEnhancedAsync(string NumUsu, string Senha, string SeuNum, string Celular, string bmp, string texto);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTextoEnhanced", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string EnviaSMSTextoEnhanced(string NumUsu, string Senha, string SeuNum, string Celular, string texto);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/EnviaSMSTextoEnhanced", ReplyAction="*")]
        System.Threading.Tasks.Task<string> EnviaSMSTextoEnhancedAsync(string NumUsu, string Senha, string SeuNum, string Celular, string texto);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaStatusLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ResetaStatusLido(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaStatusLido", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ResetaStatusLidoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaMOLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        string ResetaMOLido(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/ResetaMOLido", ReplyAction="*")]
        System.Threading.Tasks.Task<string> ResetaMOLidoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSNaoLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet StatusSMSNaoLido(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/StatusSMSNaoLido", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> StatusSMSNaoLidoAsync(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLido", ReplyAction="*")]
        [System.ServiceModel.XmlSerializerFormatAttribute(SupportFaults=true)]
        System.Data.DataSet BuscaSMSMONaoLido(string NumUsu, string Senha);
        
        [System.ServiceModel.OperationContractAttribute(Action="https://www.twwwireless.com.br/reluzcap/wsreluzcap/BuscaSMSMONaoLido", ReplyAction="*")]
        System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSMONaoLidoAsync(string NumUsu, string Senha);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ReluzCapWebServiceSoapChannel : SmsGateway.Infrastructure.Operadoras.OperadoraTwwServiceReference.ReluzCapWebServiceSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ReluzCapWebServiceSoapClient : System.ServiceModel.ClientBase<SmsGateway.Infrastructure.Operadoras.OperadoraTwwServiceReference.ReluzCapWebServiceSoap>, SmsGateway.Infrastructure.Operadoras.OperadoraTwwServiceReference.ReluzCapWebServiceSoap {
        
        public ReluzCapWebServiceSoapClient() {
        }
        
        public ReluzCapWebServiceSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ReluzCapWebServiceSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReluzCapWebServiceSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ReluzCapWebServiceSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string EnviaSMS(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem) {
            return base.Channel.EnviaSMS(NumUsu, Senha, SeuNum, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem) {
            return base.Channel.EnviaSMSAsync(NumUsu, Senha, SeuNum, Celular, Mensagem);
        }
        
        public string EnviaSMS2SN(string NumUsu, string Senha, string SeuNum1, string SeuNum2, string Celular, string Mensagem) {
            return base.Channel.EnviaSMS2SN(NumUsu, Senha, SeuNum1, SeuNum2, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2, string Celular, string Mensagem) {
            return base.Channel.EnviaSMS2SNAsync(NumUsu, Senha, SeuNum1, SeuNum2, Celular, Mensagem);
        }
        
        public string EnviaSMSQuebra(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem) {
            return base.Channel.EnviaSMSQuebra(NumUsu, Senha, SeuNum, Celular, Mensagem);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem) {
            return base.Channel.EnviaSMSQuebraAsync(NumUsu, Senha, SeuNum, Celular, Mensagem);
        }
        
        public string EnviaSMSAlt(string user, string pwd, string msgid, string phone, string msgtext) {
            return base.Channel.EnviaSMSAlt(user, pwd, msgid, phone, msgtext);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAltAsync(string user, string pwd, string msgid, string phone, string msgtext) {
            return base.Channel.EnviaSMSAltAsync(user, pwd, msgid, phone, msgtext);
        }
        
        public string EnviaSMSAge(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento) {
            return base.Channel.EnviaSMSAge(NumUsu, Senha, SeuNum, Celular, Mensagem, Agendamento);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAgeAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento) {
            return base.Channel.EnviaSMSAgeAsync(NumUsu, Senha, SeuNum, Celular, Mensagem, Agendamento);
        }
        
        public string EnviaSMSAgeQuebra(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento) {
            return base.Channel.EnviaSMSAgeQuebra(NumUsu, Senha, SeuNum, Celular, Mensagem, Agendamento);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSAgeQuebraAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Mensagem, System.DateTime Agendamento) {
            return base.Channel.EnviaSMSAgeQuebraAsync(NumUsu, Senha, SeuNum, Celular, Mensagem, Agendamento);
        }
        
        public string EnviaSMSDataSet(string NumUsu, string Senha, System.Data.DataSet DS) {
            return base.Channel.EnviaSMSDataSet(NumUsu, Senha, DS);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSDataSetAsync(string NumUsu, string Senha, System.Data.DataSet DS) {
            return base.Channel.EnviaSMSDataSetAsync(NumUsu, Senha, DS);
        }
        
        public string EnviaSMSXML(string NumUsu, string Senha, string StrXML) {
            return base.Channel.EnviaSMSXML(NumUsu, Senha, StrXML);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSXMLAsync(string NumUsu, string Senha, string StrXML) {
            return base.Channel.EnviaSMSXMLAsync(NumUsu, Senha, StrXML);
        }
        
        public System.Xml.XmlNode EnviaSMSTIM(string XMLString) {
            return base.Channel.EnviaSMSTIM(XMLString);
        }
        
        public System.Threading.Tasks.Task<System.Xml.XmlNode> EnviaSMSTIMAsync(string XMLString) {
            return base.Channel.EnviaSMSTIMAsync(XMLString);
        }
        
        public System.Data.DataSet StatusSMS(string NumUsu, string Senha, string SeuNum) {
            return base.Channel.StatusSMS(NumUsu, Senha, SeuNum);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> StatusSMSAsync(string NumUsu, string Senha, string SeuNum) {
            return base.Channel.StatusSMSAsync(NumUsu, Senha, SeuNum);
        }
        
        public System.Data.DataSet StatusSMS2SN(string NumUsu, string Senha, string SeuNum1, string SeuNum2) {
            return base.Channel.StatusSMS2SN(NumUsu, Senha, SeuNum1, SeuNum2);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> StatusSMS2SNAsync(string NumUsu, string Senha, string SeuNum1, string SeuNum2) {
            return base.Channel.StatusSMS2SNAsync(NumUsu, Senha, SeuNum1, SeuNum2);
        }
        
        public System.Data.DataSet StatusSMSDataSet(string NumUsu, string Senha, System.Data.DataSet DS) {
            return base.Channel.StatusSMSDataSet(NumUsu, Senha, DS);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> StatusSMSDataSetAsync(string NumUsu, string Senha, System.Data.DataSet DS) {
            return base.Channel.StatusSMSDataSetAsync(NumUsu, Senha, DS);
        }
        
        public System.Data.DataSet BuscaSMS(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim) {
            return base.Channel.BuscaSMS(NumUsu, Senha, DataIni, DataFim);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim) {
            return base.Channel.BuscaSMSAsync(NumUsu, Senha, DataIni, DataFim);
        }
        
        public System.Data.DataSet BuscaSMSMO(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim) {
            return base.Channel.BuscaSMSMO(NumUsu, Senha, DataIni, DataFim);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSMOAsync(string NumUsu, string Senha, System.DateTime DataIni, System.DateTime DataFim) {
            return base.Channel.BuscaSMSMOAsync(NumUsu, Senha, DataIni, DataFim);
        }
        
        public System.Data.DataSet BuscaSMSAgenda(string NumUsu, string Senha, string SeuNum) {
            return base.Channel.BuscaSMSAgenda(NumUsu, Senha, SeuNum);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSAgendaAsync(string NumUsu, string Senha, string SeuNum) {
            return base.Channel.BuscaSMSAgendaAsync(NumUsu, Senha, SeuNum);
        }
        
        public System.Data.DataSet BuscaSMSAgendaDataSet(string NumUsu, string Senha, System.Data.DataSet DS) {
            return base.Channel.BuscaSMSAgendaDataSet(NumUsu, Senha, DS);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSAgendaDataSetAsync(string NumUsu, string Senha, System.Data.DataSet DS) {
            return base.Channel.BuscaSMSAgendaDataSetAsync(NumUsu, Senha, DS);
        }
        
        public string DelSMSAgenda(string NumUsu, string Senha, System.DateTime Agendamento, string SeuNum) {
            return base.Channel.DelSMSAgenda(NumUsu, Senha, Agendamento, SeuNum);
        }
        
        public System.Threading.Tasks.Task<string> DelSMSAgendaAsync(string NumUsu, string Senha, System.DateTime Agendamento, string SeuNum) {
            return base.Channel.DelSMSAgendaAsync(NumUsu, Senha, Agendamento, SeuNum);
        }
        
        public bool AlteraSenha(string NumUsu, string SenhaAntiga, string SenhaNova) {
            return base.Channel.AlteraSenha(NumUsu, SenhaAntiga, SenhaNova);
        }
        
        public System.Threading.Tasks.Task<bool> AlteraSenhaAsync(string NumUsu, string SenhaAntiga, string SenhaNova) {
            return base.Channel.AlteraSenhaAsync(NumUsu, SenhaAntiga, SenhaNova);
        }
        
        public int VerCredito(string NumUsu, string Senha) {
            return base.Channel.VerCredito(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<int> VerCreditoAsync(string NumUsu, string Senha) {
            return base.Channel.VerCreditoAsync(NumUsu, Senha);
        }
        
        public System.DateTime VerValidade(string NumUsu, string Senha) {
            return base.Channel.VerValidade(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<System.DateTime> VerValidadeAsync(string NumUsu, string Senha) {
            return base.Channel.VerValidadeAsync(NumUsu, Senha);
        }
        
        public string EnviaSMSOTA8Bit(string NumUsu, string Senha, string SeuNum, string Celular, string Header, string Data) {
            return base.Channel.EnviaSMSOTA8Bit(NumUsu, Senha, SeuNum, Celular, Header, Data);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSOTA8BitAsync(string NumUsu, string Senha, string SeuNum, string Celular, string Header, string Data) {
            return base.Channel.EnviaSMSOTA8BitAsync(NumUsu, Senha, SeuNum, Celular, Header, Data);
        }
        
        public string EnviaSMSEnhanced(string NumUsu, string Senha, string SeuNum, string Celular, string bmp, string texto) {
            return base.Channel.EnviaSMSEnhanced(NumUsu, Senha, SeuNum, Celular, bmp, texto);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSEnhancedAsync(string NumUsu, string Senha, string SeuNum, string Celular, string bmp, string texto) {
            return base.Channel.EnviaSMSEnhancedAsync(NumUsu, Senha, SeuNum, Celular, bmp, texto);
        }
        
        public string EnviaSMSTextoEnhanced(string NumUsu, string Senha, string SeuNum, string Celular, string texto) {
            return base.Channel.EnviaSMSTextoEnhanced(NumUsu, Senha, SeuNum, Celular, texto);
        }
        
        public System.Threading.Tasks.Task<string> EnviaSMSTextoEnhancedAsync(string NumUsu, string Senha, string SeuNum, string Celular, string texto) {
            return base.Channel.EnviaSMSTextoEnhancedAsync(NumUsu, Senha, SeuNum, Celular, texto);
        }
        
        public string ResetaStatusLido(string NumUsu, string Senha) {
            return base.Channel.ResetaStatusLido(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<string> ResetaStatusLidoAsync(string NumUsu, string Senha) {
            return base.Channel.ResetaStatusLidoAsync(NumUsu, Senha);
        }
        
        public string ResetaMOLido(string NumUsu, string Senha) {
            return base.Channel.ResetaMOLido(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<string> ResetaMOLidoAsync(string NumUsu, string Senha) {
            return base.Channel.ResetaMOLidoAsync(NumUsu, Senha);
        }
        
        public System.Data.DataSet StatusSMSNaoLido(string NumUsu, string Senha) {
            return base.Channel.StatusSMSNaoLido(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> StatusSMSNaoLidoAsync(string NumUsu, string Senha) {
            return base.Channel.StatusSMSNaoLidoAsync(NumUsu, Senha);
        }
        
        public System.Data.DataSet BuscaSMSMONaoLido(string NumUsu, string Senha) {
            return base.Channel.BuscaSMSMONaoLido(NumUsu, Senha);
        }
        
        public System.Threading.Tasks.Task<System.Data.DataSet> BuscaSMSMONaoLidoAsync(string NumUsu, string Senha) {
            return base.Channel.BuscaSMSMONaoLidoAsync(NumUsu, Senha);
        }
    }
}
