using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace LIS_HP
{
    public class Frequencia
    {
        /*
         * Essa classe foi baseada em artigos sobre criptografia e algoritmos prontos e adaptados para essa linguagem e uso
         */

        //Frequencia de cada grupo de n letras baseado em texto ingles
        private readonly Dictionary<string, double> mapaScores = new Dictionary<string, double>();
        private readonly double frequenciaTotal;
        private readonly double piso;
        private readonly int tamanhoNgram;

        public Frequencia(string strFrequencias)
        {
            var listaFrequencias = Regex.Split(strFrequencias, "\n|\r\n");
            foreach (string s in listaFrequencias)
            {
                var linha = s.Split(' ');
                mapaScores.Add(linha[0], Convert.ToDouble(linha[1]));
                frequenciaTotal += Convert.ToDouble(linha[1]);
            }
            var chaves = new List<string>(mapaScores.Keys);
            tamanhoNgram = chaves[0].Length;
            foreach (string letra in chaves)
            {
                mapaScores[letra] = Math.Log10(mapaScores[letra] / frequenciaTotal);
            }

            piso = Math.Log10(0.01 / frequenciaTotal);
        }

        public double CalcularScore(string textoDecifrado)
        {
            double score = 0;
            for (int i = 0; i < textoDecifrado.Length - tamanhoNgram; i++)
            {
                string letra = textoDecifrado.Substring(i, tamanhoNgram);
                if (mapaScores.ContainsKey(letra))
                {
                    score += mapaScores[letra];
                }
                else
                {
                    score += piso;
                }
            }

            return score;
        }
    }
}
