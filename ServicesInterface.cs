using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vendas.Model.Core;

namespace Vendas.View
{
    public interface ServicesInterface
    {

        #region Cliente
        bool AddNew_Cliente(Cliente cliente);

        List<Cliente> GetAll_Cliente();

        Cliente GetBySpecification_Cliente(int codigo);

        bool Update_Cliente(Cliente cliente);

        bool Delete_Cliente(Cliente cliente);
        #endregion

        #region Vendedor
        bool AddNew_Vendedor(Vendedor vendedor);

        List<Vendedor> GetAll_Vendedor();

        Vendedor GetBySpecification_Vendedor(int codigo);

        bool Update_Vendedor(Vendedor vendedor);

        bool Delete_Vendedor(Vendedor vendedor);
        #endregion

        #region Produto
        bool Update_Produto(Produto produto);

        bool AddNew_Produto(Produto produto);

        bool Delete_Produto(Produto produto);

        List<Produto> GetAll_Produto();

        Produto GetBySpecification_Produto(int codigo);
        #endregion

        #region Venda

        bool Update_Venda(Venda venda);

        bool AddNew_Venda(Venda venda);

        bool Delete_Venda(Venda venda);

        List<Venda> GetAll_Venda();

        Venda GetBySpecification_Venda(int codigo);

        #endregion

        #region Venda_Itens

        bool AddNew_VendaItens(Venda_Itens item);

        bool Delete_VendaItens(Venda_Itens item);

        List<Venda_Itens> GetAll_VendaItens();

        #endregion

        void ExecuteComand(string query);

        DataSet GetDataSet(string query);
        
    }
}
