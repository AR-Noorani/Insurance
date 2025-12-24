using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace App.RestApi.ApiResponse
{
    /// <summary>
    /// An extension on ActionResult
    /// </summary>
    public static partial class ApiActionResult
    {
        /// <summary>
        /// It creates an action result from the response and provide a http status code for the final action result
        /// </summary>
        /// <param name="obj">Object to be sent as final result</param>
        /// <param name="statusCode">Response http status code</param>
        /// <returns></returns>
        public static IActionResult ToActionResult(this object obj, HttpStatusCode statusCode = HttpStatusCode.OK)
        {
            var apiResult = ApiResult.From(obj);
            var result = new ObjectResult(apiResult)
            {
                StatusCode = (int)statusCode
            };

            return result;
        }

        /// <summary>
        /// It creates an action result from the response and provide a http status code for the final action result
        /// </summary>
        /// <param name="obj">Object to be sent as final result</param>
        /// <param name="locationForCreatedResult">Location hyper media after creating a resource. 
        /// Resource must be available through this address after creation</param>
        /// <returns></returns>
        public static IActionResult ToActionResult(this object obj, string locationForCreatedResult)
        {
            var apiResult = ApiResult.From(obj);
            var result = new CreatedResult(locationForCreatedResult, apiResult);

            return result;
        }
    }
}
