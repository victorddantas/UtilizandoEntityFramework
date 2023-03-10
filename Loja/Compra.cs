namespace Loja
{
    public class Compra
    {
        public int Id { get; set; }
        public Produto Produto { get; internal set; } //Uma compra precisa de um produto
        public int ProdutoId { get; set; } //para criar o relacionamento entre as classes e fazer com que o entity defina essa propriedade como não nula precisamos explicitar aqui o 
                                           //id do produto que será efetuado a compra. Isso irá representar o relacionamento de um para muitos. 
                                           //um produto pode conter em várias compras, mas uma compra só pode pertencer a um único produto.). Nesse caso é necessário criar um relcionamento apenas na 
                                           //classe que receberá vários elementos da outra classe 
        public int Quantidade { get; internal set; }
        public double Preco { get; internal set; }
    }
}