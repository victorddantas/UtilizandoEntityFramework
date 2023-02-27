using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loja
{
    public class Produto
    {
        public int Id { get; internal set; }
        public string Nome { get; internal set; }
        public string Categoria { get; internal set; }

        ////Essas duas propriedades abaixo foram alteradas e criadas após a criação do banco. Para atualizar sem a necessidade de se manipular
        ////diretamente o banco (alter table), podemos utilizar o migrations, para sincronizar os dados entre o banco e a aplicação (objetos) . Essa funcionalidade 
        ////está contida no pacote Microsoft.EntityFrameworkCore.Tools. 
        ////Essa sincronizaão é feita através de duas etapas, através de comandos. Primeiro é feito adicionar a migração, criando assim uma nova versão para
        ////ser sincronozada com o banco (add-migration), depois é executado ou com um script criado previamente (script-migration), ou com o update-database
        ////executando diretamente a partir da versão mais recente criada. 
        ////Após isso é criada a pasta Migrations contendo a classes de migrações. A classe que possue o nome da migração que foi criada, em seu nome possue
        ////o nome dado a migração e data de sua criação, e dentro dessa classe há dois métodos, um para subir a nova versão e outro para retornar para uma versão anterior.
        ////Essa classe herda da classe Migration, que fornece uma API para executar a sincronização).
        ////Primeiro é necessário criar a migração inicial, como forma de versionar o estado atual da aplicação em relação ao banco de dados, após
        ////isso se cria a migração com as atualizações.
        ////Ao executar uma migração ele sempre irá utlizar como referência a tabela de histórico criada no banco quando é executada uma migração.
        ////Como não foi criada nenhuma a primeira migração, seria a migração inicial, todavia, a migração inicial dará erro pois ela está tentando criar 
        ////uma tabela que já existe. Para burlar isso podemos comentar o código da migração inicial, de forma que ele sirva apenas para registrar
        ////a execução nessa tabela de histórico, para que assim possamos executar a migração com as atualizações, essa que assim será a última 
        ////migração disponível para atualização. 

        public double PrecoUnitario { get; internal set; }
        public string Unidade { get; internal set; }

        public List<PromocaoProduto> Produtos { get; internal set; } // Uma promoção contém vários produtos e uma promoção contém vários produtos, o que contituí uma relação de muitos para muitos
                                                                     // Nesse caso é necessário criar como referência uma propriedade em todas as classes relacionadas. Nesse tipo de relacionamento
                                                                     // é  necessário criar uma tabela join para criar uma referência entre as tabela promocao e Produto (PromocaoProduto).


        public List<Compra> Compras { get; set; }

        //sobrescrevendo o método ToString para ele exibir o dado contido na instacia de produto (nesse caso o nome).
        public override string ToString()
        {
            return $"ID: {this.Id}, Produto: {this.Nome}, Categoria: {this.Categoria}, Preço: {this.PrecoUnitario}";
        }

    }
}
