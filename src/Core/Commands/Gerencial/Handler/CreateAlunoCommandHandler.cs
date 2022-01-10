using AutoMapper;
using Core.Entities.Gerencial;
using Core.Helpers;
using Core.Interfaces.Repositories.Gerencial;
using Core.Models.Responses.Gerencial;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Core.Interfaces.Helpers;
using System.IO;

namespace Core.Commands.Gerencial.Handler
{

    public class CreateAlunoCommandHandler : IRequestHandler<CreateAlunoCommand, Result<AlunoResponse>>
    {

        private readonly IAlunoRepository _repository;
		private readonly IBlobStorage _blobStorage;
        private readonly IMapper _mapper;

		public CreateAlunoCommandHandler(
			IAlunoRepository repository,
			IMapper mapper,
			IBlobStorage blobStorage)
        {
			_blobStorage = blobStorage;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<AlunoResponse>> Handle(CreateAlunoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<AlunoResponse>();

            if (String.IsNullOrEmpty(request.Request.Nome)
                || String.IsNullOrEmpty(request.Request.Segmento)
                || request.Request.DataNascimento < DateTime.Parse("1900-01-01")
            )
            {
                result.WithError("Nome, segmento ou a data de nascimento estão inválidos!");
                return result;
            }

            var registro = _mapper.Map<Aluno>(request.Request);
            registro.UnidadeAcessoId = await _repository.GetSelectedAccessUnitIdAsync();
            var response = await _repository.AddAsync(registro);

            if (!String.IsNullOrEmpty(request.Request.FotoBase64) && !String.IsNullOrEmpty(request.Request.FotoTipo))
            {

                BlobContainerClient blobContainerClient = _blobStorage.CheckIfExistsBlobContainer("eem-usuarios-fotos");

                if (blobContainerClient == null)
                {
                    blobContainerClient = await _blobStorage.CreateBlobContainerAsync("eem-usuarios-fotos");
                }

                var bytes = Convert.FromBase64String(request.Request.FotoBase64);
                Stream stream = new MemoryStream(bytes);
                string arquivoUrl = await _blobStorage.UploadFileAsync($"arquivo/{request.Request.FotoTipo}", stream, "eem-usuarios-fotos", blobContainerClient);

                if (!String.IsNullOrEmpty(arquivoUrl))
                {
                    registro.FotoUrl = arquivoUrl;
                    response = await _repository.AddAsync(registro);
                }
                
            }

            result.Value = _mapper.Map<AlunoResponse>(response);
            return result;

        }

    }

}
