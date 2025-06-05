# Job Counselor

[![Build & Test](https://github.com/alex-nikitin-dev/job-counselor/actions/workflows/ci.yml/badge.svg)]((https://github.com/alex-nikitin-dev/job-counselor/actions/workflows/ci.yml))

> **Professional Résumé Builder & Job Tracking System**  
> Clean Architecture • .NET 8 • CQRS • AI-assisted • Production-ready backend-first project

---

## 📌 Overview

**Job Counselor** is a modern backend system that helps users:

- Build professional résumés based on structured profile data
- Tailor résumés to specific job postings
- Generate cover letters using AI (offline-first with Ollama)
- Track applications, interviews, and offers
- Gain insights through analytics and funnel reports

---

## ⚙️ Architecture

| Layer          | Technologies                                |
|----------------|--------------------------------------------|
| API            | .NET 8, ASP.NET Core Minimal API            |
| Domain         | Clean Architecture, MediatR (CQRS), FluentValidation |
| Persistence    | PostgreSQL 16, EF Core 8                    |
| AI Integration | Ollama (LLaMA 3) via `IAiCoverLetterProvider` |
| Auth           | ASP.NET Core Identity, JWT, OAuth2 (LinkedIn, GitHub, Google) |
| Observability  | Serilog, OpenTelemetry (OTLP exporter)      |
| CI/CD          | GitHub Actions, Docker                      |
| Frontend       | React 19, Vite, Tailwind CSS (optional)     |

### Folder structure

```text
/src
  /Domain          - Entities, value objects, business rules
  /Application     - CQRS commands/queries, validators
  /Infrastructure  - EF Core, repositories, integrations
  /WebApi          - Minimal API endpoints, Swagger, auth
/tests
  /UnitTests       - Domain & Application tests
  /IntegrationTests- API + DB integration tests
```

---

## 🚀 Features

- Profile-driven résumé generation  
- AI-powered cover letter generation (offline-first)  
- Tailoring to job postings  
- Application tracking with stage progression  
- Résumé versioning with diff history  
- Notifications (Slack / Email)  
- Analytics dashboard  
- Full CI/CD automation (GitHub Actions)  
- Observability (traces, metrics, structured logs)  

---

## 🏗️ How to Build & Run

### Prerequisites

- .NET 8 SDK
- Docker / Docker Compose
- Node.js 20+ (for frontend if enabled)

### Local Run (Backend + DB + Ollama)

```bash
git clone https://github.com/alex-nikitin-dev/job-counselor.git
cd Job-Counselor
docker compose up --build
```

API will be available at: [http://localhost:5001/swagger](http://localhost:5001/swagger)

### Run Frontend (if enabled)

```bash
cd web-ui
npm install
npm run dev
```

---

## 🧪 Testing

```bash
# Backend tests
dotnet test

# Frontend tests (if enabled)
npm run lint
npm run test
```

GitHub Actions CI runs these on every pull request.

---

## 📊 Observability

- Health check: `/healthz`
- Logs: Serilog (JSON to console & rolling file)
- Tracing: OpenTelemetry → Tempo / Jaeger
- Metrics: Prometheus / OTLP exporter

---

## 🔄 CI/CD

- Pull requests: Automatic build, test & lint checks via GitHub Actions
- Main branch protected (status checks required)
- Container images published to GHCR / Azure Container Apps
- Postman collection included for API testing

---

## 🔐 Security

- Rate limiting (`AddRateLimiter`)
- Data protection keys persisted to Azure Key Vault or local filesystem
- OAuth2 login: LinkedIn / GitHub / Google
- JWT & Cookie authentication supported

---

## 🧭 API Modules & Endpoints

| Module         | Endpoints |
|----------------|-----------|
| Profile        | `GET /profile` / `PUT /profile` / `PUT /profile/import/linkedin` |
| Résumé         | `POST /resume/generate/base` / `POST /resume/{id}/adapt` / `GET /resume/{id}/download/pdf` |
| Cover Letter   | `POST /coverletter/generate` |
| Job Tracking   | CRUD `job`, `POST /job/{id}/apply`, `PATCH /job/{id}/stage` |
| Analytics      | `GET /dashboard/stats` |
| Health         | `GET /healthz` |

---

## 💻 Compatibility Matrix

| Component          | Version |
|--------------------|---------|
| .NET               | 8.0.x   |
| EF Core            | 8.x     |
| MediatR            | 12.x    |
| FluentValidation   | 11.x    |
| Serilog            | 3.x / 5.x |
| QuestPDF           | 2024.x  |
| OpenTelemetry .NET | 1.8+    |
| PostgreSQL         | 16      |
| React              | 19      |
| Vite               | Latest  |
| Tailwind           | 4.x     |

---

## 📜 License

MIT License

---

## 🤝 Contributing

Contributions are welcome! Please:

1. Open a pull request against `main`
2. Ensure GitHub Actions checks are ✅ green
3. Maintain code style & test coverage

---
