using System;
using EcommerceAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceAPI.Services
{
	public static class ResponseApi
	{
		//this function return success api response
		public static object Success(string message, object data)
		{
			var response = new ResponseModel
			{
				ResponseCode = 100,
				ResponseStatus = "Success",
				ResponseMessage = message,
				Data = data
			};

			return response;
		}

		// this functioin return faile Api response
        public static object Faild(int code, string message, object data)
        {
            var response = new ResponseModel
            {
                ResponseCode = code,
                ResponseStatus = "Faile",
                ResponseMessage = message,
                Errors = data
            };

            return response;
        }
    }
}

