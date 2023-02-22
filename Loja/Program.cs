using Microsoft.EntityFrameworkCore.ChangeTracking;
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

            exemploChangeTracker();


        }

     
        #region Métodos no Entity framework

        //exemplo de utilização do Entity para Listar dados na tabela  
        private static void ListarUsandoEntity()
        {
            using (var repo = new ProdutoDAOEntity())
            {
                IList<Produto> p = repo.Produtos();  //Convertendo a propiedade dbset do tipo produto em uma lista 
                Console.WriteLine("Foram encontrados {0} produto(s).\n", p.Count());
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


        #region Como o entity persiste os objetos do banco 

        private static void exemploChangeTracker()
        {
            using (var contexto = new LojaContext())
            {
                var produtos = contexto.Produtos.ToList();
                foreach (var item in produtos)
                {
                    Console.WriteLine(item); //nesse caso o WriteLine irá  chamar o método ToString() que irá trazer o a referÇEcia do objeto.
                                             //para ele trazer o dados contidos devemos sobrescrever o método ToString() na classe Produto
                }

                //O entity consegue gravar os estados e executar os comados de acordo com esses estados 
                //Um exemplo disso é se alterarmos os dados contidos na tabela apenas reatibuindo o valor em uma nova variável, alterando apenas a propriedade

                var produto1 = produtos.First();
                produto1.Nome = "Admirável mundo novo";

                contexto.SaveChanges();

                //reexibindo alteração realizada     
                produtos = contexto.Produtos.ToList();
                foreach (var item in produtos)
                {
                    Console.WriteLine(item);                              
                }


                //A classe LojaContext herda de dbContext. Essa por sua  vez possue o ChangeTracker que possue uma lista de todas as entidades 
                //que está sendo gerenciadas pelo contexto em questão. Nesse caso podemos utlizar o método Entries() para recuperar essa lista 
                //e iterar através dela para vizualizar as classes de cada entidade, ou utilizando o atributo state do Entries, os estados de cada entidade 
                //Com isso podemos observar como o entity consegue observar os estados e realizar a alteração 

                foreach (var item in contexto.ChangeTracker.Entries())
                {
                    Console.WriteLine(item.State);

                }


                Console.ReadLine();
            }
        }


        #endregion
    }
}

