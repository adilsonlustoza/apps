using System;
using System.Drawing;
using System.IO;

namespace CampoMinado.Code
{
    public static class Constantes
    {
        public static Color COR_BG_NORMAL   =  Color.FromArgb(184, 184, 184);
        public static Color COR_BG_MARCACAO = Color.FromArgb(8, 179, 247);
        public static Color COR_BG_EXPLOSAO = Color.FromArgb(189, 66, 68);

        public static Color COR_TXT_VERDE   =  Color.FromArgb(0, 100, 0);
        public static Color COR_TXT_AZUL = Color.Blue;
        public static Color COR_TXT_AMARELO = Color.Yellow;
        public static Color COR_TXT_VERMELHO = Color.Red;

        public static string IMAGE_BOMB = Path.Combine(Environment.CurrentDirectory, "Images\\bomb.png");
        public static string IMAGE_QUESTION = Path.Combine(Environment.CurrentDirectory, "Images\\question.png");
        public static string IMAGE_FLAG = Path.Combine(Environment.CurrentDirectory, "Images\\red-flag.png");

        public static string MENSAGEM_INICIAR_NOVO_JOGO =  $"Iniciar novo jogo?";
        public static string MENSAGEM_VENCEU_NOVO_JOGO  =  $"Venceu, encontrou todas as bombas.";
    }
}
