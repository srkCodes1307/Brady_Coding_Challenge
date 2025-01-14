using BradyCodeChallenge.Models;

namespace BradyCodeChallenge
{
    public static class GeneratorCalculator
    {

        public static GenerationOutput Calculate(GenerationReport report, ReferenceData referenceData)
        {
            var factors = referenceData.Factors;
            var output = new GenerationOutput
            {
                Totals = new Totals { Generators = new List<GeneratorTotal>() },
                MaxEmissionGenerators = new MaxEmissionGenerators { Days = new List<EmissionDay>() },
                ActualHeatRates = new ActualHeatRates { HeatRates = new List<ActualHeatRate>() }
            };

            // Process wind generators
            foreach (var windGen in report.Wind.WindGenerators)
            {
                var valueFactor = windGen.Name.Contains("Onshore") ? factors.ValueFactor.High : factors.ValueFactor.Low;
                var totalGeneration = windGen.Generation.Days.Sum(d => d.Energy * d.Price * valueFactor);
                output.Totals.Generators.Add(new GeneratorTotal { Name = windGen.Name, Total = (decimal)totalGeneration });
            }

            // Process gas generators
            foreach (var gasGen in report.Gas.GasGenerators)
            {
                var valueFactor = factors.ValueFactor.Medium;
                var emissionFactor = factors.EmissionsFactor.Medium;

                var totalGeneration = gasGen.Generation.Days.Sum(d => d.Energy * d.Price * valueFactor);
                output.Totals.Generators.Add(new GeneratorTotal { Name = gasGen.Name, Total = (decimal)totalGeneration });

                foreach (var day in gasGen.Generation.Days)
                {
                    var dailyEmission = day.Energy * gasGen.EmissionsRating * emissionFactor;
                    output.MaxEmissionGenerators.Days.Add(new EmissionDay { Name = gasGen.Name, Date = day.Date, Emission = (decimal)dailyEmission });
                }
            }

            // Process coal generators
            foreach (var coalGen in report.Coal.CoalGenerators)
            {
                var valueFactor = factors.ValueFactor.Medium;
                var emissionFactor = factors.EmissionsFactor.High;

                var totalGeneration = coalGen.Generation.Days.Sum(d => d.Energy * d.Price * valueFactor);
                output.Totals.Generators.Add(new GeneratorTotal { Name = coalGen.Name, Total = (decimal)totalGeneration });

                foreach (var day in coalGen.Generation.Days)
                {
                    var dailyEmission = day.Energy * coalGen.EmissionsRating * emissionFactor;
                    output.MaxEmissionGenerators.Days.Add(new EmissionDay { Name = coalGen.Name, Date = day.Date, Emission = (decimal)dailyEmission });
                }

                if (coalGen.ActualNetGeneration != 0)
                {
                    var heatRate = coalGen.TotalHeatInput / coalGen.ActualNetGeneration;
                    output.ActualHeatRates.HeatRates.Add(new ActualHeatRate { Name = coalGen.Name, HeatRate = (decimal)heatRate });
                }
            }

            // Find the highest emission generator for each day
            output.MaxEmissionGenerators.Days = output.MaxEmissionGenerators.Days
                .GroupBy(d => d.Date)
                .Select(g => g.OrderByDescending(d => d.Emission).First())
                .ToList();

            return output;
        }
    }

}
