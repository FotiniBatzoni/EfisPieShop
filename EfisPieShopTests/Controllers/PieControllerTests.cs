using EfisPieShop.Controllers;
using EfisPieShop.ViewModels;
using EfisPieShopTests.Mocks;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EfisPieShopTests.Controllers
{
    public class PieControllerTests
    {
        [Fact]
        public void List_Empty_Category_ReturnsAllPies()
        {
            //Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //Act
            var result = pieController.List("");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(10, pieListViewModel.Pies.Count());
        }

        [Fact]
        public void List_Category_CheeseCakes_ReturnsPiesOfCategory()
        {
            //Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //Act
            var result = pieController.List("Cheese cakes");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(2, pieListViewModel.Pies.Count());
        }

        [Fact]
        public void List_Category_FruitPies_ReturnsPiesOfCategory()
        {
            //Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //Act
            var result = pieController.List("Fruit pies");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(5, pieListViewModel.Pies.Count());
        }

        [Fact]
        public void List_Category_SeasonalPies_ReturnsPiesOfCategory()
        {
            //Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //Act
            var result = pieController.List("Seasonal pies");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(3, pieListViewModel.Pies.Count());
        }

        [Fact]
        public void List_Category_IncorrectCategoryName_ReturnsPiesOfCategory()
        {
            //Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //Act
            var result = pieController.List("pies");

            //Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            var pieListViewModel = Assert.IsAssignableFrom<PieListViewModel>(viewResult.ViewData.Model);
            Assert.Equal(0, pieListViewModel.Pies.Count());
        }

        [Fact]
        public void Details_IfIdDoesNotExist_ReturnsNotFound()
        {
            //Arrange
            var mockPieRepository = RepositoryMocks.GetPieRepository();
            var mockCategoryRepository = RepositoryMocks.GetCategoryRepository();

            var pieController = new PieController(mockPieRepository.Object, mockCategoryRepository.Object);

            //Act
            var result = pieController.Details(0);

            //Assert
            var viewResult = Assert.IsType<NotFoundResult>(result);
        }

    }
}
