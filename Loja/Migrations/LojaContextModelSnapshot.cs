using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Loja;

namespace Loja.Migrations
{
    [DbContext(typeof(LojaContext))]
    partial class LojaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.2")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Loja.Cliente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nome");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Loja.Compra", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Preco");

                    b.Property<int>("ProdutoId");

                    b.Property<int>("Quantidade");

                    b.HasKey("Id");

                    b.HasIndex("ProdutoId");

                    b.ToTable("Compras");
                });

            modelBuilder.Entity("Loja.Endereco", b =>
                {
                    b.Property<int>("ClienteId");

                    b.Property<string>("Bairro");

                    b.Property<string>("Cidade");

                    b.Property<string>("Complemento");

                    b.Property<string>("Logradouro");

                    b.Property<int>("Numero");

                    b.HasKey("ClienteId");

                    b.ToTable("Enderecos");
                });

            modelBuilder.Entity("Loja.Produto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Categoria");

                    b.Property<string>("Nome");

                    b.Property<double>("PrecoUnitario");

                    b.Property<string>("Unidade");

                    b.HasKey("Id");

                    b.ToTable("Produtos");
                });

            modelBuilder.Entity("Loja.Promocao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DataInicio");

                    b.Property<DateTime>("DataTermino");

                    b.Property<string>("Descricao");

                    b.HasKey("Id");

                    b.ToTable("Promocoes");
                });

            modelBuilder.Entity("Loja.PromocaoProduto", b =>
                {
                    b.Property<int>("PromocaoId");

                    b.Property<int>("ProdutoId");

                    b.HasKey("PromocaoId", "ProdutoId");

                    b.HasIndex("ProdutoId");

                    b.ToTable("PromocaoProduto");
                });

            modelBuilder.Entity("Loja.Compra", b =>
                {
                    b.HasOne("Loja.Produto", "Produto")
                        .WithMany()
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Loja.Endereco", b =>
                {
                    b.HasOne("Loja.Cliente", "Cliente")
                        .WithOne("EnderecoDeEntrega")
                        .HasForeignKey("Loja.Endereco", "ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Loja.PromocaoProduto", b =>
                {
                    b.HasOne("Loja.Produto", "Produto")
                        .WithMany("Produtos")
                        .HasForeignKey("ProdutoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Loja.Promocao", "Promocao")
                        .WithMany("Produtos")
                        .HasForeignKey("PromocaoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
