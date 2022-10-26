using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using HashidsNet;

namespace HousingRepairsOnlineApi.Domain;

public class Repair
{
    private const string ReferencePrefix = "CC";

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public string Postcode { get; set; }
    public string SOR { get; set; }

    [Column(TypeName = "jsonb")] public RepairAddress Address { get; set; }

    [Column(TypeName = "jsonb")] public RepairLocation Location { get; set; }

    [Column(TypeName = "jsonb")] public RepairProblem Problem { get; set; }

    [Column(TypeName = "jsonb")] public RepairIssue Issue { get; set; }

    [NotMapped] public string ContactPersonNumber { get; set; }

    [Column(TypeName = "jsonb")] public RepairDescription Description { get; set; }

    [NotMapped] public RepairContactDetails ContactDetails { get; set; }

    [Column(TypeName = "jsonb")] public RepairAvailability Time { get; set; }

    public string GetReference(IHashids hasher)
    {
        return string.Concat(ReferencePrefix, hasher.Encode(Id));
    }
}
