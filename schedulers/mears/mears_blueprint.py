import random
from flask import Blueprint, request
from mears.mears_response_mapper import MearsResponseMapper

mears_blueprint = Blueprint("mears_blueprint", __name__)

created_job_ids = []
TIME_STRING = "%Y-%m-%dT%H:%M:%S.%fZ"
UNAUTHORIZED_RESPONSE = {
    "Message": "Authorization has been denied for this request."
}, 401
RESPONSE_MAPPER = MearsResponseMapper()
AUTHORISED_USER = [
    "Basic bG9jYWxob3N0OmwwYzRsaDBzdA==",
    "Basic d2FsZm9yZDp3NGxmMHJk",
    "Basic d2VhdGhlcmZpZWxkOnczNHRoM3JmMTNsZA==",
]


@mears_blueprint.route("/mears/healthcheck", methods=["GET"])
def healthcheck():
    return {}


@mears_blueprint.route(
    "/mears/api/AppointmentManagement/GetAvailableSlots", methods=["POST"]
)
def get_appointments():
    if _request_is_not_authenticated(request):
        return UNAUTHORIZED_RESPONSE

    request_object = request.get_json()

    if not _validate_get_appointments_request(request_object):
        return {"errors": ["NOT_VALID"]}, 400

    return RESPONSE_MAPPER.build_appointment_response(request_object)


@mears_blueprint.route("/mears/api/JobManagement/AddJob", methods=["POST"])
def add_job():
    if _request_is_not_authenticated(request):
        return UNAUTHORIZED_RESPONSE

    mears_response_status_code = _validate_add_job_request(request.get_json())

    if mears_response_status_code != "1":
        return {"StatusCode": mears_response_status_code}

    job_id = _create_job()

    return {
        "JobId": job_id,
        "StatusCode": "1",
        "StatusMessage": "SUCCESS",
    }


@mears_blueprint.route(
    "/mears/api/AppointmentManagement/BookAppointment", methods=["POST"]
)
def book_appointment():
    if _request_is_not_authenticated(request):
        return UNAUTHORIZED_RESPONSE

    request_object = request.get_json()
    status_code = _validate_book_appointment_request(request_object)

    if status_code != "1":
        return {"StatusCode": status_code}

    return {
        "StatusCode": "1",
        "StatusMessage": "SUCCESS",
        "AppointmentId": random.randint(0, 999999999),
        "AppointmentStatus": "0",
        "Duration": 60,
        "ScheduledDateTime": request_object["AppointmentDateTime"],
    }


def _request_is_not_authenticated(request_to_check):
    return (
        "Authorization" not in request_to_check.headers
        or request_to_check.headers["Authorization"] not in AUTHORISED_USER
    )


def _validate_add_job_request(request_object):
    if not request_object:
        return "1000"
    if not request_object["ContractReference"]:
        return "4006"
    if not request_object["JobNumber"]:
        return "4031"
    if not request_object["AddressUPRN"]:
        return "4002"
    if not request_object["PriorityCode"] or not request_object["ExpenditureCode"]:
        return "4001"
    if not request_object["Trade"]:
        return "4003"
    if "JobLines" in request_object and len(request_object["JobLines"]) > 0:
        for line in request_object["JobLines"]:
            if not line["ScheduleCode"]:
                return "4004"

    return "1"


def _validate_book_appointment_request(request_object: dict) -> str:
    if not request_object or not request_object["JobId"]:
        return "1000"
    if not request_object["Trade"]:
        return "4003"
    if not request_object["ClientSystemUser"] or not request_object["AppointmentNotes"]:
        return "2003"
    if (
        not request_object["AppointmentDateTime"]
        or not request_object["SlotTimeDescription"]
    ):
        return "4012"
    if request_object["JobId"] not in created_job_ids:
        return "4009"

    return "1"


def _validate_get_appointments_request(request_object):
    if (
        "ClientSystemUser" not in request_object
        or "OperationMode" not in request_object
        or "ContractType" not in request_object
        or "TradesArray" not in request_object
        or request_object["TradesArray"] is None
        or "Priority" not in request_object
        or "AppointmentDateTime" not in request_object
        or "DaysAroundReturnedDate" not in request_object
        or "SlotDuration" not in request_object
        or "CapacityWeightingOption" not in request_object
        or "AddressCode" not in request_object
        or request_object["AddressCode"] is None
        or "IncludeCalendarCentralisationDetails" not in request_object
        or "IncludeTradeAreaDetails" not in request_object
    ):
        return False

    return True


def _create_job():
    job_id = random.randint(0, 2048)
    created_job_ids.append(job_id)
    return job_id
