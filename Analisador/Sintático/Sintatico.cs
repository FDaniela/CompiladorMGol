using CompiladorMGol.Analisador.Auxiliaries;
using CompiladorMGol.Analisador.Léxico;
using CompiladorMGol.Analisador.Exceptions;
using CompiladorMGol.Analisador;


namespace CompiladorMGol.Analisador.Sintático
{
    public class Sintatico
    {

    private TabelaDaGramatica tabelaDaGramatica = new();
    private Lexico lexico = new();
    private Gramatica gramatica = new();
    private Alfabeto alfabeto = new();
    Stack<int> pilha = new Stack<int>();
    private bool entradaNaoFoiAceita = true;
    private bool erroNaoFoiEncontrado = true;
    private Token?
     tokenAtual;
    private Stack<Token> pilhaSemantica = new();

   

    public Sintatico(){

        gramatica.Producoes();
        //gramatica.ImprimirProduçoesGramatica();
        AnaliseAscendenteSR();

        //System.Console.WriteLine(tabelaDaGramatica.ToString());
    }

    public void AnaliseAscendenteSR()
    {
       // gramatica.Producoes();
        pilha.Push(0);
        tokenAtual = lexico.Scanner();
        //System.Console.WriteLine(gramatica.LadoEsquerdo);

        while (entradaNaoFoiAceita && erroNaoFoiEncontrado)
        {
            int estadoAtual_linha = pilha.Peek();
            //var acao = "oi";
           //System.Console.WriteLine(estadoAtual_linha);
           // System.Console.WriteLine("coluna= "+gramatica.ColunaDaReducao());
           System.Console.WriteLine(tokenAtual.Classe);
            var acao = tabelaDaGramatica.GetAcao(estadoAtual_linha, gramatica.ColunaDaReducao(tokenAtual.Classe));
            System.Console.WriteLine(acao);
            if (EParaEmpilhar(acao))
            {
                RealizaAcaoDeShift(acao);
            }
            else if (EParaReduzir(acao))
            {
                RealizaAcaoDeReducao(acao);
            }
            else if (AceitarEntrada(acao))
            {
                AceitaEntrada();
            }
            else{
                System.Console.WriteLine("não rolou");
            }
        }
        
    }

    private void RealizaAcaoDeShift(string acao)
    {
        var proximoEstado = ObtemEstadoDaAcao(acao);
        int proximo_estado = int.Parse(proximoEstado);
        pilha.Push(proximo_estado);
        
        tokenAtual = lexico.Scanner();
    }

    private void RealizaAcaoDeReducao(string acao)
    {
        Gramatica acao_reducao = gramatica.Reducoes(acao);
        ImprimeAcaoReducao(acao_reducao);
        DesempilhaSimbolos();
        var t = pilha.Peek();
       var nova_acao = tabelaDaGramatica.GetAcao(t, acao_reducao.ColunaDaReducao(acao_reducao.LadoEsquerdo));
        int novo_estado = int.Parse(nova_acao);
       pilha.Push(novo_estado);
       

        void DesempilhaSimbolos()
        {
            var quantidade_simbolos_desempilhar = acao_reducao.LadoDireito.Length;
            for (int i = 0; i < quantidade_simbolos_desempilhar; i++)
                pilha.Pop();
        }
    }

    private void ImprimeAcaoReducao(Gramatica acao_reducao)
    {
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.WriteLine(acao_reducao.ToString());
        Console.ResetColor();
    }

    private void AceitaEntrada()
    {
        entradaNaoFoiAceita = false;
    }

    private string ObtemEstadoDaAcao(string acao)
    {
        // Retorna apenas a parte númerica de s3 ou r2.
        return acao.Trim().Remove(0, 1);
    }

    private bool EParaEmpilhar(string posicao)
    {
        var letra = posicao.Trim().ToCharArray()[0];
        return 's'.Equals(letra);
    }

    private bool EParaReduzir(string posicao)
    {
        var letra = posicao.Trim().ToCharArray()[0];
        return 'r'.Equals(letra);
    }

    private bool AceitarEntrada(string entrada)
    {
        return "acc".Equals(entrada.Trim().ToLower()) || "ac".Equals(entrada.Trim().ToLower());
    }





    }
}