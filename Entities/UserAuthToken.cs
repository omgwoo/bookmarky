//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Bookmarky.Entities
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserAuthToken
    {
        public int UserAuthTokenID { get; set; }
        public int UserID { get; set; }
        public string AuthToken { get; set; }
        public System.DateTime ExpireDate { get; set; }
    }
}
