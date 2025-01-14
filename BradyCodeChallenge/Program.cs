using BradyCodeChallenge;
using System.Configuration;

class Program
{
    static void Main(string[] args)
    {
        string inputFolderPath = ConfigurationManager.AppSettings["InputFolderPath"];
        string outputFolderPath = ConfigurationManager.AppSettings["OutputFolderPath"];
        string referenceDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ReferenceData.xml");
        FileSystemWatcher watcher = new FileSystemWatcher(inputFolderPath, "*.xml")
        {
            EnableRaisingEvents = true
        };

        watcher.Created += (sender, e) => FileProcessor.ProcessFile(e.FullPath, outputFolderPath, referenceDataPath);

        Console.WriteLine("Monitoring input folder for new XML files. Press Enter to exit.");
        Console.ReadLine();
    }
}