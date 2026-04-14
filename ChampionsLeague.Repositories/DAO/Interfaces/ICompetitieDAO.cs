using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Repositories.DAO.Interfaces
{
    public interface ICompetitieDAO
    {

        //Startdate ophalen zodat we checks kunnen doen voor abonnementen aan te kopen (enkel mogelijk voor start competitie)
        Task<DateOnly?> GetStartDatumAsync();

    }
}
