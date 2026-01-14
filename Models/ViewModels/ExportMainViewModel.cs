namespace CommercialManagement.Models.ViewModels
{
    public class ExportMainViewModel
    {
        public int ExpID { get; set; }
        public string? MainExpName { get; set; }
        public DateTime? LCDate { get; set; }///ReceiveDate
        public DateTime? ShipDate { get; set; } ///ExpDate
        public float? TotalValue { get; set; } ///Dollars
        public float? MainExpTaka { get; set; }
        public string? Reference { get; set; } ///MainExpRem
        public float? ExpQuanity { get; set; }
        public string? Destination { get; set; }
        public string? ApplicantName { get; set; }
        public string? BenName { get; set; }
        public string? PartyName { get; set; } /// notifying party
    }
}
