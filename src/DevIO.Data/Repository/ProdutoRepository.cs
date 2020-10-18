using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Interfaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
  public class ProdutoRepository : Repository<Produto>, IProdutoRepository
  {
    public ProdutoRepository(DataContext context) : base(context)
    {

    }

    public async Task<Produto> ObterFornecedorEndereco(Guid id)
    {
      return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
        .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Produto>> ObterProdutosFornecedores()
    {
      return await Db.Produtos.AsNoTracking().Include(f => f.Fornecedor)
        .OrderBy(p => p.Nome).ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ObterProdutosPorFornecedor(Guid fornecedorId)
    {
      return await Buscar(p => p.FornecedorId == fornecedorId);
    }
  }
}