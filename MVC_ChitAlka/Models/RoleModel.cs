namespace MVC_ChitAlka.Models
{
    public class RoleModel
    {
        public string? NickName { get; set; }
        public string? RoleName ( string nickName )=>NickName=nickName;
    }
}
