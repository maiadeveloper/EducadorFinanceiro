using System;

namespace EducadorFinanceiro.Business.Models
{
    public abstract class EntityBase
    {
        public EntityBase()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }

        public DateTime DataCadastro { get; set; }

        public bool Ativo { get; set; }

        public DateTime DataExclusao { get; set; }
    }
}
