namespace DesafioProjetoHospedagem.Models;

public class Pessoa
{
    public Pessoa() { }

    public Pessoa(string nomecompleto)
    {
        NomeCompleto = nomecompleto;
    }

    public string NomeCompleto { get; set; }
}