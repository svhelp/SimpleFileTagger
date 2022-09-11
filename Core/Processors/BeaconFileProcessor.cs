using DAL.Entities;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Core.Processors
{
    internal class BeaconFileProcessor
    {
        private const string DefaultFileNameV2 = ".sft2";

        public BeaconFileProcessor(string path)
        {
            BeaconPath = GetDataFilePath(path);
        }

        private string BeaconPath { get; }

        public void CreateBeacon(LocationEntity location)
        {
            var fileExists = File.Exists(BeaconPath);

            using var stream = new FileStream(BeaconPath, FileMode.OpenOrCreate, FileAccess.Write);
            stream.SetLength(0);

            using var streamWriter = new StreamWriter(stream);
            streamWriter.WriteLine(location.Id);

            if (!fileExists)
            {
                File.SetAttributes(BeaconPath, FileAttributes.Hidden);
            }
        }

        public void RemoveBeacon()
        {
            if (!File.Exists(BeaconPath))
            {
                return;
            }

            File.Delete(BeaconPath);
        }

        public Guid? GetExistingBeacon()
        {
            if (!File.Exists(BeaconPath))
            {
                return null;
            }

            using var stream = new FileStream(BeaconPath, FileMode.Open, FileAccess.Read);
            using var streamReader = new StreamReader(stream);
            var fileData = streamReader.ReadToEnd();

            if (string.IsNullOrEmpty(fileData))
            {
                return null;
            }

            return new Guid(fileData);
        }

        private static string GetDataFilePath(string directoryPath)
        {
            return Path.Combine(directoryPath, DefaultFileNameV2);
        }
    }
}
