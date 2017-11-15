using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain
{
    public class InMemoryMicrobreweryRepository : IMicrobreweryRepository
    {
        private HashSet<Microbrewery> _microbreweries;
        private HashSet<Microbrewery> Microbreweries {
            get
            {
                if (_microbreweries == null)
                {
                    _microbreweries = new HashSet<Microbrewery>();
                }
                return _microbreweries;
            }
            set
            {
                _microbreweries = value;
            }
        }

        public void Add(Microbrewery microbrewery)
        {
            Microbreweries.Add(microbrewery);
        }

        public void AddBeer(Guid microbreweryId, Beer beer)
        {
            Microbrewery existingMicrobrewery = Get(microbreweryId);

            if (existingMicrobrewery == null)
            {
                throw new InvalidOperationException();
            }

            existingMicrobrewery.Beers.Add(beer);
        }

        public void AddBeers(Guid microbreweryId, HashSet<Beer> beers)
        {
            foreach (Beer b in beers)
            {
                AddBeer(microbreweryId, b);
            }
        }

        public void Delete(Guid microbreweryId)
        {
            var microbreweryToUpdate = Get(microbreweryId);
            if(microbreweryToUpdate is null)
            {
                throw new InvalidOperationException();
            }
            _microbreweries.Remove(microbreweryToUpdate);
        }

        public Microbrewery GetByName(string name)
        {
            return (from x in Microbreweries
                    where String.Equals(x.Name, name, StringComparison.CurrentCultureIgnoreCase)
                    select x).FirstOrDefault();
        }

        public HashSet<Microbrewery> GetAll()
        {
            return Microbreweries;
        }

        public void Update(Microbrewery microbrewery)
        {
            Delete(microbrewery.Id);
            _microbreweries.Add(microbrewery);
        }

        public Microbrewery Get(Guid microbreweryId)
        {
            return (from x in Microbreweries
             where x.Id == microbreweryId
             select x).FirstOrDefault();
        }

        public HashSet<Beer> GetAllBeers()
        {
            HashSet<Beer> beers = new HashSet<Beer>();
            foreach(Microbrewery microbrewery in _microbreweries)
            {
                beers.UnionWith(microbrewery.Beers);
            }
            return beers;
        }

        public void UpdateBeer(Guid microbreweryId, Beer beer)
        {
           DeleteBeer(microbreweryId, beer.Id);
           var microbrewery = Get(microbreweryId);
           microbrewery.Beers.Add(beer); 
        }

        private Beer GetBeerById(Microbrewery microbrewery, Guid beerId)
        {
            var beerToUpdate = (from x in microbrewery.Beers
                                where x.Id == beerId
                                select x).FirstOrDefault();
            return beerToUpdate;
        }

        public void DeleteBeer(Guid microbreweryId, Guid beerId)
        {
            var microbrewery = Get(microbreweryId);
            if (microbrewery is null)
            {
                throw new InvalidOperationException();
            }

            var beerToUpdate = GetBeerById(microbrewery,beerId);
            if (beerToUpdate is null)
            {
                throw new InvalidOperationException();
            }
            microbrewery.Beers.Remove(beerToUpdate);
        }

        
    }
}
