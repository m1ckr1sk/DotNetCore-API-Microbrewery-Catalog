using System;
using System.Collections.Generic;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace Domain
{
    // Concrete implemementation of IFilmsRepository, to get films from the database.
    public class SqlMicrobreweryRepository : IMicrobreweryRepository
    {
        private MicrobreweryContext context;

        // Inject a FilmsRUsContext via constructor DI.
        public SqlMicrobreweryRepository(MicrobreweryContext context)
        {
            this.context = context;
        }

        public void Add(Microbrewery microbrewery)
        {
            context.Microbreweries.Add(microbrewery);
            context.SaveChanges();
        }

        public void AddBeer(Guid microbreweryId, Beer beer)
        {
            throw new NotImplementedException();
        }

        public void AddBeers(Guid microbreweryId, HashSet<Beer> beers)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid microbreweryId)
        {
            throw new NotImplementedException();
        }

        public void DeleteBeer(Guid microbreweryId, Guid beerId)
        {
            throw new NotImplementedException();
        }

        public Microbrewery Get(Guid microbreweryId)
        {
            throw new NotImplementedException();
        }

        public HashSet<Microbrewery> GetAll()
        {
            HashSet<Microbrewery> allbreweries = new HashSet<Microbrewery>(context.Microbreweries.Include(brewery => brewery.Beers));
            return allbreweries;
        }

        public HashSet<Beer> GetAllBeers()
        {
            throw new NotImplementedException();
        }

        public Microbrewery GetByName(string name)
        {
            throw new NotImplementedException();
        }

        public void Update(Microbrewery microbrewery)
        {
            throw new NotImplementedException();
        }

        public void UpdateBeer(Guid microbreweryId, Beer beer)
        {
            throw new NotImplementedException();
        }
    }
}
