using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Operaciones
    {
        public static ML.Result GetByIdUsuario(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.DAguirreSicossEntities context = new DL.DAguirreSicossEntities())
                {

                    var codigo = context.GetByIdUsuario(IdUsuario).ToList();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        foreach (var obj in codigo)
                        {
                            ML.Operaciones operacion = new ML.Operaciones();
                            operacion.Usuario = new ML.Usuario();

                            operacion.IdOperaciones = obj.IdOperacion;
                            operacion.Numero = obj.Numero.ToString();
                            operacion.Resultado = (int)obj.Resultado;
                            operacion.FechaHora = (DateTime)obj.FechaHora;
                            operacion.Usuario.IdUsuario = (int)obj.IdUsuario;

                            result.Objects.Add(operacion);

                        }
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
        public static ML.Result AddEF(ML.Operaciones operacion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DAguirreSicossEntities context = new DL.DAguirreSicossEntities())
                {

                    var codigo = context.AddOperacion(operacion.Numero, operacion.Resultado, operacion.Usuario.IdUsuario);

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
        public static ML.Result Delete(int IdOperaciones)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DAguirreSicossEntities context = new DL.DAguirreSicossEntities())
                {

                    var codigo = context.DeleteOperacion(IdOperaciones);

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
        public static ML.Result GetByNumero(ML.Operaciones operaciones)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.DAguirreSicossEntities context = new DL.DAguirreSicossEntities())
                {

                    var codigo = context.GetByNumero(operaciones.Numero).AsEnumerable().FirstOrDefault();
                    result.Objects = new List<object>();

                    if (codigo != null)
                    {
                        operaciones.Usuario = new ML.Usuario();
                        operaciones.Numero = codigo.Numero;
                        operaciones.Resultado = (int)codigo.Resultado;
                        operaciones.Usuario.IdUsuario = (int)codigo.IdUsuario;

                        result.Object = operaciones;
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
        public static ML.Result ExisteNumero(ML.Operaciones operacion)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.DAguirreSicossEntities context = new DL.DAguirreSicossEntities())
                {

                    var codigo = context.ExisteNumero(operacion.Numero).AsEnumerable().FirstOrDefault();

                    if (codigo > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
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
        
    }
}
