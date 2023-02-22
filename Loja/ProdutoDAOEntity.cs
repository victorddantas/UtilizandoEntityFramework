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
        //através do context fazemos acesso ao objeto Produto utilizando a abordagem do entity framework, 

        private LojaContext contexto; //criando campo para representar o contexto do entity para utilizar o método do entity 

        public ProdutoDAOEntity()
        {
           this.contexto = new LojaContext(); //estânciando LojaContext para evitar o erro de referência nula no campo contexto
        }
        public void Dispose()
        {
            contexto.Dispose(); 
        }
        public void Adicionar(Produto produto)
        {
            contexto.Produtos.Add(produto); //método para adicionar produtos 
            contexto.SaveChanges(); //salvando alterações 
        }
        public void Atualizar(Produto produto)
        {
            contexto.Produtos.Update(produto);//método para atualizar produtos 
            contexto.SaveChanges();
        }
        public IList<Produto> Produtos()
        {
            return contexto.Produtos.ToList();
        }
        public void Remover(Produto produto)
        {
            contexto.Produtos.Remove(produto);
            contexto.SaveChanges();

        }
    }
}
