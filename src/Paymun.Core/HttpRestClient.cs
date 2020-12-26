using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Paymun.Core {

    public class HttpRestClient {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly HttpClient _httpClient;

        public HttpRestClient(IHttpClientFactory httpClientFactory) {
            _httpClientFactory = httpClientFactory 
                ?? throw new ArgumentNullException(nameof(httpClientFactory));
            _httpClient = _httpClientFactory.CreateClient();
        }

        public async Task<TResult> PostAsync<T, TResult>(
            T data,
            string url,
            Dictionary<string, string> headers = null) {

            foreach (var item in headers ?? new Dictionary<string, string>())
                _httpClient.DefaultRequestHeaders.TryAddWithoutValidation(item.Key, item.Value);

            var result = await _httpClient.PostAsync(
                url,
                new StringContent(
                    JsonSerializer.Serialize(data),
                    Encoding.UTF8,
                    "application/json")
            );

            if (!result.IsSuccessStatusCode) {
                throw new HttpRequestException($"{result.StatusCode} {result.ReasonPhrase}");
            }

            return await JsonSerializer.DeserializeAsync<TResult>(
                await result.Content.ReadAsStreamAsync());
        }
    }
}
