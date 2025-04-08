using System;
using System.IO;

namespace FileManagement {
  class Program {
    static void Main(string[] args) {
      try {
        Console.WriteLine("Source File (input.txt) Content:");
        DisplayFileContent("input.txt");

        string[] lines = ReadFile("input.txt");
        int noOfLines = lines.Length;
        int noOfWords = CountWords(lines);

        WriteFile("output.txt", noOfLines, noOfWords);
        Console.WriteLine("\nFile processed successfully!");

        Console.WriteLine("\nProcessed File (output.txt) Content:");
        DisplayFileContent("output.txt");
      }
      catch (FileNotFoundException e) {
        Console.WriteLine("File Not Found: " + e.Message);
      }
      catch (IOException e) {
        Console.WriteLine("IO Error: " + e.Message);
      }
      catch (Exception e) {
        Console.WriteLine("An error occurred: " + e.Message);
      }
    }

    static string[] ReadFile(string filePath) {
      return File.ReadAllText(filePath).Split('\n');
    }

    static int CountWords(string[] lines) {
      int count = 0;
      foreach (var line in lines) {
        count += line.Trim().Split(' ').Length;
      }
      return count;
    }

    static void WriteFile(string outputPath, int lineCount, int wordCount) {
      if (!File.Exists(outputPath)) {
        File.Create(outputPath).Close();
        Console.WriteLine("Output file created!");
      }
      File.WriteAllText(outputPath, $"No of lines = {lineCount}\nNo of words = {wordCount}");
    }

    static void DisplayFileContent(string filePath) {
      string content = File.ReadAllText(filePath);
      Console.WriteLine(content);
    }
  }
}
