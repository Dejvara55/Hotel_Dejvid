using System;
using System.Collections.Generic;

namespace Hotel_Dejvid.Models;

public partial class Zaposleni
{
    public int ZaposleniId { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Pozicija { get; set; } = null!;

    public string BrojTelefona { get; set; } = null!;
}
