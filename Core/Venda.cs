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
    public class Venda
    {
        [Key]
        public int codigo { get; set; }

        public int codigo_Vendedor { get; set; }

        public int codigo_Cliente { get; set; }

        //[ForeignKey("codigo_Vendedor")]
        //[MaxLength(20)]
        //public Vendedor vendedor { get; set; }

        //[ForeignKey("cpf_Cliente")]
        //[MaxLength(20)]
        //public Cliente cliente { get; set; }

        [MaxLength(20)]
        public string forma_pagamento { get; set; }

        [MaxLength(50)]
        public string data { get; set; }

        public float total_compra { get; set; }

        public float desconto { get; set; }

        public float valor_final { get; set; }
    }
}
