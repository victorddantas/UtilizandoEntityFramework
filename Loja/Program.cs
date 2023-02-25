using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
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

        #region Como o entity persiste os objetos do banco - Monitoramento de mudança dos estados dos objetos gerenciados pelo entity

        private static void exemploChangeTracker()
        {
            using (var contexto = new LojaContext())
            {
                # region log para visualizar os comandos realizados pelo entity framework

                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create()); //no loggerFactory será colocado um log especifico do entity e vai colocá-lo aqui 

                #endregion

                #region LISTANDO PRODUTOS PARA EXBIÇÃO DOS ESTADOS 

                var produtos = contexto.Produtos.ToList();

                Console.WriteLine("======= Lista de produtos =======");
                foreach (var item in produtos)
                {
                    Console.WriteLine(item); //nesse caso o WriteLine irá  chamar o método ToString() que irá trazer o a referência do objeto.
                                             //para ele trazer o dados contidos devemos sobrescrever o método ToString() na classe Produto
                }

                #endregion

                #region EXEMPLO DE ESTADO DE GRAVAÇÃO AO ALTERAR O VALOR DE UMA PROPRIEDADE (ATUALIZAR)

                //O entity consegue GRAVAR os estados e executar os comados de acordo com esses estados 
                //Um exemplo disso é se alterarmos os dados contidos na tabela apenas reatibuindo o valor em uma nova variável, alterando apenas a propriedade

                //var produto1 = produtos.First();
                //produto1.Nome = "A arte da guerra";


                //A classe LojaContext herda de dbContext. Essa por sua
                //vez possue o ChangeTracker que possue uma lista de todas as entidades 
                //que está sendo gerenciadas pelo contexto em questão. Nesse caso podemos utlizar o método Entries() (dentro do método criado exibeEntries) para recuperar essa lista 
                //e iterar através dela para vizualizar as classes de cada entidade, ou utilizando o atributo state do Entries, os estados de cada entidade 
                //Com isso podemos observar como o entity consegue observar os estados e realizar a alteração 

                //exibeEntries(contexto.ChangeTracker.Entries());//exbindo os estados 


                //contexto.SaveChanges();//salvando alterações


                //exibeEntries(contexto.ChangeTracker.Entries());//exibindo os estados 



                //Assim que é feito o select no banco (método toList), o contexto armazenou uma entidade para cada registro que obteve do banco 
                //E assim atribuí o estado para cada entidade( se não há  alteração "unchanged" se há "Modified"). 

                #endregion

                #region EXEMPLO DE ESTADO DE GRAVAÇÃO AO INSERIR
                //para o caso de INSERIR um novo produto no banco, o estado exbido é o Added


                //var produto2 = new Produto()
                //{
                //    Nome = "O Príncipe",
                //    Categoria = "livros",
                //    Preco = 41.50
                //};

                //contexto.Produtos.Add(produto2);


                //exibeEntries(contexto.ChangeTracker.Entries());//exibindo os estados 


                //contexto.SaveChanges();//salvando alterações


                // exibeEntries(contexto.ChangeTracker.Entries());//exibindo os estados 

                #endregion

                #region EXEMPLO DE ESTADO DE GRAVAÇÃO AO REMOVER
                //para o caso de REMOVER um novo produto no banco, o estado exbido é o Deleted

                //var p1 = produtos.Last();
                //contexto.Produtos.Remove(p1);

                //exibeEntries(contexto.ChangeTracker.Entries());//exibindo os estados 

                //contexto.SaveChanges();//salvando alterações

                //exibeEntries(contexto.ChangeTracker.Entries());//exibindo os estados 

                //Console.ReadLine();

                #endregion

                #region EXEMPLO DE ESTADO DE GRAVAÇÃO AO INSERIR E REMOVER EM SEGUIDA 

                //para o caso de INSERIR E REMOVER EM SEGUIDA esse mesmo produto no banco, o estado exbido é o Detached 
                Produto p2 = new Produto()
                {
                    Nome = "A arte da guerra",
                    Categoria = "Livros",
                    Preco = 40.80,
                };
            
                contexto.Produtos.Add(p2);

                exibeEntries(contexto.ChangeTracker.Entries());//exibindo os estados 

                contexto.Produtos.Remove(p2);

                exibeEntries(contexto.ChangeTracker.Entries());//exibindo os estados 

                //contexto.SaveChanges();//salvando alterações

                //Para visualizar esse estado, precisamos verificar o estado do objeto que está sendo inserido e deletado através de sua referência, pois os mesmo é 
                //removido da lista de monitoramento do ChangeTracker
                //Objetos ao serem excluídos já são movidos para esse estado (Detached)

                var entry = contexto.Entry(p2); //o Entry é uma estância de um objeto do tipo EntityEntry
                Console.WriteLine("\n======= Estado dos objetos não monitorados =======");
                Console.WriteLine(entry.Entity.ToString() + " - " + entry.State); //exibindo os produtos e seu estado 



                Console.ReadLine();

                #endregion
            }

        }

        private static void exibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("\n======= Entries =======");
            foreach (var item in entries)
            {
                Console.WriteLine(item.Entity.ToString() + " - " + item.State);

            }
        }


        #endregion
    }
}

