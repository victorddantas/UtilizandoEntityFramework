using Loja.Migrations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

            //exemploChangeTracker();
            //compraDeProdutos();
            //cadastrandoPromocoes();
            //cadastrandoClienteEndereco();
            //adicionandoProdutosEspecificos();
            //realizandoConsultaDeEntidadesRelacionadas();
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
                    Console.WriteLine(item.PrecoUnitario);
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
            p.PrecoUnitario = 19.89;

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
                p.PrecoUnitario = 25.50;


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
            p.PrecoUnitario = 19.89;

            using (var repo = new ProdutoDAO())
            {
                repo.Adicionar(p);
            }
        }

        //exemplo de utilização do ADO.NET para Listar dados na tabela  
        private static IList<Produto> ListarUsandoAdoNet()
        {
            ProdutoDAO produto = new ProdutoDAO();

            var p = produto.Produtos();

            foreach (var item in p)
            {

                Console.WriteLine(item.Nome);
                Console.WriteLine(item.PrecoUnitario);
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
            p.PrecoUnitario = 22.50;

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
                //    PrecoUnitario = 41.50
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
                    PrecoUnitario = 40.80,
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

        #endregion

        #region Realizando compras com os produtos da loja - Relacionamento de Um para muitos 
        private static void compraDeProdutos()
        {

            //Criando o produto que será comprado 
            //Nesse caso não foi utilizado um produto já contido no banco, nesse caso o entity consegue controlar isso, e faz adição tando do produto na tabela produto quanto
            //da compra na tabela de compra, respeitando o relacionamento pelo ID
            var livro = new Produto();
            livro.Nome = "Alface";
            livro.Categoria = "Alimento";
            livro.Unidade = "Unidade";
            livro.PrecoUnitario = 2.20;

            //instaciando a classe compra para efetuar a compra do produto
            var compra = new Compra();
            compra.Quantidade = 6;
            compra.Produto = livro; //fazendo referência ao produto libro que será adicionado
            compra.Preco = livro.PrecoUnitario * compra.Quantidade;

            //instanciando o contexto para utilizar o método add para adicionar uma nova compra

            using (var contexto = new LojaContext())
            {
                #region log para visualizar os comandos realizados pelo entity framework

                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create()); //no loggerFactory será colocado um log especifico do entity e vai colocá-lo aqui 

                #endregion

                contexto.Compras.Add(compra);

                //utilizando o método exibeEntries podemos vizualizar os estados dos dois objetos que serão adicionado, 
                exibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();
                Console.ReadLine();
            }
        }
        #endregion

        #region Cadastrando produtos em promoções  - Relacionamento de muitos para muitos
        private static void cadastrandoPromocoes()
        {
            var produto4 = new Produto() { Nome = "Panettone", Categoria = "alimentos", PrecoUnitario = 15.00, Unidade = "caixa" };
            var produto5 = new Produto() { Nome = "Uvas", Categoria = "alimentos", PrecoUnitario = 9.00, Unidade = "gramas" };
            var produto6 = new Produto() { Nome = "Pernil", Categoria = "alimentos", PrecoUnitario = 70.00, Unidade = "gramas" };

            //criando promoção e adicionando os produtos nessa promoção 

            var promocaoDeNatal = new Promocao();
            promocaoDeNatal.Descricao = "Natal 2022";
            promocaoDeNatal.DataInicio = DateTime.Now;
            promocaoDeNatal.DataTermino = DateTime.Now.AddDays(30);
            promocaoDeNatal.IncluiProduto(produto4);
            promocaoDeNatal.IncluiProduto(produto5);
            promocaoDeNatal.IncluiProduto(produto6);




            using (var contexto = new LojaContext())
            {
                #region log para visualizar os comandos realizados pelo entity framework

                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create()); //no loggerFactory será colocado um log especifico do entity e vai colocá-lo aqui 

                #endregion



                contexto.Promocoes.Add(promocaoDeNatal);

                //utilizando o método exibeEntries podemos vizualizar os estados dos dois objetos que serão adicionado, 
                exibeEntries(contexto.ChangeTracker.Entries());

                //contexto.SaveChanges();


                //Para caso de uma exclusão de uma promoção, o entity no arquivo de migração, define que uma exclusão deve ser realizada em cascata, ou seja, tanto as promoções
                //quanto os dados da tabela de relação PromocaoProdutos serão excluídas considerando a relação pelo Id's.A tabela de join foi criada com uma chave estrangeira para a
                //tabela de promoção, e nessa chave foi definido o trigger OnDeleteCascade. Quando a promoção foi excluída, os registros relacionados foram excluídos em cascata.

                var promocao = contexto.Promocoes.Find(2);
                contexto.Promocoes.Remove(promocao);

                exibeEntries(contexto.ChangeTracker.Entries());

                //contexto.SaveChanges();

                Console.ReadLine();
            }

        }
        #endregion

        #region Cadastrando Clientes e endereço de entrega - Relacionamento de um para um
        private static void cadastrandoClienteEndereco()
        {
            //criando cliente já adicionando um endereço. Como não definimos uma propriedade DbSet para Endereco, só podemos definir um endereço através de cliente 
            var cliente1 = new Cliente();
            cliente1.Nome = "Cliente1";
            cliente1.EnderecoDeEntrega = new Endereco()
            {
                Numero = 12,
                Logradouro = "Rua 1",
                Complemento = "Ap 205",
                Bairro = "Jardim 1",
                Cidade = "Cidade 1"

            };


            using (var contexto = new LojaContext())
            {
                #region log para visualizar os comandos realizados pelo entity framework

                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create()); //no loggerFactory será colocado um log especifico do entity e vai colocá-lo aqui 

                #endregion

                contexto.Clientes.Add(cliente1);


                exibeEntries(contexto.ChangeTracker.Entries());

                contexto.SaveChanges();

                Console.ReadLine();
            }
        }

        #endregion

        #region Consultas avançadas com entity
        private static void adicionandoProdutosEspecificos()
        {
            using (var contexto = new LojaContext())
            {
                #region log para visualizar os comandos realizados pelo entity framework

                var serviceProvider = contexto.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create()); //no loggerFactory será colocado um log especifico do entity e vai colocá-lo aqui 

                #endregion

                //Criando promoção e adicionando produtos especifico da tabela nessa prmoção
                var promocao = new Promocao();
                promocao.Descricao = "Promoção Livos"; ;
                promocao.DataInicio = new DateTime(2022, 1, 1);
                promocao.DataTermino = new DateTime(2022, 1, 1);

                //consultando os produtos que serão adicionados na promoção
                var produtos = contexto.Produtos.Where(p => p.Categoria == "Livros").ToList();

                //iterando a lista acima para adicionar os produtos na tabela de produção
                foreach (var item in produtos)
                {
                    promocao.IncluiProduto(item);
                }

                //adicionando a promoção no contexto
                contexto.Promocoes.Add(promocao);

                exibeEntries(contexto.ChangeTracker.Entries());

                //contexto.SaveChanges();


            }
        }
        private static void realizandoConsultaDeEntidadesRelacionadas()
            {
           
                using (var contexto2 = new LojaContext())
                 {

                #region log para visualizar os comandos realizados pelo entity framework

                var serviceProvider = contexto2.GetInfrastructure<IServiceProvider>();
                var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
                loggerFactory.AddProvider(SqlLoggerProvider.Create()); //no loggerFactory será colocado um log especifico do entity e vai colocá-lo aqui 

                #endregion

                //Consultando os produtos de uma promoção - Relação de muitos para muitos 
                //Por questões de performance,ferramentas de ORM geralemnete não retornam entidades relacionadas juntos com um select realizado 
                //Para realizar esse tipo de consulta é necessário realizar um select com join

                var promocao = contexto2.Promocoes.Include(p => p.Produtos).ThenInclude(pp => pp.Produto).Where(ppp => ppp.Id == 1002).FirstOrDefault(); // o include indica um join, onde explicitamos qual será a tabela relacionada 
                                                                                                               // nesse caso utilizamos o relacionamento entre Produtos e promoção (propriedade Produtos na tabela promoção
                                                                                                               // que retorna uma lista de PromocaoProduto) e mais um relacionamento, descendo um nível para incluir a tabela 
                                                                                                               //de produtos através da propriedade Produto na tabela PromocaoProduto. Sendo assim estabelecemos um join entre
                                                                                                               //Promoção e produto utlizanso a tabela PromocaoProduto.

                 

                Console.WriteLine("\n ========== Produtos da Promoção ==========");
                    foreach (var item in promocao.Produtos)
                    {
                        Console.WriteLine(item.Produto);
                    }

                //Consultando os endereços de um cliente - Relação de um para um


                var cliente = contexto2.Clientes.Include(c => c.EnderecoDeEntrega).Where(cc => cc.Id == 1).FirstOrDefault(); ;

                Console.WriteLine($"O endereço do cliente é: {cliente.EnderecoDeEntrega.Logradouro}");



                //Consultando as compras de um produto - Relação de um para muitos. (Nesse caso foi necessário criar uma  propriedade do tipo compras em produtos para referênciar as compras em produtos)

                var produto = contexto2.Produtos.Include(p => p.Compras).Where(pp => pp.Id == 2002).FirstOrDefault();

                foreach (var item in produto.Compras)
                {
                    Console.WriteLine($"As compras do produto da categoria: {produto.Categoria} -- {produto.Nome} foram: {item.Quantidade}.");
                }

                //Consultando as compras de um produto utilizando o where para filtrar pelo valor das compras
                //Nesse caso não é possível fazer where pois estamos realizando uma consulta em Poduto não em compra 
                //Nesse caso teremos que fazer um segundo select filtrando as compras do produto consultado


                //Primeira consulta
                var produto2 = contexto2.Produtos.Where(pp => pp.Id == 4002).FirstOrDefault(); 

                //Segunda consulta 
                contexto2.Entry(produto2).Collection(p => p.Compras).Query().Where(c => c.Preco < 15).Load(); //na entry que está na referência "produto", através da coleção representada na propriedade
                                                                                                      //compras executamos uma consulta com a condição where e depois carregar a query para referência
                                                                                                      //"produto", para que sejá filtrado os produtos.

                foreach (var item in produto2.Compras)
                {
                    Console.WriteLine($"As compras do produto da categoria: {produto2.Categoria} -- {produto2.Nome} foram: {item.Quantidade}.");
                }

                Console.ReadLine();

            }
        }
        #endregion

        private static void exibeEntries(IEnumerable<EntityEntry> entries)
        {
            Console.WriteLine("\n======= Entries =======");
            foreach (var item in entries)
            {
                Console.WriteLine(item.Entity.ToString() + " - " + item.State);

            }
        }

    }
}

