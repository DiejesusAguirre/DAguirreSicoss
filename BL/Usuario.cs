using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result GetByIdLogin(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DAguirreSicossEntities context = new DL.DAguirreSicossEntities())
                {

                    var codigo = context.GetByUsuario(usuario.UserName, usuario.Password).FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        ML.Usuario usuarioItem = new ML.Usuario();

                        usuarioItem.IdUsuario = codigo.IdUsuario;
                        usuarioItem.UserName = codigo.Usuario;
                        usuarioItem.Password = codigo.Password;
                        
                        result.Object = usuarioItem;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
        public static ML.Result AddUsuario(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DAguirreSicossEntities context = new DL.DAguirreSicossEntities())
                {

                    var codigo = context.AddUsuario(usuario.UserName, usuario.Password);

                    if (codigo > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Hubo un error";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }
    }
}
