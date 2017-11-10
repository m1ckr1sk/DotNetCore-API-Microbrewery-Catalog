using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IMicrobreweryRepository
    {
        void Add(Microbrewery microbrewery);

        HashSet<Microbrewery> GetAll();

        Microbrewery GetByName(string name);

        Microbrewery Get(Guid microbreweryId);

        void Update(Microbrewery microbrewery);

        void Delete(Guid microbreweryId);

        HashSet<Beer> GetAllBeers();

        void AddBeer(Guid microbreweryId, Beer beer);

        void AddBeers(Guid microbreweryId, HashSet<Beer> beers);

        void UpdateBeer(Guid microbreweryId, Beer beer);

        void DeleteBeer(Guid microbreweryId, Guid beerId);

    }
}
