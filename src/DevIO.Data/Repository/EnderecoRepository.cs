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
  public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
  {
    public EnderecoRepository(DataContext context) : base(context)
    {

    }

    public async Task<Endereco> ObterEnderecoPorFornecedor(Guid fornecedorId)
    {
      return await Db.Enderecos.AsNoTracking().Include(f => f.Fornecedor)
        .FirstOrDefaultAsync(e => e.FornecedorId == fornecedorId);
    }
  }
}