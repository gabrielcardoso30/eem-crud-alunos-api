using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Helpers;
using Core.Interfaces.Repositories.Security;
using Core.Models.Responses.Security;
using MediatR;

namespace Core.Queries.Security.Handler
{
    public class GetUsuarioByIdQueryHandler : IRequestHandler<GetUsuarioByIdQuery, Result<UsuarioResponse>>
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        public GetUsuarioByIdQueryHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
        }
        public async Task<Result<UsuarioResponse>> Handle(GetUsuarioByIdQuery query, CancellationToken cancellationToken)
        {
            var result = new Result<UsuarioResponse>();

            var perfil = await _usuarioRepository.GetByIdWithPermission(query.Id);
            var cpf = perfil.CPF;
            
            if (perfil == null)
            {
                result.WithNotFound("Usuario não encontrado!");
                return result;
            }

            result.Value = _mapper.Map<UsuarioResponse>(perfil);

            return result;
        }
    }
}
