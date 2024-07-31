using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Moq;
using Xunit;
using AdvC_Final.Areas.Account.Controllers;
using AdvC_Final.Areas.Account.Models;

public class HomeControllerTests
{
    private DbContextOptions<PetsContext> _options;
    private PetsContext _context;
    private HomeController _controller;

    public HomeControllerTests()
    {
        // Set up in-memory database
        _options = new DbContextOptionsBuilder<PetsContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        _context = new PetsContext(_options);

        // Set up controller with mocked user
        _controller = new HomeController(_context);
        var user = new Mock<ClaimsPrincipal>();
        user.Setup(u => u.Identity.Name).Returns("TestUser");
        _controller.ControllerContext = new ControllerContext
        {
            HttpContext = new DefaultHttpContext { User = user.Object }
        };
    }

    [Fact]
    public void Index_Returns_ViewResult()
    {
        // Act
        var result = _controller.Index();

        // Assert
        Assert.IsType<ViewResult>(result);
    }

    [Fact]
    public void Pets_Returns_ViewResult_With_Pets_List()
    {
        // Arrange
        _context.Pets.Add(new Pets { Id = 1, ownerName = "TestUser", petName = "TestPet", petType = "Dog" });
        _context.SaveChanges();

        // Act
        var result = _controller.Pets() as ViewResult;

        // Assert
        var model = Assert.IsAssignableFrom<IEnumerable<Pets>>(result.ViewData.Model);
        Assert.Single(model);
    }

    [Fact]
    public void EditPet_Get_Returns_ViewResult_With_Pet()
    {
        // Arrange
        _context.Pets.Add(new Pets { Id = 1, ownerName = "TestUser", petName = "TestPet", petType = "Dog" });
        _context.SaveChanges();

        // Act
        var result = _controller.EditPet(1) as ViewResult;

        // Assert
        var model = Assert.IsType<Pets>(result.ViewData.Model);
        Assert.Equal(1, model.Id);
    }

    [Fact]
    public void EditPet_Post_ValidModel_RedirectsToPets()
    {
        // Arrange
        _context.Pets.Add(new Pets { Id = 1, ownerName = "TestUser", petName = "TestPet", petType = "Dog" });
        _context.SaveChanges();
        var pet = _context.Pets.First();
        pet.petName = "UpdatedPet";

        // Act
        var result = _controller.EditPet(pet) as RedirectToActionResult;

        // Assert
        Assert.Equal("Pets", result.ActionName);
    }

    [Fact]
    public void EditPet_Post_InvalidModel_ReturnsViewResult()
    {
        // Arrange
        var pet = new Pets { Id = 1, ownerName = "TestUser", petName = "", petType = "Dog" }; // Invalid model (missing petName)
        _controller.ModelState.AddModelError("petName", "Required");

        // Act
        var result = _controller.EditPet(pet) as ViewResult;

        // Assert
        Assert.Equal("Edit", result.ViewData["Action"]);
    }

    [Fact]
    public void AddPet_Get_Returns_ViewResult_With_Pet()
    {
        // Arrange
        _context.Pets.Add(new Pets { Id = 1, ownerName = "TestUser", petName = "TestPet", petType = "Dog" });
        _context.SaveChanges();

        // Act
        var result = _controller.AddPet(1) as ViewResult;

        // Assert
        var model = Assert.IsType<Pets>(result.ViewData.Model);
        Assert.Equal(1, model.Id);
    }

    [Fact]
    public void AddPet_Post_ValidModel_RedirectsToIndex()
    {
        // Arrange
        var pet = new Pets { Id = 0, ownerName = "TestUser", petName = "NewPet", petType = "Cat" };

        // Act
        var result = _controller.AddPet(pet) as RedirectToActionResult;

        // Assert
        Assert.Equal("Index", result.ActionName);

        // Verify the pet was added
        var addedPet = _context.Pets.FirstOrDefault(p => p.petName == pet.petName);
        Assert.NotNull(addedPet);
        Assert.Equal("NewPet", addedPet.petName);
    }

    [Fact]
    public void RemovePet_Get_Returns_ViewResult_With_Pet()
    {
        // Arrange
        _context.Pets.Add(new Pets { Id = 1, ownerName = "TestUser", petName = "TestPet", petType = "Dog" });
        _context.SaveChanges();

        // Act
        var result = _controller.RemovePet(1) as ViewResult;

        // Assert
        var model = Assert.IsType<Pets>(result.ViewData.Model);
        Assert.Equal(1, model.Id);
    }

    [Fact]
    public void RemovePet_Post_ValidModel_RedirectsToPets()
    {
        // Arrange
        _context.Pets.Add(new Pets { Id = 1, ownerName = "TestUser", petName = "TestPet", petType = "Dog" });
        _context.SaveChanges();
        var pet = _context.Pets.First();

        // Act
        var result = _controller.RemovePet(pet) as RedirectToActionResult;

        // Assert
        Assert.Equal("Pets", result.ActionName);
    }
}
