using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using Enigma_Emulator;

namespace LIS_HP
{
    class Decoder
    {
        //Criar um 'mapa'com os caracteres nas posições das chaves
        private const string Dicionario = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        //Classe que calcula pontuação das frases com base em quão próximo do ingles

        private static void Main(string[] args)
        {
            string codigo = "HGNCAZM";
            Console.WriteLine();

            var orderList = new List<string>
            {
                "I-II-III",
                "I-III-II",
                "II-I-III",
                "II-III-I",
                "III-I-II",
                "III-II-I"
            };

            var maquina = new EnigmaMachine();
            var config = new EnigmaSettings
            {
                Rings = new[] { 'C', 'E', 'A' },
                Grund = new[] { 'A', 'A', 'A' },
                Order = "III-I-II",
                Reflector = 'B'
            };

            maquina.setSettings(config.Rings, config.Grund, config.Order, config.Reflector);
            maquina.addPlug('V', 'A');
            Console.WriteLine(maquina.runEnigma(codigo));

            //var frequencias = new Dictionary<string, string>
            //{
            //    {"Danish",Properties.Resources.DanishQuadgrams},
            //    {"English",Properties.Resources.EnglishQuadgrams},
            //    {"Finnish",Properties.Resources.FinnishQuadgrams},
            //    {"French",Properties.Resources.FrenchQuadgrams},
            //    {"German",Properties.Resources.GermanQuadgrams},
            //    {"Icelandic",Properties.Resources.IcelandicQuadgrams},
            //    {"Polish",Properties.Resources.PolishQuadgrams},
            //    {"Russian",Properties.Resources.RussianQuadgrams},
            //    {"Spanish",Properties.Resources.SpanishQuadgrams},
            //    {"Swedish",Properties.Resources.SwedishQuadgrams}
            //};

            //foreach (var f in frequencias)
            //{
            //    Console.WriteLine($"{f.Key}\t-\t{QuebrarCriptografia(codigo, new Frequencia(f.Value))}");
            //}
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

        private class EnigmaSettings
        {
            public char[] Rings { get; set; }
            public char[] Grund { get; set; }
            public string Order { get; set; }
            public char Reflector { get; set; }
            public List<string> Plugs = new List<string>();
        }
    }
}
