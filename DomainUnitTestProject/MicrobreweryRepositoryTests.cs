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

        Beer beer = new Beer
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
            mbr.AddBeer(firstMicrobrewery.Id, beer);

            var beers = mbr.GetByName("First").Beers;

            beers.Should().Contain(beer);
            beers.Count.Should().Be(1);
        }

        [Fact]
        public void AddingABeerToAnNewMicrobreweryThrowsInvalidOperationException()
        {
            // Don't add "first" microbrewery
            Action a = () => { mbr.AddBeer(firstMicrobrewery.Id, beer); };

            a.ShouldThrow<InvalidOperationException>();
            
        }

        [Fact]
        public void AddingThreeBeersToAnExistingMicrobreweryWorks()
        {
            mbr.Add(firstMicrobrewery);
            mbr.AddBeers(firstMicrobrewery.Id, new HashSet<Beer>{ beer, secondBeer, thirdBeer});

            var beers = mbr.GetByName("First").Beers;

            beers.Count.Should().Be(3);
        }

        [Fact]
        public void AddingThreeBeersToANewMicrobreweryThrowsInvalidOperationException()
        {
            Action a = () => { mbr.AddBeers(firstMicrobrewery.Id, new HashSet<Beer> { beer, secondBeer, thirdBeer }); };

            a.ShouldThrow<InvalidOperationException>();
        }

        [Fact]
        public void RetrievingMicrobreweriesWithBeersWorks()
        {
            mbr.Add(firstMicrobrewery);
            mbr.AddBeers(firstMicrobrewery.Id, new HashSet<Beer> { beer, secondBeer });

            mbr.Add(secondMicrobrewery);
            mbr.AddBeer(secondMicrobrewery.Id, thirdBeer);

            var all = mbr.GetAll();
            var first = (from x in all
                         where x.Name == "First"
                         select x).First();
            first.Beers.ShouldBeEquivalentTo(new HashSet<Beer> { beer, secondBeer });
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


    }
}
