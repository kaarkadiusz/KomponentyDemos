using Microsoft.JSInterop;
using Piaskownica.Metadata;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Piaskownica.Services
{
    public class LocalStorageService
    {
        private IJSRuntime JSRuntime { get; }

        public LocalStorageService(IJSRuntime jSRuntime)
        {
            JSRuntime = jSRuntime;
        }

        public async Task<bool> SetValueAsync<T>(string key, T value) where T : class
        {
            try
            {
               await JSRuntime.InvokeVoidAsync(JavascriptFunctionName.SetLocalStorage, key, System.Text.Json.JsonSerializer.Serialize(value));
            }
            catch
            {
                return false;
            }

            return true;
        }
        public async Task<T?> GetValueAsync<T>(string key) where T : class
        {
            try
            {
                string value = await JSRuntime.InvokeAsync<string>(JavascriptFunctionName.GetLocalStorage, key);
                return System.Text.Json.JsonSerializer.Deserialize<T?>(value);
            }
            catch
            {
                return default; 
            }
        }
    }
}
