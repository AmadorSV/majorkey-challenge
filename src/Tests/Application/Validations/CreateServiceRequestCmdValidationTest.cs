using System;
using System.Threading.Tasks;
using Application.Features.ServiceRequest.Commands;
using Domain.Enum;
using FluentAssertions;
using FluentValidation.TestHelper;
using Moq;
using NUnit.Framework;

namespace Tests.Application.Validations
{
    [TestFixture]
    public class CreateServiceRequestCmdValidationTest
    {
        CreateServiceRequestCmdValidator _validator;

        [SetUp]
        public void Setup()
        {
            _validator = new CreateServiceRequestCmdValidator();
        }
        
        [Test]
        public async Task ShouldHaveErrors()
        {
            var result = await _validator.TestValidateAsync(new CreateServiceRequestCmd());

            result.Errors.Should().HaveCountGreaterThan(0);
        }
        
        [Test]
        public async Task ShouldHaveNoErrors()
        {
            var command = new CreateServiceRequestCmd
            {
                Description = "TestDescription",
                BuildingCode = "TestCode",
                CreatedBy = "TestCreatedBy",
                CreatedDate = DateTime.Now,
                CurrentStatus = CurrentStatus.Canceled
            };
            var result = await _validator.TestValidateAsync(command);

            result.Errors.Should().HaveCount(0);
        }
        
        [Test]
        public async Task FieldLength_ExeedsLimit_HaveErrors()
        {
            // Arrange
            var command = new CreateServiceRequestCmd();
            command.Description = It.IsAny<string>();
            command.CreatedBy = "";
            command.BuildingCode = "";
            
            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            
            result.ShouldHaveValidationErrorFor(x => x.BuildingCode);
            result.ShouldHaveValidationErrorFor(x => x.Description);
            result.ShouldHaveValidationErrorFor(x => x.CreatedBy);
        }
        
        [Test]
        public async Task FieldLength_NotExeededLimit_HaveNoErrors()
        {
            // Arrange
            var command = new CreateServiceRequestCmd();
            command.BuildingCode = "1567";
            command.Description = "1567";
            command.CreatedBy = "1567";
            
            // Act
            var result = await _validator.TestValidateAsync(command);

            // Assert
            result.ShouldNotHaveValidationErrorFor(x => x.BuildingCode);
            result.ShouldNotHaveValidationErrorFor(x => x.Description);
            result.ShouldNotHaveValidationErrorFor(x => x.CreatedBy);
        }

    }
}