using CommercialManagement.Models.ApplicationDBContext;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using CommercialManagement.Models.DynamicMenu;

namespace SAMLitigation.Services.ServiceImple
{
    public class DynamicMenuServiceImple : DynamicMenuService
    {
        private readonly CommercialDBContext _context;
        private readonly ILogger<DynamicMenuServiceImple> _logger;

        public DynamicMenuServiceImple(CommercialDBContext context, ILogger<DynamicMenuServiceImple> logger)
        {
            _context = context;
            _logger = logger;
        }

        public List<MenuItem> ReadFullMenuAndSubmenus()
        {
            try
            {
                var menuItems = _context.Set<MenuItem>()
                    .FromSqlRaw("EXEC GetFullMenusAndSubmenus")
                    .AsEnumerable()
                    .ToList();

                _logger.LogInformation("Loaded {Count} menu items from database", menuItems.Count);
                return menuItems ?? new List<MenuItem>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading full menus from stored procedure");
                return new List<MenuItem>();
            }
        }

        public List<UserWebMenu> GetUserWebMenuByUserId(decimal userId)
        {
            try
            {
                if (userId <= 0)
                {
                    _logger.LogWarning("Invalid UserId: {UserId}", userId);
                    return new List<UserWebMenu>();
                }

                var param = new SqlParameter("@UserID", userId);

                var userMenus = _context.Set<UserWebMenu>()
                    .FromSqlRaw("EXEC GetMenuListByUserID @UserID", param)
                    .AsEnumerable()
                    .ToList();

                _logger.LogInformation("Loaded {Count} menus for user {UserId}", userMenus.Count, userId);
                return userMenus ?? new List<UserWebMenu>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error loading user menus for UserId: {UserId}", userId);
                return new List<UserWebMenu>();
            }
        }

        public WebControllerMethodDetails GetWebControllerMethodDetails(decimal controllerMethodSl)
        {
            try
            {
                if (controllerMethodSl <= 0)
                {
                    _logger.LogWarning("Invalid ControllerMethodSL: {SL}", controllerMethodSl);
                    return null;
                }

                var param = new SqlParameter("@ControllerMethodSL", controllerMethodSl);

                var details = _context.Set<WebControllerMethodDetails>()
                    .FromSqlRaw("EXEC GetControllerMethodDetailsByControllerMethodSL @ControllerMethodSL", param)
                    .AsEnumerable()
                    .FirstOrDefault();

                if (details == null)
                {
                    _logger.LogWarning("No controller method details found for SL: {SL}", controllerMethodSl);
                }

                return details;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting controller method details for SL: {SL}", controllerMethodSl);
                return null;
            }
        }

        public List<TreeNode> BuildTree(List<MenuItem> items, List<UserWebMenu> userMenus)
        {
            try
            {
                if (items == null || !items.Any())
                {
                    _logger.LogWarning("No menu items provided to build tree");
                    return new List<TreeNode>();
                }

                if (userMenus == null || !userMenus.Any())
                {
                    _logger.LogWarning("No user menus provided to build tree");
                    return new List<TreeNode>();
                }

                // Filter items based on user's menu access
                var filteredItems = FindUserWebMenusInFullMenus(userMenus, items);

                if (!filteredItems.Any())
                {
                    _logger.LogWarning("No filtered menu items after user permission check");
                    return new List<TreeNode>();
                }

                // Group by ParentID for quick lookup
                var lookup = filteredItems
                    .GroupBy(x => x.ParentID)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Get root menu IDs from user menus
                var rootParents = userMenus
                    .Select(x => x.MenuID)
                    .Distinct()
                    .ToList();

                var tree = new List<TreeNode>();

                foreach (var rootId in rootParents)
                {
                    if (lookup.ContainsKey(rootId))
                    {
                        var node = BuildNode(rootId, lookup, new HashSet<decimal>());
                        if (node != null && (node.Children.Any() || !string.IsNullOrEmpty(node.DisplayName)))
                        {
                            tree.Add(node);
                        }
                    }
                }

                _logger.LogInformation("Built menu tree with {Count} root nodes", tree.Count);
                return tree;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error building menu tree");
                return new List<TreeNode>();
            }
        }

        public TreeNode BuildNode(decimal parentId, Dictionary<decimal, List<MenuItem>> lookup, HashSet<decimal> visited)
        {
            try
            {
                // Cycle detection
                if (visited.Contains(parentId))
                {
                    _logger.LogWarning("Cycle detected at parentId: {ParentId}", parentId);
                    return new TreeNode { Id = parentId };
                }

                if (!lookup.ContainsKey(parentId))
                {
                    return null;
                }

                visited.Add(parentId);

                var first = lookup[parentId].First();

                TreeNode node = new TreeNode
                {
                    Id = parentId,
                    Code = first.ParentCode,
                    DisplayName = first.IsMenu ? first.ParentCode : first.DisplayName,
                    IconName = first.MenuIconName ?? "bi-circle",
                    ParentCode = first.ParentCode,
                    ChildCode = first.ChildCode,
                    IsMenu = first.IsMenu,
                    Children = new List<TreeNode>()
                };

                foreach (var item in lookup[parentId])
                {
                    if (item.IsDisplayable)
                    {
                        TreeNode child = new TreeNode
                        {
                            Id = item.ChieldID,
                            Code = item.ChildCode,
                            DisplayName = item.DisplayName,
                            IconName = item.IconName ?? "bi-circle",
                            ParentCode = first.ParentCode,
                            ChildCode = item.ChildCode,
                            IsMenu = false,
                            Children = new List<TreeNode>()
                        };

                        // Recursive build for nested children
                        if (lookup.ContainsKey(item.ChieldID))
                        {
                            var childNode = BuildNode(item.ChieldID, lookup, visited);
                            if (childNode != null && childNode.Children != null)
                            {
                                child.Children.AddRange(childNode.Children);
                                child.IsMenu = true;
                            }
                        }

                        node.Children.Add(child);
                    }
                }

                // Remove from visited to allow sibling branches
                visited.Remove(parentId);

                return node;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error building node for parentId: {ParentId}", parentId);
                return null;
            }
        }

        private List<MenuItem> FindUserWebMenusInFullMenus(List<UserWebMenu> userWebMenu, List<MenuItem> fullMenuList)
        {
            try
            {
                // Get user's root menu IDs
                var userRoots = userWebMenu.Select(u => u.MenuID).ToHashSet();

                // Result set
                HashSet<MenuItem> result = new HashSet<MenuItem>();

                // Lookup by ParentID for fast recursion
                var lookup = fullMenuList
                    .GroupBy(m => m.ParentID)
                    .ToDictionary(g => g.Key, g => g.ToList());

                // Add all children for each user root
                foreach (var rootId in userRoots)
                {
                    AddAllChildren(rootId, lookup, result);
                }
                return result.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error filtering user menus");
                return new List<MenuItem>();
            }
        }

        private void AddAllChildren(decimal parentId, Dictionary<decimal, List<MenuItem>> lookup, HashSet<MenuItem> result)
        {
            if (!lookup.ContainsKey(parentId))
                return;

            foreach (var item in lookup[parentId])
            {
                if (!result.Contains(item))
                {
                    result.Add(item);
                    // Recursively add children
                    AddAllChildren(item.ChieldID, lookup, result);
                }
            }
        }
    }
}