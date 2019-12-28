namespace ApiTpl.Core
{
    public class ApiSlimResponse
    {
        public ApiSlimResponse(int code, string msg)
        {
            Code = code;
            Msg = msg;
        }

        public int Code { get; private set; }

        public string Msg { get; private set; }

        public static ApiSlimResponse GetSucceed(string msg)
        {
            return new ApiSlimResponse(ApiReturnCode.Succeed, msg);
        }

        public static ApiSlimResponse GetSystemError(string msg)
        {
            return new ApiSlimResponse(ApiReturnCode.SystemError, msg);
        }

        public static ApiSlimResponse GetResult(int code, string msg)
        {
            return new ApiSlimResponse(code, msg);
        }
    }
}
