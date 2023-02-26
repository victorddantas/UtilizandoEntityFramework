using Microsoft.EntityFrameworkCore;
using System;

namespace Loja
{
    public class LojaContext : DbContext //DbContext é uma classe do EntityFrameworkCore - O contexto deve sempre herdar dela 
    {
        //definindo as classes que seão persistidas pelo Context- Uma propriedade que vai representar o conjunto de objetos definidos  na classe produto.
        //O nome é o mesmo da tabela (Produtos).
        public DbSet<Produto> Produtos { get; set; } 
        public DbSet<Compra> Compras { get; set; }

        //Definindo o banco de dados que será utilizado. Nesse caso será necessário utilizar um evento de configuração, sobrescrevendo o método OnConfiguring

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=LojaDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"); //utilizando o sqlserver disponível na seguinte string de conexão
        }

    }
}