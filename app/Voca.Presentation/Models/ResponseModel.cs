using System.Collections.Generic;
using Voca.Domain.Entities;

namespace Voca.Presentation.Models
{
    public class ResponseModel
    {
        public string status { get; set; }
        public List<string> error_messages { get; set; }
        public List<Noun> nouns { get; set; }
    }
}