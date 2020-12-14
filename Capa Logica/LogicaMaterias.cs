using Capa_de_Datos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_Logica
{
    public class LogicaMaterias : logicaBase
    {
        public bool Actualizar(int IdDato, object NuevosDatos)
        {
            bool respuesta = MateriasDal.GetInstance.Actualizar(IdDato, NuevosDatos);
            return respuesta;
        }

        public bool Borrar(int IdDato)
        {
            return MateriasDal.GetInstance.EliminarDatos(IdDato);
        }

        public bool Insertar(object datoAInsertar)
        {
            MateriasDal.GetInstance.InsertarDatos(datoAInsertar);
            return true;
        }

        public object Listar()
        {
            return MateriasDal.GetInstance.Listar();
        }
    }
}
