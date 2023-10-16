using System.Text.Json;
using Microsoft.JSInterop;
using QuickOrderSystemClassLibrary;

namespace QuickOrderSystemCustomerWebApp.Data;

public class ProductCartService
{
    private readonly IJSRuntime _jsRuntime;
    private const string CartKey = "user_cart";

    public ProductCartService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task AddProductToCartAsync(Product product)
    {
        var cart = await GetCartAsync();
        var cartItem = cart.FirstOrDefault(p => p.Product.Id == product.Id);
        if (cartItem == null)
        {
            cart.Add(new CardItem { Product = product, Quantity = 1});
        }
        else
        {
            cartItem.Quantity++;
        }

        await SaveCartAsync(cart);
    }

    public async Task RemoveProductFromCartAsync(Product product)
    {
        var cart = await GetCartAsync();
        var cartItem = cart.FirstOrDefault(p => p.Product.Id == product.Id);
        if (cartItem != null)
        {
            cartItem.Quantity--;
            if (cartItem.Quantity <= 0)
            {
                cart.Remove(cartItem);
            }

            await SaveCartAsync(cart);
        }
    }

    public async Task ClearProductFromCartAsync(Product product)
    {
        var cart = await GetCartAsync();
        var cartItem = cart.FirstOrDefault(p => p.Product.Id == product.Id);
        if (cartItem != null)
        {
            cart.Remove(cartItem);
            await SaveCartAsync(cart);
        }
    }

    public async Task<List<CardItem>> GetCartAsync()
    {
        var cartString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CartKey);
        if (string.IsNullOrEmpty(cartString))
            return new List<CardItem>();

        return JsonSerializer.Deserialize<List<CardItem>>(cartString);
    }

    public async Task UpdateProductQuantityAsync(Product product, int quantity)
    {
        var cart = await GetCartAsync();
        var cartItem = cart.FirstOrDefault(p => p.Product.Id == product.Id);
        if (cartItem != null)
        {
            if (quantity <= 0)
            {
                cart.Remove(cartItem);
            }
            else
            {
                cartItem.Quantity = quantity;
            }

            await SaveCartAsync(cart);
        }
    }

    private async Task SaveCartAsync(List<CardItem> cart)
    {
        var cartString = JsonSerializer.Serialize(cart);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CartKey, cartString);
    }

    public async Task ClearCartAsync()
    {
        await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", CartKey);
    }

    public OrderItem ConvertToOrderItem(CardItem cardItem)
    {
        return new OrderItem
        {
            ProductId = cardItem.Product.Id,
            Quantity = cardItem.Quantity
        };
    }
}