using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface ICompetitieService
    {
        Task<DateOnly?> GetStartDatumAsync();

    }
}
