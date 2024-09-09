using System;
using System.Collections.Generic;

namespace Hotel_Dejvid.Models;

public partial class Placanje
{
    public int PlacanjeId { get; set; }

    public int RezervacijaId { get; set; }

    public decimal Iznos { get; set; }

    public DateOnly DatumPlacanje { get; set; }

    public string VrstaPlacanja { get; set; } = null!;

    public virtual Rezervacija? Rezervacija { get; set; }
}
