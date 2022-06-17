namespace RBSSample.Shared.Dtos.Location;

public class LocationAuditableDto : LocationDto
{
    public DateTime CreatedOn { get; set; }
    public string CreatedBy { get; set; } = string.Empty;
    public DateTime LastModifiedOn { get; set; }
    public string LastModifiedBy { get; set; } = string.Empty;
}
