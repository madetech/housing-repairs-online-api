import datetime

TIME_STRING = "%Y-%m-%dT%H:%M:%S.%fZ"


class MearsResponseMapper:
    def _get_slot_days(self, request_object):
        if request_object["TradesArray"] != "373049":
            return self._build_slot_days(
                request_object["AppointmentDateTime"],
                int(request_object["DaysAroundReturnedDate"]),
            )

        return []

    def _build_slot_days(self, date, number_of_days):
        date_time = datetime.datetime.strptime(date, TIME_STRING)
        slot_days = []
        slot_days.append(self._build_slot_day(date_time))

        for i in range(1, number_of_days + 1):
            slot_days.append(
                self._build_slot_day(date_time + datetime.timedelta(days=i))
            )
            slot_days.append(
                self._build_slot_day(date_time - datetime.timedelta(days=i))
            )

        return slot_days

    def _build_slot_day(self, date):
        return {
            "SlotDate": date.strftime(TIME_STRING),
            "ResourceCapacity": 5,
            "NonBookingDay": False,
            "Slots": [
                self._build_slot("08:00", "12:00"),
                self._build_slot("13:00", "15:00"),
            ],
        }

    def _build_slot(self, slot_start_time, slot_end_time):
        return {
            "Description": f"{slot_start_time}-{slot_end_time}",
            "StartTime": f"{slot_start_time}:00",
            "EndTime": f"{slot_end_time}:00",
            "Bookable": True,
            "AvailableSlotCapacity": 24,
            "MaximumSlotCapacity": 33,
        }

    def build_appointment_response(self, request_object):
        return {
            "StatusCode": "1",
            "StatusMessage": "SUCCESS",
            "ContractReference": "null",
            "SlotDays": self._get_slot_days(request_object),
            "GasComplianceDetails": "null",
            "CalendarCentralisationDetails": {
                "CentralisedInJobPriorityDays": True,
                "CentralDate": "2022-09-24T14:22:10.512Z",
            },
            "TradeAreaDetails": {
                "Active": False,
                "AppointmentArea": "null",
            },
        }
