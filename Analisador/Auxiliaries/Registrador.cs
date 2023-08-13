using System.Text;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Registrador
    {

        private readonly StringBuilder linhaAcumulada;
        private readonly StringBuilder conteudoAcumulado;
        private readonly StringBuilder tabAcumulado;
        int tabQuantidade=0;

        public Registrador()
        {
            linhaAcumulada = new StringBuilder();
            conteudoAcumulado = new StringBuilder();
            tabAcumulado = new StringBuilder();
        }

        public void Registro(string msg) => linhaAcumulada.Append(" " + msg);

        public void RegistroQuebraDeLinha()
        {
            conteudoAcumulado.Append(linhaAcumulada.ToString().TrimStart());
            linhaAcumulada.Clear();
        }
        public void RegistroTab()
        {
            linhaAcumulada.Append("\t");
            tabQuantidade++;
        }

        public void RemoverTab()
        {
            tabQuantidade--;
            int ultimoIndex = linhaAcumulada.Length - 1;
            if (ultimoIndex >= 0 && linhaAcumulada[ultimoIndex] == '\t')
            {
                linhaAcumulada.Length = ultimoIndex;
            }
        }

        public override string ToString() => conteudoAcumulado.ToString();
    }
}