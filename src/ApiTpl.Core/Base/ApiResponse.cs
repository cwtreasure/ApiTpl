namespace ApiTpl.Core
{
    public class ApiResponse<T>
    {
        public int Code { get; set; }

        public string Msg { get; set; }

        public T Data { get; set; }

        public static ApiResponse<T> GetSucceed(T data, string msg)
        {
            return new ApiResponse<T>
            {
                Code = ApiReturnCode.Succeed,
                Msg = msg,
                Data = data
            };
        }

        public static ApiResponse<T> GetFail(int code, string msg)
        {
            return new ApiResponse<T>
            {
                Code = code,
                Msg = msg,
                Data = default(T)
            };
        }
    }
}
