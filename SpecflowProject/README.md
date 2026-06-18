Test Automation Framework

Overview

This project demonstrates a Behaviour-Driven Development (BDD) test automation framework for validating the login functionality of the website https://www.saucedemo.com/.

The framework is built using:

.NET 8
C#
Playwright
Reqnroll (BDD)
NUnit
log4net
Extent Reporting

The framework follows the Page Object Model (POM) design pattern to promote maintainability, readability, and scalability.

Project Structure
├── Config
│   └── appsettings.json
├── ExtentReports
│   └── ExtentReports.html
├── Features
│   └── Login.feature
├── Hooks
│   └── ExtentReportHooks.cs
│   └── Hooks.cs
├── Logs
│   └── logfile.log
├── Resources
│   └── log4net.config
├── StepDefinitions
│   └── LoginFeatureStepDefinitions.cs
├── Utilities
│   └── ConstantStrings.cs
│   └── LoggerManager.cs
│   └── LogTypeEnum.cs
│   └── ScreenshotHelper.cs
└── README.md

Design Choices
Behaviour-Driven Development (BDD)

Reqnroll was selected to enable test scenarios to be written in a business-readable format using Gherkin syntax.

This improves collaboration between technical and non-technical stakeholders and provides clear traceability between requirements and automated tests.

Playwright

Playwright was selected due to:

Automatic waiting and synchronisation
Cross-browser support
Reduced test flakiness
Modern API design
Fast execution times
Page Object Model

The Page Object Model design pattern was implemented to:

Separate test logic from page interactions
Improve maintainability
Encourage code reuse
Reduce duplication
Logging

log4net is used to provide visibility into test execution and simplify troubleshooting.

Reporting

Extent Reports is used to provide detailed execution reports including:
Test results
Execution duration
Failure information
Screenshots captured on failure
Test Coverage
Positive Scenario
Successful login with valid credentials
Negative Scenarios
Invalid username and password
Empty username
Empty password
Locked user account
Prerequisites

Before running the tests, ensure the following are installed:

.NET 8 SDK
Git
Playwright browsers

Verify installation:

dotnet --version
Installation

Clone the repository:

git clone <repository-url>
cd SpecflowProject

Restore dependencies:

dotnet restore

Install Playwright browsers:

pwsh bin/Debug/net8.0/playwright.ps1 install
Running Tests

Run all tests:

dotnet test

Run tests by tag:

dotnet test --filter Category=Smoke
Reporting

Generate Extent Reports results after test execution:

dotnet test

Logging

Execution logs are generated during test execution and can be used to assist with debugging and failure analysis.
Logs are saved in the Logs directory and can be configured in log4net.config.

Assumptions
The application under test is available and accessible.
Test accounts remain valid throughout execution.
Test data is stable and predictable.
Potential Future Improvements

Given additional time, the following enhancements could be implemented:

CI/CD integration using GitHub Actions
Parallel test execution
Multiple environment support (QA/UAT/Production)
Browser selection through configuration
Dockerised execution
API and UI test integration
Enhanced test data management
Visual regression testing
Author Notes

This framework was designed with maintainability, scalability, and readability in mind. The focus was on demonstrating sound automation engineering principles rather than maximising test volume.