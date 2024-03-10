using Blazored.Toast.Services;
using Microsoft.AspNetCore.Components;
using System.Net;
using Toolbelt.Blazor;

namespace IndustryFour.Client.HttpInterceptor
{
    public class HttpInterceptorService
    {
        private readonly HttpClientInterceptor _interceptor;
        private readonly NavigationManager _navigationManager;
        private readonly IToastService _toastService;

        public HttpInterceptorService(HttpClientInterceptor interceptor, NavigationManager navigationManager, IToastService toastService)
        {
            _interceptor = interceptor;
            _navigationManager = navigationManager;
            _toastService = toastService;
        }

        public void RegisterEvent() => _interceptor.AfterSend += HandleResponse;
        public void DisposeEvent() => _interceptor.AfterSend -= HandleResponse;

        private void HandleResponse(object sender, HttpClientInterceptorEventArgs e)
        {
            if (e.Response == null)
            {
                _navigationManager.NavigateTo("/internalerror");
                throw new HttpResponseException("Server not available");
            }

            var message = "";

            if (!e.Response.IsSuccessStatusCode)
            {
                switch (e.Response.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        _navigationManager.NavigateTo("/notfound");
                        message = "Resource not found";
                        break;

                    case HttpStatusCode.Unauthorized:
                        _navigationManager.NavigateTo("/unauthorized");
                        message = "Unauthorized access";
                        break;

                    case HttpStatusCode.BadRequest:
                        message = "Invalid request. Please try again.";
                        _toastService.ShowError(message);
                        break;

                    default:
                        _navigationManager.NavigateTo("internalerror");
                        message = "Unhandled error";
                        break;
                }

                throw new HttpResponseException(message);
            }
        }
    }
}
