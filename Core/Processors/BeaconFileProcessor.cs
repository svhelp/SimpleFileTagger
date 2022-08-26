using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Processors
{
    internal class BeaconFileProcessor : IDisposable
    {
        private const string DefaultFileNameV2 = ".sft2";

        public BeaconFileProcessor(string path)
        {
            Stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        }

        private FileStream Stream { get; }

        public static string GetDataFilePath(string directoryPath)
        {
            return Path.Combine(directoryPath, DefaultFileNameV2);
        }

        public void CreateBeacon(LocationEntity location)
        {
            Stream.SetLength(0);
            using var streamWriter = new StreamWriter(Stream);
            streamWriter.WriteLine(location.Id);
        }

        public Guid? GetExistingBeacon()
        {
            using var streamReader = new StreamReader(Stream);
            var fileData = streamReader.ReadToEnd();

            if (string.IsNullOrEmpty(fileData))
            {
                return null;
            }

            return new Guid(fileData);
        }

        public void Dispose()
        {
            Stream.Dispose();
        }
    }
}
