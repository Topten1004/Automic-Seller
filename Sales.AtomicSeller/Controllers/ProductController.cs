using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sales.AtomicSeller.Business.IServices;
using Sales.AtomicSeller.Consts;
using Sales.AtomicSeller.Entities;

namespace Sales.AtomicSeller.Controllers
{
    /// <summary>
    /// Producto managment.
    /// </summary>
    [Authorize(Policy = IdentityConsts.ADMINISTRATOR_POLICY)]
    public class ProductController : Controller
    {
        /// <summary>
        /// logger.
        /// </summary>
        private readonly ILogger<ProductController> logger;
        /// <summary>
        /// product service.
        /// </summary>
        private readonly IAtomicServiceService productService;
        /// <summary>
        /// contructor.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="productService"></param>
        public ProductController(ILogger<ProductController> logger,
            IAtomicServiceService productService)
        {
            this.logger = logger;
            this.productService = productService;
        }
        /// <summary>
        /// product list.
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            var products = await productService.Get();
            return View(products);
        }
        /// <summary>
        /// product edit view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            AtomicService product;
            if (id.HasValue && id > 0)
            {
                product = await productService.Get(id.Value);
            }
            else
            {
                product = new AtomicService();
            }

            return PartialView("_Edit", product);
        }
        /// <summary>
        /// product row view.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Row(int id)
        {
            var category = await productService.Get(id);
            return PartialView("_Row", category);
        }
        /// <summary>
        /// product post (add/edit).
        /// </summary>
        /// <param name="product"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Edit(AtomicService product)
        {
            try
            {
                if (product.Id > 0)
                    await productService.Update(product);
                else
                    await productService.Add(product);
                await productService.SaveChange();
                return await Task.Run(() => StatusCode(StatusCodes.Status200OK, Json(new { Id = product.Id })));
            }
            catch (Exception ex)
            {
                return await Task.Run(() => StatusCode(StatusCodes.Status500InternalServerError, Json(new { ex.Message })));
            }
        }
        /// <summary>
        /// product delete.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            await productService.Remove(id);
            await productService.SaveChange();
            return RedirectToAction("Index");
        }
    }
}
