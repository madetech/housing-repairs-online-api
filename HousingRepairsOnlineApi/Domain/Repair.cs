using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace HousingRepairsOnlineApi.Domain
{
    public class Repair
    {
        public Guid Id { get; set; }
        public string Postcode { get; set; }
        public string SOR { get; set; }

        [Column(TypeName = "jsonb")]
        public RepairAddress Address { get; set; }

        [Column(TypeName = "jsonb")]
        public RepairLocation Location { get; set; }

        [Column(TypeName = "jsonb")]
        public RepairProblem Problem { get; set; }

        [Column(TypeName = "jsonb")]
        public RepairIssue Issue { get; set; }
        public string ContactPersonNumber { get; set; }

        [Column(TypeName = "jsonb")]
        public RepairDescription Description { get; set; }

        [Column(TypeName = "jsonb")]
        public RepairContactDetails ContactDetails { get; set; }

        [Column(TypeName = "jsonb")]
        public RepairAvailability Time { get; set; }
    }
}
