using System;
using System.Collections.Generic;

namespace Domain
{
    public class Microbrewery
    {
        private HashSet<Beer> _beers;

        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Owner { get; set; }

        public Boolean IsActive { get; set; }

        public int NumberOfStills { get; set; }

        public DateTime FoundedOn { get; set; }

        public HashSet<Beer> Beers
        {
            get
            {
                if (_beers == null)
                {
                    _beers = new HashSet<Beer>();
                }

                return _beers;
            }
            set
            {
                _beers = value;
            }
        }
    }
}
