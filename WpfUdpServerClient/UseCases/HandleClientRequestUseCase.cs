﻿using System.Net;
using WpfUdpServerClient.Entities;

namespace WpfUdpServerClient.UseCases
{
    public class HandleClientRequestUseCase
    {
        private readonly IRequestLimiter _requestLimiter;
        private readonly IComponentPriceService _componentPriceService;

        public HandleClientRequestUseCase(IRequestLimiter requestLimiter, IComponentPriceService componentPriceService)
        {
            _requestLimiter = requestLimiter;
            _componentPriceService = componentPriceService;
        }

        public string HandleRequest(IPEndPoint remoteEP, string componentName, ClientRequestInfo clientInfo)
        {
            if (_requestLimiter.IsRequestAllowed(clientInfo))
            {
                string response = _componentPriceService.GetComponentPrice(componentName);
                clientInfo.AddRequest(DateTime.Now); 
                return response;
            }
            else
            {
                return "Rate limit exceeded. Try again later.";
            }
        }
    }
}
