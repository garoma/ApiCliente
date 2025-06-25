namespace Domain.Entities;

public class Cliente
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Nome { get; private set; }
    public string Email { get; private set; }

    public Cliente(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }

    public void Atualizar(string nome, string email)
    {
        Nome = nome;
        Email = email;
    }
}
