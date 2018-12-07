using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EscritorArquivo
{
    public class Folder
    {
        public string RootPath { get; set; }
        public Status StatusFolder { get; set; }
        public string FolderPath { get; set; }

        public Folder(Status status, string rootPath)
        {
            FolderPath = status.Equals(Status.Success)
                        ? Directory.Exists(Path.Combine(rootPath, @"Files\"))
                            ? Path.Combine(rootPath, @"Files\")
                            : Directory.CreateDirectory(Path.Combine(rootPath, @"Files\")).FullName
                        : status.Equals(Status.Error)
                            ? Directory.Exists(Path.Combine(rootPath, @"Log\"))
                            ? Path.Combine(rootPath, @"Log\")
                            : Directory.CreateDirectory(Path.Combine(rootPath, @"Log\")).FullName : "";
        }
        protected Folder() { }
    }
}
