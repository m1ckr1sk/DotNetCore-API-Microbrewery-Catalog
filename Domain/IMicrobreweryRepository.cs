using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IMicrobreweryRepository
    {
        void Add(Microbrewery microbrewery);

        void AddBeer(Microbrewery microbrewery, Beer beer);

        void AddBeers(Microbrewery microbrewery, HashSet<Beer> beers);

        HashSet<Microbrewery> GetAll();

        Microbrewery GetByName(string name);

        Microbrewery Get(Guid id);

        void Update(Microbrewery microbrewery);

        void Delete(Guid id);

        HashSet<Beer> GetAllBeers();

        void UpdateBeer(Beer beer);

        void DeleteBeer(Beer beer);

    }
}
