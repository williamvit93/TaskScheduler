using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscritorArquivo
{
    public class File
    {
        public Status FileStatus { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
        public string CreationDate { get; set; }
        public StreamWriter Writer { get; set; }

        public File(Status status, string path)
        {
            FileStatus = status;
            CreationDate = $"{DateTime.Now.ToString("MM.dd.yyyy HH.mm.ss", CultureInfo.InvariantCulture)}";
            FileName = FileStatus.Equals(Status.Success)
                ? $"File {CreationDate}.txt"
                : FileStatus.Equals(Status.Error)
                    ? $"Log {CreationDate}.txt"
                    : "";
            Path = path;
            Writer = new StreamWriter(Path + FileName);
        }
        protected File() { }

        public string CreateErrorText(Exception ex)
        {
            StringBuilder text = new StringBuilder();
            text.AppendLine("Erro de processamento:");
            text.AppendLine($"Detalhes: {ex.Message}");

            return text.ToString();
        }

        public void WriteFile(string text)
        {
            Writer.Write(text);
            Writer.Close();
        }
    }
}
