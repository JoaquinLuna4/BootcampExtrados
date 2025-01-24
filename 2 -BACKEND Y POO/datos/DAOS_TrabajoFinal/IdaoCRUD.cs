using LibraryTrabajoFinal.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryTrabajoFinal.DAOS
{
    public interface IDaoCRUD
    {
        int SignUp(string nombre, string email, string pass, string role);
        UsuariosCRUD? GetAllUser(string nombre);
        UsuariosCRUD? SearchUser(string nombre);
        int UpdateUserByName(string nombre, string email);
        int DeleteUserByName(string nombre);
    }
}
