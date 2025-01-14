using BradyCodeChallenge.Helper;
using BradyCodeChallenge.Models;
using System.Xml.Linq;

namespace BradyCodeChallenge
{
    public static class FileProcessor
    {
        public static void ProcessFile(string filePath, string outputFolderPath, string referenceDataPath)
        {
            try
            {
                Console.WriteLine($"Processing file: {filePath}");
                XDocument inputDoc = XDocument.Load(filePath);
                XDocument referenceDoc = XDocument.Load(referenceDataPath);

                ReferenceData referenceData = XmlHelper.DeserializeXml<ReferenceData>(referenceDataPath);
                GenerationReport generationReport = XmlHelper.DeserializeXml<GenerationReport>(filePath);

                var result = GeneratorCalculator.Calculate(generationReport, referenceData);
                string outputFilePath = Path.Combine(outputFolderPath, Path.GetFileNameWithoutExtension(filePath) + "-Result.xml");
                XmlHelper.SerializeToXmlAndSave(result, outputFilePath);

                Console.WriteLine($"File processed successfully. Output saved to: {outputFilePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing file: {ex.Message}");
            }
        }
    }
}
