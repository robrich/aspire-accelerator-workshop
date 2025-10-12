namespace AspireBrownfield.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<List<WeatherForecast>?> GetWeatherAsync(CancellationToken cancellationToken = default) =>
        await httpClient.GetFromJsonAsync<List<WeatherForecast>>("/api/weatherforecast", cancellationToken);
}

public record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
