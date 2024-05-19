using System;
namespace EcommerceAPI.Models
{
	public class ResponseModel
    {
		public int ResponseCode { get; set; }
		public string ResponseStatus { get; set; }
		public string ResponseMessage { get; set; }
		public object? Data { get; set; }
		public object? Errors { get; set; }
	}
}

