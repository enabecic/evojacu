﻿using Microsoft.AspNetCore.Mvc;

namespace evojacu.Helpers
{
    [ApiController]
    public abstract class MyBaseEndpoint<TRequest, TResponse> : ControllerBase
    {
        public abstract Task<TResponse> Obradi(TRequest request, CancellationToken cancellationToken = default);

    }
}