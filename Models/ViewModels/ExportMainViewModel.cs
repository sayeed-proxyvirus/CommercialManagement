namespace CommercialManagement.Models.ViewModels
{
    public class ExportMainViewModel
    {
        public int ExpID { get; set; }
        public string? MainExpName { get; set; }
        public DateTime? LCDate { get; set; }///ReceiveDate
        public DateTime? ShipDate { get; set; } ///ExpDate
        public decimal? TotalValue { get; set; } ///Dollars
        public decimal? MainExpTaka { get; set; }
        public string? Reference { get; set; } ///MainExpRem
        public decimal? ExpQuanity { get; set; }
        public string? Destination { get; set; }
        public int? ApplicantID { get; set; }
        public string? ApplicantName { get; set; }
        public int? BenID { get; set; }
        public string? BenName { get; set; }
        public int? PartyID { get; set; }
        public string? PartyName { get; set; } /// notifying party
    }
}
