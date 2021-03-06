﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MyClient.ChatServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="ChatServiceReference.IChatContract")]
    public interface IChatContract {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/Registration", ReplyAction="http://tempuri.org/IChatContract/RegistrationResponse")]
        bool Registration(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/Registration", ReplyAction="http://tempuri.org/IChatContract/RegistrationResponse")]
        System.Threading.Tasks.Task<bool> RegistrationAsync(string name, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/Enter", ReplyAction="http://tempuri.org/IChatContract/EnterResponse")]
        MyClient.ChatServiceReference.EnterResponse Enter(MyClient.ChatServiceReference.EnterRequest request);
        
        // CODEGEN: Идет формирование контракта на сообщение, так как операция может иметь много возвращаемых значений.
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/Enter", ReplyAction="http://tempuri.org/IChatContract/EnterResponse")]
        System.Threading.Tasks.Task<MyClient.ChatServiceReference.EnterResponse> EnterAsync(MyClient.ChatServiceReference.EnterRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/Exit", ReplyAction="http://tempuri.org/IChatContract/ExitResponse")]
        bool Exit(int userID, int sessionID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/Exit", ReplyAction="http://tempuri.org/IChatContract/ExitResponse")]
        System.Threading.Tasks.Task<bool> ExitAsync(int userID, int sessionID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/SendMessage", ReplyAction="http://tempuri.org/IChatContract/SendMessageResponse")]
        void SendMessage(int userID, string str);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/SendMessage", ReplyAction="http://tempuri.org/IChatContract/SendMessageResponse")]
        System.Threading.Tasks.Task SendMessageAsync(int userID, string str);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/GetServerMessages", ReplyAction="http://tempuri.org/IChatContract/GetServerMessagesResponse")]
        MyChat.Message[] GetServerMessages(int sizeChat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/GetServerMessages", ReplyAction="http://tempuri.org/IChatContract/GetServerMessagesResponse")]
        System.Threading.Tasks.Task<MyChat.Message[]> GetServerMessagesAsync(int sizeChat);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/GetUsersOnline", ReplyAction="http://tempuri.org/IChatContract/GetUsersOnlineResponse")]
        string[] GetUsersOnline();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IChatContract/GetUsersOnline", ReplyAction="http://tempuri.org/IChatContract/GetUsersOnlineResponse")]
        System.Threading.Tasks.Task<string[]> GetUsersOnlineAsync();
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="Enter", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class EnterRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public string name;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public string password;
        
        public EnterRequest() {
        }
        
        public EnterRequest(string name, string password) {
            this.name = name;
            this.password = password;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.MessageContractAttribute(WrapperName="EnterResponse", WrapperNamespace="http://tempuri.org/", IsWrapped=true)]
    public partial class EnterResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=0)]
        public bool EnterResult;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=1)]
        public int sessionID;
        
        [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://tempuri.org/", Order=2)]
        public int userID;
        
        public EnterResponse() {
        }
        
        public EnterResponse(bool EnterResult, int sessionID, int userID) {
            this.EnterResult = EnterResult;
            this.sessionID = sessionID;
            this.userID = userID;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IChatContractChannel : MyClient.ChatServiceReference.IChatContract, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ChatContractClient : System.ServiceModel.ClientBase<MyClient.ChatServiceReference.IChatContract>, MyClient.ChatServiceReference.IChatContract {
        
        public ChatContractClient() {
        }
        
        public ChatContractClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ChatContractClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChatContractClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ChatContractClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public bool Registration(string name, string password) {
            return base.Channel.Registration(name, password);
        }
        
        public System.Threading.Tasks.Task<bool> RegistrationAsync(string name, string password) {
            return base.Channel.RegistrationAsync(name, password);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        MyClient.ChatServiceReference.EnterResponse MyClient.ChatServiceReference.IChatContract.Enter(MyClient.ChatServiceReference.EnterRequest request) {
            return base.Channel.Enter(request);
        }
        
        public bool Enter(string name, string password, out int sessionID, out int userID) {
            MyClient.ChatServiceReference.EnterRequest inValue = new MyClient.ChatServiceReference.EnterRequest();
            inValue.name = name;
            inValue.password = password;
            MyClient.ChatServiceReference.EnterResponse retVal = ((MyClient.ChatServiceReference.IChatContract)(this)).Enter(inValue);
            sessionID = retVal.sessionID;
            userID = retVal.userID;
            return retVal.EnterResult;
        }
        
        public System.Threading.Tasks.Task<MyClient.ChatServiceReference.EnterResponse> EnterAsync(MyClient.ChatServiceReference.EnterRequest request) {
            return base.Channel.EnterAsync(request);
        }
        
        public bool Exit(int userID, int sessionID) {
            return base.Channel.Exit(userID, sessionID);
        }
        
        public System.Threading.Tasks.Task<bool> ExitAsync(int userID, int sessionID) {
            return base.Channel.ExitAsync(userID, sessionID);
        }
        
        public void SendMessage(int userID, string str) {
            base.Channel.SendMessage(userID, str);
        }
        
        public System.Threading.Tasks.Task SendMessageAsync(int userID, string str) {
            return base.Channel.SendMessageAsync(userID, str);
        }
        
        public MyChat.Message[] GetServerMessages(int sizeChat) {
            return base.Channel.GetServerMessages(sizeChat);
        }
        
        public System.Threading.Tasks.Task<MyChat.Message[]> GetServerMessagesAsync(int sizeChat) {
            return base.Channel.GetServerMessagesAsync(sizeChat);
        }
        
        public string[] GetUsersOnline() {
            return base.Channel.GetUsersOnline();
        }
        
        public System.Threading.Tasks.Task<string[]> GetUsersOnlineAsync() {
            return base.Channel.GetUsersOnlineAsync();
        }
    }
}
