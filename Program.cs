using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LIS_HP
{
    class Program
    {
        //Para codificar, somar chave com posição e dividir pelo comprimento do dicionário.
        //Isso devolve a posição final.

        //Criar um 'mapa'com os caracteres nas posições das chaves
        private const int Key = 19;
        private const string Dicionario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.,;!?";

        //Fazer apenas uma classe devido a simplicidade
        private static void Main(string[] args)
        {
            string codigo = LerArquivo(args[0]);
            Console.WriteLine(DecodificarString(Key, codigo));
            Console.ReadKey();
        }

        private static string DecodificarString(int chave, string codigo)
        {
            var textoDecodificado = new StringBuilder();
            foreach (char c in codigo)
            {
                int i = Dicionario.IndexOf(c);

                //Se o caracter estiver no dicionário, traduz
                if (i >= 0)
                {
                    /*  
                     * Posição da letra codificada menos o valor da chave (adicionei o tamanho do dicionario ao indice e depois dividi para que nao fosse preciso usar condicionais
                     * no caso da posição ser menor que a chave, o que resultaria em um número negativo.
                     */
                    textoDecodificado.Append(Dicionario[(i + Dicionario.Length - chave) % Dicionario.Length]);
                }
                //Se nao encontrar 
                else
                {
                    //Caso seja '#' substitui para espaço, do contrário usa o próprio char (como no caso de novas linhas)
                    textoDecodificado.Append(c == '#' ? ' ' : c);
                }
            }

            return textoDecodificado.ToString();
        }

        private static string LerArquivo(string strArquivo)
        {
            string codigo = "";
            using (var sr = new StreamReader(strArquivo))
            {
                codigo = sr.ReadToEnd();
            }
            return codigo;
        }
    }
}
