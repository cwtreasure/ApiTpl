namespace ApiTpl.Service.UserSvc
{
    using ApiTpl.Core;
    using ApiTpl.Core.Domains;

    public class AddUserInput : BaseInput
    {
        public string Name { get; set; }

        public int Gender { get; set; }

        public User BuildUser()
        {
            return new User
            {
                Name = Name,
                Gender = Gender,
            };
        }
    }
}
