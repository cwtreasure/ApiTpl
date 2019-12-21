namespace ApiTpl.Service.UserSvc
{
    using ApiTpl.Core;

    public class AddUserReq : IBaseReq
    {
        /// <summary>
        /// User Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// User Gender
        /// </summary>
        public int Gender { get; set; }

        public (int Code, string Msg) Valid()
        {
            if (string.IsNullOrWhiteSpace(Name)) return (ApiReturnCode.ParamError, "Name can not be empty");
            if (Gender <= 0) return (ApiReturnCode.ParamError, "Gender must greater than 0");
            return (ApiReturnCode.Succeed, string.Empty);
        }

        public AddUserInput BuildAddUserInput(string traceId, string userIp)
        {
            return new AddUserInput
            {
                Name = Name,
                Gender = Gender,
                TraceId = traceId,
                UserIp = userIp
            };
        }
    }
}
