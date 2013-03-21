﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18034
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SmsGateway.Domain.CoreContext.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SmsGateway.Domain.CoreContext.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The API used in this contract is not valid..
        /// </summary>
        internal static string exception_ApiOperadoraInvalida {
            get {
                return ResourceManager.GetString("exception_ApiOperadoraInvalida", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot associate transient or null entity.
        /// </summary>
        internal static string exception_CannotAssociateTransientOrNullEntity {
            get {
                return ResourceManager.GetString("exception_CannotAssociateTransientOrNullEntity", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot set the new contract because its null, transient or not enabled..
        /// </summary>
        internal static string exception_RenovacaoContrato {
            get {
                return ResourceManager.GetString("exception_RenovacaoContrato", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot set the current client for this contact because it is null, transient or not enabled..
        /// </summary>
        internal static string exception_SetarClienteDocontato {
            get {
                return ResourceManager.GetString("exception_SetarClienteDocontato", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot set the current contract for this cliente because it is null, transient or not enabled..
        /// </summary>
        internal static string exception_SetarContratoAtual {
            get {
                return ResourceManager.GetString("exception_SetarContratoAtual", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The status API must be selected.
        /// </summary>
        internal static string validation_ApiStatus {
            get {
                return ResourceManager.GetString("validation_ApiStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The contact&apos;s client cannot be null..
        /// </summary>
        internal static string validation_ClienteContato {
            get {
                return ResourceManager.GetString("validation_ClienteContato", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The status code cannot be empty or null..
        /// </summary>
        internal static string validation_CodigoStatus {
            get {
                return ResourceManager.GetString("validation_CodigoStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The status description cannot be empty or null..
        /// </summary>
        internal static string validation_DescricaoStatus {
            get {
                return ResourceManager.GetString("validation_DescricaoStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The administrator&apos;s email is not valid..
        /// </summary>
        internal static string validation_EmailAdministrador {
            get {
                return ResourceManager.GetString("validation_EmailAdministrador", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The administrator&apos;s name cannot be empty or null..
        /// </summary>
        internal static string validation_NomeAdministrador {
            get {
                return ResourceManager.GetString("validation_NomeAdministrador", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client&apos;s name cannot be empty or null..
        /// </summary>
        internal static string validation_NomeCliente {
            get {
                return ResourceManager.GetString("validation_NomeCliente", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The contact&apos;s name cannot be empty or null..
        /// </summary>
        internal static string validation_NomeContato {
            get {
                return ResourceManager.GetString("validation_NomeContato", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The contact&apos;s number cannot be empty or null..
        /// </summary>
        internal static string validation_NumeroContato {
            get {
                return ResourceManager.GetString("validation_NumeroContato", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The administrator&apos;s password cannot be empty or null..
        /// </summary>
        internal static string validation_SenhaAdministrador {
            get {
                return ResourceManager.GetString("validation_SenhaAdministrador", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The client&apos;s password cannot be empty or null..
        /// </summary>
        internal static string validation_SenhaCliente {
            get {
                return ResourceManager.GetString("validation_SenhaCliente", resourceCulture);
            }
        }
    }
}