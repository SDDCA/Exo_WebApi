using Exo.WebApi.Models;
using Exo.WebApi.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Exo.WebApi.Controllers
{
    
    [Produces("application/json")]
[Route("api/[controller]")]
[ApiController]
public class UsuariosController : ControllerBase
{
    private readonly UsuarioRepository _usuarioRepository;

    public UsuariosController(UsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    [HttpGet]
    public IActionResult Listar()
    {
        return Ok(_usuarioRepository.Listar());
    }

    [HttpPost]  
    public IActionResult Post(Usuario usuario)
    {
        Usuario usuarioBuscado = _usuarioRepository.login(usuario.Email, usuario.Senha);

        if (usuarioBuscado == null)
        {
            return NotFound("Email ou senha inválidos!");
        }

        var claims = new[] 
        {
            new Claim(JwtRegisteredClaimNames.Email, usuarioBuscado.Email),
            new Claim(JwtRegisteredClaimNames.Jti, usuarioBuscado.Id.ToString()),
        };

        var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("U2FsdGVkX1+S8W5n8VmZKoyHq4+d9l9vqOb8abmHk7M="));

        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "exo.webApi",
            audience: "exo.webApi",
            claims: claims,
            expires: DateTime.Now.AddMinutes(30),
            signingCredentials: creds
        );

        return Ok(new { token = new JwtSecurityTokenHandler().WriteToken(token) });
    }

    [HttpGet("{id}")]
    public IActionResult BuscarPorId(int id)
    {
        Usuario usuario = _usuarioRepository.BuscarPorId(id);
        if (usuario == null)
        {
            return NotFound();
        }
        return Ok(usuario);
    }

    [Authorize]
    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Usuario usuario)
    {
        _usuarioRepository.Atualizar(id, usuario);
        return StatusCode(204);
    }

    [Authorize]
    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        try
        {
            _usuarioRepository.Deletar(id);
            return StatusCode(204);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}

    
}