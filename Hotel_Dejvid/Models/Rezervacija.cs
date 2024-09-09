using System;
using System.Collections.Generic;

namespace Hotel_Dejvid.Models;

public partial class Rezervacija
{
    public int RezervacijaId { get; set; }

    public int? GostId { get; set; }

    public int? SobaId { get; set; }

    public DateOnly? DatumDolaska { get; set; }

    public DateOnly? DatumOdlaska { get; set; }

    public string? Status { get; set; }

    public virtual Gost? Gost { get; set; }

    public virtual Placanje RezervacijaNavigation { get; set; } = null!;

    public virtual Soba? Soba { get; set; }
}
