using System.Diagnostics;
using AutoMapper;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Cart;
using Deli.Entities;
using Deli.Interface;
using Deli.Repository;
using Microsoft.EntityFrameworkCore;

namespace Deli.Services
{
    public interface ICartService
    {
        Task<(CartDto? data,String? error)> GetMyCart(Guid userId);
        Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm);
        
        Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid cartId, int quantity);
    }
    
    public class CartService : ICartService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;
        public CartService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }
        public async Task<(CartDto? data, string? error)> GetMyCart(Guid userId)
        {
            var (carts,totalCount) = await _repositoryWrapper.Cart.GetAll<CartDto>(
                x => x.UserId == userId
            );
            if (carts == null || !carts.Any())
            {
                return (null, "لم يتم العثور على السلة");
            }
            //total price
            carts.ForEach(x =>
            {
                x.TotalPrice = (decimal)x.CartOrderDto.Sum(x => x.Price * x.Quantity);
            });

            var cartDtos = _mapper.Map<List<CartDto>>(carts);
            return (cartDtos.FirstOrDefault(), null);
        }
        public async Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm)
        {
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.ItemOrders));
            if (cart == null)
            {
                cart = new Cart()
                {
                    UserId = userId,
                    ItemOrders = new List<ItemOrder>()
                };
                await _repositoryWrapper.Cart.Add(cart);
            }
    
            foreach (var cartOrder in cartForm.OrderCarForm)
            {
                var product = await _repositoryWrapper.Item.Get(x => x.Id == cartOrder.ItemId);
                if (product == null)
                {
                    return (null, "Product Not Found");
                }

                // Check if the quantity is available
                if (product.Quantity < cartOrder.Quantity)
                {
                    return (null, "Requested quantity is not available");
                }

                var cartProductEntity = await _repositoryWrapper.ItemOrder.Get(x =>
                    x.CartId == cart.Id && x.ItemId == cartOrder.ItemId);
        
                if (cartProductEntity == null) 
                {
                    var newCartProduct = new ItemOrder()
                    {
                        CartId = cart.Id,
                        ItemId = cartOrder.ItemId,
                        Quantity = cartOrder.Quantity,
                    };
                    await _repositoryWrapper.ItemOrder.Add(newCartProduct);
                }
                else 
                {
                    return (null, "المنتج موجود بالفعل في السلة");
                }
        
            }
    
            var result = await _repositoryWrapper.Cart.Update(cart);
            if (result == null)
            {
                return (null, "لا يمكن اضافة المنتجات الى السلة");
            }
    
            return ("تم اضافة المنتجات الى السلة", null);
        }
        public async Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid cartId, int quantity)
        {
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.ItemOrders));
            if (cart == null)
            {
                return (null, "لم يتم العثور على السلة");
            }
            
            var cartProduct = cart.ItemOrders.FirstOrDefault(x => x.ItemId == cartId);
            if (cartProduct == null)
            {
                return (null, "لم يتم العثور على المنتج في السلة");
            }
            
            if (cartProduct.Quantity > quantity)
            {
                cartProduct.Quantity -= quantity;
                await _repositoryWrapper.ItemOrder.Update(cartProduct);
            }
            else
            {
                await _repositoryWrapper.ItemOrder.Delete(cartProduct.Id);
            }
            
            return ("تم حذف المنتج من السلة", null);
        }
    }
}

public class PdfService
{
    public void GeneratePdfFromHtml(string html, string outputPdfPath)
    {
        // Save the HTML to a temporary file
        var tempHtmlPath = Path.GetTempFileName() + ".html";
        File.WriteAllText(tempHtmlPath, html);

        // Set up the Wkhtmltopdf process
        var startInfo = new ProcessStartInfo
        {
            FileName = "wkhtmltopdf",
            Arguments = $"{tempHtmlPath} {outputPdfPath}",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        // Start the process
        using var process = Process.Start(startInfo);

        // Wait for the process to finish
        process.WaitForExit();

        // Clean up the temporary HTML file
        File.Delete(tempHtmlPath);
    }
}