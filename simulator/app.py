from flask import Flask, request

app = Flask(__name__)


@app.route("/healthcheck")
def healthcheck():
    return "OK"


@app.route("/addresses/<id>")
def get_by_id(id):
    return {
        "id": "1234",
        "line_1": "123 London Street",
        "line_2": "Rose Court",
        "postcode": "WF10PQ",
        "town_or_city": "London",
        "uprn_list": [{"type": "INTERNAL", "value": "12345"}],
    }



@app.route("/addresses")
def get_by_postcode():
    if "postcode" in request.args:
        if request.args["postcode"].replace(" ", "") == "E112LE":
            return [
                {
                    "id": "94e12c7b-c60c-4b13-ae7a-b8a375250418",
                    "line_1": "13 Rectory Crescent",
                    "line_2": "Wanstead",
                    "postcode": "E112LE",
                    "town_or_city": "London",
                    "uprn_list": [{"type": "INTERNAL", "value": "1000039"}],
                },
                {
                    "id": "523f42a3-344c-4d5b-b6e2-f808d37387b7",
                    "line_1": "21 Rectory Crescent",
                    "line_2": "Wanstead",
                    "postcode": "E112LE",
                    "town_or_city": "London",
                    "uprn_list": [{"type": "INTERNAL", "value": "56789"}],
                },
            ]
        else:
            return []
    return {"errors": ["NOT_PROVIDED"]}, 400


if "__main__" == __name__:
    app.run(host="0.0.0.0", port=3010)
