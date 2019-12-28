namespace ApiTpl.Core
{
    public static class ApiReturnCode
    {
        public static int Succeed => 1000;

        public static int ParamError => 1001;

        public static int OperationFail => 1050;

        public static int UnAuth => 1401;

        public static int SystemError => 1999;
    }
}
