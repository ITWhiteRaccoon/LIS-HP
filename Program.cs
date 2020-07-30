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
        //Criar um 'mapa'com os caracteres nas posições das chaves
        private const string MapaChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ.,;!?";

        //Fazer apenas uma classe devido a simplicidade
        private static void Main(string[] args)
        {
            Console.WriteLine(LerArquivo(args[0]));
            Console.ReadKey();
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
