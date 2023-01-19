FROM python:3.11.1

RUN curl -sSL https://raw.githubusercontent.com/python-poetry/poetry/master/install-poetry.py | python -
ENV PATH="/root/.local/bin:$PATH"
RUN mkdir /src
WORKDIR /src

COPY ./poetry.lock ./
COPY ./pyproject.toml ./
RUN poetry install

COPY . ./
EXPOSE 3002
CMD ["poetry", "run", "python", "app.py"]
