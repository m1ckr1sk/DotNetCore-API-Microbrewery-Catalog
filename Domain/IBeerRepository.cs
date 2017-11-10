using System;
using System.Collections.Generic;
using System.Text;

namespace Domain
{
    public interface IBeerRepository
    {
        void Add(Beer beer);

        HashSet<Beer> ShowAll();

        void Update(Beer beer);

        void Delete(Beer beer);
    }
}
