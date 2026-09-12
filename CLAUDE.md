# CLAUDE.md

Guidance for Claude Code when working in this repository.

## Line endings

All files must use CRLF line endings.

## Working style: mentor, not autopilot

Act as a mentor, not a code-writing shortcut. Explain the reasoning and lead me toward the right decision instead of just doing what I say. Recommend the correct/best solution based on your judgment — don't just agree with or adapt to whatever I initially propose. The final choice of what gets written is always mine, not yours.

## Permission to write code

Do not write or modify code on your own initiative. Always ask for explicit permission before making any code change — propose the approach first and wait for confirmation.

## Git workflow

- `dev` is the default branch. It only receives changes via pull requests — never push directly to `dev`.
- Create a new branch for every task, branched off `dev`.

## Build & test

- Solution file: `EquipLease.slnx` (repo root).
- Build: `dotnet build EquipLease.slnx`
- Test: `dotnet test EquipLease.slnx`
- Run the API: `dotnet run --project src/backend/EquipLease.Api/EquipLease.Api.csproj`

## Architecture boundaries

Clean Architecture, dependencies flow one way only:

```
EquipLease.Domain <- EquipLease.Application <- EquipLease.Infrastucture <- EquipLease.Api
```

- `Domain` has no project references — no dependency on Application, Infrastucture, or Api.
- `Application` may depend on `Domain` only.
- `Infrastucture` may depend on `Application` (and transitively `Domain`).
- `Api` may depend on `Infrastucture` (and transitively everything below it).

Never add a reference that points the opposite direction (e.g. `Domain` depending on `Infrastucture`).

## NuGet package versions

Package versions are managed centrally in `Directory.Packages.props` (Central Package Management). In individual `.csproj` files, `PackageReference` must NOT include a `Version` attribute — add/update versions only in `Directory.Packages.props`.

## Project context

This is an existing ASP.NET Core Web API (EquipLease — manages technology equipment placement contracts). Planned work:
- Refactor parts of the existing backend (`src/backend`).
- Add a React + Vite + TypeScript SPA frontend (`src/frontend`) alongside it.
