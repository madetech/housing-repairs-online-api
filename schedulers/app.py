from flask import Flask

from mears.mears_blueprint import mears_blueprint
from hact_blueprint import hact_blueprint

app = Flask(__name__)
app.register_blueprint(mears_blueprint)
app.register_blueprint(hact_blueprint)

if "__main__" == __name__:
    app.run(host="0.0.0.0", port=3002)
