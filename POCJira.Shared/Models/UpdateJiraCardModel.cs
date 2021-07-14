using System;

namespace POCJira.Shared.Models
{
    public class UpdateJiraCardModel
    {
        public String Id { get; set; }

        public String Nome { get; set; }

        public String Status { get; set; }

        public String Descricao { get; set; }

        public String Valor { get; set; }

        public String Responsavel { get; set; }
    }
}
