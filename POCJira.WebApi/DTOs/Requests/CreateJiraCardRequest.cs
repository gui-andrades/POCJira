using System;

namespace POCJira.WebApi.DTOs.Requests
{
    public class CreateJiraCardRequest
    {
        public String Id { get; set; }

        public String Nome { get; set; }

        public String Descricao { get; set; }
    }
}
