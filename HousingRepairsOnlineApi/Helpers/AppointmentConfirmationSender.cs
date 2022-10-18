using System;
using System.Threading.Tasks;
using HashidsNet;
using HousingRepairsOnlineApi.Domain;
using HousingRepairsOnlineApi.UseCases;

namespace HousingRepairsOnlineApi.Helpers
{
    public class AppointmentConfirmationSender : IAppointmentConfirmationSender
    {
        private readonly ISendAppointmentConfirmationEmailUseCase sendAppointmentConfirmationEmailUseCase;
        private readonly ISendAppointmentConfirmationSmsUseCase sendAppointmentConfirmationSmsUseCase;
        private readonly IHashids hasher;

        public AppointmentConfirmationSender(
            ISendAppointmentConfirmationEmailUseCase sendAppointmentConfirmationEmailUseCase,
            ISendAppointmentConfirmationSmsUseCase sendAppointmentConfirmationSmsUseCase,
            IHashids hasher)
        {
            this.sendAppointmentConfirmationEmailUseCase = sendAppointmentConfirmationEmailUseCase;
            this.sendAppointmentConfirmationSmsUseCase = sendAppointmentConfirmationSmsUseCase;
            this.hasher = hasher;
        }

        public void Execute(Repair repair)
        {
            var bookingReference = repair.GetReference(hasher);

            switch (repair?.ContactDetails?.Type)
            {
                case AppointmentConfirmationSendingTypes.Email:
                    sendAppointmentConfirmationEmailUseCase.Execute(repair.ContactDetails.Value, bookingReference, repair.Time.Display);
                    break;
                case AppointmentConfirmationSendingTypes.Sms:
                    sendAppointmentConfirmationSmsUseCase.Execute(repair.ContactDetails.Value, bookingReference,
                        repair.Time.Display);
                    break;
                default:
                    throw new Exception($"Invalid contact type: {repair?.ContactDetails?.Type}");
            }
        }
    }
}
