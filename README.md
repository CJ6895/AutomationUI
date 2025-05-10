# AutomationUI
Welcome to my QA Automation Framework! This project serves as a portfolio to showcase my skills in building maintainable and scalable test automation solutions using Selenium WebDriver and MSTest Framework in C#.

### 🚀 Overview
This framework is designed to demonstrate real-world automation practices used for UI testing of web applications. It follows key automation principles such as:
<pre>
	•	Page Object Model (POM) design pattern
	•	Reusable utilities and helpers
	•	Integration with test reporting
	•	Easy scalability for future test case additions
</pre>

### 🛠️ Tech Stack
<pre>
	•	Language: C#
	•	Automation Tool: Selenium WebDriver
	•	Test Framework: MSTest
	•	Build Tool: .NET Core CLI / Visual Studio
	•	Package Manager: NuGet
	•	Browser Support: Chrome (via WebDriverManager)
</pre>
### 📁 Project Structure
<pre>
AutomationFramework/
└── Tests/
    └── Tests.UI/
        ├── Dependencies/          → External libraries and packages
        ├── Helpers/               → Utility/helper classes
        │   ├── Browser.cs         → Handles browser setup and WebDriver management
        │   └── FindBy.cs          → Custom find logic or element locators
        ├── Pages/                 → Page Object Model classes
        ├── Tests/                 → Test classes containing MSTest test cases
        ├── BasePage.cs            → Base class for all page objects
        ├── MSTest.cs              → Custom MSTest-related setup (e.g., attributes, base test class)
        ├── ProjectSetup.cs        → Initialization/configuration logic
        └── testsettings.json      → Test configuration (e.g., environment, browser)
</pre>
### ✅ Features
<pre>
  •	Clean separation between test logic and UI locators
	•	Cross-browser execution support
	•	Screenshot capture on test failure
	•	Easily extendable and readable test code
	•	Follows best practices in naming, structuring, and reusability
</pre>
### 📌 Notes
#### This project is under active development as part of my QA automation portfolio. Feel free to explore the code and structure to see my approach to automated software testing.
