
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------


namespace Oc.Carbon.DataAccess
{

using System;
    using System.Collections.Generic;
    
public partial class MessageTemplate
{

    public int Id { get; set; }

    public string Name { get; set; }

    public string Descript { get; set; }

    public string TemplateText { get; set; }

    public System.DateTime CreateDate { get; set; }

    public Nullable<System.DateTime> InactiveDate { get; set; }

    public int PortId { get; set; }

    public string HeaderText { get; set; }



    public virtual Port Port { get; set; }

}

}
