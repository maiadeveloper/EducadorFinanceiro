using EducadorFinanceiro.Business.Interfaces;
using EducadorFinanceiro.Business.Models;
using EducadorFinanceiro.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EducadorFinanceiro.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : EntityBase, new()
    {
        protected readonly MeuDbContext _meuDbContext;
        protected readonly DbSet<TEntity> dbSet;

        public Repository(MeuDbContext meuDbContext)
        {
            _meuDbContext = meuDbContext;
            dbSet = meuDbContext.Set<TEntity>();
        }

        public async Task Adicionar(TEntity entity)
        {
            dbSet.Add(entity);
           await SaveChanges();
        }

        public async Task Atualizar(TEntity entity)
        {
            dbSet.Update(entity);
            await SaveChanges();
        }

        public async Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicate)
        {
            return await dbSet.AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<TEntity> ObterPorId(Guid id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task<List<TEntity>> ObterTodos()
        {
            return await dbSet.ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            dbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }

        public async Task<int> SaveChanges()
        {
            return await _meuDbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _meuDbContext?.Dispose();
        }
    }
}
