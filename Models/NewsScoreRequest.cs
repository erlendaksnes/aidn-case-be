namespace aidn_case_be.Models
{
    public enum MeasurementType
    {
        TEMP,
        HR,
        RR
    }

    public class Measurement
    {
        public MeasurementType Type { get; set; }
        public int Value { get; set; }
    }

    public class NewsScoreRequest
    {
        public List<Measurement> Measurements { get; set; } = new();
    }
}