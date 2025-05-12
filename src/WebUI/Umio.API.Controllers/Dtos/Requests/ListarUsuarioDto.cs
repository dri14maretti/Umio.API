using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Umio.API.Controllers.Dtos.Requests
{
    public class ListarUsuarioDto
    {
        public Guid Id { get; set; }
        public Guid ProvedorId { get; set; }
        public Guid ClienteId { get; set; }
    }
}