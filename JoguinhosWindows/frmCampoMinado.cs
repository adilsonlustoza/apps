﻿using CampoMinado.View;
using System;
using System.Windows.Forms;

namespace CampoMinado
{
    public partial class frmCampoMinando : Form
    {       
        private  CampoMinadoMatriz matrizMinada { get; set; }     
        public frmCampoMinando()
        {
            InitializeComponent();         
        }

        private void frmCampoMinando_Load(object sender, EventArgs e)
        {
            this.Reiniciar();
           
        }
        public void Reiniciar()
        {               
            
            matrizMinada = new CampoMinadoMatriz();
           
            if(matrizMinada.InvokeRequired)
            {
                var restart = new Action(Reiniciar);
                matrizMinada.Invoke(restart);
            }
            else 
            {
            var totalLinhas = int.Parse(this.txtLinhas.Text);
            var totalColunas = int.Parse(this.txtColunas.Text);
            var totalBombas = int.Parse(this.txtBombas.Text);

             if(totalLinhas<3 || totalColunas<3 || totalBombas< 1)
                {
                    if (MessageBox.Show("As linhas devem ser no mínimo 3, colunas 3 e bombas 1! Deseja aplcar estes valores? ", "Atenção", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        totalLinhas = 3;
                        totalColunas = 3;
                        totalBombas = 1;

                        this.txtLinhas.Text = totalLinhas.ToString();
                        this.txtColunas.Text = totalLinhas.ToString();
                        this.txtBombas.Text = totalLinhas.ToString();


                    }
                    else
                        this.Reiniciar();
                }
                    

            matrizMinada.Reiniciar(totalLinhas, totalColunas, totalBombas,this.pnlCampos);  
            
           
            }

            this.FormataBotaoRestart();
            this.RedimensionarContainers();
        }


        private void FormataBotaoRestart()
        {
            btnRestart.FlatAppearance.MouseOverBackColor = btnRestart.BackColor;
            btnRestart.BackColorChanged += (s, e) => {
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
        private void btnRestart_Click(object sender, EventArgs e)
        {
            this.Reiniciar();
        }

      
    }
}
