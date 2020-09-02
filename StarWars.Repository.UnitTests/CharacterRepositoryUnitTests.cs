using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarWars.Core.Repositories;
using StarWars.Repository.DbModels;
using System;
using Microsoft.EntityFrameworkCore.InMemory;
using StarWars.Repository.Mappers;
using Moq;

namespace StarWars.Repository.UnitTests
{
    [TestClass]
    public class CharacterRepositoryUnitTests
    {
        private ICharacterRepository _characterRepository;
        private Mock<ICharacterMapper> _mapperMock;

        [TestInitialize]
        public void Setup()
        {
            _mapperMock = new Mock<ICharacterMapper>();
        }

        StarWarsDbContext GetContext()
        {
            var options = new DbContextOptionsBuilder<StarWarsDbContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
            return new StarWarsDbContext(options);
        }

        [TestMethod]
        public void Create_OnlyCharName_CharacterCreated()
        {
            //arrange
            var charName = "CharName1";
            var dmCharacter = new Core.Domain.Character
            {
                Name = charName
            };
            var dbCharacter = new DbModels.Character
            {
                Name = charName
            };

            _mapperMock.Setup(m => m.Map(It.Is<Core.Domain.Character>(c => c.Equals(dmCharacter)))).Returns(dbCharacter);

            //act
            //using (var context = GetContext())
            //{

            //}
        }
    }
}
