using Newtonsoft.Json;
using NLayerProject.Web.DTOs;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NLayerProject.Web.ApiServices
{
    public class CategoryApiService
    {
        private readonly HttpClient _httpClient;

        public CategoryApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;

        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            IEnumerable<CategoryDto> categoryDtos;
            var response = await _httpClient.GetAsync("categories");

            if (response.IsSuccessStatusCode)
            {
                categoryDtos = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                categoryDtos = null;
            }

            return categoryDtos;
        }

        public async Task<CategoryDto> AddAsync(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync("categories", stringContent);

            if (response.IsSuccessStatusCode)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());

                return categoryDto;
            }
            else
            {
                //loglama yapmalı
                return null;
            }

        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var response = await _httpClient.GetAsync($"categories/{id}");

            CategoryDto categoryDto;
            if (response.IsSuccessStatusCode)
            {
                categoryDto = JsonConvert.DeserializeObject<CategoryDto>(await response.Content.ReadAsStringAsync());
            }
            else
            {
                categoryDto = null;
            }

            return categoryDto;

        }

        public async Task<bool> UpdateAsync(CategoryDto categoryDto)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(categoryDto), Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync("categories", stringContent);

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> RemoveAsync(int id)
        {
            var response = await _httpClient.DeleteAsync($"categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                return true;
            }
            else
            {
                return false;
            }
        }





        public async Task<IEnumerable<string>> GetAllOrigin()
        {
            var originDto = new OriginParameterDto()
            {
                UserName = "testUserName",
                Password = "testPassword"
            };
            var stringContent = new StringContent(JsonConvert.SerializeObject(originDto), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("userToken", "tokenKey");
            var response = await _httpClient.PostAsync("https://test.com/api/origin", stringContent);

            List<string> result;
            if (response.IsSuccessStatusCode)
            {
                result = JsonConvert.DeserializeObject<Origin>(await response.Content.ReadAsStringAsync()).Data;
            }

            else
            {
                result = null;
            }

            return result;
        }

        public class Origin
        {
            public List<string> Data { get; set; }
        }

        public class OriginParameterDto
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }

    }
}
