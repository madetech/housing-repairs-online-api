# housing-repairs-online-api

# Copilot

Services running in each environment run in separate AWS accounts, however all services are deployed by pipelines from the build and deploy account.

DON'T run `copilot svc deploy` or any other copilot command from any other account than `rnd-bnd`. 

Before running any copilot commands, run this

1. Define AWS profile env var `export AWS_PROFILE=rnd-bnd`
2. Authenticate with AWS `aws sso login`

## Local Development
1. `mv sample.env .env`
2. Set values in `.env`
3. `docker compose up`

## Environment Variables

Variables which are used for running the service in isolated (without connected services) are stored in the `.env`, whereas variables needed for communicating with other services are stored in `docker-compose.yml` within the `housing-repairs-online-api` solution