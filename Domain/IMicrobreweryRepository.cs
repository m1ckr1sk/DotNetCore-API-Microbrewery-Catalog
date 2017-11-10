using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IMicrobreweryRepository
    {
        void Add(Microbrewery microbrewery);

        void AddBeer(Guid microbreweryId, Beer beer);

        void AddBeers(Guid microbreweryId, HashSet<Beer> beers);

        HashSet<Microbrewery> GetAll();

        Microbrewery GetByName(string name);

        Microbrewery Get(Guid microbreweryId);

        void Update(Microbrewery microbrewery);

        void Delete(Guid microbreweryId);

        HashSet<Beer> GetAllBeers();

        void UpdateBeer(Beer beer);

        void DeleteBeer(Beer beer);

    }
}
