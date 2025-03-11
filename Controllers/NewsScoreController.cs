namespace aidn_case_be.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsScoreController : ControllerBase
    {
        [HttpPost]
        public IActionResult CalculateNewsScore([FromBody] NewsScoreRequest request)
        {
            if (request == null || request.Measurements == null || request.Measurements.Count != 3)
            {
                return BadRequest("Invalid request: Exactly three measurements (TEMP, HR, RR) must be provided.");
            }

            int totalScore = 0;

            foreach (var measurement in request.Measurements)
            {
                int score;
                switch (measurement.Type)
                {
                    case MeasurementType.TEMP:
                        score = CalculateTempScore(measurement.Value);
                        break;
                    case MeasurementType.HR:
                        score = CalculateHRScore(measurement.Value);
                        break;
                    case MeasurementType.RR:
                        score = CalculateRRScore(measurement.Value);
                        break;
                    default:
                        return BadRequest($"Invalid measurement type: {measurement.Type}");
                }

                if (score == -1)
                {
                    return BadRequest($"Invalid value for {measurement.Type}: {measurement.Value}");
                }

                totalScore += score;
            }

            return Ok(new { score = totalScore });
        }

        private int CalculateTempScore(int temp)
        {
            if (temp > 31 && temp <= 35) return 3;
            if (temp > 35 && temp <= 36) return 1;
            if (temp > 36 && temp <= 38) return 0;
            if (temp > 38 && temp <= 39) return 1;
            if (temp > 39 && temp <= 42) return 2;
            return -1;
        }

        private int CalculateHRScore(int hr)
        {
            if (hr > 25 && hr <= 40) return 3;
            if (hr > 40 && hr <= 50) return 1;
            if (hr > 50 && hr <= 90) return 0;
            if (hr > 90 && hr <= 110) return 1;
            if (hr > 110 && hr <= 130) return 2;
            if (hr > 130 && hr <= 220) return 3;
            return -1;
        }

        private int CalculateRRScore(int rr)
        {
            if (rr > 3 && rr <= 8) return 3;
            if (rr > 8 && rr <= 11) return 1;
            if (rr > 11 && rr <= 20) return 0;
            if (rr > 20 && rr <= 24) return 2;
            if (rr > 24 && rr <= 60) return 3;
            return -1;
        }
    }
}