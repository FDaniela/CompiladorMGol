# PROJETO: Desenvolvimento de um compilador

Este repositório tem como objetivo o desenvolvimento de um compilador que recebe como entrada um arquivo fonte em uma linguagem Mgol, realiza a fase de análise, síntese e semântica gerando um arquivo objeto em linguagem C. O arquivo final deverá ser compilável em compilador C, ou seja, o código gerado deverá estar completo para compilação e execução.

Este repositório tem como objetivo o desenvolvimento de um compilador que aceitará como entrada um arquivo fonte escrito na linguagem de programação Mgol (uma linguagem criada especificamente para este estudo de caso). O compilador passará pelas etapas de análise e síntese, produzindo como saída um arquivo objeto na linguagem C. Esse arquivo resultante estará pronto para ser compilado e executado em um compilador C, ou seja, o código gerado será completo e funcional para fins de compilação e execução.

<p align="center">
  <img src="https://github.com/FDaniela/CompiladorMGol/assets/102395421/52a7459d-ab8b-4dd1-b908-094655759615">
</p>

Os módulos a serem implementados contemplam:

# Etapa 1 - Analisador Léxico

Esta etapa visa o desenvolvimento de um analisador léxico e da tabela de símbolos. Resultando na leitura do arquivo fonte e produção de tokens para a análise léxica e tabela de símbolos.

# Etapa 2 - Analisador Sintático

Esta etapa visa o desenvolvimento do analisador sintático ascendente SLR(1) para verificação de sintaxe com dados obtidos do analisador léxico e também a recuperação do erro com reestabelecimento da análise. Resultando na obtenção dos tokens, produção da árvore sintática através do modelo de análise sintática e implementação de rotina de tratamento e recuperação do erro sintático.

# Etapa 3 - Analisador Semântico

Esta etapa visa o desenvolvimento do analisador semântico e geração de código final a partir do método tradução dirigida pela sintaxe. Resultando na realização de análise semântica e produção de código final em conjunto com a análise sintática.

---

# Arquitetura Geral

<p align="center">
  <img src="https://github.com/FDaniela/CompiladorMGol/assets/102395421/1c7c4241-4c53-49a5-9d47-cb9cf450b72c">
</p>!


Ao final de todos teremos como sistema e resultado do estudo de caso, um pequeno compilador que compilará o programa fonte (linguagem Mgol), Fonte.ALG (a) em PROGRAMA.C (b).

<p align="center">
  <img src="https://github.com/FDaniela/CompiladorMGol/assets/102395421/4ced8373-8ec7-4340-910b-cda62c43a684">
</p>

---

# Tabelas Auxiliares

## TABELA 1 - Símbolos do alfabeto

| **Definições**                       | **Significado**                                                                                                                                              |
|--------------------------------------|--------------------------------------------------------------------------------------------------------------------------------------------------------------|
| **Dígitos**                          | 0,1,2,3,4,5,6,7,8,9                                                                                                                                        |
| **Letras (maiúsculas e minúsculas)** | A, B, ...,Z ,a ,..., z                                                                                                                                     |
| **Demais caracteres**                |  ,(vírgula), ;(ponto e vírgula), :(dois pontos), .(ponto), !, ?, \, * ,+ ,- , / , (, ), {, }, [,], <, >, =‘(aspas simples), “ (aspas duplas), _(underline) |

## TABELA 2 - Palavras reservadas
  
| Token     | Significado                                  |
|-----------|----------------------------------------------|
| **inicio**    | Delimita o início do programa                |
| **varinicio** | Delimita o início da declaração de variáveis |
| **varfim**    | Delimita o fim da declaração de variáveis    |
| **escreva**   | Imprime na saída padrão                      |
| **leia**      | Lê da saída padrão                           |
| **se**        | Estrutura condicional                        |
| **entao**     | Elemento de estrutura condicional            |
| **fimse**     | Elemento de estrutura condicional            |
| **repita**    | Elemento de estrutura de repetição           |
| **fimrepita** | Elemento de estrutura de repetição           |
| **fim**       | Delimita o fim do programa                   |
| **inteiro**   | Tipo de dado inteiro                         |
| **literal**   | Tipo de dado literal                         |
| **real**      | Tipo de dado real                            |

## TABELA 3 - Tabela de tokens

| **Token**      | **Significado**                                          | **Características/Padrão**                 |
|----------------|----------------------------------------------------------|--------------------------------------------|
| **Num**        | Constante numérica                                       | D+ (\. D + )? ((E\|e)(+\|−)? D + ) ?       |
| **Lit**        | Constante literal                                        | ".*"                                       |
| **id**         | Identificador                                            | L(L\|D\|_)∗                                |
| **Comentário** | Texto entre { }                                          | {.*}                                       |
| **EOF**        | Final de Arquivo                                         | Flag da linguagem (EOF é um único símbolo) |
| **OPR**        | Operadores relacionais                                   | <, >, >= , <= , =, <>                      |
| **RCB**        | Atribuição                                               | <-                                         |
| **OPM**        | Operadores aritméticos                                   | + , -, *, /                                |
| **AB_P**       | Abre Parênteses                                          | (                                          |
| **FC_P**       | Fecha Parênteses                                         | )                                          |
| **PT_V**       | Ponto e vírgula                                          | ;                                          |
| **ERRO**       | Qualquer símbolo diferente de qualquer palavra definida. |                                            |
| **Vir**        | Vírgula                                                  | ,                                          |
| **Ignorar**    | Tabulação, Espaço, Salto de Linha                        | Reconhecidos e ignorados.                  |
