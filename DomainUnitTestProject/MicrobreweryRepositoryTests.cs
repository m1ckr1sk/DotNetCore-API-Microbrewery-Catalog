using System;
using Xunit;
using Domain;
using FluentAssertions;
using System.Collections.Generic;
using System.Linq;

namespace DomainUnitTestProject
{
    public class MicrobreweryRepositoryTests
    {
        MicrobreweryRepository mbr = new MicrobreweryRepository();

        Microbrewery firstMicrobrewery = new Microbrewery
        {
            Name = "First",
            FoundedOn = new DateTime(2017, 03, 01),
            Id = Guid.NewGuid()
        };

        Microbrewery secondMicrobrewery = new Microbrewery
        {
            Name = "Second",
            FoundedOn = new DateTime(2017, 03, 01),
            Id = Guid.NewGuid()
        };

        Beer firstBeer = new Beer
        {
            Id = Guid.NewGuid(),
            Abv = 10,
            IsGlutenFree = false,
            Name = "Thumper"
        };

        Beer secondBeer = new Beer
        {
            Id = Guid.NewGuid(),
            Abv = 1,
            IsGlutenFree = true,
            Name = "Flower"
        };

        Beer thirdBeer = new Beer
        {
            Id = Guid.NewGuid(),
            Abv = 5,
            IsGlutenFree = false,
            Name = "Bambi"
        };

        [Fact]
        public void AddingFirstMicrobreweryWorks()
        {
            mbr.Add(firstMicrobrewery);
            mbr.GetAll().Should().Contain(firstMicrobrewery);
            mbr.GetAll().Count.Should().Be(1);
        }

        [Fact]
        public void GettingFirstMicrobreweryByNameWorks()
        {
            mbr.Add(firstMicrobrewery);
            mbr.GetByName("First").ShouldBeEquivalentTo(firstMicrobrewery);
            mbr.GetAll().Count.Should().Be(1);
        }

        [Fact]
        public void GettingNonExistentMicrobreweryByNameReturnsNull()
        {
            mbr.Add(firstMicrobrewery);
            mbr.GetByName("Second").Should().BeNull();
        }

        [Fact]
        public void AddingABeerToAnExistingMicrobreweryWorks()
        {            
            mbr.Add(firstMicrobrewery);
            mbr.AddBeer(firstMicrobrewery.Id, firstBeer);

            var beers = mbr.GetByName("First").Beers;

            beers.Should().Contain(firstBeer);
            beers.Count.Should().Be(1);
        }

        [Fact]
        public void AddingABeerToAnNewMicrobreweryThrowsInvalidOperationException()
        {
            // Don't add "first" microbrewery
            Action a = () => { mbr.AddBeer(firstMicrobrewery.Id, firstBeer); };

            a.ShouldThrow<InvalidOperationException>();
            
        }

        [Fact]
        public void AddingThreeBeersToAnExistingMicrobreweryWorks()
        {
            mbr.Add(firstMicrobrewery);
            mbr.AddBeers(firstMicrobrewery.Id, new HashSet<Beer>{ firstBeer, secondBeer, thirdBeer});

            var beers = mbr.GetByName("First").Beers;

            beers.Count.Should().Be(3);
        }

        [Fact]
        public void AddingThreeBeersToANewMicrobreweryThrowsInvalidOperationException()
        {
            Action a = () => { mbr.AddBeers(firstMicrobrewery.Id, new HashSet<Beer> { firstBeer, secondBeer, thirdBeer }); };

            a.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void RetrievingMicrobreweriesWithBeersWorks()
        {
            mbr.Add(firstMicrobrewery);
            mbr.AddBeers(firstMicrobrewery.Id, new HashSet<Beer> { firstBeer, secondBeer });

            mbr.Add(secondMicrobrewery);
            mbr.AddBeer(secondMicrobrewery.Id, thirdBeer);

            var all = mbr.GetAll();
            var first = (from x in all
                         where x.Name == "First"
                         select x).First();
            first.Beers.ShouldBeEquivalentTo(new HashSet<Beer> { firstBeer, secondBeer });
        }

        [Fact]
        public void UpdateToAMicrobreweryPersists()
        {
            Microbrewery m = new Microbrewery
            {
                Name = "Test",
                FoundedOn = new DateTime(2017, 03, 01),
                Id = Guid.NewGuid()
            };
            
            mbr.Add(m);

            Microbrewery n = new Microbrewery
            {
                Name = "Updated",
                FoundedOn = new DateTime(2017, 04, 08),
                Id = m.Id
            };

            mbr.GetByName("Test").Should().NotBeNull();
            mbr.GetByName("Updated").Should().BeNull();

            mbr.Update(n);

            mbr.GetByName("Test").Should().BeNull();
            var updatedMicrobrewery = mbr.GetByName("Updated");
            updatedMicrobrewery.Should().NotBeNull();
            updatedMicrobrewery.Id.Should().Be(m.Id);
        }

        [Fact]
        public void UpdateToNonexistantMicrobreweryThrowsInvalidOperationException()
        {
            Microbrewery m = new Microbrewery
            {
                Name = "Test",
                FoundedOn = new DateTime(2017, 03, 01),
                Id = Guid.NewGuid()
            };

            Action a = () => { mbr.Update(m); };

            a.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void DeleteMicrobrewery()
        {
            mbr.Add(firstMicrobrewery);
            mbr.Add(secondMicrobrewery);

            mbr.GetAll().Count.Should().Be(2);

            mbr.Delete(firstMicrobrewery.Id);

            var all = mbr.GetAll();
            all.Count.Should().Be(1);
            mbr.GetAll().Count.Should().Be(1);

            all.Should().Contain(secondMicrobrewery);
            all.Should().NotContain(firstMicrobrewery);
        }

        [Fact]
        public void GetAllBeers()
        {
            mbr.Add(firstMicrobrewery);
            mbr.Add(secondMicrobrewery);

            mbr.GetAll().Count.Should().Be(2);

            mbr.AddBeer(firstMicrobrewery.Id, firstBeer);
            mbr.AddBeer(firstMicrobrewery.Id, secondBeer);
            mbr.AddBeer(secondMicrobrewery.Id, thirdBeer);

            var all = mbr.GetAllBeers();
            all.Count.Should().Be(3);

            all.Should().Contain(firstBeer);
            all.Should().Contain(secondBeer);
            all.Should().Contain(thirdBeer);
        }


        [Fact]
        public void UpdateToABeerPersists()
        {
            Beer m = new Beer
            {
                Name = "Test",
                Id = Guid.NewGuid()
            };

            firstMicrobrewery.Beers.Add(m);
            mbr.Add(firstMicrobrewery);

            Beer n = new Beer
            {
                Name = "Updated",
                Id = m.Id
            };

            var allBeers = mbr.GetAllBeers();
            var first = (from x in allBeers
                         where x.Name == "Test"
                         select x).First();
            var updated = (from x in allBeers
                         where x.Name == "Updated"
                           select x) as IEnumerable<Beer>;
            updated.Count().Should().Be(0);
            first.Should().NotBeNull();

            mbr.UpdateBeer(firstMicrobrewery.Id, n);

            allBeers = mbr.GetAllBeers();
            var original = (from x in allBeers
                     where x.Name == "Test"
                     select x) as IEnumerable<Beer>;
            var updatedBeer = (from x in allBeers
                           where x.Name == "Updated"
                           select x) ;
            original.Count().Should().Be(0);
            updatedBeer.Should().NotBeNull();

        }
    }
}
