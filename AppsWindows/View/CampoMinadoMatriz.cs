using CampoMinado.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CampoMinado.View
{
    public class CampoMinadoMatriz : TableLayoutPanel, ICampoMinado
    {
        private ICollection<List<CampoMinadoAcao>> matrizDeCampos { get; set; }
        private static Random rnd { get; set; }
        private int qtdeBombas { get; set; } = 10;
        private int qtdeLinhas { get; set; } = 10;
        private int qtdeColunas { get; set; } = 10;
        private CampoMinadoMatriz matrixMinada { get; set; }
        public CampoMinadoMatriz()
        {

            matrixMinada = this;
            matrixMinada.AutoSize = true;
            matrixMinada.Name = "MatrixMinada";
            matrixMinada.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            matrixMinada.GrowStyle = TableLayoutPanelGrowStyle.AddRows;

        }

        private void SorteiaBomba(ref int n)
        {
            try
            {
                int colunaDaBomba = 1;
                int linhaDaBomba = 1;
                int bombaPosicao = 1;
                CampoMinadoAcao campo=null;

                if (rnd == null)
                    rnd = new Random();

                bombaPosicao = rnd.Next(1, (qtdeLinhas * qtdeColunas));

                linhaDaBomba = Math.Abs((bombaPosicao / qtdeLinhas) > 0 ? (bombaPosicao / qtdeLinhas) : 1);

                if (linhaDaBomba >= qtdeLinhas)
                    linhaDaBomba = qtdeLinhas - 1;
                
                if (bombaPosicao > linhaDaBomba)
                    colunaDaBomba = (bombaPosicao % qtdeColunas) > 0 ? (bombaPosicao % qtdeColunas) : (bombaPosicao / qtdeLinhas);

                if (bombaPosicao < colunaDaBomba)
                    colunaDaBomba = bombaPosicao ;

                if (bombaPosicao > qtdeLinhas)                
                     campo = matrizDeCampos.ElementAt(linhaDaBomba).ElementAt(colunaDaBomba);         
                else                
                     campo = matrizDeCampos.ElementAt(0).ElementAt(colunaDaBomba);                

                if (!campo.Minado)
                {
                    campo.Minado = true;
                    campo.Seguro = false;
                    ++n;
                }
                else
                    SorteiaBomba(ref n);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void Recomecar(int totalLinhas = 0, int totalColunas = 0, int totalBombas = 0, Control container = null)
        {
            qtdeLinhas = totalLinhas;
            qtdeColunas = totalColunas;
            qtdeBombas = totalBombas;

            if (container == null)
            {
                this.Controls.Clear();
                container = this.Parent;
            }

            var tasks = new Task[3];
            tasks[0] = Task.Run(() => MatrizDeCampos(qtdeLinhas, qtdeColunas, container));
            tasks[0].Wait();
            tasks[1] = Task.Run(() => ContaBombas());
            tasks[1].Wait();
            tasks[2] = Task.Run(() => ChecaVizinhos());
            tasks[2].Wait();
            Task.WhenAll(tasks);
            //Container possui acoes na Thread principal
            PreecheTabela();




        }
        private void PreecheTabela()
        {
            try
            {
                if (matrixMinada.IsHandleCreated)
                {
                    if (!matrixMinada.InvokeRequired)
                    {
                        matrixMinada.Invoke((MethodInvoker)delegate
                        {
                            this.PreencheMatrizMinada();
                        });
                    }
                }
                else
                {
                    this.PreencheMatrizMinada();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private void PreencheMatrizMinada()
        {
            matrixMinada.ColumnCount = matrizDeCampos.Count> 0 ? matrizDeCampos.ElementAt(0).Count: 1;

            for (int i = 0; i < matrizDeCampos.Count; i++)
            {
                var linha = matrizDeCampos.ElementAtOrDefault(i);
                matrixMinada.RowCount = linha.Count;

                for (int j = 0; j < linha.Count; j++)
                {
                    var campo = linha[j];
                    matrixMinada.Controls.Add(campo);
                }

            }

            matrixMinada.Refresh();
        }
        private void ContaBombas()
        {
            int bomba = 0;

            try
            {
                if (qtdeBombas < (qtdeLinhas * qtdeColunas))                
                    while (bomba < qtdeBombas)                    
                        SorteiaBomba(ref bomba);                   
                
            }
            catch (Exception e)
            {
                throw e;
            }

        }

        private void ChecaVizinhos()
        {
            CampoMinadoAcao campo = null;

            try
            {
                for (int i = 0; i < matrizDeCampos.Count; i++)
                {
                    var camposLinha = matrizDeCampos.ElementAtOrDefault(i);

                    for (int j = 0; j < camposLinha.Count; j++)
                    {
                        campo = camposLinha.ElementAtOrDefault(j);

                        if (campo.Minado) continue;

                        campo.Vizinhos.Add(camposLinha.ElementAtOrDefault(j + 1));
                        campo.Vizinhos.Add(camposLinha.ElementAtOrDefault(j - 1));

                        campo.Vizinhos.Add(matrizDeCampos.ElementAtOrDefault(i - 1)?.ElementAtOrDefault(j + 1));
                        campo.Vizinhos.Add(matrizDeCampos.ElementAtOrDefault(i - 1)?.ElementAtOrDefault(j));
                        campo.Vizinhos.Add(matrizDeCampos.ElementAtOrDefault(i - 1)?.ElementAtOrDefault(j - 1));

                        campo.Vizinhos.Add(matrizDeCampos.ElementAtOrDefault(i + 1)?.ElementAtOrDefault(j + 1));
                        campo.Vizinhos.Add(matrizDeCampos.ElementAtOrDefault(i + 1)?.ElementAtOrDefault(j));
                        campo.Vizinhos.Add(matrizDeCampos.ElementAtOrDefault(i + 1)?.ElementAtOrDefault(j - 1));

                        campo.TotalVizinhosMinados = campo.Seguro ? campo.Vizinhos.Where(p => p != null && p.Minado).Count() : 0;
                        campo.VizinhosSeguro = campo.TotalVizinhosMinados == 0 ? true : false;

                    }

                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        private void MatrizDeCampos(int totalLinhas, int totalColunas, Control container = null)
        {
            matrizDeCampos = new List<List<CampoMinadoAcao>>();
            var largura = container.Width / totalColunas;
            var altura = container.Height / totalLinhas;

            try
            {
                for (int linha = 1; linha <= totalLinhas; linha++)
                {
                    var camposLinha = new List<CampoMinadoAcao>();

                    for (int coluna = 1; coluna <= totalColunas; coluna++)
                    {
                        var campo = new CampoMinadoAcao(linha, coluna, largura, altura);
                        campo.SetContainer(this);
                        camposLinha.Add(campo);
                    }

                    matrizDeCampos.Add(camposLinha);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        #region [-------------------------------ICampoMinado--------------------------]
        public void Reiniciar(int totalLinhas = 0, int totalColunas = 0, int totalBombas = 0, Control container = null)
        {
            try
            {
                this.Recomecar(
                                totalLinhas = totalLinhas == 0 ? qtdeLinhas : totalLinhas,
                                totalColunas = totalColunas == 0 ? qtdeColunas : totalColunas,
                                totalBombas = totalBombas == 0 ? qtdeBombas : totalBombas,
                                container
                                );
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void Finaliza()
        {
            if (this.Parent != null)
                Application.Exit();
        }
        public void ConfereVenceuJogo()
        {
            int qtdeBombasEncontradas = 0;

            matrizDeCampos.ToList().ForEach(linha => linha.ForEach(campo =>
            {
                if (campo.Minado && campo.Marcado && campo.Fechado)
                    qtdeBombasEncontradas++;
            }));

            if (qtdeBombas <= qtdeBombasEncontradas)
            {
                if (MessageBox.Show(Constantes.MENSAGEM_INICIAR_NOVO_JOGO, Constantes.MENSAGEM_VENCEU_NOVO_JOGO, MessageBoxButtons.YesNo) == DialogResult.Yes)
                    this.Reiniciar();
            }

        }

        #endregion
    }
}
