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
using Core.Enums.Gerencial;

namespace Core.Commands.Gerencial.Handler
{

    public class UpdateAlunoCommandHandler : IRequestHandler<UpdateAlunoCommand, Result<AlunoResponse>>
    {

        private readonly IAlunoRepository _repository;
		private readonly IBlobStorage _blobStorage;
        private readonly IMapper _mapper;

		public UpdateAlunoCommandHandler(
			IAlunoRepository repository,
			IMapper mapper,
			IBlobStorage blobStorage)
        {
			_blobStorage = blobStorage;
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<Result<AlunoResponse>> Handle(UpdateAlunoCommand request, CancellationToken cancellationToken)
        {

            var result = new Result<AlunoResponse>();
            IDictionary<string, string> dic = Enum.GetValues(typeof(EnumSegmento)).Cast<object>().ToDictionary(v => ((Enum)v).ObterDescricaoEnum(), k => ((Enum)k).Valor());
            var oldRegister = await _repository.GetById(request.Id);

            if (oldRegister == null)
            {
                result.WithNotFound("Registro não encontrado!");
                return result;
            }

            if (String.IsNullOrEmpty(request.Request.Nome)
                || String.IsNullOrEmpty(request.Request.Segmento)
                || request.Request.DataNascimento < DateTime.Parse("1900-01-01")
            )
            {
                result.WithError("Nome, segmento ou a data de nascimento estão inválidos!");
                return result;
            }

            if (String.IsNullOrEmpty(dic.Where(gc => gc.Value == request.Request.Segmento).FirstOrDefault().Value))
            {
                result.WithError("Segmento não encontrado. Informe um que seja válido!");
                return result;
            }

            if (request.Request.Segmento.ToUpper() is "FUNDAMENTAL" && String.IsNullOrEmpty(request.Request.Email))
            {
                result.WithError("Para alunos do ensino fundamental, o e-mail é obrigatório!");
                return result;
            }

            var registerForUpdate = _mapper.Map(request.Request, oldRegister);
            if (await _repository.UpdateAsync(oldRegister))
                result.Value = _mapper.Map<AlunoResponse>(registerForUpdate);

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
                    registerForUpdate = _mapper.Map(request.Request, oldRegister);
                    if (await _repository.UpdateAsync(oldRegister))
                        result.Value = _mapper.Map<AlunoResponse>(registerForUpdate);
                }

            }

            return result;

        }

    }

}
