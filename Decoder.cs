using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using ShellProgressBar;

namespace LIS_HP
{
    class Decoder
    {
        //Criar um 'mapa'com os caracteres nas posições das chaves
        private const string Dicionario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.,;!?";
        //Classe que calcula pontuação das frases com base em quão próximo do ingles
        private static Frequencia _frequencia;

        private static void Main(string[] args)
        {
            Console.WriteLine("Por favor informe o caminho para a mensagem criptografada:");
            string caminho = Console.ReadLine();
            string codigo = LerArquivo(caminho);

            _frequencia = new Frequencia(Properties.Resources.EnglishQuadgrams);

            Console.WriteLine(QuebrarCriptografia(codigo));
            Console.ReadKey();
        }

        private static string QuebrarCriptografia(string codigo)
        {
            //Guarda uma lista dos scores de cada uma das chaves possíveis
            var listaScores = new List<double>();
            using (var pbar = new ProgressBar(Dicionario.Length, "Decifrando mensagem...", new ProgressBarOptions { ProgressCharacter = '─' }))
            {
                for (int i = 0; i < Dicionario.Length; i++)
                {
                    //Decifra o texto, calcula o quanto parece com ingles e guarda a pontuacao
                    string textoDecifrado = DecifrarString(i, codigo);
                    listaScores.Add(_frequencia.CalcularScore(textoDecifrado));
                    pbar.Tick($"Testando chave {i + 1} de {Dicionario.Length}.");
                }
            }
            //Retorna o texto decifrado da chave com maior score
            return DecifrarString(listaScores.IndexOf(listaScores.Max()), codigo);
        }

        private static string DecifrarString(int chave, string codigo)
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
            string codigo;
            using (var sr = new StreamReader(strArquivo))
            {
                codigo = sr.ReadToEnd();
            }
            return codigo;
        }
    }
}
