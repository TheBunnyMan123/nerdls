using System;
using System.IO;
using System.Globalization;

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
            #pragma warning disable CS0168 // Disable unused variable warning
            try {
                if (File.Exists(tempFiles[i])) { //Vid: 󰕧
                    if (Executable(tempFiles[i])) {
                        tempFinalFiles += "\x1b[37;49;1m      " + tempFiles[i];
                    }else {
                        switch(tempFiles[i].Split(".").Last().ToLower()) {
                            case "png" or "tif" or "tiff" or "bmp" or "jpg" or "jpeg" or "gif" or "eps":
                                tempFinalFiles += "\x1b[35;49;1m      " + tempFiles[i];
                                break;
                            case "swf" or "mp4" or "mov" or "avi" or "wmv" or "avchd" or "webm" or "flv":
                                tempFinalFiles += "\x1b[35;49;1m      " + tempFiles[i];
                                break;
                            case "pcm" or "wav" or "aiff" or "mp3" or "aac" or "ogg" or "wma" or "flac" or "alac" or "midi":
                                tempFinalFiles += "\x1b[35;49;1m      " + tempFiles[i];
                                break;
                            case "zip" or "rar" or "gz" or "7z" or "cab" or "xz" or "tar" or "kgb" or "pea" or "bz2" or "lz" or "lzma" or "lzo" or "zstd" or "tb2" or "tbz" or "tbz2" or "tz2" or "taz" or "tgz" or "tlz" or "txz" or "tz" or "taz" or "tzst":
                                tempFinalFiles += "\x1b[36;49;1m    󰞹  " + tempFiles[i];
                                break;
                            case "iso" or "dmg" or "img":
                                tempFinalFiles += "\x1b[36;49;1m    󰗮  " + tempFiles[i];
                                break;
                            default:
                                tempFinalFiles += "\x1b[32;49;1m      " + tempFiles[i];
                                break;
                        }
                    }
                    if (new StringInfo(tempFinalFiles).LengthInTextElements >= width-10-tempFiles[i].Length) {
                        finalFiles += "\n" + tempFinalFiles;
                        tempFinalFiles = "";
                    }else if (i == (tempFiles.Length-1)) {
                        finalFiles += "\n" + tempFinalFiles;
                        tempFinalFiles = "";
                    }
                }else {
                    tempFinalFiles += "\x1b[34;49;1m      " + tempFiles[i];
                    if (new StringInfo(tempFinalFiles).LengthInTextElements >= width-10-tempFiles[i].Length) {
                        finalFiles += "\n" + tempFinalFiles;
                        tempFinalFiles = "";
                    }else if (i == (tempFiles.Length-1)) {
                        finalFiles += "\n" + tempFinalFiles;
                        tempFinalFiles = "";
                    }
                }
            }catch(System.IO.FileNotFoundException e) {
                // Console.WriteLine(tempFiles[i] + " not found (most likely a broken link)");
            }
        }
        Console.WriteLine(finalFiles);
   }

    static bool Executable(string filePath) {
        try {
            var firstBytes = new byte[13];
            using(var fileStream = File.Open(filePath, FileMode.Open))
            {
                fileStream.Read(firstBytes, 0, 12);
            }
            // Console.WriteLine(System.Text.Encoding.UTF8.GetString(firstBytes));
            var Bytes = System.Text.Encoding.UTF8.GetString(firstBytes);
            return (Bytes.Contains("ELF") || Bytes.Contains("MZ") || Bytes.Contains("FEEDFACE") || Bytes.Contains("FEEDFACF") || Bytes.Contains("sh"));
        } catch(System.UnauthorizedAccessException e) {
            #pragma warning disable CS0168 // Disable warning "The variable 'e' is declared but never used"
            return false;
        }
    }
}