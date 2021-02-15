using System.Collections.Generic;
using System.Threading.Tasks;
using model = Roulette.Entities.Models;

namespace Roulette.Entities.Interfaces
{
    public interface IRouletteAdapter
    {
        Task SetRoulette(model.Roulette roulette);
        Task<model.Roulette> GetRoulette(int Id);
        Task<List<model.Roulette>> GetRoulettes();
    }
}
