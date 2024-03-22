using CouchDB.Driver.Extensions;
using CRUD_couchdb.Dominio;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace CRUD_couchdb.Controllers
{
[ApiController]
[Route("[controller]")]
public class ProdutoController : ControllerBase
{
    private readonly CouchContext _couchContext;

    public ProdutoController(CouchContext couchContext)
    {
        _couchContext = couchContext;
    }

    [HttpGet]
    public async Task<List<Produto>> Get()
    {
        return await _couchContext
               .Produtos
               .ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<Produto> Get([FromRoute] string id)
    {
        return await _couchContext
               .Produtos
               .FirstOrDefaultAsync(x => x.Id.Equals(id));
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] Produto produto)
    {
        var produtoCriado = await _couchContext
                            .Produtos
                            .AddAsync(produto);

        var url = Request.GetDisplayUrl();
        return Created($"{url}/{produtoCriado.Id}", produtoCriado);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete([FromRoute] string id)
    {
        var produto = await _couchContext
                      .Produtos
                      .Where(x => x.Id == id)
                      .FirstOrDefaultAsync();

        await _couchContext.Produtos
        .RemoveAsync(produto);

        return Ok();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update([FromRoute] string id, [FromBody] Produto produto)
    {
        var produtoEncontrado = await _couchContext
                                .Produtos
                                .Where(x => x.Id == id)
                                .FirstOrDefaultAsync();

        if (produtoEncontrado != null)
        {
            produto.Rev = produtoEncontrado.Rev;
            produto.Id = produtoEncontrado.Id;
            await _couchContext.Produtos
            .AddOrUpdateAsync(produto);
        }

        return Ok();
    }
}
}
