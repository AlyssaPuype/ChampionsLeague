using ChampionsLeague.Domains.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ChampionsLeague.Services.Services.Interfaces
{
    public interface IAbonnementService
    {
        Task<IEnumerable<Abonnement>> GetAllAsync();
        Task<Abonnement?> GetByIdAsync(int id);
    }
