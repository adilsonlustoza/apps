using CampoMinado.View;
using System;
using System.Windows.Forms;

namespace CampoMinado
{
    public partial class frmCampoMinando : Form
    {
        private CampoMinadoMatriz matrizMinada { get; set; }
        private int numeroBombas { get; set; } = 10;

        private  bool firstLoad { get; set; } = true;

        public frmCampoMinando()
        {
            InitializeComponent();
        }
        #region [--Metodos--]

        public void Reiniciar()
        {

            if (this.firstLoad)
            {
                this.comboBoxNivel.SelectedIndex = 1;
                this.firstLoad = false;
            }

            this.matrizMinada = new CampoMinadoMatriz();

            if (this.matrizMinada.InvokeRequired)
            {
                var restart = new Action(Reiniciar);
                this.matrizMinada.Invoke(restart);
            }
            else
            {

                var totalLinhas = 10;
                var totalColunas = 10;
                var totalBombas = this.numeroBombas;


                this.txtBombas.Text = totalBombas.ToString();
                this.matrizMinada.Reiniciar(totalLinhas, totalColunas, totalBombas, this.pnlCampos);

            }

            this.FormataBotaoRestart();
            this.RedimensionarContainers();
        }
        private void FormataBotaoRestart()
        {
            btnRestart.FlatAppearance.MouseOverBackColor = btnRestart.BackColor;
            btnRestart.BackColorChanged += (s, e) =>
            {
                btnRestart.FlatAppearance.MouseOverBackColor = btnRestart.BackColor;
            };
            toolTipoRestart.SetToolTip(btnRestart, "Reiniciar");
        }
        private void RedimensionarContainers()
        {
            try
            {
                this.pnlObjetos.AutoSize = true;
                this.pnlObjetos.Refresh();

                this.pnlCampos.Controls.Clear();
                this.pnlCampos.Controls.Add(matrizMinada);
                this.pnlCampos.AutoSize = true;
                this.pnlCampos.Refresh();

                this.AutoSize = true;
                this.Refresh();

            }
            catch (Exception)
            {

                throw;
            }
        }
        private void SoNumeros(object sender, KeyPressEventArgs e)
        {
            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
        #endregion


        #region [--Eventos--]



        private void frmCampoMinando_Load(object sender, EventArgs e)
        {
            this.Reiniciar();

        }
        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.Reiniciar();
        }

        private void txtLinhas_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SoNumeros(sender, e);
        }

        private void txtColunas_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SoNumeros(sender, e);
        }

        private void txtBombas_KeyPress(object sender, KeyPressEventArgs e)
        {
            this.SoNumeros(sender, e);
        }


        private void comboBoxNivel_SelectedIndexChanged(object sender, EventArgs e)
        {
            var dificuldade = this.comboBoxNivel.SelectedItem;

            switch (dificuldade.ToString().ToLower())
            {
                case "iniciante":
                    this.numeroBombas = 5;
                    break;

                case "normal":
                    this.numeroBombas = 10;
                    break;

                case "experiente":
                    this.numeroBombas = 15;
                    break;


                default:
                    throw new Exception();

            }

            this.Reiniciar();
        }

        #endregion

    }
}
