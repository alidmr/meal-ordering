using System.Net.Http.Json;
using MealOrdering.Shared.CustomExceptions;
using MealOrdering.Shared.ResponseModels;

namespace MealOrdering.Client.Utilities
{
    public static class HttpClientExtension
    {
        public static async Task<T> GetServiceResponseAsync<T>(this HttpClient client, String url, bool throwSuccessException = false)
        {
            var httpRes = await client.GetFromJsonAsync<ServiceResponse<T>>(url);

            return !httpRes.Success && throwSuccessException ? throw new ApiException(httpRes.Message) : httpRes.Value;
        }


        public static async Task<TResult> PostGetServiceResponseAsync<TResult, TValue>(this HttpClient client, String url, TValue value, bool throwSuccessException = false)
        {
            var httpRes = await client.PostAsJsonAsync(url, value);

            if (httpRes.IsSuccessStatusCode)
            {
                var res = await httpRes.Content.ReadFromJsonAsync<ServiceResponse<TResult>>();

                return !res.Success && throwSuccessException ? throw new ApiException(res.Message) : res.Value;
            }

            throw new HttpException(httpRes.StatusCode.ToString());
        }

        public static async Task<BaseResponse> PostGetBaseResponseAsync<TValue>(this HttpClient client, String url, TValue value, bool throwSuccessException = false)
        {
            var httpRes = await client.PostAsJsonAsync(url, value);

            if (httpRes.IsSuccessStatusCode)
            {
                var res = await httpRes.Content.ReadFromJsonAsync<BaseResponse>();

                return !res.Success && throwSuccessException ? throw new ApiException(res.Message) : res;
            }

            throw new HttpException(httpRes.StatusCode.ToString());
        }




    }
}
