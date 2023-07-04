using Microsoft.EntityFrameworkCore;
using WebRhProject.Data;
using WebRhProject.Models;
using WebRhProject.Services.Exceptions;

public class UsuarioService
{
    private readonly Contexto _context;

    public UsuarioService(Contexto context)
    {
        _context = context;
    }

    public List<Usuario> FindAll()
    {
        return _context.Usuario.ToList();
    }

    public void Insert(Usuario obj)
    {
        _context.Add(obj);
        _context.SaveChanges();
    }

    public Usuario FindById(int id)
    {
        return _context.Usuario.Include(obj => obj.Colaborador).FirstOrDefault(obj => obj.Id == id);
    }

    public void Remove(int id)
    {
        var obj = _context.Usuario.Find(id);
        _context.Usuario.Remove(obj);
        _context.SaveChanges();
    }

    public void Update(Usuario obj)
    {
        if (!_context.Usuario.Any(x => x.Id == obj.Id))
        {
            throw new NotFoundException("Id not found");
        }

        try
        {
            _context.Update(obj);
            _context.SaveChanges();
        }
        catch (DbUpdateConcurrencyException e)
        {
            throw new DbConcurrencyException(e.Message);
        }
    }

    public bool EmailExists(string email)
    {
        return _context.Usuario.Any(u => u.Email == email);
    }
    public Usuario FindByEmailAndPassword(string email, string senha)
    {
        return _context.Usuario.FirstOrDefault(u => u.Email == email && u.Senha == senha);
    }
    public bool ExistsByColaboradorId(int colaboradorId)
    {
        return _context.Usuario.Any(u => u.ColaboradorId == colaboradorId);
    }
    public Usuario FindByEmail(string email)
    {
        return _context.Usuario.Include(u => u.Colaborador).SingleOrDefault(u => u.Email == email);
    }



}
