using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class FileName
    {

        static void Teste()
        {
            string caminhoArquivo = "C:\\Users\\harpe\\OneDrive\\Documentos\\Visual Studio 2022\\CompiladorMGol\\CompiladorMGol\\Analisador\\Resource\\Fonte2.alg";

            if (File.Exists(caminhoArquivo))
            {
                using (StreamReader reader = new StreamReader(caminhoArquivo))
                {
                    string palavraAtual = "";
                    int caractere;

                    while (!reader.EndOfStream)
                    {
                        caractere = reader.Read();
                        Console.WriteLine((char)caractere);
                        char c = (char)caractere;

                        if (char.IsWhiteSpace(c))
                        {
                            if (!string.IsNullOrEmpty(palavraAtual))
                            {
                                Console.WriteLine("Palavra encontrada: " + palavraAtual);
                                palavraAtual = "";
                            }
                        }
                        else
                        {
                            palavraAtual += c;
                        }
                    }

                    // Verifica se há uma palavra no final do arquivo
                    if (!string.IsNullOrEmpty(palavraAtual))
                    {
                        Console.WriteLine("Palavra encontrada: " + palavraAtual);
                    }
                }
            }
            else
            {
                Console.WriteLine("O arquivo não existe.");
            }
        }
    }
}
