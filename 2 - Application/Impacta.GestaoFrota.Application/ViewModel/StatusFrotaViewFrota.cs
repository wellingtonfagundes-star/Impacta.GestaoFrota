using System;
using System.Collections.Generic;
using System.Text;

namespace Impacta.GestaoFrota.Application.ViewModel
{
    public class StatusFrotaViewFrota
    {
        public int IdStatusFrota { get; set; }

        public string NomeStatus { get; set; } = null!;

        public string? Descricao { get; set; }
    }
}
