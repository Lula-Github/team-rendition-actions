# Semantic Versioning with GitVersion

This repository uses [GitVersion](https://gitversion.net/) to automatically handle semantic versioning based on Git history. Versioning is automated through GitHub Actions, ensuring consistent and predictable version numbers across all releases.

## 📋 Prerequisites

- Git
- GitHub Actions (enabled for your repository)
- Basic understanding of [Semantic Versioning](https://semver.org/)

## 🔧 Setup

### GitVersion Configuration

The repository includes a `GitVersion.yml` configuration file:

```yaml
mode: ContinuousDeployment
branches:
  main:
    regex: ^master$|^main$
    mode: ContinuousDelivery
    tag: ''
    increment: Patch
    prevent-increment-of-merged-branch-version: true
    track-merge-target: false
    source-branches: ['develop', 'release']
    is-release-branch: false
  develop:
    regex: ^dev(elop)?$
    mode: ContinuousDeployment
    tag: alpha
    increment: Minor
    track-merge-target: true
    source-branches: []
    is-release-branch: false
  release:
    regex: ^release?[/-]
    mode: ContinuousDelivery
    tag: beta
    increment: None
    source-branches: ['develop']
    is-release-branch: true
  feature:
    regex: ^features?[/-]
    mode: ContinuousDelivery
    tag: useBranchName
    increment: Inherit
    source-branches: ['develop']
    is-release-branch: false
  hotfix:
    regex: ^hotfix(es)?[/-]
    tag: beta
    increment: Patch
    source-branches: ['main', 'master']
    is-release-branch: false
```

### GitHub Actions Workflow

Create `.github/workflows/versioning.yml`:

```yaml
name: GitVersion

on:
  push:
    branches:
      - main
      - develop
      - 'release/**'
      - 'hotfix/**'
  pull_request:
    branches:
      - main
      - develop

jobs:
  versioning:
    runs-on: ubuntu-latest
    steps:
      - name: Checkout
        uses: actions/checkout@v3
        with:
          fetch-depth: 0

      - name: Install GitVersion
        uses: gittools/actions/gitversion/setup@v0.9.15
        with:
          versionSpec: '5.x'

      - name: Determine Version
        id: gitversion
        uses: gittools/actions/gitversion/execute@v0.9.15

      - name: Display Version
        run: |
          echo "SemVer: ${{ steps.gitversion.outputs.semVer }}"
          echo "FullSemVer: ${{ steps.gitversion.outputs.fullSemVer }}"
          echo "Major: ${{ steps.gitversion.outputs.major }}"
          echo "Minor: ${{ steps.gitversion.outputs.minor }}"
          echo "Patch: ${{ steps.gitversion.outputs.patch }}"
```

## 🚀 Usage

### Branch Naming Convention

Follow these branch naming patterns for automatic versioning:

- `main` or `master`: Production code
- `develop`: Development code
- `release/*`: Release branches
- `feature/*`: Feature branches
- `hotfix/*`: Hotfix branches

### Version Increments

The version number will automatically increment based on:

1. **Major Version (X.0.0)**
   - Breaking changes
   - Add `+semver: major` to commit message

2. **Minor Version (0.X.0)**
   - New features (non-breaking)
   - Add `+semver: minor` to commit message

3. **Patch Version (0.0.X)**
   - Bug fixes and small changes
   - Default increment for merges to main/master

### Commit Messages

Use conventional commit messages with optional GitVersion flags:

```
feat: add new feature
fix: resolve bug
docs: update documentation
+semver: minor
```

## 📝 Examples

### Feature Branch Workflow

1. Create a feature branch:

   ```bash
   git checkout -b feature/new-feature
   ```

2. Make changes and commit:

   ```bash
   git commit -m "feat: add new capability
   +semver: minor"
   ```

3. Merge to develop:

   ```bash
   git checkout develop
   git merge feature/new-feature
   ```

### Release Process

1. Create a release branch:

   ```bash
   git checkout -b release/1.0.0
   ```

2. Make release preparations and commit:

   ```bash
   git commit -m "chore: prepare 1.0.0 release"
   ```

3. Merge to main/master:

   ```bash
   git checkout main
   git merge release/1.0.0
   ```

## 🔍 Version Information

To check the current version locally:

```bash
dotnet-gitversion
```

## 🤝 Contributing

1. Fork the repository
2. Create your feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes using conventional commits
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

## 📚 Additional Resources

- [GitVersion Documentation](https://gitversion.net/docs/)
- [Semantic Versioning Specification](https://semver.org/)
- [Conventional Commits](https://www.conventionalcommits.org/)
