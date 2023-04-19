using Newtonsoft.Json;
using RestSharp;
using ValidationDemoApi.Entities;


namespace ValidationDemoApi.Helper
{
    public class CurrencyConvert
    {
        public string CurrencyConversion(CurrencyModel currencyModel)
        {
            double amount = currencyModel.Amount;
            string from = currencyModel.From;
            string to = currencyModel.To;

            var Url = "https://api.apilayer.com/fixer/convert?";
            var client = new RestClient(Url + "to="+to.ToUpper()+"&from="+from.ToUpper()+"&amount="+amount);

            var request = new RestRequest(Method.GET);
            request.AddHeader("apikey", "ZlUQAALrPOecw9xZa8ZDbqQSpfyE9jKd");

            IRestResponse response = client.Execute(request);
            Console.WriteLine(response.Content);
            CurrencyResModel model = new CurrencyResModel();
            string myDeserializedClass = JsonConvert.SerializeObject(response.Content).ToString() ?? response.Content;
            return response.Content;
        }
    }
}
