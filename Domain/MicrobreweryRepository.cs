using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Domain
{
    public class MicrobreweryRepository : IMicrobreweryRepository
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

        public Microbrewery Get(Guid Id)
        {
            return (from x in Microbreweries
             where x.Id == Id
             select x).FirstOrDefault();
        }

        public HashSet<Beer> GetAllBeers()
        {
            throw new NotImplementedException();
        }

        public void UpdateBeer(Beer beer)
        {
            throw new NotImplementedException();
        }

        public void DeleteBeer(Beer beer)
        {
            throw new NotImplementedException();
        }

        
    }
}
