using System;
using System.Collections.Generic;

namespace Loja
{
    public class Promocao
    {
        public int Id { get; set; }
        public string Descricao { get; internal set; }
        public DateTime DataInicio { get; internal set; }
        public DateTime DataTermino { get; internal set; }
        public List<PromocaoProduto> Produtos { get; internal set; } // Uma promoção contém vários produtos e uma promoção contém vários produtos, o que contituí uma relação de muitos para muitos
                                                                     // Nesse caso é necessário criar como referência uma propriedade em todas as classes relacionadas. Nesse tipo de relacionamento
                                                                     // é  necessário criar uma tabela join para criar uma referência entre as tabela promocao e Produto (PromocaoProduto).

        //instanciando a lista de PromocaoProduto no contrutor para nã obter uma exceçõa do tipo referência nula 
        public Promocao()
        {
            this.Produtos = new List<PromocaoProduto>();
        }
       
        //Metodo para inclusão de produtos na promoção
        internal void IncluiProduto(Produto produto)
        {
            this.Produtos.Add(new PromocaoProduto() { Produto = produto });
        }
        
    }
}