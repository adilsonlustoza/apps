
using System.Windows.Forms;

namespace CampoMinado.Code
{
    public interface ICampoMinado
    {
        void Reiniciar(int totalLinhas = 0, int totalColunas = 0, int totalBombas = 0, Control container = null);
        void Finaliza();
        void ConfereVenceuJogo();
    }
}
