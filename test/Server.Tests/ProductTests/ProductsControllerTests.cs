using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Server.Application.Common;
using Server.Application.Features.Products.CreateProduct;
using Server.Application.Features.Products.DeleteProductById;
using Server.Application.Features.Products.GetAllProducts;
using Server.Application.Features.Products.GetProductById;
using Server.Application.Features.Products.UpdateProduct;
using Server.Presentation.Controllers;

namespace Server.Tests.ProductTests;

public sealed class ProductsControllerTests
{
    [Fact]
    public async Task GetAll_ShouldReturnSuccess()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        var products = new List<GetAllProductsQueryResponse>
        {
            new GetAllProductsQueryResponse(
                Guid.NewGuid().ToString(),
                "Product 1",
                50,
                100,
                DateTime.Now,
                null)
        };

        var resultResponse = new Result<List<GetAllProductsQueryResponse>>(null, null, products);

        mediator.Setup(m => m.Send(It.IsAny<GetAllProductsQuery>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(resultResponse);

        ProductsController productsController = new(mediator.Object);
        CancellationToken cancellationToken = new();

        // Act
        var result = await productsController.GetAll(cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<Result<List<GetAllProductsQueryResponse>>>(okResult.Value);

        Assert.NotNull(returnValue.Data);
        Assert.Equal(products.Count, returnValue.Data.Count);
    }

    [Fact]
    public async Task GetById_ShouldReturnSuccess()
    {
        // Arrange 
        var mediator = new Mock<IMediator>();
        var productId = Guid.NewGuid().ToString();
        var product = new GetProductByIdQueryResponse(
                productId,
                "Product 1",
                50,
                100,
                DateTime.Now,
                null
        );

        var resultResponse = new Result<GetProductByIdQueryResponse>(null, null, product);

        mediator.Setup(m => m.Send(It.Is<GetProductByIdQuery>(q => q.Id == productId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultResponse);

        ProductsController productsController = new(mediator.Object);
        CancellationToken cancellationToken = new();

        // Act
        var result = await productsController.GetById(productId, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<Result<GetProductByIdQueryResponse>>(okResult.Value);

        Assert.NotNull(returnValue.Data);
        Assert.Equal(productId, returnValue.Data.Id);
    }

    [Fact]
    public async Task Create_ShouldReturnSuccess()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        CreateProductCommand createProductCommand = new(
            "Product 1",
            50,
            100);

        Result<CreateProductCommandResponse> response = new(
            "Product was successfully deleted", null, new CreateProductCommandResponse(
                Guid.NewGuid().ToString(),
                createProductCommand.Name,
                createProductCommand.Price,
                createProductCommand.Stock,
                DateTime.Now));

        CancellationToken cancellationToken = new();

        mediator
            .Setup(p => p.Send(createProductCommand, cancellationToken))
            .ReturnsAsync(response);

        ProductsController productsController = new(mediator.Object);

        // Act
        var result = await productsController.Create(createProductCommand, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<Result<CreateProductCommandResponse>>(okResult.Value);

        Assert.Equal(response, returnValue);
        mediator.Verify(m => m.Send(createProductCommand, cancellationToken), Times.Once);
    }

    [Fact]
    public async Task DeleteById_ShouldReturnSuccess()
    {
        // Arrange
        var mediator = new Mock<IMediator>();
        var productId = Guid.NewGuid().ToString();

        var deleteProductResponse = new DeleteProductByIdCommandResponse(
            productId,
            "Product 1",
            50,
            100,
            DateTime.Now);

        var resultResponse = new Result<DeleteProductByIdCommandResponse>(
            null,
            null,
            deleteProductResponse);

        mediator.Setup(m => m.Send(It.Is<DeleteProductByIdCommand>(c => c.Id == productId), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultResponse);

        ProductsController productsController = new(mediator.Object);
        CancellationToken cancellationToken = new();

        // Act
        var result = await productsController.DeleteById(productId, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<Result<DeleteProductByIdCommandResponse>>(okResult.Value);

        Assert.NotNull(returnValue.Data);
        Assert.Equal(productId, returnValue.Data.Id);
        Assert.Equal("Product 1", returnValue.Data.Name);
    }

    [Fact]
    public async Task Update_ShouldReturnSuccess()
    {
        // Arrange
        var mediator = new Mock<IMediator>();

        var updateProductCommand = new UpdateProductCommand(
            Guid.NewGuid().ToString(),
            "Product 1",
            50,
            100);

        var updateProductResponse = new UpdateProductCommandResponse(
            updateProductCommand.Id,
            updateProductCommand.Name,
            updateProductCommand.Price,
            updateProductCommand.Stock,
            DateTime.Now.AddMonths(-1),
            DateTime.Now);

        var resultResponse = new Result<UpdateProductCommandResponse>(
            null,
            null,
            updateProductResponse);

        mediator.Setup(m => m.Send(It.IsAny<UpdateProductCommand>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(resultResponse);

        ProductsController productsController = new(mediator.Object);
        CancellationToken cancellationToken = new();

        // Act
        var result = await productsController.Update(updateProductCommand, cancellationToken);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsAssignableFrom<Result<UpdateProductCommandResponse>>(okResult.Value);

        Assert.NotNull(returnValue.Data);
        Assert.Equal(updateProductCommand.Id, returnValue.Data.Id);
        Assert.Equal(updateProductCommand.Name, returnValue.Data.Name);
        Assert.Equal(updateProductCommand.Price, returnValue.Data.Price);
        Assert.Equal(updateProductCommand.Stock, returnValue.Data.Stock);
        Assert.NotNull(returnValue.Data.UpdatedAt);
    }
}