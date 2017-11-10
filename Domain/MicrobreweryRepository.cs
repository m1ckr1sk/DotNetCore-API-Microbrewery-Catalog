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

        public void AddBeer(Microbrewery microbrewery, Beer beer)
        {
            Microbrewery existingMicrobrewery = GetByName(microbrewery.Name);

            if (existingMicrobrewery == null)
            {
                throw new InvalidOperationException();
            }

            existingMicrobrewery.Beers.Add(beer);
        }

        public void AddBeers(Microbrewery microbrewery, HashSet<Beer> beers)
        {
            foreach (Beer b in beers)
            {
                AddBeer(microbrewery, b);
            }
        }

        public void Delete(Guid id)
        {
            var microbreweryToUpdate = Get(id);
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
