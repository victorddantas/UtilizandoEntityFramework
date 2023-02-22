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
        public double Preco { get; internal set; }


        //sobrescrevendo o método ToString para ele exibir o dado contido na instacia de produto (nesse caso o nome).
        public override string ToString()
        {
            return "Produto: " + this.Nome;
        }

    }
}
