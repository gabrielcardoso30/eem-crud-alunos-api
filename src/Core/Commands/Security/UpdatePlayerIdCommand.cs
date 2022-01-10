using MediatR;
using System;
using Core.Helpers;

namespace Core.Commands.Security
{
    public class UpdatePlayerIdCommand : IRequest<Result<Result>>
    {
        public Guid Id;
        public string PlayerId { get; set; }

        public UpdatePlayerIdCommand(Guid id, string playerId)
        {
            Id = id;
            PlayerId = playerId;
        }
    }
}
