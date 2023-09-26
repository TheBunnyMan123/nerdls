using System;
using System.IO;
using System.Globalization;

class Program {
   static void Main() {
        int width = Console.WindowWidth-5;
        string[] files = Directory.GetFileSystemEntries(AppDomain.CurrentDomain.BaseDirectory);
        string finalFiles = "";
        string tempFinalFiles = "";
        string[] tempFiles = files;

        for (int i = 0; i < (files.Length); i++) {
            tempFiles[i] = files[i].Split("/").Last();
        }
        Array.Sort<string>(tempFiles);

        for (int i = 0; i < (files.Length); i++) {
            if (File.Exists(files[i])) {
                tempFinalFiles += "\x1b[32;49;1m     " + tempFiles[i];
                if (new StringInfo(tempFinalFiles).LengthInTextElements >= width-10-tempFiles[i].Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }else if (i == files.Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }
            }else {
                tempFinalFiles += "\x1b[34;49;1m     " + tempFiles[i];
                if (new StringInfo(tempFinalFiles).LengthInTextElements >= width-10-tempFiles[i].Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }else if (i == files.Length) {
                    finalFiles += "\n" + tempFinalFiles;
                    tempFinalFiles = "";
                }
            }
        }
        Console.WriteLine(finalFiles);
   }
}