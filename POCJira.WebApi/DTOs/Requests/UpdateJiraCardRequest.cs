using System;

namespace POCJira.WebApi.DTOs.Requests
{
    public class UpdateJiraCardRequest
    {
        public String Id { get; set; }

        public String Nome { get; set; }

        public String Status { get; set; }

        public String Descricao { get; set; }

        public String Valor { get; set; }

        public String Responsavel { get; set; }
    }
}
