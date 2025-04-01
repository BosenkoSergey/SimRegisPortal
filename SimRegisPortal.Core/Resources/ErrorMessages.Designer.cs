﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimRegisPortal.Core.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class ErrorMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ErrorMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("SimRegisPortal.Core.Resources.ErrorMessages", typeof(ErrorMessages).Assembly);
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
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The server was unable to complete your request. Please try again later. If this problem persists, please contact support. Server logs contain details of this error..
        /// </summary>
        public static string Exception_Others {
            get {
                return ResourceManager.GetString("Exception.Others", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to You don’t have permission to access the {0}..
        /// </summary>
        public static string Exception_Resource_Forbidden {
            get {
                return ResourceManager.GetString("Exception.Resource.Forbidden", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The requested {0} was not found..
        /// </summary>
        public static string Exception_Resource_NotFound {
            get {
                return ResourceManager.GetString("Exception.Resource.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Session has expired. Please sign in again..
        /// </summary>
        public static string Exception_UserSession_Expired {
            get {
                return ResourceManager.GetString("Exception.UserSession.Expired", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Session not found. Please sign in again..
        /// </summary>
        public static string Exception_UserSession_NotFound {
            get {
                return ResourceManager.GetString("Exception.UserSession.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is not valid..
        /// </summary>
        public static string Validation_Email_Invalid {
            get {
                return ResourceManager.GetString("Validation.Email.Invalid", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Email is required..
        /// </summary>
        public static string Validation_Email_Required {
            get {
                return ResourceManager.GetString("Validation.Email.Required", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to No account found with this email..
        /// </summary>
        public static string Validation_Login_Email_NotFound {
            get {
                return ResourceManager.GetString("Validation.Login.Email.NotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid login or password..
        /// </summary>
        public static string Validation_Login_InvalidCredentials {
            get {
                return ResourceManager.GetString("Validation.Login.InvalidCredentials", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An account with this email already exists..
        /// </summary>
        public static string Validation_SignUp_Email_AlreadyExists {
            get {
                return ResourceManager.GetString("Validation.SignUp.Email.AlreadyExists", resourceCulture);
            }
        }
    }
}
