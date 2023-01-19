from flask import Blueprint, request
import datetime

hact_blueprint = Blueprint("hact_blueprint", __name__)


@hact_blueprint.route("/healthcheck", methods=["GET"])
def healthcheck():
    return "OK"


@hact_blueprint.route("/hact/availableAppointments", methods=["GET"])
def get_available_appointments():
    args = request.args
    date = args.get("fromDate")
    request_date = datetime.datetime.strptime(date, "%Y-%m-%dT%H:%M:%S")

    if request_date < datetime.datetime.now():
        request_date = datetime.datetime.now()

    return {"Appointments": build_hact_appointments(request_date)}


def build_hact_appointment(earliest_time, latest_time):
    earliest_time = earliest_time.isoformat()
    latest_time = latest_time.isoformat()
    return {
        "RepairsProviderDegreeOfPreference": 0,
        "RepairsProviderPreferenceDescription": "Provider Preference",
        "MeetsSLA": True,
        "MeetsCustomerPreference": True,
        "CustomerDegreeOfPreference": 1,
        "Date": earliest_time,
        "TimeOfDay": {
            "Name": "time",
            "EarliestArrivalTime": earliest_time,
            "LatestArrivalTime": latest_time,
            "LatestCompletionTime": "2020-07-10T13:00:00",
        },
        "Reference": {
            "ID": "1234",
            "Description": "job",
            "AllocatedBy": "Dave Davidson",
        },
    }


def build_hact_appointments(request_date):
    appointments = []

    for i in range(1, 5):
        appointments.append(
            build_hact_appointment(
                get_date_time(i, 9, request_date),
                get_date_time(i, 10, request_date),
            )
        ),
        appointments.append(
            build_hact_appointment(
                get_date_time(i, 15, request_date),
                get_date_time(i, 16, request_date),
            )
        ),

    return appointments


def get_date_time(days_to_add, hours, fromDate=datetime.datetime.now()):
    return (fromDate + datetime.timedelta(days=days_to_add)).replace(
        hour=hours, minute=0, second=0, microsecond=0
    )
