# Analisador Léxico

<p align="center">
  <img src="https://github.com/FDaniela/CompiladorMGol/assets/102395421/dc0c2e9e-5db2-4481-ac8f-05812af66144" alt="Descrição da imagem">
</p>

## Descrição

3.1 **Analisador Sintático SLR(1):**
   - Reconhecerá as sentenças formadas a partir da gramática livre de contexto na TABELA 1.

3.2 **Passos de Projeto:**
   a. Construir o autômato LR(0) para a gramática livre de contexto da TABELA 1 (item 2.2).
   b. Obter os conjuntos FIRST/FOLLOW dos não terminais da gramática (item 2.1).
   c. Construir a tabela de análise sintática SLR com as colunas AÇÃO (shift, reduce, accept e error) e DESVIOS (goto), baseadas nos itens 2.1 e 2.2.
      i. A tabela pode ser construída em um arquivo .csv. O upload pode ser realizado em uma matriz ou estrutura de dados à critério do programador.
      ii. As lacunas da tabela sintática – coluna AÇÕES (espaços sem ações de redução/empilhamento/aceita) devem ser preenchidas com códigos de erros que deverão indicar o tipo de erro sintático encontrado (se falta operador aritmético, relacional, atribuição, aguarda um id, um se, um “(“ , etc.).

**Algoritmo de Análise Sintática:**

<p align="center">
  <img src="https://github.com/FDaniela/CompiladorMGol/assets/102395421/c39117f0-f340-4f02-b376-d93768b09d79" alt="Descrição da imagem">
</p>

3.3 **Implementação do Algoritmo de Análise Sintática Shift-Reduce (PARSER):**
   - Uma estrutura de dados do tipo pilha deverá ser criada para apoiar o reconhecimento da sentença (implementação do autômato de pilha). Ela é inicializada com o estado 0 (estado inicial do autômato LR) ao topo. As operações de empilhamento e desempilhamento apontadas no algoritmo serão realizadas sobre esta pilha.
   - No algoritmo de análise, todas as vezes em que houver um movimento com o apontador de entrada o programa deverá chamar a função “SCANNER” que retornará um TOKEN e seus atributos em a. O campo de a que será utilizado na análise é a “classe”.
   - Todas as vezes que for acionada uma consulta ACTION ou GOTO, a(s) tabela(s) desenvolvida(s) no item 3.2(c) deverá ser consultada.
   - Imprimir a produção significa apresentar na saída padrão (tela do computador) a regra que foi reduzida.

**Parte de Tratamento de Erros:**
   - Ao invocar uma rotina de recuperação de ERRO (item 3.4 abaixo), esta deverá reestabelecer a análise sintática e também imprimir na saída (tela do computador) uma mensagem que informe o tipo do erro sintático encontrado (mensagem o mais específica possível) e a linha e coluna do código da entrada (programa fonte) onde ocorreu o erro.

3.4 **Implementação de Rotina de Tratamento de Erros:**
   a. Realizar uma pesquisa sobre os métodos para recuperação do erro no analisador sintático (modo pânico, correção global, à nível de frase, outros), escolher e implementar pelo menos um modelo ou uma compilação de modelos de tratamento de erros para análise sintática.
   b. Ao encontrar um erro, o PARSER emite mensagem conforme item 3.3.5, reestabelece a análise conforme o item 3.4.a. e continua o processo para todo o restante do código fonte.

3.5 **Invocações do PARSER:**
   a. O SCANNER nas linhas (1) e (6) do algoritmo de análise;
   b. Realizará as análises consultando a tabela de análise conforme linhas (4) a (11) do algoritmo da Figura 1;
   c. Uma rotina que emitirá o tipo do erro sintático encontrado (mensagem na tela informando que houve erro sintático e qual terminal era aguardado para leitura, linha e coluna onde ocorreu o erro), linha (13) do algoritmo de análise na FIGURA1;
   d. Uma rotina que fará uma recuperação do erro (modo pânico ou outro) para continuar a análise sintática até que o final do programa fonte seja alcançado, linha (13) do algoritmo de análise na FIGURA1.



## Resultado

O **PARSER** realizará o processo de análise sintática:

- Invocando o **SCANNER**, sempre que necessitar de um novo TOKEN.
- Inserindo e removendo o topo da pilha.
- Consultando as tabelas ACTION e GOTO para decidir sobre as produções a serem aplicadas até a raiz da árvore sintática seja alcançada e não haja mais tokens a serem reconhecidos pelo SCANNER.
- Mostrando na tela os erros cometidos, bem como sua localização no código fonte (linha, coluna).
- Reestabelecendo a análise para que o restante do código fonte seja analisado.

<p align="center">
  <img src="https://github.com/FDaniela/CompiladorMGol/assets/102395421/c3eb98a9-a6e9-4283-a90c-40c2fdc9e887" alt="Descrição da imagem">
</p>

