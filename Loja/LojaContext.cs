using Microsoft.EntityFrameworkCore;
using System;

namespace Loja
{
    public class LojaContext : DbContext //DbContext é uma classe do EntityFrameworkCore - O contexto deve sempre herdar dela 
    {
        //definindo as classes que serão persistidas pelo Context - Uma propriedade que vai representar o conjunto de objetos definidos na classe produto e compras.
        //O nome é o mesmo da tabela (Produtos e Compras).
        public DbSet<Produto> Produtos { get; set; } 
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }


        //sobrescrevendo o método OnModelCreating

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PromocaoProduto>().HasKey(pp => new {pp.PromocaoId, pp.ProdutoId}); //definindo na tabela de relação entro produto e promoção (relação de muitos para muitos)
                                                                                                    //seja criada uma chave composta na migração estabelecendo para o criador de modelo que para a entidade PomocaoProduto tem como chave
                                                                                                    //a composição das duas chaves PromocaoId e ProdutoId.

            modelBuilder.Entity<Endereco>().ToTable("Enderecos"); //definindo o nome da tabela, pois essa será criada pelo mapeamento, mas ela não está incluída como uma classe que sera
                                                                  //persistida pelo entity (DbSet), nesse caso se não for definido ela levará o mesmo nome da classe


            modelBuilder.Entity<Endereco>().Property<int>("ClienteId");//definindo que no banco a tabela ClienteId deverá conter o campo ClienteId que não foi definido na classe Endereco
                                                                  //Esse conceito é chamado de shadow property.          


            modelBuilder.Entity<Endereco>().HasKey("ClienteId");//definindo que a tabela endereço possuí como primary key ClienteId


            base.OnModelCreating(modelBuilder); 

        }

        //Definindo o banco de dados que será utilizado. Nesse caso será necessário utilizar um evento de configuração, sobrescrevendo o método OnConfiguring

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LojaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //utilizando o sqlserver disponível na seguinte string de conexão
        }

    }
}