# CompiladorMgol
Este repositório tem como objetivo o desenvolvimento de um compilador que recebe como entrada um arquivo fonte em uma linguagem Mgol, realiza a fase de análise, síntese e semântica gerando um arquivo objeto em linguagem C. O arquivo final deverá ser compilável em compilador C, ou seja, o código gerado deverá estar completo para compilação e execução.

Este repositório tem como objetivo o desenvolvimento de um compilador que aceitará como entrada um arquivo fonte escrito na linguagem de programação Mgol (uma linguagem criada especificamente para este estudo de caso). O compilador passará pelas etapas de análise e síntese, produzindo como saída um arquivo objeto na linguagem C. Esse arquivo resultante estará pronto para ser compilado e executado em um compilador C, ou seja, o código gerado será completo e funcional para fins de compilação e execução.

| Índice                                                                 |
|------------------------------------------------------------------------|
| [Analisador Léxico](../master/Analisador/analisadorLexico.cs)          |
| [Analisador Sintático](../master/Analisador/analisadorLexico.cs)       |
| [Analisador Semântico](../master/Analisador/analisadorLexico.cs)       |


Os módulos a serem implementados contemplam:

# Etapa 1 - Analisador Léxico

Esta etapa visa o desenvolvimento de um **analisador léxico** e da **tabela de símbolos**. Resultando na leitura do arquivo fonte e produção de tokens para a análise léxica e tabela de símbolos.

# Etapa 2 - Analisador Sintático

Esta etapa visa o desenvolvimento do **analisador sintático ascendente** **SLR(1)** para verificação de sintaxe com dados obtidos do analisador léxico e também a **recuperação do erro** com reestabelecimento da análise. Resultando na obtenção dos tokens, produção da árvore sintática através do modelo de análise sintática e implementação de rotina de tratamento e recuperação do erro sintático.

# Etapa 3 - Analisador Semântico

Esta etapa visa o desenvolvimento do analisador semântico e geração de
código final a partir do método tradução dirigida pela sintaxe. Resultando na realização de análise semântica e produção de código final em conjunto com a análise sintática.


---


## Autômato finito determinístico

//imagem

**Legenda**

| **Estado** |    q1    |     q3    |     q6     |  q7 | q8 |     q10    | q11 | q12 | q13 | q14 | q15 |  q17 |  q18 |  q19 | q21 |
|:----------:|:--------:|:---------:|:----------:|:---:|:--:|:----------:|:---:|:---:|:---:|:---:|:---:|:----:|:----:|:----:|:---:|
| **Token**  | Num(int) | Num(real) | Num(cient) | Vir | id | Comentário | EOF | OPR | OPR | OPR | RCB | AB_P | FC_P | PT_V | Lit |

**Tabela de transição**

|               |                    | _COLUNA1_ | _COLUNA2_ | _COLUNA3_ | _COLUNA4_ | _COLUNA5_ | _COLUNA6_ | _COLUNA7_ | _COLUNA8_ | _COLUNA9_ | _COLUNA10_ | _COLUNA11_ | _COLUNA12_ | _COLUNA13_ | _COLUNA14_ | _COLUNA15_ | _COLUNA16_ | _COLUNA17_ | _COLUNA18_ | _COLUNA19_ | _COLUNA20_ | _COLUNA21_ | _COLUNA22_ |
|---------------|--------------------|-----------|-----------|-----------|-----------|-----------|-----------|-----------|-----------|-----------|------------|------------|------------|------------|------------|------------|------------|------------|------------|------------|------------|------------|------------|
|               | **Alfabeto\Estados**   |   **q0**  |   **q1**  |   **q2**  |   **q3**  |   **q4**  |   **q5**  |   **q6**  |   **q7**  |   **q8**  |   **q9**   |   **q10**  |   **q11**  |   **q12**  |   **q13**  |   **q14**  |   **q15**  |   **q16**  |   **q17**  |   **q18**  |   **q19**  |   **q20**  |   **q21**  |
| **_LINHA1_**  | **espaço**         | q0        |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA2_**  | **salto de linha** | q0        |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA3_**  | **tabulação**      | q0        |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA4_**  | **D**              | q1        | q1        | q3        | q3        | q6        | q6        | q6        |           | q8        | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA5_**  | **L**              | q8        |           |           |           |           |           |           |           | q8        | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA6_**  | **E \| e**         |           | q4        |           | q4        |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA7_**  | **,**              | q7        |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA8_**  | **;**              | q19       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA9_**  | **:**              |           |           |           |           |           |           |           |           |           |            |            |            |            |            |            |            |            |            |            |            |            |            |
| **_LINHA10_** | **.**              |           | q2        |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA11_** | **!**              |           |           |           |           |           |           |           |           |           |            |            |            |            |            |            |            |            |            |            |            |            |            |
| **_LINHA12_** | **?**              |           |           |           |           |           |           |           |           |           |            |            |            |            |            |            |            |            |            |            |            |            |            |
| **_LINHA13_** | __/__              |           |           |           |           |           |           |           |           |           |            |            |            |            |            |            |            |            |            |            |            |            |            |
| **_LINHA14_** | __*__              | q16       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA15_** | **+**              | q16       |           |           |           | q5        |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA16_** | **-**              | q16       |           |           |           | q5        |           |           |           |           | q9         |            |            | q15        |            |            |            |            |            |            |            | q20        |            |
| **_LINHA17_** | **/**              | q16       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA18_** | **(**              | q17       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA19_** | **)**              | q18       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA20_** | **{**              | q9        |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA21_** | **}**              |           |           |           |           |           |           |           |           |           | q10        |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA22_** | **[**              |           |           |           |           |           |           |           |           |           |            |            |            |            |            |            |            |            |            |            |            |            |            |
| **_LINHA23_** | **]**              |           |           |           |           |           |           |           |           |           |            |            |            |            |            |            |            |            |            |            |            |            |            |
| **_LINHA24_** | **<**              | q12       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA25_** | **>**              | q14       |           |           |           |           |           |           |           |           | q9         |            |            | q13        |            |            |            |            |            |            |            | q20        |            |
| **_LINHA26_** | **=**              | q13       |           |           |           |           |           |           |           |           | q9         |            |            | q13        |            | q13        |            |            |            |            |            | q20        |            |
| **_LINHA27_** | **'**              |           |           |           |           |           |           |           |           |           |            |            |            |            |            |            |            |            |            |            |            |            |            |
| **_LINHA28_** | **"**              | q20       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q21        |            |
| **_LINHA29_** | **_**              |           |           |           |           |           |           |           |           | q8        | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |
| **_LINHA30_** | **EOF**            | q11       |           |           |           |           |           |           |           |           | q9         |            |            |            |            |            |            |            |            |            |            | q20        |            |

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
| **Comentário** | Texto entre { }                                           | {.*}                                       |
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