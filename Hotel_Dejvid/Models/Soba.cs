using System;
using System.Collections.Generic;

namespace Hotel_Dejvid.Models;

public partial class Soba
{
    public int SobaId { get; set; }

    public string BrojSobe { get; set; } = null!;

    public string TipSobe { get; set; } = null!;

    public decimal CenaPoNoci { get; set; }

    public bool Dostupna { get; set; }

    public virtual Rezervacija SobaNavigation { get; set; } = null!;
}
