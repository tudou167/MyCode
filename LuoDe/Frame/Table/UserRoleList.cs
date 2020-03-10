using System.Collections.Generic;

public class UserRoleList
{
    //拥有的角色列表
    public List<RoleData> userRoleList;
    public UserRoleList() { }
    public UserRoleList(List<RoleData> userRoleList) {
        this.userRoleList = userRoleList;
    }

}
