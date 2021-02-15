using Roulette.Entities.Utils;
using System.Collections.Generic;

namespace Roulette.Entities.Responses
{
    public class Response
    {
        public int StatusCode { get; set; }
        public List<Error> Errors { get; set; }
        public object Data { get; set; }
        
        public Response()
        {
            this.Errors = new List<Error>();
        }
    }
}
