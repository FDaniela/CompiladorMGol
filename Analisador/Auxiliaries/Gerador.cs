using System.Text;

namespace CompiladorMGol.Analisador.Auxiliaries
{

    public class Gerador
    {

        private StreamWriter? escritaArquivo;
        private Registrador erros = new();
        private Registrador codigo = new();
        private Registrador variaveis = new();
        private Registrador temporarias = new();
        private Registrador cabecalho = new();


        public Gerador()
        {
            CriacaoArquivoObj();
        }

        private void CriacaoArquivoObj()
        {
            var path = "Analisador/Resource/Codes";
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var filePath = Path.Combine(path, "programa.c");

            if (File.Exists(filePath))
                File.Delete(filePath);

            escritaArquivo = new StreamWriter(File.Create(filePath));

        }

        public void ImprimeVariavel(string vari)
        {
            variaveis.Registro(vari);
            variaveis.RegistroQuebraDeLinha();
        }

        public void ArquivoFinal(string msg) => codigo.Registro(msg);

        public void QuebraDeLinha() => codigo.RegistroQuebraDeLinha();

        public void QuebraDeLinhaVariavel() => variaveis.RegistroQuebraDeLinha();

        public void ImprimeVariaveisTemporarias(string t)
        {
            temporarias.Registro(t);
            temporarias.RegistroQuebraDeLinha();
        }

        public void GerarCodigoFinal()
        {
    
            cabecalho.Registro("#include <stdio.h>\n");
            cabecalho.RegistroQuebraDeLinha();
            cabecalho.Registro("typedef char literal[256];\n");
            cabecalho.RegistroQuebraDeLinha();
            cabecalho.Registro("void main(void)\n");
            cabecalho.RegistroQuebraDeLinha();
            cabecalho.Registro("{\n");
            cabecalho.RegistroQuebraDeLinha();

            escritaArquivo.WriteLine(cabecalho.ToString());


            escritaArquivo.Write("/*----Variaveis temporarias----*/\n");
            escritaArquivo.Write(temporarias.ToString());
            escritaArquivo.WriteLine("/*------------------------------*/\n");

            escritaArquivo.WriteLine(variaveis.ToString());

            codigo.Registro("\n}");
            codigo.RegistroQuebraDeLinha();

            escritaArquivo.Write(codigo.ToString());

            GerarSaidaConsole();

            escritaArquivo.Flush();
            escritaArquivo.Close();
        }

        private void GerarSaidaConsole()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.WriteLine("--------------------     SAÍDA DO CÓDIGO OBJETO     --------------------");
            Console.WriteLine();
            Console.WriteLine(cabecalho.ToString());
            Console.WriteLine("/*----Variaveis temporarias----*/");
            Console.Write(temporarias.ToString());
            Console.WriteLine("/*------------------------------*/\n");
            Console.WriteLine(variaveis.ToString());
            Console.WriteLine(codigo.ToString());
            Console.WriteLine();
            Console.WriteLine("---------------------------------------------------------------------------");
        }
    }
}