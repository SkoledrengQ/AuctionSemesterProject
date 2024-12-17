using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using API.Dtos;
using WebApp.Models;

namespace WebApp.ServiceLayer
{
	public class BidService : IBidService
	{
		private readonly HttpClient _httpClient;

		public BidService()
		{
			_httpClient = new HttpClient();
		}

		public async Task<BidResult> PlaceBidAsync(BidDto bidDto)
		{
			var content = new StringContent(
				JsonSerializer.Serialize(bidDto),
				Encoding.UTF8,
				"application/json");

			var response = await _httpClient.PostAsync("https://localhost:7005/api/Bid", content);

			if (response.IsSuccessStatusCode)
			{
				return BidResult.Success();
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
			{
				var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
				return BidResult.Failure(error?.Message ?? "Bid rejected due to a concurrency conflict.");
			}
			else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				var error = await response.Content.ReadFromJsonAsync<ErrorResponse>();
				return BidResult.Failure(error?.Message ?? "Bid validation failed.");
			}

			return BidResult.Failure("An unexpected error occurred. Please try again.");
		}
	}

	public class ErrorResponse
	{
		public string Message { get; set; }
	}
}
