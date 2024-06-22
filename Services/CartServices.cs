using System.Diagnostics;
using System.Runtime.InteropServices.JavaScript;
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
        Task<(CartDto? data,String? error)> GetMyCart(Guid userId,string language);
        Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm, string language);

        Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid cartId, int quantity, string language);
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
        public async Task<(CartDto? data, string? error)> GetMyCart(Guid userId, string language)
        {
            var (carts,totalCount) = await _repositoryWrapper.Cart.GetAll<CartDto>(
                x => x.UserId == userId
            );
            if (carts == null || !carts.Any())
            {
                return (null, ErrorResponseException.GenerateLocalizedResponse("Cart not found", "لم يتم العثور على السلة", language));
            }
            //total price
            var cart = carts.FirstOrDefault();
            cart.TotalPrice = 0;
            
                    foreach (var cartorderdto in cart.CartOrderDto)
                    {
                        var item = await _repositoryWrapper.Item.Get(i => i.Id == cartorderdto.ItemId);
                        var date = DateTime.Now;
                        var sale = await _repositoryWrapper.Sale.Get(s =>
                            s.ItemId == item.Id && date >= s.StartDate && date <= s.EndDate);
                        if (sale != null)
                        {
                            item.Price = sale.SalePrice;
                            cartorderdto.Price = sale.SalePrice;
                        }
                        cartorderdto.Name = ErrorResponseException.GenerateLocalizedResponse(item.Name, item.ArName, language);
                        if(cartorderdto.Quantity!=0)
                        cart.TotalPrice = cart.TotalPrice+ (decimal)(item.Price * cartorderdto.Quantity);
                    }
                
            

         
            return (cart, null);
        }
        public async Task<(string? message, string? error)> AddToCart(Guid userId, CartForm cartForm,string language)
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
                    return (null,ErrorResponseException.GenerateLocalizedResponse("Product not found", "لم يتم العثور على المنتج", language));
                }

                // Check if the quantity is available
                if (product.Quantity < cartOrder.Quantity)
                {
                    return (null, ErrorResponseException.GenerateLocalizedResponse("Quantity not available", "الكمية غير متوفرة", language));
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
                    return (null, ErrorResponseException.GenerateLocalizedResponse("Product already exists in the cart", "المنتج موجود بالفعل في السلة", language));
                }
        
            }
            
    
            var result = await _repositoryWrapper.Cart.Update(cart);
            if (result == null)
            {
                return (null, ErrorResponseException.GenerateLocalizedResponse("Error in adding products to cart", "خطأ في اضافة المنتجات الى السلة", language));
            }
    
            return (ErrorResponseException.GenerateLocalizedResponse("Items added to cart","تمت اضافة المنتجات الى السلة",language), null);
        }
        public async Task<(string? message, string? error)> DeleteFromCart(Guid userId, Guid cartId, int quantity,string language)
        {
            var cart = await _repositoryWrapper.Cart.Get(x => x.UserId == userId, i => i.Include(x => x.ItemOrders));
            if (cart == null)
            {
                return (null, ErrorResponseException.GenerateLocalizedResponse("Cart not found", "لم يتم العثور على السلة", language));
            }
            
            var cartProduct = cart.ItemOrders.FirstOrDefault(x => x.ItemId == cartId);
            if (cartProduct == null)
            {
                return (null, ErrorResponseException.GenerateLocalizedResponse("Product not found in the cart", "لم يتم العثور على المنتج في السلة", language));
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
            
            return (ErrorResponseException.GenerateLocalizedResponse("Product is removed from the cart","تم حف المنتج من السلة",language), null);
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