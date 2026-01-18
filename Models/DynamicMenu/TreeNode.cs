namespace CommercialManagement.Models.DynamicMenu
{
    public class TreeNode
    {
        public decimal Id { get; set; }
        public string? Code { get; set; }
        public string? DisplayName { get; set; }
        public string? ParentCode { get; set; }
        public string? ChieldCode { get; set; }
        public bool IsMenu { get; set; }
        public string? MenuIconName { get; set; }
        public string? IconName { get; set; }
        public List<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}
