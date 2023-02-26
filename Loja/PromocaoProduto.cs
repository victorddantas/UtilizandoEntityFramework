using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja
{
    public class PromocaoProduto
    { //essa classe foi criada para estabeler um relacionamento entre a classe promoção e produto, assim o entity usará essa classe para criar uma tabela de relacionamento. 

        public int ProdutoId { get; set; } //para criar o relacionamento entre as classes e fazer com que o entity defina essa propriedade como não nula precisamos explicitar aqui o 
                                           //id do produto que será efetuado a compra. Isso irá representar o relacionamento entre as tabela. Porém nesse caso para criar uma tabela 
                                           //o entity exige um indentificador unic, porém em uma tabela de relacionamento isso não faz sentido, então nesse caso temos que sobrescrever
                                           //no contexto, o método responsável pela criação do modelo (OnModelCreating).
        public int PromocaoId { get; set; }


        public Produto Produto { get; set; } //Uma promoção contém produtos e produtos contém promoções

        public Promocao Promocao { get; set; }
    }
}
