using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Vendas.Control.SqlDBConnection;
using System.Reflection;
using Vendas.View;
using Vendas.Model.Core;


namespace Vendas.View
{
    public partial class Form_Vendas : Form
    {

        private float valorFinal = 0;
        private float valorCompra = 0;
        private float desconto = 0;
        Cliente cliente;
        bool alteraCliente = false;
        Vendedor vendedor;
        bool alteraVendedor = false;
        Produto produto;
        bool alteraProduto = false;
        Venda venda;
        Venda_Itens vendaItens;
        bool alteraVenda = false;

        public Form_Vendas()
        {
            RemoteStatic.RegistrarCanal();
            InitializeComponent();
            pnlLogin.BringToFront();
        }

        public void Form_Vendas_OnLoad(object sender, EventArgs e)
        {
            montaGrids();
        }

        #region TELA LOGIN
        private void btn_pnlLogin_Entra_Click(object sender, EventArgs e)
        {
            try
            {
                if (comboBox_pnlLogin_Vendedor.SelectedValue != null)
                {
                    var cdgLogin = int.Parse(comboBox_pnlLogin_Vendedor.SelectedValue.ToString());
                    //vendedor = RemoteStatic.remoteObject.GetBySpecification_Vendedor(cdgLogin);
                    if (tb_pnlLogin_Senha.Text == cdgLogin.ToString() && comboBox_pnlLogin_Vendedor.SelectedValue != null)
                    {
                        pnlLogin.Visible = false;
                        tb_pnlLogin_Senha.Text = "";
                    }
                    else
                    {
                        ToolTip tp = new ToolTip();
                        int VisibleTime = 1000;
                        tp.Show("Senha incorreta!", tb_pnlLogin_Senha, 232, 0, VisibleTime);
                    }
                }
                else
                {
                    ToolTip tp = new ToolTip();
                    int VisibleTime = 1000;
                    tp.Show("Selecione um vendedor cadastrado!", comboBox_pnlLogin_Vendedor, 232, 0, VisibleTime);
                }
            }
            catch
            {

            }
        }
        #endregion

        #region MONTA GRIDVIEWS
        //MONTA GRIDS
        public void montaGrids()
        {
            try
            {
                // TODO: This line of code loads data into the 'vendasOkDataSet.Produto' table. You can move, or remove it, as needed.
                this.produtoTableAdapter.Fill(this.vendasOkDataSet.Produto);
                // TODO: This line of code loads data into the 'vendasOkDataSet.Vendedor' table. You can move, or remove it, as needed.
                this.vendedorTableAdapter.Fill(this.vendasOkDataSet.Vendedor);
                // TODO: This line of code loads data into the 'vendasOkDataSet.Venda' table. You can move, or remove it, as needed.
                this.vendaTableAdapter.Fill(this.vendasOkDataSet.Venda);
                // TODO: This line of code loads data into the 'vendasOkDataSet.Cliente' table. You can move, or remove it, as needed.
                this.clienteTableAdapter.Fill(this.vendasOkDataSet.Cliente);

                var clientes = RemoteStatic.remoteObject.GetAll_Cliente();
                dg_ViewCliente.DataSource = clientes;

                dg_ViewCliente.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewCliente.Columns["CODIGO"].HeaderText = "Código";

                dg_ViewCliente.Columns["CPF"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewCliente.Columns["CPF"].HeaderText = "CPF";

                dg_ViewCliente.Columns["NOME"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewCliente.Columns["NOME"].HeaderText = "Nome";

                dg_ViewCliente.Columns["ENDERECO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewCliente.Columns["ENDERECO"].HeaderText = "Endereço";

                dg_ViewCliente.Columns["TELEFONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewCliente.Columns["TELEFONE"].HeaderText = "Telefone";

                var vendedores = RemoteStatic.remoteObject.GetAll_Vendedor();
                dg_ViewVendedor.DataSource = vendedores;

                dg_ViewVendedor.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendedor.Columns["CODIGO"].HeaderText = "Código";

                dg_ViewVendedor.Columns["NOME"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendedor.Columns["NOME"].HeaderText = "Nome";

                dg_ViewVendedor.Columns["TELEFONE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendedor.Columns["TELEFONE"].HeaderText = "Telefone";

                var produtos = RemoteStatic.remoteObject.GetAll_Produto();
                dg_ViewProdutos.DataSource = produtos;

                dg_ViewProdutos.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewProdutos.Columns["CODIGO"].HeaderText = "Código";

                dg_ViewProdutos.Columns["DESCRICAO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewProdutos.Columns["DESCRICAO"].HeaderText = "Descrição";

                dg_ViewProdutos.Columns["PRECO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewProdutos.Columns["PRECO"].HeaderText = "Preço";

                var vendas = RemoteStatic.remoteObject.GetAll_Venda();
                dg_ViewVendas.DataSource = vendas;

                dg_ViewVendas.Columns["CODIGO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["CODIGO"].HeaderText = "Código";

                dg_ViewVendas.Columns["CODIGO_VENDEDOR"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["CODIGO_VENDEDOR"].HeaderText = "Vendedor";

                dg_ViewVendas.Columns["CODIGO_CLIENTE"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["CODIGO_CLIENTE"].HeaderText = "Cliente";

                dg_ViewVendas.Columns["FORMA_PAGAMENTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["FORMA_PAGAMENTO"].HeaderText = "Pagamento";

                dg_ViewVendas.Columns["DATA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["DATA"].HeaderText = "Data";

                dg_ViewVendas.Columns["TOTAL_COMPRA"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["TOTAL_COMPRA"].HeaderText = "Total";

                dg_ViewVendas.Columns["DESCONTO"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["DESCONTO"].HeaderText = "Desconto";

                dg_ViewVendas.Columns["VALOR_FINAL"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                dg_ViewVendas.Columns["VALOR_FINAL"].HeaderText = "Pago";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString(), "Report", MessageBoxButtons.AbortRetryIgnore, MessageBoxIcon.Error);
            }
        }
        private void Form_Vendas_Load(object sender, EventArgs e)
        {
            montaGrids();
        }
        #endregion

        #region MENU PRINCIPAL: EVENTOS DE CLICK
        private void pb_Menu_AddCliente_Click(object sender, EventArgs e)
        {
            pnl_Cliente.Visible = true;
            pnl_Home.Visible = false;
            pnl_Produtos.Visible = false;
            pnl_Vendas.Visible = false;
            pnl_Vendedor.Visible = false;
        }

        private void pb_Menu_AddVendedor_Click(object sender, EventArgs e)
        {
            pnl_Cliente.Visible = false;
            pnl_Home.Visible = false;
            pnl_Produtos.Visible = false;
            pnl_Vendas.Visible = false;
            pnl_Vendedor.Visible = true;
        }

        private void pb_Menu_AddProduto_Click(object sender, EventArgs e)
        {
            pnl_Cliente.Visible = false;
            pnl_Home.Visible = false;
            pnl_Produtos.Visible = true;
            pnl_Vendas.Visible = false;
            pnl_Vendedor.Visible = false;
        }

        private void pb_Menu_AddVenda_Click(object sender, EventArgs e)
        {
            pnl_Cliente.Visible = false;
            pnl_Home.Visible = false;
            pnl_Produtos.Visible = false;
            pnl_Vendas.Visible = true;
            pnl_Vendedor.Visible = false;
        }

        private void pb_Menu_ConfigLocal_Click(object sender, EventArgs e)
        {
            pnl_Cliente.Visible = false;
            pnl_Home.Visible = true;
            pnl_Produtos.Visible = false;
            pnl_Vendas.Visible = false;
            pnl_Vendedor.Visible = false;
        }

        private void pb_Menu_Logoff_Click(object sender, EventArgs e)
        {
            pnlLogin.Visible = true;
        }

        private void pb_Menu_Logoff_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Efetuar logoff", pb_pnl_Menu_Logoff, 0, 63, VisibleTime);
        }
        #endregion

        #region CADASTRO: CLIENTE
        //EVENTOS: CADASTRO CLIENTE
        private void pb_pnlCliente_Add_Click(object sender, EventArgs e)
        {
            alteraCliente = false;
            pnlCliente_Add.Visible = true;
            limpaCamposCliente();
        }

        private void pb_pnlCliente_Add_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Cadastrar novo cliente", pb_pnlCliente_Add, 0, 63, VisibleTime);
        }

        private void pb_pnlCliente_Edit_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Editar cadastro de cliente", pb_pnlCliente_Edit, 0, 63, VisibleTime);
        }

        private void pb_pnlCliente_Del_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Deletar cadastro de cliente", pb_pnlCliente_Del, 0, 63, VisibleTime);
        }

        private void pb_pnlCliente_AddCancel_Click(object sender, EventArgs e)
        {
            pnlCliente_Add.Visible = false;
            limpaCamposCliente();
        }

        private void pb_pnlCliente_SubmitToDb_Click(object sender, EventArgs e)
        {

            if (alteraCliente == false && 
                tb_pnlCliente_Add_Cpf.Text != "" && 
                tb_pnlCliente_Add_Cpf.Text.Length <= 11 && 
                tb_pnlCliente_Add_Nome.Text != "")
            {
                cliente = new Cliente();
                cliente.cpf = tb_pnlCliente_Add_Cpf.Text;
                cliente.nome = tb_pnlCliente_Add_Nome.Text;
                cliente.telefone = tb_pnlCliente_Add_Telefone.Text;
                cliente.endereco = tb_pnlCliente_Add_Endereco.Text;

                RemoteStatic.remoteObject.AddNew_Cliente(cliente);
                MessageBox.Show("Cliente cadastrado!", "Informação", MessageBoxButtons.OK);

                //Cliente novoCliente = new Cliente(tb_pnlCliente_Add_Cpf.Text, tb_pnlCliente_Add_Nome.Text, tb_pnlCliente_Add_Endereco.Text.ToString(), tb_pnlCliente_Add_Telefone.Text);
            }
            else if (alteraCliente ==  true)
            {
                cliente.cpf = tb_pnlCliente_Add_Cpf.Text;
                cliente.nome = tb_pnlCliente_Add_Nome.Text;
                cliente.telefone = tb_pnlCliente_Add_Telefone.Text;
                cliente.endereco = tb_pnlCliente_Add_Endereco.Text;

                RemoteStatic.remoteObject.Update_Cliente(cliente);
            }
            else
            {
                MessageBox.Show(this, "Os campos CPF e NOME são obrigatórios, e o CPF deve conter no máximo 11 dígitos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            montaGrids();
            pnlCliente_Add.Visible = false;

            limpaCamposCliente();
        }

        public void limpaCamposCliente()
        {
            //LimpaCampos
            tb_pnlCliente_Add_Cpf.Text = "";
            tb_pnlCliente_Add_Nome.Text = "";
            tb_pnlCliente_Add_Endereco.Text = "";
            tb_pnlCliente_Add_Telefone.Text = "";
        }

        public void carregaClienteData()
        {
            alteraCliente = true;
            int index = dg_ViewCliente.SelectedRows[0].Index;

            DataGridViewRow row = dg_ViewCliente.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewCliente["CODIGO", row.Index].Value);
            if (index >= 0)
            {
                cliente = RemoteStatic.remoteObject.GetBySpecification_Cliente(codigo);

                tb_pnlCliente_Add_Cpf.Text = cliente.cpf.ToString();
                tb_pnlCliente_Add_Nome.Text = cliente.nome.ToString();
                tb_pnlCliente_Add_Endereco.Text = cliente.endereco.ToString();
                tb_pnlCliente_Add_Telefone.Text = cliente.telefone.ToString();

                pnlCliente_Add.Visible = true;
                pnlCliente_Add.BringToFront();
            }
        }

        public void dg_ViewCliente_EditDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            carregaClienteData();
            montaGrids();
        }

        private void pb_pnlCliente_Edit_Click(object sender, EventArgs e)
        {
            carregaClienteData();
            montaGrids();
        }

        private void pb_pnlCliente_Del_Click(object sender, EventArgs e)
        {
            int index = dg_ViewCliente.SelectedRows[0].Index;

            DataGridViewRow row = dg_ViewCliente.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewCliente["CODIGO", row.Index].Value);

            DialogResult teste = MessageBox.Show("Tem certeza que deseja excluir o cliente?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            string yesno = teste.ToString();

            if (yesno == "Yes")
            {
                if (RemoteStatic.remoteObject.Delete_Cliente(RemoteStatic.remoteObject.GetBySpecification_Cliente(codigo)))
                    MessageBox.Show("Cliente excluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Erro! Cliente não excluído.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            montaGrids();
        }
        #endregion

        #region CADASTRO: VENDEDOR
        //EVENTO: CADASTRO VENDEDOR
        private void pb_pnlVendedor_Add_Click(object sender, EventArgs e)
        {
            pnlVendedor_Add.Visible = true;
        }

        private void pb_pnlVendedor_Add_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Cadastrar novo vendedor", pb_pnlVendedor_Add, 0, 63, VisibleTime);
        }

        private void pb_pnlVendedor_Edit_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Editar cadastro de vendedor", pb_pnlVendedor_Edit, 0, 63, VisibleTime);
        }

        private void pb_pnlVendedor_Del_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Deletar cadastro de vendedor", pb_pnlVendedor_Del, 0, 63, VisibleTime);
        }
        private void pb_pnlVendedor_AddCancel_Click(object sender, EventArgs e)
        {
            pnlVendedor_Add.Visible = false;
            limpaCamposVendedor();
        }
        private void pb_pnlVendedor_SubmitToDb_Click(object sender, EventArgs e)
        {
            if (alteraVendedor == false &&
                tb_pnlVendedor_Add_Nome.Text != "" &&
                tb_pnlVendedor_Add_Telefone.Text != "")
            {
                vendedor = new Vendedor();
                vendedor.nome = tb_pnlVendedor_Add_Nome.Text;
                vendedor.telefone = tb_pnlVendedor_Add_Telefone.Text;

                RemoteStatic.remoteObject.AddNew_Vendedor(vendedor);
                MessageBox.Show("Vendedor cadastrado!", "Informação", MessageBoxButtons.OK);

                //Cliente novoCliente = new Cliente(tb_pnlCliente_Add_Cpf.Text, tb_pnlCliente_Add_Nome.Text, tb_pnlCliente_Add_Endereco.Text.ToString(), tb_pnlCliente_Add_Telefone.Text);
            }
            else if (alteraVendedor == true)
            {
                vendedor.nome = tb_pnlVendedor_Add_Nome.Text;
                vendedor.telefone = tb_pnlVendedor_Add_Telefone.Text;

                RemoteStatic.remoteObject.Update_Vendedor(vendedor);
            }
            else
            {
                MessageBox.Show(this, "Os campos NOME e TELEFONE são obrigatórios.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            montaGrids();
            pnlVendedor_Add.Visible = false;

            limpaCamposVendedor();
        }

        public void limpaCamposVendedor()
        {
            //LimpaCampos
            tb_pnlVendedor_Add_Nome.Text = "";
            tb_pnlVendedor_Add_Telefone.Text = "";
        }

        public void carregaVendedorData()
        {
            alteraVendedor = true;
            int index = dg_ViewVendedor.SelectedRows[0].Index;

            DataGridViewRow row = dg_ViewVendedor.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewVendedor["CODIGO", row.Index].Value);
            if (index >= 0)
            {
                vendedor = RemoteStatic.remoteObject.GetBySpecification_Vendedor(codigo);

                tb_pnlVendedor_Add_Nome.Text = vendedor.nome.ToString();
                tb_pnlVendedor_Add_Telefone.Text = vendedor.telefone.ToString();

                pnlVendedor_Add.Visible = true;
                pnlVendedor_Add.BringToFront();
            }
        }

        public void dg_ViewVendedor_EditDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            carregaVendedorData();
            montaGrids();
        }

        private void pb_pnlVendedor_Edit_Click(object sender, EventArgs e)
        {
            carregaVendedorData();
            montaGrids();
        }

        private void pb_pnlVendedor_Del_Click(object sender, EventArgs e)
        {
            int index = dg_ViewVendedor.SelectedRows[0].Index;

            DataGridViewRow row = dg_ViewVendedor.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewVendedor["CODIGO", row.Index].Value);

            DialogResult teste = MessageBox.Show("Tem certeza que deseja excluir o vendedor?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            string yesno = teste.ToString();

            if (yesno == "Yes")
            {
                if (RemoteStatic.remoteObject.Delete_Vendedor(RemoteStatic.remoteObject.GetBySpecification_Vendedor(codigo)))
                    MessageBox.Show("Vendedor excluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Erro! Vendedor não excluído.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            montaGrids();
        }
        #endregion

        #region CADASTRO: PRODUTO
        //EVENTO: CADASTRO PRODUTOS
        private void pb_pnlProdutos_Add_Click(object sender, EventArgs e)
        {
            pnlProduto_Add.Visible = true;
        }
        private void pb_pnlProdutos_Add_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Cadastrar novo produto", pb_pnlProdutos_Add, 0, 63, VisibleTime);
        }

        private void pb_pnlProdutos_Edit_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Editar cadastro de produto", pb_pnlProdutos_Edit, 0, 63, VisibleTime);
        }

        private void pb_pnlProdutos_Del_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Deletar cadastro de produto", pb_pnlProdutos_Del, 0, 63, VisibleTime);
        }
        private void pb_pnlProduto_AddCancel_Click(object sender, EventArgs e)
        {
            pnlProduto_Add.Visible = false;
            limpaCamposProduto();
        }

        private void pb_pnlProduto_SubmitToDb_Click(object sender, EventArgs e)
        {
            if (alteraProduto == false &&
                            tb_pnlProduto_Add_Descricao.Text != "" &&
                            tb_pnlProduto_Add_Preco.Text != "")
            {
                produto = new Produto();
                produto.descricao = tb_pnlProduto_Add_Descricao.Text;
                produto.preco = float.Parse(tb_pnlProduto_Add_Preco.Text);

                RemoteStatic.remoteObject.AddNew_Produto(produto);
                MessageBox.Show("Produto cadastrado!", "Informação", MessageBoxButtons.OK);
            }
            else if (alteraProduto == true)
            {
                produto.descricao = tb_pnlProduto_Add_Descricao.Text;
                produto.preco = float.Parse(tb_pnlProduto_Add_Preco.Text);

                RemoteStatic.remoteObject.Update_Produto(produto);
            }
            else
            {
                MessageBox.Show(this, "Os campos DESCRIÇÃO e PREÇO são obrigatórios.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            montaGrids();
            pnlProduto_Add.Visible = false;

            limpaCamposProduto();
        }

        public void limpaCamposProduto()
        {
            tb_pnlProduto_Add_Descricao.Text = "";
            tb_pnlProduto_Add_Preco.Text = "";
        }

        public void carregaProdutoData()
        {
            alteraProduto = true;

            int index = dg_ViewProdutos.SelectedRows[0].Index;
            DataGridViewRow row = dg_ViewProdutos.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewProdutos["CODIGO", row.Index].Value);

            if (index >= 0)
            {
                produto = RemoteStatic.remoteObject.GetBySpecification_Produto(codigo);

                tb_pnlProduto_Add_Descricao.Text = produto.descricao.ToString();
                tb_pnlProduto_Add_Preco.Text = produto.preco.ToString();

                pnlProduto_Add.Visible = true;
                pnlProduto_Add.BringToFront();
            }
        }

        public void dg_ViewProdutos_EditDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            carregaProdutoData();
            montaGrids();
        }

        private void pb_pnlProdutos_Edit_Click_1(object sender, EventArgs e)
        {
            carregaProdutoData();
            montaGrids();
        }

        private void pb_pnlProdutos_Del_Click(object sender, EventArgs e)
        {
            int index = dg_ViewProdutos.SelectedRows[0].Index;

            DataGridViewRow row = dg_ViewProdutos.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewProdutos["CODIGO", row.Index].Value);

            DialogResult teste = MessageBox.Show("Tem certeza que deseja excluir o produto?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            string yesno = teste.ToString();

            if (yesno == "Yes")
            {
                if (RemoteStatic.remoteObject.Delete_Produto(RemoteStatic.remoteObject.GetBySpecification_Produto(codigo)))
                    MessageBox.Show("Vendedor excluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Erro! Vendedor não excluído.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            montaGrids();
        }
        #endregion

        #region CADASTRO: VENDA
        //EVENTO: CADASTRO VENDA
        private void pb_pnlVendas_Add_Click(object sender, EventArgs e)
        {
            pnlVenda_Add.Visible = true;
        }
        private void pb_pnlVendas_Add_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Cadastrar nova venda", pb_pnlVendas_Add, 0, 63, VisibleTime);
        }

        private void pb_pnlVendas_Del_MouseHover(object sender, EventArgs e)
        {
            ToolTip tp = new ToolTip();
            int VisibleTime = 1000;
            tp.Show("Deletar cadastro de venda", pb_pnlVendas_Del, 0, 63, VisibleTime);
        }
        private void pb_pnlVendas_AddCancel_Click(object sender, EventArgs e)
        {
            pnlVenda_Add.Visible = false;
            limpaCamposVenda();
        }

        public void limpaCamposVenda()
        {
            tb_pnlVendas_Desconto.Text = "";
            tb_pnlVendas_TotalCompras.Text = "";
            tb_pnlVendas_ValorFinal.Text = "";
            comboBox_pnlVendas_AddCliente.Text = "";
            comboBox_pnlVendas_AddFormaPagamento.Text = "";
            comboBox_pnlVendas_AddProduto.Text = "";
            comboBox_pnlVendas_AddVendedor.Text = "";
            dg_pnlVendas_AddProdutos.Refresh();
        }

        private void pb_pnlVendas_SubmitToDb_Click(object sender, EventArgs e)
        {
            if (alteraVenda == false &&
                             tb_pnlVendas_Desconto.Text != "" &&
                             tb_pnlVendas_TotalCompras.Text != "" &&
                             tb_pnlVendas_ValorFinal.Text != "" &&
                             comboBox_pnlVendas_AddCliente.Text != "" &&
                             comboBox_pnlVendas_AddFormaPagamento.Text != "" &&
                             //comboBox_pnlVendas_AddProduto.Text != "" &&
                             comboBox_pnlVendas_AddVendedor.Text != "")
            {
                /*Popula tabela dbo.Venda*/
                venda = new Venda();
                venda.codigo_Cliente = int.Parse(comboBox_pnlVendas_AddCliente.SelectedValue.ToString());
                venda.codigo_Vendedor = int.Parse(comboBox_pnlVendas_AddVendedor.SelectedValue.ToString());
                venda.data = dateTimePicker_pnlVendas_Add.Text.ToString();
                venda.forma_pagamento = comboBox_pnlVendas_AddFormaPagamento.Text;
                venda.total_compra = float.Parse(tb_pnlVendas_TotalCompras.Text);
                venda.desconto = float.Parse(tb_pnlVendas_Desconto.Text);
                venda.valor_final = float.Parse(tb_pnlVendas_ValorFinal.Text);
                RemoteStatic.remoteObject.AddNew_Venda(venda);

                /*Popula tabela dbo.VendaItens*/
                int codigoVenda = RemoteStatic.remoteObject.GetAll_Venda().Count();
                foreach (DataGridViewRow drRowAux in dg_pnlVendas_AddProdutos.Rows)
                {
                    vendaItens = new Venda_Itens();
                    vendaItens.codigo_Venda = codigoVenda;
                    vendaItens.codigo_Produto = int.Parse(drRowAux.Cells[0].Value.ToString());
                    vendaItens.quantidade = int.Parse(drRowAux.Cells[3].Value.ToString());
                    RemoteStatic.remoteObject.AddNew_VendaItens(vendaItens);
                }
                
                MessageBox.Show("Venda cadastrada!", "Informação", MessageBoxButtons.OK);
            }
            else
            {
                MessageBox.Show(this, "Todos campos devem ser preenchidos.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            montaGrids();
            pnlVenda_Add.Visible = false;

            limpaCamposVenda();
        }

        /*BUTTON ADD PRODUTO NO PAINEL DE PRODUTOS DA VENDA*/
        private void btn_pnlVendas_AddProduto_Click(object sender, EventArgs e)
        {

            var index = this.comboBox_pnlVendas_AddProduto.SelectedValue.ToString();
            produto = RemoteStatic.remoteObject.GetBySpecification_Produto(int.Parse(index));
            dg_pnlVendas_AddProdutos.Rows.Add(produto.codigo,produto.descricao,produto.preco);

        }

        /*EVENTO REMOVE PRODUTO NO PAINEL DE PRODUTOS DA VENDA*/
        public void dg_pnlVendas_AddProdutosEditDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Remover este produto", dg_pnlVendas_AddProdutosRemoverItem));

                int mouseOverRow = dg_pnlVendas_AddProdutos.HitTest(e.X, e.X).RowIndex;
                m.Show(dg_pnlVendas_AddProdutos, new Point(e.X, e.Y));

            }
        }

        public void dg_pnlVendas_AddProdutosRemoverItem(object sender, EventArgs e)
        {
            if (this.dg_pnlVendas_AddProdutos.SelectedRows.Count > 0)
            {
                dg_pnlVendas_AddProdutos.Rows.RemoveAt(dg_pnlVendas_AddProdutos.SelectedRows[0].Index);
            }
        }

        /*BUTTON CALCULAR CONTA*/
        private void btn_pnlVendas_CalcularConta_Click(object sender, EventArgs e)
        {
            valorFinal = 0;
            valorCompra = 0;
            desconto = 0;
            int aux = dg_pnlVendas_AddProdutos.RowCount;
            if (aux > 0)
            {
                DataGridViewRow drRowAux;
                for (int i = 0; i < aux; i++)
                {
                    drRowAux = dg_pnlVendas_AddProdutos.Rows[i];

                    if (drRowAux.Cells[3].Value == null)
                    {
                        drRowAux.Cells[3].Value = 1;
                    }
                    var auxValor = drRowAux.Cells[2].Value.ToString();
                    var auxQtd = drRowAux.Cells[3].Value.ToString();
                    valorCompra = valorCompra + (float.Parse(auxValor) * float.Parse(auxQtd));
                }

                tb_pnlVendas_TotalCompras.Text = valorCompra.ToString();

                if (tb_pnlVendas_Desconto.Text == "")
                {
                    desconto = 0;
                    tb_pnlVendas_Desconto.Text = "0";
                }
                else
                {
                    desconto = float.Parse(tb_pnlVendas_Desconto.Text);
                }

                valorFinal = valorCompra - desconto;
                tb_pnlVendas_ValorFinal.Text = valorFinal.ToString();
            }
        }

        private void dg_ViewVendas_EditDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int index = dg_ViewVendas.SelectedRows[0].Index;

            DataGridViewRow row = dg_ViewVendas.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewVendas["CODIGO", row.Index].Value);

            DialogResult teste = MessageBox.Show("Tem certeza que deseja excluir o registro dessa venda?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            string yesno = teste.ToString();

            if (yesno == "Yes")
            {
                if (RemoteStatic.remoteObject.Delete_Venda(RemoteStatic.remoteObject.GetBySpecification_Venda(codigo)))
                    MessageBox.Show("Vendedor excluído", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Erro! Vendedor não excluído.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            montaGrids();
        }

        private void pb_pnlVendas_Del_Click(object sender, EventArgs e)
        {
            int index = dg_ViewVendas.SelectedRows[0].Index;

            DataGridViewRow row = dg_ViewVendas.Rows[index];
            var codigo = Convert.ToInt32(dg_ViewVendas["CODIGO", row.Index].Value);

            DialogResult teste = MessageBox.Show("Tem certeza que deseja excluir a venda?", "Informação", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
            string yesno = teste.ToString();

            if (yesno == "Yes")
            {
                if (RemoteStatic.remoteObject.Delete_Venda(RemoteStatic.remoteObject.GetBySpecification_Venda(codigo)))
                    MessageBox.Show("Venda excluída", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Erro! Venda não excluída.", "Informação", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            montaGrids();
        }
        #endregion

        private void pb_Menu_Info_Click(object sender, EventArgs e)
        {
            if (pnl_Menu_Info.Visible == false)
            {
                pnl_Menu_Info.Visible = true;
                pb_Menu_Info.Image = global::Vendas.View.Properties.Resources.next_1;
            }
            else
            {
                pnl_Menu_Info.Visible = false;
                pb_Menu_Info.Image = global::Vendas.View.Properties.Resources.back1;
            }
        }

        private void pnl_Home_Text_Paint(object sender, PaintEventArgs e)
        {

        }
    }   
}
