
namespace CampoMinado
{
    partial class frmCampoMinando
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCampoMinando));
            this.pnlCampos = new System.Windows.Forms.Panel();
            this.toolTipoRestart = new System.Windows.Forms.ToolTip(this.components);
            this.pnlObjetos = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBombas = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtColunas = new System.Windows.Forms.TextBox();
            this.txtLinhas = new System.Windows.Forms.TextBox();
            this.btnRestart = new System.Windows.Forms.Button();
            this.pnlObjetos.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCampos
            // 
            resources.ApplyResources(this.pnlCampos, "pnlCampos");
            this.pnlCampos.BackColor = System.Drawing.SystemColors.Control;
            this.pnlCampos.Name = "pnlCampos";
            // 
            // pnlObjetos
            // 
            resources.ApplyResources(this.pnlObjetos, "pnlObjetos");
            this.pnlObjetos.BackColor = System.Drawing.SystemColors.Control;
            this.pnlObjetos.Controls.Add(this.label4);
            this.pnlObjetos.Controls.Add(this.txtBombas);
            this.pnlObjetos.Controls.Add(this.label3);
            this.pnlObjetos.Controls.Add(this.label2);
            this.pnlObjetos.Controls.Add(this.label1);
            this.pnlObjetos.Controls.Add(this.txtColunas);
            this.pnlObjetos.Controls.Add(this.txtLinhas);
            this.pnlObjetos.Controls.Add(this.btnRestart);
            this.pnlObjetos.Name = "pnlObjetos";
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.Name = "label4";
            // 
            // txtBombas
            // 
            resources.ApplyResources(this.txtBombas, "txtBombas");
            this.txtBombas.Name = "txtBombas";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.Name = "label3";
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.Name = "label2";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // txtColunas
            // 
            resources.ApplyResources(this.txtColunas, "txtColunas");
            this.txtColunas.Name = "txtColunas";
            // 
            // txtLinhas
            // 
            resources.ApplyResources(this.txtLinhas, "txtLinhas");
            this.txtLinhas.Name = "txtLinhas";
            // 
            // btnRestart
            // 
            resources.ApplyResources(this.btnRestart, "btnRestart");
            this.btnRestart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRestart.FlatAppearance.BorderSize = 0;
            this.btnRestart.Name = "btnRestart";
            this.btnRestart.TabStop = false;
            this.btnRestart.UseVisualStyleBackColor = false;
            this.btnRestart.Click += new System.EventHandler(this.btnRestart_Click);
            // 
            // frmCampoMinando
            // 
            resources.ApplyResources(this, "$this");
            this.BackColor = System.Drawing.SystemColors.Control;
            this.Controls.Add(this.pnlCampos);
            this.Controls.Add(this.pnlObjetos);
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmCampoMinando";
            this.Load += new System.EventHandler(this.frmCampoMinando_Load);
            this.pnlObjetos.ResumeLayout(false);
            this.pnlObjetos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlCampos;
        private System.Windows.Forms.ToolTip toolTipoRestart;
        private System.Windows.Forms.Panel pnlObjetos;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBombas;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtColunas;
        private System.Windows.Forms.TextBox txtLinhas;
        private System.Windows.Forms.Button btnRestart;
     //   private System.Windows.Forms.Panel pnlAcoes;
    }
}

