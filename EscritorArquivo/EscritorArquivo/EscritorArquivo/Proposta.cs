using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscritorArquivo
{
    public class Proposta
    {

        public int cod_corretor { get; set; }
        public Int64 cod_cotacao { get; set; }

        public Proposta(int cod_corretor, int cod_cotacao)
        {
            this.cod_corretor = cod_corretor;
            this.cod_cotacao = cod_cotacao;
        }

        public Proposta()
        {

        }

        public static void ShowPropostas(string text)
        {
            Console.Write(text);
        }

        public static string CreatePropostasText(IEnumerable<Proposta> propostas)
        {
            StringBuilder text = new StringBuilder();
            foreach (var proposta in propostas)
            {
                text.AppendLine($"Código do corretor: {proposta.cod_corretor}");
                text.AppendLine($"Código da cotação: {proposta.cod_cotacao}");
                text.AppendLine("");
            }
            text.AppendLine($"Executado em {DateTime.Now.ToShortTimeString()}");
            return text.ToString();
        }

    }
}
