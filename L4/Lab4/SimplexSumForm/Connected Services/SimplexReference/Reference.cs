﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimplexSumForm.SimplexReference {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="A", Namespace="http://RAM/")]
    [System.SerializableAttribute()]
    public partial class A : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string sField;
        
        private int kField;
        
        private float fField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false)]
        public string s {
            get {
                return this.sField;
            }
            set {
                if ((object.ReferenceEquals(this.sField, value) != true)) {
                    this.sField = value;
                    this.RaisePropertyChanged("s");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public int k {
            get {
                return this.kField;
            }
            set {
                if ((this.kField.Equals(value) != true)) {
                    this.kField = value;
                    this.RaisePropertyChanged("k");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=2)]
        public float f {
            get {
                return this.fField;
            }
            set {
                if ((this.fField.Equals(value) != true)) {
                    this.fField = value;
                    this.RaisePropertyChanged("f");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://RAM/", ConfigurationName="SimplexReference.SimplexSoap")]
    public interface SimplexSoap {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/Add", ReplyAction="*")]
        int Add(int x, int y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/Add", ReplyAction="*")]
        System.Threading.Tasks.Task<int> AddAsync(int x, int y);
        
        // CODEGEN: Контракт генерации сообщений с именем s из пространства имен http://RAM/ не отмечен как обнуляемый
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/Concat", ReplyAction="*")]
        SimplexSumForm.SimplexReference.ConcatResponse Concat(SimplexSumForm.SimplexReference.ConcatRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/Concat", ReplyAction="*")]
        System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.ConcatResponse> ConcatAsync(SimplexSumForm.SimplexReference.ConcatRequest request);
        
        // CODEGEN: Контракт генерации сообщений с именем a1 из пространства имен http://RAM/ не отмечен как обнуляемый
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/Sum", ReplyAction="*")]
        SimplexSumForm.SimplexReference.SumResponse Sum(SimplexSumForm.SimplexReference.SumRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/Sum", ReplyAction="*")]
        System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.SumResponse> SumAsync(SimplexSumForm.SimplexReference.SumRequest request);
        
        // CODEGEN: Контракт генерации сообщений с именем AddSResult из пространства имен http://RAM/ не отмечен как обнуляемый
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/AddS", ReplyAction="*")]
        SimplexSumForm.SimplexReference.AddSResponse AddS(SimplexSumForm.SimplexReference.AddSRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://RAM/AddS", ReplyAction="*")]
        System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.AddSResponse> AddSAsync(SimplexSumForm.SimplexReference.AddSRequest request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ConcatRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Concat", Namespace="http://RAM/", Order=0)]
        public SimplexSumForm.SimplexReference.ConcatRequestBody Body;
        
        public ConcatRequest() {
        }
        
        public ConcatRequest(SimplexSumForm.SimplexReference.ConcatRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://RAM/")]
    public partial class ConcatRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string s;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public double d;
        
        public ConcatRequestBody() {
        }
        
        public ConcatRequestBody(string s, double d) {
            this.s = s;
            this.d = d;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class ConcatResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="ConcatResponse", Namespace="http://RAM/", Order=0)]
        public SimplexSumForm.SimplexReference.ConcatResponseBody Body;
        
        public ConcatResponse() {
        }
        
        public ConcatResponse(SimplexSumForm.SimplexReference.ConcatResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://RAM/")]
    public partial class ConcatResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string ConcatResult;
        
        public ConcatResponseBody() {
        }
        
        public ConcatResponseBody(string ConcatResult) {
            this.ConcatResult = ConcatResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SumRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="Sum", Namespace="http://RAM/", Order=0)]
        public SimplexSumForm.SimplexReference.SumRequestBody Body;
        
        public SumRequest() {
        }
        
        public SumRequest(SimplexSumForm.SimplexReference.SumRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://RAM/")]
    public partial class SumRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SimplexSumForm.SimplexReference.A a1;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public SimplexSumForm.SimplexReference.A a2;
        
        public SumRequestBody() {
        }
        
        public SumRequestBody(SimplexSumForm.SimplexReference.A a1, SimplexSumForm.SimplexReference.A a2) {
            this.a1 = a1;
            this.a2 = a2;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class SumResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="SumResponse", Namespace="http://RAM/", Order=0)]
        public SimplexSumForm.SimplexReference.SumResponseBody Body;
        
        public SumResponse() {
        }
        
        public SumResponse(SimplexSumForm.SimplexReference.SumResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://RAM/")]
    public partial class SumResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public SimplexSumForm.SimplexReference.A SumResult;
        
        public SumResponseBody() {
        }
        
        public SumResponseBody(SimplexSumForm.SimplexReference.A SumResult) {
            this.SumResult = SumResult;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddSRequest {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddS", Namespace="http://RAM/", Order=0)]
        public SimplexSumForm.SimplexReference.AddSRequestBody Body;
        
        public AddSRequest() {
        }
        
        public AddSRequest(SimplexSumForm.SimplexReference.AddSRequestBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://RAM/")]
    public partial class AddSRequestBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=0)]
        public int x;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
        public int y;
        
        public AddSRequestBody() {
        }
        
        public AddSRequestBody(int x, int y) {
            this.x = x;
            this.y = y;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class AddSResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="AddSResponse", Namespace="http://RAM/", Order=0)]
        public SimplexSumForm.SimplexReference.AddSResponseBody Body;
        
        public AddSResponse() {
        }
        
        public AddSResponse(SimplexSumForm.SimplexReference.AddSResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="http://RAM/")]
    public partial class AddSResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string AddSResult;
        
        public AddSResponseBody() {
        }
        
        public AddSResponseBody(string AddSResult) {
            this.AddSResult = AddSResult;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface SimplexSoapChannel : SimplexSumForm.SimplexReference.SimplexSoap, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SimplexSoapClient : System.ServiceModel.ClientBase<SimplexSumForm.SimplexReference.SimplexSoap>, SimplexSumForm.SimplexReference.SimplexSoap {
        
        public SimplexSoapClient() {
        }
        
        public SimplexSoapClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SimplexSoapClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimplexSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SimplexSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public int Add(int x, int y) {
            return base.Channel.Add(x, y);
        }
        
        public System.Threading.Tasks.Task<int> AddAsync(int x, int y) {
            return base.Channel.AddAsync(x, y);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SimplexSumForm.SimplexReference.ConcatResponse SimplexSumForm.SimplexReference.SimplexSoap.Concat(SimplexSumForm.SimplexReference.ConcatRequest request) {
            return base.Channel.Concat(request);
        }
        
        public string Concat(string s, double d) {
            SimplexSumForm.SimplexReference.ConcatRequest inValue = new SimplexSumForm.SimplexReference.ConcatRequest();
            inValue.Body = new SimplexSumForm.SimplexReference.ConcatRequestBody();
            inValue.Body.s = s;
            inValue.Body.d = d;
            SimplexSumForm.SimplexReference.ConcatResponse retVal = ((SimplexSumForm.SimplexReference.SimplexSoap)(this)).Concat(inValue);
            return retVal.Body.ConcatResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.ConcatResponse> SimplexSumForm.SimplexReference.SimplexSoap.ConcatAsync(SimplexSumForm.SimplexReference.ConcatRequest request) {
            return base.Channel.ConcatAsync(request);
        }
        
        public System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.ConcatResponse> ConcatAsync(string s, double d) {
            SimplexSumForm.SimplexReference.ConcatRequest inValue = new SimplexSumForm.SimplexReference.ConcatRequest();
            inValue.Body = new SimplexSumForm.SimplexReference.ConcatRequestBody();
            inValue.Body.s = s;
            inValue.Body.d = d;
            return ((SimplexSumForm.SimplexReference.SimplexSoap)(this)).ConcatAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SimplexSumForm.SimplexReference.SumResponse SimplexSumForm.SimplexReference.SimplexSoap.Sum(SimplexSumForm.SimplexReference.SumRequest request) {
            return base.Channel.Sum(request);
        }
        
        public SimplexSumForm.SimplexReference.A Sum(SimplexSumForm.SimplexReference.A a1, SimplexSumForm.SimplexReference.A a2) {
            SimplexSumForm.SimplexReference.SumRequest inValue = new SimplexSumForm.SimplexReference.SumRequest();
            inValue.Body = new SimplexSumForm.SimplexReference.SumRequestBody();
            inValue.Body.a1 = a1;
            inValue.Body.a2 = a2;
            SimplexSumForm.SimplexReference.SumResponse retVal = ((SimplexSumForm.SimplexReference.SimplexSoap)(this)).Sum(inValue);
            return retVal.Body.SumResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.SumResponse> SimplexSumForm.SimplexReference.SimplexSoap.SumAsync(SimplexSumForm.SimplexReference.SumRequest request) {
            return base.Channel.SumAsync(request);
        }
        
        public System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.SumResponse> SumAsync(SimplexSumForm.SimplexReference.A a1, SimplexSumForm.SimplexReference.A a2) {
            SimplexSumForm.SimplexReference.SumRequest inValue = new SimplexSumForm.SimplexReference.SumRequest();
            inValue.Body = new SimplexSumForm.SimplexReference.SumRequestBody();
            inValue.Body.a1 = a1;
            inValue.Body.a2 = a2;
            return ((SimplexSumForm.SimplexReference.SimplexSoap)(this)).SumAsync(inValue);
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        SimplexSumForm.SimplexReference.AddSResponse SimplexSumForm.SimplexReference.SimplexSoap.AddS(SimplexSumForm.SimplexReference.AddSRequest request) {
            return base.Channel.AddS(request);
        }
        
        public string AddS(int x, int y) {
            SimplexSumForm.SimplexReference.AddSRequest inValue = new SimplexSumForm.SimplexReference.AddSRequest();
            inValue.Body = new SimplexSumForm.SimplexReference.AddSRequestBody();
            inValue.Body.x = x;
            inValue.Body.y = y;
            SimplexSumForm.SimplexReference.AddSResponse retVal = ((SimplexSumForm.SimplexReference.SimplexSoap)(this)).AddS(inValue);
            return retVal.Body.AddSResult;
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.AddSResponse> SimplexSumForm.SimplexReference.SimplexSoap.AddSAsync(SimplexSumForm.SimplexReference.AddSRequest request) {
            return base.Channel.AddSAsync(request);
        }
        
        public System.Threading.Tasks.Task<SimplexSumForm.SimplexReference.AddSResponse> AddSAsync(int x, int y) {
            SimplexSumForm.SimplexReference.AddSRequest inValue = new SimplexSumForm.SimplexReference.AddSRequest();
            inValue.Body = new SimplexSumForm.SimplexReference.AddSRequestBody();
            inValue.Body.x = x;
            inValue.Body.y = y;
            return ((SimplexSumForm.SimplexReference.SimplexSoap)(this)).AddSAsync(inValue);
        }
    }
}
