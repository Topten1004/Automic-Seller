using Sales.AtomicSeller.Entities;
using Sales.AtomicSeller.Repositories;

namespace Sales.AtomicSeller.Business.IServices
{
    /// <summary>
    /// Cart service.
    /// </summary>
    public interface ICartService : IRepository<Cart>
    {
        /// <summary>
        /// Add product to cart.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ValueTask<Cart> AddCartItem(int productId, bool monthOrAnnaul);
        /// <summary>
        /// Get current cart.
        /// </summary>
        /// <param name="createIfNotExist"></param>
        /// <returns></returns>
        ValueTask<Cart> GetCurrent(bool createIfNotExist);
        /// <summary>
        /// Remove product from cart.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ValueTask<Cart> RemoveCartItem(int productId);
        /// <summary>
        /// Up product quantity.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ValueTask<Cart> UpCartItemQuantity(int productId);
        /// <summary>
        /// Down product quantity.
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        ValueTask<Cart> DownCartItemQuantity(int productId);
        /// <summary>
        /// Get current cart product count.
        /// </summary>
        /// <returns></returns>
        ValueTask<int> GetCurrentCount();
        /// <summary>
        /// Get cart by session id.
        /// </summary>
        /// <param name="sessionId"></param>
        /// <returns></returns>
        ValueTask<Cart> GetBySessionId(string sessionId);
        ValueTask<Cart> SetTransactionId(int id, string transactionId);
        ValueTask<Cart> GetByTransactionId(string transactionId);
        ValueTask<Cart> SetSessionId(int id, string sessionId);
    }
}
