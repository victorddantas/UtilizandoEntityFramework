﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
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
        }

        //exemplo de utilização do Entity para salvar dados na tabela  
        #region Métodos no Entity framework
        private static void GravarUsandoEntity()
        {
            Produto p = new Produto();
            p.Nome = "Harry Potter e a Ordem da Fênix";
            p.Categoria = "Livros";
            p.Preco = 19.89;

            //Ao invés de se utilizar um DAO para cada classe, utlizamos o Context para persistir todas as classes do Projeto
            using (var contexto = new LojaContext())
            {
                contexto.Produtos.Add(p);
                contexto.SaveChanges();
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

