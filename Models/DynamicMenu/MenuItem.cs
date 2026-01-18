namespace CommercialManagement.Models.DynamicMenu
{
    public class MenuItem
    {
        public decimal ParentID { get; set; }
        public string? ParentCode { get; set; }
        public decimal ChieldID { get; set; }
        public string? ChildCode { get; set; }
        public string? DisplayName { get; set; }
        public string? MenuIconName { get; set; }
        public string? IconName { get; set; }
        public Boolean IsMenu { get; set; }
        public Boolean IsDisplayable { get; set; }

    }
}
