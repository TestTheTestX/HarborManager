using HarborManager.Contracts;
using HarborManager.WebApi.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace HarborManager.WebApi.Test
{
    public class BookingsTest
    {
        [Fact]
        public void GetById_Id30NotFound_StatusCodeResult404()
        {
            // Arrange
            int id = 30;
            var mockLogic = new Mock<IBookings>();
            mockLogic.Setup(ml => ml.GetById(id)).Returns((BookingDTO)null);
            var controller = new BookingsController(mockLogic.Object);

            // Act
            IActionResult result = controller.GetById(id);
           
            // Assert
            var requestResult = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(404, requestResult.StatusCode);
        }
    }
}
