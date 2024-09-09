using System;
using System.Collections.Generic;

namespace Hotel_Dejvid.Models;

public partial class Gost
{
    public int GostId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string BrojTelefona { get; set; } = null!;

    public virtual Rezervacija GostNavigation { get; set; } = null!;
}
