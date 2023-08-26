# Analisador Léxico

## Descrição

Desenvolver um programa computacional na linguagem escolhida que implemente:

1. **TOKEN - Estrutura Composta Heterogênea:**
    - Esta estrutura *(nó, registro, classe ...)*, denominada TOKEN, armazenará, no momento apropriado da análise, a classificação da palavra e seus atributos. Ela possuirá três campos (os atributos):

        - **Classe**: armazenará a classificação do lexema reconhecido;
        - **Lexema**: armazenará a palavra computada;
        - **Tipo**: armazenará o tipo de dado do lexema quando for possível determiná-lo nesta análise (inteiro, real ou literal) ou NULO em casos que serão definidos abaixo.

2. **TABELA DE SÍMBOLOS - Estrutura de Dados:**
    - Esta estrutura *(hash table, map, lista,...)* armazenará, EXCLUSIVAMENTE, tokens ID e palavras reservadas da linguagem reconhecidas no programa fonte pelo scanner durante o processo de análise.
    - Cada item da tabela será um nó do tipo TOKEN como definido no item 1.
    - As operações a serem realizadas para manipulação da Tabela de Símbolos são: Inserção e Busca e Atualização.
    - Estruturas de dados disponíveis em bibliotecas da linguagem escolhida podem ser utilizadas.
    - Ao iniciar o programa, a tabela de símbolos deverá ser preenchida com todas as PALAVRAS RESERVADAS da linguagem disponíveis na TABELA 2. Os campos classe, lexema e tipo serão preenchidos com a palavra reservada.

3. **SCANNER - Função:**
    - Possuirá o cabeçalho: `token SCANNER (parâmetros de entrada)`
        - Esta função retornará um único TOKEN (definido no item 1) a cada chamada.
        - SCANNER é o nome do procedimento.
        - Parâmetros de entrada serão definidos pelo programador para ajustar a leitura do arquivo fonte para palavra por palavra.
    - Implementará a máquina reconhecedora de padrões projetada no AFD definido na atividade complementar T1.1.
    - Efetuará a leitura do texto fonte caractere por caractere. Partindo do estado inicial do AFD, após a leitura do caractere, consulta-se a tabela de transições e realiza-se uma transição de estado. Essa mudança de estados é realizada até que um estado final seja alcançado ou que não haja possibilidade de transição. Os caracteres são unidos para a formação de uma palavra (lexema). Ao encontrar um estado final, uma cadeia de caracteres será reconhecida por um padrão (classe). Nesse momento, são preenchidos os campos de um novo nó TOKEN. Associado ao estado final temos uma classe, a palavra reconhecida é o lexema, o campo tipo será preenchido conforme:
        - Se a classe = NUM, sendo uma constante numérica inteira, Tipo = “inteiro”, se real, Tipo = “real. O token é retornado por SCANNER.
        - Se a classe = LIT, Tipo= “literal” e retornar o TOKEN para quem invocou o SCANNER.
        - Se a classe = ID, preencher Tipo = NULO. Verificar se o lexema deste TOKEN está na tabela de símbolos:
            - Se estiver, retornar na função SCANNER o TOKEN que está na tabela de símbolos;
            - Se não estiver, inserir o novo TOKEN na TABELA DE SÍMBOLOS e retorná-lo na função SCANNER.
        - Se a classe = ERRO:
            - Emitir, na saída padrão, a descrição do tipo do erro (mensagem para o programador com o tipo do erro identificado) seguida da linha e coluna (do código fonte) nas quais o erro ocorreu.
            - O(A)(s) aluno(a)(s) deverão mapear todos os tipos de erros léxicos possíveis dentro do escopo deste projeto.
            - Exemplo de mensagem a ser emitida na saída: “ERRO LÉXICO – Caractere inválido na linguagem, linha 2, coluna 1”.
            - Retornar, na função SCANNER, o TOKEN com os campos classe=ERROR, lexema=NULO e tipo=NULO.
        - Se a classe for caractere em branco, espaço, salto de linha, tabulação ou comentário, o scanner reconhece e ignora, reinicia o processo para um novo TOKEN.
        - Se a classe for diferente das anteriores, preencher o campo TIPO com NULO e retornar o TOKEN na função SCANNER.

4. **PRINCIPAL:**
    - Efetuará a abertura do arquivo fonte.
    - Conterá uma estrutura de repetição que:
        - Invocará a função SCANNER para que retorne um TOKEN por chamada.
        - A cada TOKEN retornado pelo SCANNER:
            - Se classe = ERRO, ignorar, pois já foi tratado dentro da função SCANNER.
            - Caso contrário, emitir mensagem como no exemplo: Classe: Num, Lexema: 123, Tipo: NULL.
    - O loop só finalizará após a leitura de todo o Código Fonte.

## Resultado

O Scanner deverá ler todo o texto fonte realizando todas as tarefas especificadas na seção 4. O resultado esperado é a emissão na saída padrão de todos os TOKENs reconhecidos. Observe o desenho abaixo, nela é apresentada uma amostra da saída do programa a ser desenvolvido.