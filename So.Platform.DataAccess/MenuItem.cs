
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
    
public partial class MenuItem
{

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
    public MenuItem()
    {

        this.MenuItems = new HashSet<MenuItem>();

    }


    public int Id { get; set; }

    public int MenuItemId { get; set; }

    public string DisplayName { get; set; }

    public string Descript { get; set; }

    public Nullable<int> SeqNbr { get; set; }

    public string NavURL { get; set; }

    public string State { get; set; }

    public Nullable<int> IconVisualId { get; set; }

    public Nullable<int> AuthItemId { get; set; }

    public string TemplateUrl { get; set; }

    public Nullable<int> MenuId { get; set; }

    public Nullable<int> ParentMenuItemId { get; set; }

    public Nullable<int> ModStateId { get; set; }



    public virtual AuthItem AuthItem { get; set; }

    public virtual IconVisual IconVisual { get; set; }

    public virtual Menu Menu { get; set; }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]

    public virtual ICollection<MenuItem> MenuItems { get; set; }

    public virtual MenuItem MenuItem1 { get; set; }

    public virtual ModState ModState { get; set; }

}

}
