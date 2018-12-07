using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Dapper;

namespace EscritorArquivo
{
    public class Repo : IDisposable
    {
        private readonly SqlConnection cn;

        public Repo()
        {
            cn = new SqlConnection();
            this.cn.ConnectionString = ConfigurationManager.AppSettings["ConnectionString"];
        }

        public IEnumerable<Proposta> GetPropostas()
        {
            var query = @" select top 10 cod_corretor, cod_cotacao from tb_re_cotacao
                            where cod_corretor is not null and cod_corretor != 0 
                            order by dat_calculo desc";

            var propostas = cn.Query<Proposta>(query);


            return propostas;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(cn);
            cn.Dispose();
        }
    }
}
