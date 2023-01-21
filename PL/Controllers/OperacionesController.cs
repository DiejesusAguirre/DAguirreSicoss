using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL.Controllers
{
    public class OperacionesController : Controller
    {
        // GET: Operaciones
        public ActionResult GetAll(int? IdUsuario, int? Resultado, int? Numero)
        {
            Resultado = (Resultado == null) ? 0 : Resultado;
            Numero = (Numero == null) ? 0 : Numero;
            ML.Result result = new ML.Result();
            ML.Operaciones operaciones = new ML.Operaciones();
            operaciones.Usuario = new ML.Usuario();
            operaciones.Usuario.IdUsuario = (operaciones.Usuario.IdUsuario == 0) ? IdUsuario.Value : operaciones.Usuario.IdUsuario;
            operaciones.Resultado = (operaciones.Resultado == 0) ? Resultado.Value : operaciones.Resultado;
            operaciones.Numero = (operaciones.Numero == "") ? Numero.ToString() : operaciones.Numero;
            if (operaciones.Resultado == 0)
            {
                result = BL.Operaciones.GetByIdUsuario(operaciones.Usuario.IdUsuario);
                if (result.Correct)
                {
                    operaciones.Operacioness = result.Objects;
                    return View(operaciones);
                }
                else
                {
                    result.ErrorMessage = "Algo salio mal";
                    return View(result);
                }
            }
            else
            {
                ML.Result result2 = new ML.Result();
                ML.Operaciones ope = new ML.Operaciones();
                result2 = BL.Operaciones.GetByIdUsuario(operaciones.Usuario.IdUsuario);
                if (result2.Correct)
                {
                    ope.Operacioness = result2.Objects;
                    operaciones.Operacioness = ope.Operacioness;
                    return View(operaciones);
                }
                else
                {
                    result.ErrorMessage = "Algo salio mal";
                    return View(result2);
                }
                ;
            }
            
        }
        [HttpPost]
        public ActionResult Calculo(ML.Operaciones operaciones)
        {
            ML.Result result = new ML.Result();
            ML.Operaciones operaciones1 = new ML.Operaciones();
            operaciones1.Resultado = 0;

            for (int i = 0; i < operaciones.Numero.Length; i++)
            {
                operaciones1.Resultado += Convert.ToInt32(operaciones.Numero.Substring(i, 1));
            }
            int resultado = operaciones1.Resultado;
            if (resultado > 9)
            {
                do
                {
                    while (resultado != 0)
                    {
                        operaciones.Resultado += resultado % 10;
                        resultado /= 10;
                        int len = int.Parse(operaciones.Resultado.ToString());
                        int[] arr = new int[len.ToString().Length];
                        if (arr.Length > 1)
                        {
                            resultado = operaciones.Resultado;
                            operaciones.Resultado = 0;
                        }
                    }
                } while (operaciones.Resultado > 9);
            }
            else
            {
                operaciones.Resultado = resultado;
            }

            ML.Result resultMostrar = new ML.Result();
            resultMostrar = BL.Operaciones.ExisteNumero(operaciones);
            if (resultMostrar.Correct != true)
            {
                result = BL.Operaciones.AddEF(operaciones);
                if (result.Correct)
                {
                    return RedirectToAction("GetAll", "Operaciones", new { IdUsuario = operaciones.Usuario.IdUsuario, Resultado = operaciones.Resultado, Numero = operaciones.Numero });
                }
                else
                {
                    result.ErrorMessage = "Algo salio mal";
                    return View(result);
                }
            }
            else
            {
                ML.Operaciones operaciones2 = new ML.Operaciones();
                operaciones2.Usuario = new ML.Usuario();
                result = BL.Operaciones.GetByNumero(operaciones);
                if (result.Correct)
                {
                    operaciones2 = (ML.Operaciones)result.Object;
                    return RedirectToAction("GetAll", "Operaciones", new { IdUsuario = operaciones2.Usuario.IdUsuario, Resultado = operaciones2.Resultado, Numero = operaciones2.Numero });
                }
                else
                {
                    result.ErrorMessage = "Algo salio mal";
                    return View(result);
                }
            }
        }
        [HttpGet]
        public ActionResult Delete(int IdOperaciones, int IdUsuario)
        {
            ML.Result result = new ML.Result();
            result = BL.Operaciones.Delete(IdOperaciones);
            if (result.Correct)
            {
                ViewBag.Message = "Se elimino correctamente";
                ViewBag.IdUsuario = IdUsuario;
                return PartialView("Modal");
            }

            else
            {
                ViewBag.Message = "Algo ocurrio" + result.ErrorMessage;
                return PartialView("Modal");
            }
        }
    }
}
//operaciones1.Resultado = 0;
//operaciones1.Numero = operaciones.Numero;

//while (operaciones1.Numero != null)
//{
//    Convert.ToString(operaciones1.Resultado += Int32.Parse(operaciones1.Numero) % 10);
//    //operaciones1.Numero /= 10;
//}

//int resultado = operaciones1.Resultado;
//if (resultado > 9)
//{
//    do
//    { 
//        while (resultado != 0)
//        { 
//            operaciones.Resultado += resultado % 10;
//            resultado /= 10;
//            long len = long.Parse(operaciones.Resultado.ToString());
//            long[] arr = new long[len.ToString().Length];
//            if (arr.Length > 1)
//            {
//                resultado = operaciones.Resultado;
//                operaciones.Resultado = 0;
//            }
//        }
//    } while (operaciones.Resultado > 9);
//}
//else
//{
//    operaciones.Resultado = resultado;
//}