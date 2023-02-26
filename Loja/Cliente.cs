namespace Loja
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; internal set; }
        public Endereco EnderecoDeEntrega { get; internal set; } //Um cliente possui um endereço e um endreço possuí apenas um cliente. No entity, ao se criar migrações, ele faz um mapeamento através das propriedades
                                                                 //Na classe endereço não foi definida no contexto, então assim as alterações em endereço só poderão ser feitas 
                                                                 //através da classe cliente. Isso vária da arquitetura, a forma como o sistema trabalha, isso deve ser definido no
                                                                 //desenvolvimento. Na classe Endereco nesse caso não definimos uma propriedade de Id, nesse caso vamos assumir a abordagem
                                                                 //de que o Id de um endereço será a primary key da classe na qual ela depende, nesse caso cliente. Para definirmos isso
                                                                 //devemos sobrescrever o método de criação de modelos do entity (OnModelCreating), definindo que no banco deverá existir
                                                                 //uma propriedade "ClienteId", e também defini-lá como chave primaria, pois em uma aplicação POO não faz sentido criar uma propriedade Cliente em endereço, isso só 
                                                                 //se  aplica em modelos relacionais. Para que o entity, durante a migração, saiba que a classe Cliente é a principal
                                                                 //deve-se também criar uma propriedade do tipo Cliente na classe Endereco.  
    }
}