using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using WeatherForecast.Models;

namespace WeatherForecast;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
        if (string.IsNullOrEmpty(cityTb.Text))
        {
            MessageBox.Show("Please enter a city");
            return;
        }

        string value = unitsCb.Text;
        string units = value == "Celsius" ? "metric" : "standart";
        string query = new ForecastQueryBuilder()
            .AddCity(cityTb.Text)
            .AddUnits(units)
            .Build();
        
        HttpClient client = new HttpClient();
        var response = await client.GetAsync(query);
        var stream = await response.Content.ReadAsStreamAsync();
        var objResponse = JsonSerializer.Deserialize(stream,
            typeof(ForecastResponse), new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            });

        if (objResponse is ForecastResponse forecastResponse)
        {
            //MessageBox.Show(forecastResponse.List.First().Main.Temp.ToString());
            var dtos = new List<ForecastDTO>();
            foreach (var forecast in forecastResponse.List)
            {
                var forecastDTO = new ForecastDTO()
                {
                    Icon = $"https://openweathermap.org/img/wn/{forecast.Weather[0].Icon}@2x.png",
                    Date = forecast.dt_txt,
                    Temp = forecast.Main.Temp,
                    FeelsLike = forecast.Main.FeelsLike,
                    WeatherMain = forecast.Weather[0].Main,
                    WeatherDescription = forecast.Weather[0].Description,
                    WindSpeed = forecast.Wind.Speed,
                    WindDeg = forecast.Wind.Deg,
                    WindGust = forecast.Wind.Gust
                };
                Console.WriteLine($"Temp: {forecast.Main.Temp}, FeelsLike: {forecast.Main.FeelsLike}");
                dtos.Add(forecastDTO);
            }
            listBox.ItemsSource = dtos;
        }
    }
}