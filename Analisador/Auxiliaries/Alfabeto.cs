using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompiladorMGol.Analisador.Auxiliaries
{
    public class Alfabeto
    {
        public char 
        ESPACO = ' ',
        SALTO_DE_LINHA='\n',
        TABULACAO='\t',
        VIRGULA = ',',
        PONTO_E_VIRGULA = ';',
        DOIS_PONTOS = ':',
        PONTO = '.',
        EXCLAMACAO = '!',
        INTEROGACAO = '?',
        BARRA_INVERTIDA = '\\',
        ASTERISTICO = '*',
        MAIS = '+',
        MENOS = '-',
        BARRA = '/',
        ABRE_PARENTESES = '(',
        FECHA_PARENTENSES = ')',
        ABRE_CHAVES = '{',
        FECHA_CHAVES = '}',
        ABRE_COLCHETES = '[',
        FECHA_COLCHETES = ']',
        MENOR = '<',
        MAIOR = '>',
        IGUAL = '=',
        ASPAS_SIMPLES = '\'',
        ASPAS_DUPLAS = '"',
        SOBRE_LINHA = '_';

        public bool CaractereEspecial(Char caracter)
        {
            if(caracter == VIRGULA || 
               caracter == PONTO_E_VIRGULA ||
               caracter ==DOIS_PONTOS || 
               caracter == PONTO|| 
               caracter == INTEROGACAO|| 
               caracter == BARRA_INVERTIDA|| 
               caracter ==ASTERISTICO||
               caracter == MAIS||
               caracter == MENOS||
               caracter == BARRA||
               caracter == ABRE_PARENTESES||
               caracter == FECHA_PARENTENSES||
               caracter == ABRE_CHAVES|| 
               caracter == FECHA_CHAVES||
               caracter == ABRE_COLCHETES||
               caracter == FECHA_COLCHETES||
               caracter == MENOR||
               caracter == MAIOR||
               caracter == IGUAL||
               caracter == ASPAS_SIMPLES||
               caracter == ASPAS_DUPLAS||
               caracter == SOBRE_LINHA)
            {
                return true;
            }
            return false;
        }

        public bool CaracterDigito(Char caracter)
        {
            if (Char.IsDigit(caracter))
            {
                return true;
            }
            return false;

        }

        public bool CaracterLetra(Char caracter)
        {
            if (Char.IsLetter(caracter))
            {
                return true;
            }
            return false;

        }
   
        public bool CaracterIgnorados(Char caracter)
        {
            if(
            caracter == ESPACO ||
            caracter == SALTO_DE_LINHA||
            caracter == TABULACAO
            
            
            || caracter == '\r'
            )
           // ||
           // caracter =='?'
           // ||
            
            {
                return true;
            }
            return false;
        }
    

        public bool CaracterPonto(Char caracter)
        {
            if(caracter == PONTO)
            {
                return true;
            }
            return false;
        }

        public bool CaracterValido(Char caracter)
        {
            if(CaracterDigito(caracter) || CaractereEspecial(caracter)|| CaracterIgnorados(caracter) || CaracterLetra(caracter)) return true;
            return false;
        }
        
    }
}
