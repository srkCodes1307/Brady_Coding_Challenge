using BradyCodeChallenge;

class Program
{
    static void Main(string[] args)
    {
        string inputFolderPath = "C:\\Users\\SADIRAJU\\Downloads\\Brady Code Challenge - ETRM - June 2021\\Brady Code Challenge_June 2021";
        string outputFolderPath = "C:\\Users\\SADIRAJU\\Downloads\\Brady Code Challenge - ETRM - June 2021\\Brady Code Challenge_June 2021";
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