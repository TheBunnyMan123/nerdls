using System;
using System.IO;
using System.Globalization;
using Myrmec;

class Program {
   static void Main() {
        int width = Console.WindowWidth-5;
        string[] files = Directory.GetFileSystemEntries(Environment.CurrentDirectory);
        string finalFiles = "";
        string tempFinalFiles = "";
        string[] tempFiles = files;

        for (int i = 0; i < (files.Length); i++) {
            tempFiles[i] = files[i].Split("/").Last();
        }
        Array.Sort<string>(tempFiles);

        for (int i = 0; i < (tempFiles.Length); i++) {
            if (File.Exists(tempFiles[i])) {
                // // create a sniffer instance.
                // Sniffer sniffer = new Sniffer();

                // // populate with mata data.
                // // FileTypes.Common contains file types that we usually see.
                // sniffer.Populate(FileTypes.Common);

                // // get file head byte, may be 20 bytes enough.
                // byte[] fileHead = ReadFileHead();

                // // start match.
                // List<string> results = sniffer.Match(fileHead);
                // foreach (string type in results) {
                //     Console.WriteLine(tempFiles[i] + ": " + type);
                // };
                // Console.WriteLine(tempFiles[i]);
                // Console.WriteLine(Executable(tempFiles[i]));
                if (Executable(tempFiles[i])) {
                    tempFinalFiles += "\x1b[37;49;1m      " + tempFiles[i];
                }else {
                    tempFinalFiles += "\x1b[32;49;1m      " + tempFiles[i];
                }
                if (new StringInfo(tempFinalFiles).LengthInTextElements >= width-10-tempFiles[i].Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }else if (i == tempFiles.Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }
            }else {
                tempFinalFiles += "\x1b[34;49;1m      " + tempFiles[i];
                if (new StringInfo(tempFinalFiles).LengthInTextElements >= width-10-tempFiles[i].Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }else if (i == tempFiles.Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }
            }
        }
        Console.WriteLine(finalFiles);
   }

    static bool Executable(string filePath)
    {
        var firstBytes = new byte[13];
        using(var fileStream = File.Open(filePath, FileMode.Open))
        {
            fileStream.Read(firstBytes, 0, 12);
        }
        // Console.WriteLine(System.Text.Encoding.UTF8.GetString(firstBytes));
        var Bytes = System.Text.Encoding.UTF8.GetString(firstBytes);
        return (Bytes.Contains("ELF") || Bytes.Contains("MZ") || Bytes.Contains("FEEDFACE") || Bytes.Contains("FEEDFACF") || Bytes.Contains("sh"));
    }
}