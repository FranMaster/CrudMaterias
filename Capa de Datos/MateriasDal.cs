using Capa_Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_Datos
{
    public class MateriasDal : DataAccessLayerBase
    {
        private static MateriasDal instance;
        public static MateriasDal GetInstance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MateriasDal();
                }
                return instance;
            }
        }

        private MateriasDal()
        {
            Listar();
        }


        public bool InsertarDatos(object materia)
        {
            try
            {
                int respuesta = 0;
                Materias DatosInsert = (Materias)materia;
                int activo = DatosInsert.Activa ? 1 : 0;
                respuesta = Conexion.GetInstance.Escribir($"INSERT INTO `MATERIAS` (`Descripcion`," +
                    $" `AnioCurso`, `Activa`) VALUES ('{DatosInsert.Descripcion}', '{DatosInsert.AnioCursado}', '{activo}')");
                if (respuesta > 0)
                {
                    return true;

                }
                return false;
            }
            catch (Exception e)
            {

                return false;
            }

        }

        public object Listar()
        {
            try
            {
                DataTable Materiases = Conexion.GetInstance.Leer("SELECT * FROM MATERIAS");
                //copiado StackOverFlow ,Permite Transformar DataTable en Listas
                if (Materiases != null)
                {
                    var empList = Materiases.AsEnumerable()
                      .Select(dataRow => new Materias
                      {
                          Descripcion = dataRow.Field<string>("Descripcion"),
                          AnioCursado = dataRow.Field<int>("AnioCurso"),
                          Id = dataRow.Field<int>("Id"),
                          Activa = dataRow.Field<bool>("Activa")
                       
                      }).ToList();
                    return empList;
                }
                return new List<Materias>();
            }
            catch (Exception e)
            {

                return null;
            }

        }

        public bool EliminarDatos(int IdDato)
        {
            try
            {
                int respuesta = 0;
                respuesta = Conexion.GetInstance.Escribir($"DELETE FROM MATERIAS WHERE Id ={IdDato}");
                if (respuesta > 0)
                {
                    return true;

                }
                return false;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public bool Actualizar(int IdDato, object datosNuevos)
        {
            try
            {
                int respuesta = 0;
                Materias DatosInsert = (Materias)datosNuevos;
                int activo = DatosInsert.Activa ? 1 : 0;
                respuesta = Conexion.GetInstance.Escribir($"UPDATE `tpcuarentena`.`materias` SET `Descripcion` = " +
                    $"'{DatosInsert.Descripcion}', `AnioCurso` = '{DatosInsert.AnioCursado}', `Activa` = {activo} " +
                    $"WHERE (`ID` = {IdDato});" );
                  if (respuesta > 0)
                {
                    return true;

                }
                return false;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}
