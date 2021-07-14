using System;

namespace POCJira.Repository.Entities
{
    public class ChamadoEntity
    {
        public long Id { get; set; }

        public String Status { get; set; }

        public String Nome { get; set; }

        public String Descricao { get; set; }

        public long Valor { get; set; }

        public String Responsavel { get; set; }
    }
}
