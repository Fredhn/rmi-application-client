using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Vendas.Model.Core
{
    [Serializable]
    public class Vendedor
    {
        [Key]
        public int codigo { get; set; }

        [MaxLength(200)]
        public string nome { get; set; }

        [MaxLength(20)]
        public string telefone { get; set; }
    }
}
