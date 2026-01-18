namespace CommercialManagement.Models.DynamicMenu
{
    public class UserWebMenu
    {
        public decimal UserId { get; set; }
        public decimal RoleID { get; set; }
        public decimal MenuID { get; set; }
        public string? DisplayNameInMenu { get; set; }
        public string? MenuIconName { get; set; }
    }
}
