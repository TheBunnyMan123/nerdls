using System;
using System.IO;

class Program {
   static void Main() {
        int width = Console.WindowWidth;
        int wrapTimes = 1;
        string[] files = Directory.GetFileSystemEntries(AppDomain.CurrentDomain.BaseDirectory);
        string finalFiles = "";
        for (int i = 0; i < (files.Length); i++) {
            if (File.Exists(files[i])) {
                var tempFinalFiles = finalFiles +"     " + files[i].Split("/").Last();
                if (tempFinalFiles.Length >= (width * wrapTimes)) {
                    finalFiles += "\n     " + files[i].Split("/").Last();
                    wrapTimes += 1;
                }else {
                    finalFiles += "     " + files[i].Split("/").Last();
                }
            }else {
                var tempFinalFiles = finalFiles + "     " + files[i].Split("/").Last();
                if (tempFinalFiles.Length >= (width * wrapTimes)) {
                    finalFiles += "\n     " + files[i].Split("/").Last();
                    wrapTimes += 1;
                }else {
                    finalFiles += "     " + files[i].Split("/").Last();
                }
            }
        }
        Console.WriteLine(finalFiles);
   }
}