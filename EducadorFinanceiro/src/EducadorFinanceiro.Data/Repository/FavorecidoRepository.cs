using EducadorFinanceiro.Business.Interfaces;
using EducadorFinanceiro.Business.Models;
using EducadorFinanceiro.Data.Context;

namespace EducadorFinanceiro.Data.Repository
{
    public class FavorecidoRepository : Repository<Favorecido>, IFavorecidoRepository
    {
        public FavorecidoRepository(MeuDbContext meuDbContext) : base(meuDbContext) { }
    }
}
