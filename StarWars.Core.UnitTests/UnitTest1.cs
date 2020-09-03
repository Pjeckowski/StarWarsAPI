using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StarWars.Core.BusinessRuleValidators;
using StarWars.Core.Contract;
using StarWars.Core.Domain;
using StarWars.Core.Exceptions;
using StarWars.Core.Repositories;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace StarWars.Core.UnitTests
{
    [TestClass]
    [ExcludeFromCodeCoverage]
    public class CharacterServiceUnitTests
    {
        ICharacterService _characterService;
        private  Mock<ICharacterRepository> _characterRepositoryMock;
        private  Mock<ICreateRuleValidator<Character>> _addCharacterValidatorMock;
        private  Mock<IUpdateRuleValidator<Character>> _updateCharacterValidatorMock;
        private  Mock<IDeleteRuleValidator<Character>> _deleteCharacterValidatorMock;

        [TestInitialize]
        public void Setup()
        {
            _characterRepositoryMock = new Mock<ICharacterRepository>();
            _addCharacterValidatorMock = new Mock<ICreateRuleValidator<Character>>();
            _updateCharacterValidatorMock = new Mock<IUpdateRuleValidator<Character>>();
            _deleteCharacterValidatorMock = new Mock<IDeleteRuleValidator<Character>>();

            _characterService = new CharacterService(_characterRepositoryMock.Object, _addCharacterValidatorMock.Object,
                _updateCharacterValidatorMock.Object, _deleteCharacterValidatorMock.Object);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task DeleteByName_ExistingNameGiven_CharacterReturnedRemoveMethodCalled()
        {
            //arrange
            var charName = "CharName1";
            var charFromExistanceCheck = new Character
            {
                Name = charName
            };
            var charFromDelete = new Character
            {
                Name = charName
            };

            _characterRepositoryMock.Setup(m => m.GetByNameAsync(It.Is<string>(s => s.Equals(charName)))).ReturnsAsync(charFromExistanceCheck);
            _deleteCharacterValidatorMock.Setup(m => m.ValidateAsync(It.Is<Character>(c => c.Equals(charFromExistanceCheck)))).Verifiable();
            _characterRepositoryMock.Setup(m => m.DeleteByNameAsync(It.Is<string>(s => s.Equals(charName)))).ReturnsAsync(charFromDelete);

            //act
            var result = await _characterService.DeleteByNameAsync(charName);

            //assert
            result.Should().Be(charFromDelete);
            _characterRepositoryMock.Verify(m => m.GetByNameAsync(It.Is<string>(s => s.Equals(charName))), Times.Once);
            _deleteCharacterValidatorMock.Verify(m => m.ValidateAsync(It.Is<Character>(c => c.Equals(charFromExistanceCheck))), Times.Once);
            _characterRepositoryMock.Verify(m => m.DeleteByNameAsync(It.Is<string>(s => s.Equals(charName))), Times.Once);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public async Task DeleteByName_NotExisitngNameGiven_NullReturnedRemoveNotCalled()
        {
            //arrange
            var charName = "CharName1";
            var charFromExistanceCheck = (Character)null;

            _characterRepositoryMock.Setup(m => m.GetByNameAsync(It.Is<string>(s => s.Equals(charName)))).ReturnsAsync(charFromExistanceCheck);
            _deleteCharacterValidatorMock.Setup(m => m.ValidateAsync(It.IsAny<Character>())).Verifiable();

            //act
            var result = await _characterService.DeleteByNameAsync(charName);

            //assert
            result.Should().BeNull();
            _characterRepositoryMock.Verify(m => m.GetByNameAsync(It.Is<string>(s => s.Equals(charName))), Times.Once);
            _deleteCharacterValidatorMock.Verify(m => m.ValidateAsync(It.IsAny<Character>()), Times.Never);
            _characterRepositoryMock.Verify(m => m.DeleteByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        [TestCategory("Unit")]
        public void DeleteByName_ExistingNameGivenValidationErrorThrown_ValidationErrorRethrown()
        {
            //arrange
            var charName = "CharName1";
            var charFromExistanceCheck = new Character
            {
                Name = charName
            };
            var validationException = new BusinessRuleException("NastyUserTriedSomethingBad!");

            _characterRepositoryMock.Setup(m => m.GetByNameAsync(It.Is<string>(s => s.Equals(charName)))).ReturnsAsync(charFromExistanceCheck);
            _deleteCharacterValidatorMock.Setup(m => m.ValidateAsync(It.Is<Character>(c => c.Equals(charFromExistanceCheck)))).ThrowsAsync(validationException);

            //act
            Func<Task<Character>> func = async() => await _characterService.DeleteByNameAsync(charName);

            //assert
            func.Should().Throw<BusinessRuleException>().WithMessage(validationException.Message);
            _characterRepositoryMock.Verify(m => m.GetByNameAsync(It.Is<string>(s => s.Equals(charName))), Times.Once);
            _deleteCharacterValidatorMock.Verify(m => m.ValidateAsync(It.Is<Character>(c => c.Equals(charFromExistanceCheck))), Times.Once);
            _characterRepositoryMock.Verify(m => m.DeleteByNameAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
