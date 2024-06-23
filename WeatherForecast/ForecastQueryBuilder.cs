using System.Text;

namespace WeatherForecast;

public class ForecastQueryBuilder
{
    private const string _baseApi = "https://api.openweathermap.org/data/2.5/forecast";
    private const string _key = "74d4efab1b9f7bb4af01fca75ba183f8";
    private Dictionary<string, string> _queryParameters = new Dictionary<string, string>();

    public ForecastQueryBuilder AddCity(string city)
    {
        _queryParameters.Add("q", city);
        return this;
    }

    public ForecastQueryBuilder AddUnits(string units)
    {
        _queryParameters.Add("units", units);
        return this;
    }

    public string Build()
    {
        var queryBuilder = new StringBuilder(_baseApi);
        queryBuilder.Append($"?appid={_key}");
        foreach (var item in _queryParameters)
        {
            queryBuilder.Append($"&{item.Key}={item.Value}");
        }

        return queryBuilder.ToString();
    }
}
