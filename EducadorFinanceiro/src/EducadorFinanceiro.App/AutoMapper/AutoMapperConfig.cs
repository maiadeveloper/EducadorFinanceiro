using AutoMapper;
using EducadorFinanceiro.App.ViewModels;
using EducadorFinanceiro.Business.Models;

namespace EducadorFinanceiro.App.AutoMapper
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<Categoria, CategoriaViewModel>().ReverseMap();
            CreateMap<SubCategoria, SubCategoriaViewModel>().ReverseMap();
            CreateMap<Favorecido, FavorecidoViewModel>().ReverseMap();
            CreateMap<Lancamento, LancamentoViewModel>().ReverseMap();
        }
    }
}
