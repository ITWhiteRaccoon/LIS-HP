using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace LIS_HP
{
    class Decoder
    {
        //Criar um 'mapa'com os caracteres nas posições das chaves
        private const string Dicionario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //Classe que calcula pontuação das frases com base em quão próximo do ingles

        private static void Main(string[] args)
        {
            Console.WriteLine("Por favor informe a mensagem criptografada:");
            string codigo = Console.ReadLine();
            Console.WriteLine();

            var frequencias = new Dictionary<string, string>
            {
                {"Danish",Properties.Resources.DanishQuadgrams},
                {"English",Properties.Resources.EnglishQuadgrams},
                {"Finnish",Properties.Resources.FinnishQuadgrams},
                {"French",Properties.Resources.FrenchQuadgrams},
                {"German",Properties.Resources.GermanQuadgrams},
                {"Icelandic",Properties.Resources.IcelandicQuadgrams},
                {"Polish",Properties.Resources.PolishQuadgrams},
                {"Russian",Properties.Resources.RussianQuadgrams},
                {"Spanish",Properties.Resources.SpanishQuadgrams},
                {"Swedish",Properties.Resources.SwedishQuadgrams}
            };

            foreach (var f in frequencias)
            {
                Console.WriteLine($"{f.Key}\t-\t{QuebrarCriptografia(codigo, new Frequencia(f.Value))}");
            }
            Console.WriteLine("Pressione qualquer tecla para sair.");
            Console.ReadKey();
        }

        private static string QuebrarCriptografia(string codigo, Frequencia freq)
        {
            //Guarda uma lista dos scores de cada uma das chaves possíveis
            var listaStrings = new List<string>();
            foreach (char c in codigo)
            {
                foreach (char l in Dicionario)
                {
                    listaStrings.Add(codigo.Replace(c, l));
                }
            }

            var scores = new List<double>();

            for (var i = 0; i < listaStrings.Count; i++)
            {
                scores.Insert(i, freq.CalcularScore(listaStrings[i]));
            }

            var maiorIndex = 0;
            double maiorValor = scores[0];
            for (var i = 0; i < scores.Count; i++)
            {
                if (scores[i] > maiorValor)
                {
                    maiorIndex = i;
                    maiorValor = scores[i];
                }
            }
            //Retorna o texto decifrado da chave com maior score
            return listaStrings[maiorIndex];
        }
    }
}
