using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //GravarUsandoAdoNet();       
            //ListarUsandoAdoNet();
            //DeletarUsandoAdoNet();
            //AtualizarUsandoAdoNet();

            //GravarUsandoEntity();
            //ListarUsandoEntity();
            //DeletarUsandoEntity();
            //AtualizarUsandoEntity();
        }

        #region Métodos no Entity framework

        //exemplo de utilização do Entity para Listar dados na tabela  
        private static void ListarUsandoEntity()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> p = repo.Produtos();  //Convertendo a propiedade dbset do tipo produto em uma lista 
                Console.WriteLine("Foram encontrados {0} produto(s).", p.Count());
                foreach (var item in p) //Iterando a lista para mostrar os dados obtidos 
                {
                    Console.WriteLine(item.Nome);
                    Console.WriteLine(item.Preco);
                    Console.WriteLine(item.Categoria + "\n");
                }

                Console.ReadLine();
            }
        }
       
        //exemplo de utilização do Entity para salvar dados na tabela  
        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            //Ao invés de se utilizar um DAO para cada classe, utlizamos o Context para persistir todas as classes do Projeto
            using (var contexto = new ProdutoDAOEntity())
            {
                contexto.Adicionar(p);
            }
        }

        //exemplo de utilização do Entity para deletar dados na tabela  
        private static void DeletarUsandoEntity()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> p = repo.Produtos(); //criando uma lista de produtos  para excluir

                foreach (var item in p)
                {
                   repo.Remover(item);  //removendo os itens
                }
                ListarUsandoEntity();
            }
        }

        //exemplo de utilização do Entity para atualizar dados na tabela  
        private static void AtualizarUsandoEntity()
        {
            using (var repo = new ProdutoDAOEntity())
            {

                Produto p = new Produto(); //informando o produto que será atualizado 
                p.Id = 1003;
                p.Nome = "A arte da Guerra";
                p.Categoria = "Livros";
                p.Preco = 25.50;

                
                repo.Atualizar(p); //método de atualização
                ListarUsandoEntity();
            }
        }

        #endregion

        #region Métodos em ADO.NET

        //exemplo de utilização do ADO.NET para salvar dados na tabela  
        private static void GravarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }

        //exemplo de utilização do ADO.NET para Listar dados na tabela  
        private static IList<Produto> ListarUsandoAdoNet()
        {
            ProdutoDAO produto = new ProdutoDAO();

           var p =  produto.Produtos();
           
            foreach (var item in p)
            {

                Console.WriteLine(item.Nome);
                Console.WriteLine(item.Preco);
                Console.WriteLine(item.Categoria + "\n");
               
            }

            Console.ReadLine();
            return p;
           
        }

        //exemplo de utilização do ADO.NET para Deletar dados na tabela  
        private static void DeletarUsandoAdoNet()
        {
          

            Produto p = new Produto();
            p.Id = 1;
            

            using (var repo = new ProdutoDAO())
            {
                repo.Remover(p);
            }

        }

        private static void AtualizarUsandoAdoNet()
        {
            Produto p = new Produto();
            p.Id = 2;
            p.Nome = "Admirável mundo novo";
            p.Categoria = "Livros";
            p.Preco = 22.50;

            using (var repo = new ProdutoDAO())
            {
                repo.Atualizar(p);
            }
        }

        #endregion
    }
}

