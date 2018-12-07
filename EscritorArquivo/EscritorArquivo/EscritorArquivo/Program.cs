using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace EscritorArquivo
{
    class Program
    {
        static void Main(string[] args)
        {
            bool executed = false;
            TimeSpan timeScheduled = new TimeSpan(11, 39, 0);
            DateTime dateStarted = DateTime.Now.Date;
            double timeToRun = 0;

            Task integrationTask = new Task(() => ExecuteIntegration());

            while ((!executed) || dateStarted < DateTime.Now.Date)
            {
                string timeNow = DateTime.Now.ToString("HH:mm", CultureInfo.InvariantCulture);
                string stringTimeScheduled = timeScheduled.ToString(@"hh\:mm");
                timeToRun = (timeScheduled.TotalMilliseconds - DateTime.Now.TimeOfDay.TotalMilliseconds);

                if (timeNow == stringTimeScheduled || ((!executed) && timeToRun <= 0))
                {
                    if (integrationTask.Status != TaskStatus.Running)
                    {
                        integrationTask.Start();
                        timeToRun = timeToRun < 0 ? timeToRun + new TimeSpan(24, 0, 0).TotalMilliseconds : timeToRun;
                    }

                    while (!integrationTask.IsCompleted)
                    {
                        executed = false;
                    }
                    executed = true;
                }
                else
                {
                    Console.WriteLine($"Periodo até a próxima execução {ConvertToHours(timeToRun)}");
                    Thread.Sleep(Convert.ToInt32(timeToRun));
                }
            }
            Console.WriteLine($"Período até a próxima execução {ConvertToHours(timeToRun)}");
            Thread.Sleep(Convert.ToInt32(timeToRun));
            Program.Main(args);
        }

        private static string ConvertToHours(double timeToRun)
        {
            var seconds = timeToRun / 1000;
            var minutes = seconds / 60;
            var stringHoursMinutes = String.Format("{0:n2}", (minutes / 60)).Split('.');
            var hourMinutes = (Convert.ToInt32(stringHoursMinutes[1]) * 60) / 100;
            var hours = Math.Truncate((minutes / 60));

            return hours == 0 ? $"{hourMinutes}min" : $"{hours}:{hourMinutes}h";
        }

        public static void ExecuteIntegration()
        {
            const string RootPath = @"C:\Desenvolvimento\EscritorArquivo\EscritorArquivo\Integration\";
            string text = "";

            try
            {
                Repo repo = new Repo();
                var propostas = repo.GetPropostas();
                Folder folder = new Folder(Status.Success, RootPath);
                var file = new File(Status.Success, folder.FolderPath);
                text = Proposta.CreatePropostasText(propostas);
                file.WriteFile(text);
            }
            catch (Exception ex)
            {
                Folder logFolder = new Folder(Status.Error, RootPath);
                var errorFile = new File(Status.Error, logFolder.FolderPath);
                text = errorFile.CreateErrorText(ex);
                errorFile.WriteFile(text);
            }
            Proposta.ShowPropostas(text);
        }
    }
}


