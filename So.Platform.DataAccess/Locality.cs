
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
    
public partial class Locality
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public Locality()
    {

        this.Pers = new HashSet<Per>();

    }


    public int Id { get; set; }

    public string Name { get; set; }

    public string Code { get; set; }

    public string Descript { get; set; }

    public Nullable<System.DateTime> CreateDate { get; set; }

    public Nullable<System.DateTime> DateLastMaint { get; set; }

    public string CountryCode { get; set; }



    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<Per> Pers { get; set; }

}

}
