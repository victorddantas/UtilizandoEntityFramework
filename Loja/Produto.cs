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

        //Essas duas propriedades abaixo foram alteradas e criadas após a criação do banco. Para atualizar sem a necessidade de se manipualar
        //diretamente o banco (alter table), podemos utilizar o migrations, para sincronizar os dados entre o banco e a aplicação (objetos) . Essa funcionalidade 
        //está contida no pacote Microsoft.EntityFrameworkCore.Tools. 
        //Essa sincronizaão é feita através de duas etapas, através de comandos. Primeiro é feito adicionar a migração, criando assim uma nova versão para
        //ser sincronozada com o banco (add-migration), depois é executado ou com um script criado previamente (script-migration), ou com o update-database
        //executando diretamente a partir da versão mais recente criada. 
        //Após isso é criada a pasta Migrations contendo a classes de migrações. A classe que possue o nome da migração que foi criada, emseu nome possue
        //o nome e data de sua criação, e dentro dessa classe há dois métodos, uma para subir a nova versão e outro para retornar para uma versão anterior.
        //Essa classe herda da classe Migration, que fornece uma API para executar a sincronização).

        public double PrecoUnitario { get; internal set; }
        public string Unidade { get; internal set; }


        //sobrescrevendo o método ToString para ele exibir o dado contido na instacia de produto (nesse caso o nome).
        public override string ToString()
        {
            return $"ID: {this.Id}, Produto: {this.Nome}, Categoria: {this.Categoria}, Preço: {this.PrecoUnitario}";
        }

    }
}
