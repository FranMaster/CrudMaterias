using Capa_Entidades;
using Capa_Logica;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AplicacionEscuela.AdministrarMaterias
{
    public partial class RelacionarMaterias : Form
    {
        public Profesor ProfesorSeleccionado { get; set; }
        public RelacionarMaterias(object Filtro)
        {
            InitializeComponent();
            ProfesorSeleccionado=(Profesor)Filtro;
            ListarRelaciones();


        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                List<Materias> lista = (List<Materias>)new LogicaMaterias().Listar();
                int? IndiceEsperado = null;
                for (int i = 0; i <= lista.Count - 1; i++)
                {
                    if (lista[i].Descripcion == ((Materias)comboBox1.SelectedItem).Descripcion)
                    {
                        IndiceEsperado = i;
                        lista[i].DictadaPor = new List<Profesor>();
                    }
                }
                if (IndiceEsperado != null)
                {
                    lista[IndiceEsperado.Value].DictadaPor.Add(ProfesorSeleccionado);
                    new LogicaMaterias().Actualizar(IndiceEsperado.Value, lista[IndiceEsperado.Value]);
                    ListarRelaciones();
                }
            }
            catch (Exception r)
            {

                MessageBox.Show(r.Message);
            }
            

        }
        public void ListarRelaciones()
        {
            List<Materias> lista = (List<Materias>)new LogicaMaterias().Listar();
            List<Materias> listaMateriasRelacionadas = new List<Materias>();
            this.comboBox1.DataSource = lista;
            this.comboBox1.DisplayMember = "Descripcion";
            foreach (var Materia in lista)
            {
                if (Materia.DictadaPor != null && Materia.DictadaPor.Count > 0)
                {
                    for (int i = 0; i <= Materia.DictadaPor.Count - 1; i++)
                    {
                        if (Materia.DictadaPor[i].Dni == ProfesorSeleccionado.Dni)
                        {
                            listaMateriasRelacionadas.Add(Materia);
                        }
                    }
                }
                else
                {
                    continue;
                }


            }

            dataGridView1.DataSource = null;
            dataGridView1.DataSource = listaMateriasRelacionadas;
        }
    }
}
