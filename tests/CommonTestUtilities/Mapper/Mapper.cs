using AutoMapper;
using Tropical.Application.Services.autoMapper;

namespace CommonTestUtilities.Mapper
{
    public static class MapperBuilder
    {//tive que instalar o automapper nos packages, pois a interface IMapper não seria reconhecida
        public static IMapper Build() {
            var idEncipter = IdEncipterBuilder.IdEncipterBuilder.Build();
          return  new AutoMapper.MapperConfiguration(options => options.AddProfile(new AutoMapping(idEncipter)))
                        .CreateMapper();
        }
    }
}
