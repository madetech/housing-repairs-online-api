using HashidsNet;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.Dtos;

namespace HousingRepairsOnlineApi.Extensions;

public static class RepairExtensions
{
    public static BookAppointmentRequest ToBookAppointmentRequest(this Repair repair, IHashids hasher)
    {
        return new BookAppointmentRequest
        {
            Reference = repair.GetReference(hasher),
            Appointment =
                new Appointment { StartTime = repair.Time.StartDateTime, EndTime = repair.Time.EndDateTime },
            ContactDetails = new ContactDetails
            {
                Email = repair.ContactDetails.Type == "email" ? repair.ContactDetails.Value : null,
                MobileNumber = repair.ContactDetails.Type == "tel" ? repair.ContactDetails.Value : null,
                PhoneNumber = repair.ContactPersonNumber
            },
            SorCode = repair.SOR,
            LocationId = repair.Address.LocationId,
            JobDescription = repair.Description.Text
        };
    }
}
