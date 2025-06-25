using Domain.Entities;
using Domain.Interfaces;
using Shared.DTOs;

namespace Application.Services;

public class ClienteService
{
    private readonly IClienteRepository _repo;

    public ClienteService(IClienteRepository repo)
    {
        _repo = repo;
    }

    public async Task<List<Cliente>> ListarAsync() => await _repo.ObterTodosAsync();

    public async Task<Cliente?> ObterPorIdAsync(Guid id) => await _repo.ObterPorIdAsync(id);

    public async Task<Cliente> CriarAsync(ClienteDTO dto)
    {
        var cliente = new Cliente(dto.Nome, dto.Email);
        await _repo.AdicionarAsync(cliente);
        return cliente;
    }

    public async Task AtualizarAsync(Guid id, ClienteDTO dto)
    {
        var cliente = await _repo.ObterPorIdAsync(id);
        if (cliente is null) throw new Exception("Cliente não encontrado");

        cliente.Atualizar(dto.Nome, dto.Email);
        await _repo.AtualizarAsync(cliente);
    }

    public async Task RemoverAsync(Guid id)
    {
        await _repo.RemoverAsync(id);
    }
}
