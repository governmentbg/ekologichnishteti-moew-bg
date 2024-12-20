using System;
using System.Collections.Generic;

namespace Zopoesht2Migration.DbContexts.Zopoesht2.Models;

public partial class Operatorcorrespondence
{
    public int Id { get; set; }

    public int? Settlementid { get; set; }

    public string? Correspondenceaddress { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    public string? Email { get; set; }

    public string? Contactperson { get; set; }

    public string? Postalcode { get; set; }

    public virtual ICollection<Operator> Operators { get; set; } = new List<Operator>();

    public virtual Settlement? Settlement { get; set; }
}
