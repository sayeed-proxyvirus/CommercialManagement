namespace CommercialManagement.Models.ViewModels
{
    public class ExportLCViewModel
    {
        public int ContactId { get; set; }
        public string? ExpLCNo { get; set; }
        public string? ContactNo { get; set; }
        public string? StyleDesc { get; set; }
        public int? TotalPcs { get; set; }
        public decimal? TotalPrice { get; set; }
    }
}
