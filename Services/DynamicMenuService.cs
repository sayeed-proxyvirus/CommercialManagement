using CommercialManagement.Models.DynamicMenu;

namespace SAMLitigation.Services
{
    public interface DynamicMenuService
    {

        List<MenuItem> ReadFullMenuAndSubmenus();
        List<UserWebMenu> GetUserWebMenuByUserId(decimal UserId);
        List<TreeNode> BuildTree(List<MenuItem> items, List<UserWebMenu> uItems);
        TreeNode BuildNode(decimal parentId, Dictionary<decimal, List<MenuItem>> lookup, HashSet<decimal> visited);
        WebControllerMethodDetails GetWebControllerMethodDetails(decimal ControllerMethodSL);

    }
}