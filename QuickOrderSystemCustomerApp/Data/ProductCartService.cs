using System.Text.Json;
using Microsoft.JSInterop;
using QuickOrderSystemClassLibrary;

namespace QuickOrderSystemCustomerApp.Data;

public class ProductCartService
{
    private readonly IJSRuntime _jsRuntime;
    private const string CART_KEY = "user_cart";

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
            cart.Add(new CartItem {Product = product, Quantity = 1});
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

    public async Task<List<CartItem>> GetCartAsync()
    {
        var cartString = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", CART_KEY);
        if (string.IsNullOrEmpty(cartString))
            return new List<CartItem>();

        return JsonSerializer.Deserialize<List<CartItem>>(cartString);
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

    private async Task SaveCartAsync(List<CartItem> cart)
    {
        var cartString = JsonSerializer.Serialize(cart);
        await _jsRuntime.InvokeVoidAsync("localStorage.setItem", CART_KEY, cartString);
    }
}

public class CartItem
{
    public Product Product { get; set; }
    public int Quantity { get; set; }
}