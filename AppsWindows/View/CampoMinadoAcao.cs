﻿using CampoMinado.Code;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CampoMinado.View
{
    public class CampoMinadoAcao : Button  
    {      
        public IList<CampoMinadoAcao> Vizinhos { get; set; } = null;
        public bool Aberto { get; set; } = false;
        public bool Minado { get; set; } = false;
        public bool Marcado { get; set; } = false;
        public bool Seguro { get; set; } = true;
        public bool Fechado { get; set; } = true;
        public bool VizinhosSeguro { get; set; } = true;
        public bool MinasEncontradas { get; set; } = false;
        public int TotalVizinhosMinados { get; set; } = 0;
        private ICampoMinado iCampoMinadoAcao { get; set; } = null;

        private BackgroundWorker backgroundWorker { get; set; }


        public CampoMinadoAcao(int linha, int coluna,int largura =0 , int altura=0)
        {

            this.InitializeBackgroundWorker();
            this.MouseUp += CampoMinadoBotaoClick;
            this.Paint += CampoMinadoBotaoPaint;
            this.Width = largura;
            this.Height = altura;
            this.TextAlign = ContentAlignment.MiddleCenter;
            this.BackColor = Constantes.COR_BG_NORMAL;
            this.Tag = $"[{linha} - {coluna}]";
            this.Location = new Point( (this.Width * coluna) - this.Width, (this.Height * linha) -this.Height);
            this.Vizinhos = new List<CampoMinadoAcao>();
            this.Padding = new Padding(0);
            this.Margin = new Padding(0);
            this.AutoSize = true;
            this.Refresh();


        }
        private void InitializeBackgroundWorker()
        {
            if (this.backgroundWorker == null)
                this.backgroundWorker = new BackgroundWorker();
            backgroundWorker.DoWork += new DoWorkEventHandler(DoWorker);         
         
        }       


        private void DoWorker(object sender, DoWorkEventArgs e)
        {
            ClicadoEsquerdo(sender, e);
        }

        public void SetContainer(ICampoMinado campoMinadoAcao)
        {
            iCampoMinadoAcao = campoMinadoAcao;
        }
  
        private void CampoMinadoBotaoPaint(object sender, PaintEventArgs e)
        {
           this.DesenhaBotao3D(sender, e);
        }
        private void CampoMinadoBotaoClick(object sender, MouseEventArgs e)
        {           
            switch (e.Button)
            {
                case MouseButtons.Left:
                    backgroundWorker.RunWorkerAsync();                 
                    break;

                case MouseButtons.Right:
                    ClicadoDireito();
                    break;
            }

            this.iCampoMinadoAcao.ConfereVenceuJogo();
        }

       

        private void ClicadoEsquerdo(object sender,DoWorkEventArgs e)
        {
            Task.Run(() =>
            {
                var result = this.Invoke(new Action(() =>
                {                 
                    
                    if (this.Image != null)
                        if (MessageBox.Show("Este campo está marcado, deseja realmente abri-lo ? ", "Atenção", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            this.Image = null;

                    if (this.Seguro && this.Fechado)
                        this.AbrirCampo(this);

                    else if (this.Fechado && this.Minado)
                    {
                        this.Aberto = true;
                        this.Image = Image.FromFile(Constantes.IMAGE_BOMB);

                        if (MessageBox.Show("Você perdeu, iniciar novo jogo? ", "Bomba", MessageBoxButtons.YesNo) == DialogResult.Yes)
                            this.iCampoMinadoAcao.Reiniciar();
                        else
                            this.iCampoMinadoAcao.Finaliza();
                    }                   

                }));
            });


            e.Result = DateTime.Now.Ticks;
           
        }
        private void ClicadoDireito()
        {
            this.Marcado = !Marcado;           

            if (string.IsNullOrEmpty(this.Text) && this.Marcado && this.Image==null) {
                this.Image = Image.FromFile(Constantes.IMAGE_FLAG);
                this.Image.Tag = Constantes.IMAGE_FLAG;
            }
            else if (string.IsNullOrEmpty(this.Text) && !this.Marcado && this.Image != null)
            {
                this.Image = Image.FromFile(Constantes.IMAGE_QUESTION);
                this.Image.Tag = Constantes.IMAGE_QUESTION;
            }
            else if (!string.IsNullOrEmpty(this.Text))
                this.Image = null;
             
            

            
        }
        private void DesenhaBotao3D(object sender, PaintEventArgs e)
        {
           
           ControlPaint.DrawBorder(e.Graphics, (sender as CampoMinadoAcao).ClientRectangle,
           SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
           SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
           SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset,
           SystemColors.ControlLightLight, 5, ButtonBorderStyle.Outset);           

        }

      
        private void AbrirCampo(CampoMinadoAcao campo)
        {
          
            if (campo.Seguro && campo.Fechado && campo.VizinhosSeguro)
                FormataCampoSeguro(campo);
            else if (campo.Seguro && !campo.VizinhosSeguro)
                FormataCampoInseguro(campo);
            else if (campo.Seguro && campo.Fechado && campo.Image != null)
                campo.Image = null;
            else
                return;

        }
        private void FormataCampoInseguro(CampoMinadoAcao campo)
        {
            int minadosTotal = campo.TotalVizinhosMinados;

            switch (minadosTotal)
            {
                case 1:
                    campo.ForeColor = Constantes.COR_TXT_VERDE;
                    break;
                case 2:
                    campo.ForeColor = Constantes.COR_TXT_AZUL;
                    break;
                case 3:
                    campo.ForeColor = Constantes.COR_TXT_AMARELO;
                    break;
                default:
                    campo.ForeColor = Constantes.COR_TXT_VERMELHO;
                    break;
            }
            campo.FlatAppearance.BorderColor = Color.Gray;
            campo.FlatAppearance.BorderSize = 1;
            campo.FlatStyle = FlatStyle.Popup;
            campo.Text = minadosTotal.ToString();
            campo.Font = new Font("Arial", 10, FontStyle.Bold, GraphicsUnit.Point);
            campo.Refresh();
        }
        private void FormataCampoSeguro(CampoMinadoAcao campo)
        {
            campo.Fechado = false;
            campo.FlatStyle = FlatStyle.System;
            campo.FlatAppearance.BorderColor = Color.Gray;
            campo.FlatAppearance.BorderSize = 1;
            campo.Vizinhos.ToList().ForEach(vizinho => { if(vizinho!=null) this.AbrirCampo(vizinho); } );
          
        }
    }

}
