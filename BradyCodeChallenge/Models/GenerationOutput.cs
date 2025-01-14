using System.Xml.Serialization;

namespace BradyCodeChallenge.Models
{
    [XmlRoot("GenerationOutput")]
    public class GenerationOutput
    {
        [XmlElement("Totals")]
        public Totals Totals { get; set; }

        [XmlElement("MaxEmissionGenerators")]
        public MaxEmissionGenerators MaxEmissionGenerators { get; set; }

        [XmlElement("ActualHeatRates")]
        public ActualHeatRates ActualHeatRates { get; set; }
    }

    public class Totals
    {
        [XmlElement("Generator")]
        public List<GeneratorTotal> Generators { get; set; } = new List<GeneratorTotal>();
    }

    public class GeneratorTotal
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Total")]
        public decimal Total { get; set; }
    }

    public class MaxEmissionGenerators
    {
        [XmlElement("Day")]
        public List<EmissionDay> Days { get; set; } = new List<EmissionDay>();
    }

    public class EmissionDay
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("Date")]
        public DateTime Date { get; set; }

        [XmlElement("Emission")]
        public decimal Emission { get; set; }
    }

    public class ActualHeatRates
    {
        [XmlElement("ActualHeatRate")]
        public List<ActualHeatRate> HeatRates { get; set; } = new List<ActualHeatRate>();
    }

    public class ActualHeatRate
    {
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("HeatRate")]
        public decimal HeatRate { get; set; }
    }

}
