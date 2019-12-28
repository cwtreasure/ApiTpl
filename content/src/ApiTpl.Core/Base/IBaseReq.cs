namespace ApiTpl.Core
{
    public interface IBaseReq
    {
        (int Code, string Msg) Valid();
    }
}
