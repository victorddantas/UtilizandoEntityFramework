using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja
{
    public class ProdutoDAOEntity : IProdutoDAO, IDisposable
    {
        private SqlConnection conexao;
        public ProdutoDAOEntity()
        {
            this.conexao = new SqlConnection("Server=(localdb)\\mssqllocaldb;Database=LojaDB;Trusted_Connection=true;");
            this.conexao.Open();
        }
        public void Dispose()
        {
            this.conexao.Close();
        }
        public void Adicionar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public void Atualizar(Produto produto)
        {
            throw new NotImplementedException();
        }

        public IList<Produto> Produtos()
        {
            throw new NotImplementedException();
        }

        public void Remover(Produto produto)
        {
            throw new NotImplementedException();
        }
    }
}
