namespace ApiTpl.Core.ApiClients
{
    using System;
    using WebApiClient;
    using WebApiClient.Attributes;

    public interface IDemoApi : IHttpApi
    {
        [HttpPost("/api/demo")]
        ITask<string> AddDemoAsync([JsonContent]AddDemoApiReq dto, [Timeout]TimeSpan timeout);

        [HttpGet("/api/demo/{id}")]
        ITask<GetDemoByIdResp> GetDemoByIdAsync(int id, [Timeout]TimeSpan timeout);
    }
}
