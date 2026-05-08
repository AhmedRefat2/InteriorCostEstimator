using InteriorCostEstimator.Application.Features.ProjectFeature.DTOs;
using InteriorCostEstimator.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;


namespace InteriorCostEstimator.Application.Features.ProjectFeature.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;

        public AiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<AiResponseDto?> AnalyzeRoomAsync(
            IFormFile image)
        {
            using var content = new MultipartFormDataContent();

            using var stream = image.OpenReadStream();

            var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(image.ContentType);

            content.Add(
                fileContent,
                "file",
                image.FileName
            );

            var response = await _httpClient.PostAsync(
                $"/analyze-room",
                content
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception("AI service failed");

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<AiResponseDto>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }


        public async Task<AiAddProductResponseDto?> AddProductToAiAsync(
             IFormFile image,
             int AI_Id)
        {
            using var content = new MultipartFormDataContent();

            using var stream = image.OpenReadStream();

            var fileContent = new StreamContent(stream);

            fileContent.Headers.ContentType =
                new MediaTypeHeaderValue(image.ContentType);

            content.Add(
                fileContent,
                "file",
                image.FileName
            );

            content.Add(
                new StringContent(AI_Id.ToString()),
                "product_id"
            );

            var response = await _httpClient.PostAsync(
                "/add-product",
                content
            );

            if (!response.IsSuccessStatusCode)
                throw new Exception("AI Add Product failed");

            var json = await response.Content.ReadAsStringAsync();

            return JsonSerializer.Deserialize<AiAddProductResponseDto>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
        }
    }
}
