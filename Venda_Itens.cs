using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vendas.Model.Core
{
    [Serializable]
    public class Venda_Itens
    {
        [Key]
        public int codigo { get; set; }

        public int codigo_Venda { get; set; }

        public int codigo_Produto { get; set; }

        //[ForeignKey("codigo_Venda")]
        //public Venda codigo_venda { get; set; }

        //[ForeignKey("codigo_Produto")]
        //public Produto produto { get; set; }

        public int quantidade { get; set; }
    }
}
