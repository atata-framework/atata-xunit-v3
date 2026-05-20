# Atata.Xunit.v3

[![Atata Templates](https://img.shields.io/badge/get-Atata_Templates-green.svg?color=4BC21F)](https://marketplace.visualstudio.com/items?itemName=YevgeniyShunevych.AtataTemplates)\
[![Slack](https://img.shields.io/badge/join-Slack-green.svg?colorB=4EB898)](https://join.slack.com/t/atata-framework/shared_invite/zt-5j3lyln7-WD1ZtMDzXBhPm0yXLDBzbA)
[![Atata docs](https://img.shields.io/badge/docs-Atata_Framework-orange.svg)](https://atata.io)
[![X](https://img.shields.io/badge/follow-@AtataFramework-blue.svg)](https://x.com/AtataFramework)

**Atata.Xunit.v3** is a C#/.NET library that integrates [Atata](https://github.com/atata-framework/atata) with xUnit v3.

*The package targets .NET 8.0 and .NET Framework 4.7.2.*

## Features

Atata.Xunit.v3 provides seamless integration between Atata Framework and xUnit v3 testing framework, offering:

- **Test suite and fixture base classes**. `AtataTestSuite`, `AtataGlobalFixture`, `AtataCollectionFixture`, and `AtataClassFixture<TClass>` for different testing scopes.
- **Xunit-aware context configuration**. Automatic integration of Xunit test names, suite names, traits.
- **Logging**: Integration with xUnit's test output for Atata logs.
- **Error handling**. Built-in Atata error handling (screenshots, page snapshots, etc.) on test failures.

## Installation

Install the package via .NET CLI:

```bash
dotnet add package Atata.Xunit.v3
```

Or using Package Manager:

```powershell
Install-Package Atata.Xunit.v3
```

## Usage

### Global fixture

For global setup across all test suites, create a class that inherits from `AtataGlobalFixture`:

```cs
public sealed class GlobalFixture : AtataGlobalFixture
{
    protected override void ConfigureAtataContextGlobalProperties(AtataContextGlobalProperties globalProperties)
    {
        // Configure global properties for AtataContext
    }

    protected override void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
        // Configure base AtataContext configuration
    }

    protected override void ConfigureGlobalAtataContext(AtataContextBuilder builder)
    {
        // Configure global AtataContext
    }

    protected override void OnBeforeGlobalSetup()
    {
        // Custom logic before global setup
    }

    protected override void OnAfterGlobalSetup()
    {
        // Custom logic after global setup
    }

    protected override void OnBeforeGlobalTeardown()
    {
        // Custom logic before global teardown
    }

    protected override void OnAfterGlobalTeardown()
    {
        // Custom logic after global teardown
    }
}
```

All the methods are virtual and are not required to be overridden.
You can choose to override only those that are relevant to your setup.
Mostly you will only need to override `ConfigureAtataContextBaseConfiguration` and `ConfigureGlobalAtataContext`.

### Test suite class

```cs
public sealed class SampleTests : AtataTestSuite
{
    [Fact]
    public void SampleTest()
    {
        // Test method implementation
    }

    protected override void ConfigureTestAtataContext(AtataContextBuilder builder)
    {
        // Optional test method-specific configuration
    }
}
```

Use `Context` property of the base class to access the current `AtataContext` instance.

### Class fixture

Create class fixture:

```cs
public sealed class SomeClassFixture<TClass> : AtataClassFixture<TClass>
{
    protected override void ConfigureSuiteAtataContext(AtataContextBuilder builder)
    {
        // Class fixture-specific configuration
    }
}
```

Apply class fixture to test suite class:

```cs
public sealed class SomeTests :
    AtataTestSuite,
    IClassFixture<SomeClassFixture<SomeTests>>
{
    //...
}
```

It is important to match the generic type parameter of `SomeClassFixture<TClass>` with the test suite class (`SomeTests` in this case).

### Collection fixture

Create collection definition class:

```cs
[CollectionDefinition(Name)]
public sealed class SomeCollection : ICollectionFixture<SomeCollectionFixture>
{
    public const string Name = "Some collection";
}
```

Create collection fixture:

```cs
public sealed class SomeCollectionFixture : AtataCollectionFixture
{
    public SomeCollectionFixture()
        : base(SomeCollection.Name)
    {
    }

    protected override void ConfigureCollectionAtataContext(AtataContextBuilder builder)
    {
        // Collection-specific configuration
    }
}
```

Apply collection fixture to test suite class:

```cs
[Collection(SomeCollection.Name)]
public sealed class SomeTests : AtataTestSuite
{
    //...
}
```

## Examples

Check out example project: [Atata Samples / Using Xunit](https://github.com/atata-framework/atata-samples/tree/main/Xunit)

## Community

- Slack: [https://atata-framework.slack.com](https://join.slack.com/t/atata-framework/shared_invite/zt-5j3lyln7-WD1ZtMDzXBhPm0yXLDBzbA)
- X: https://x.com/AtataFramework
- Stack Overflow: https://stackoverflow.com/questions/tagged/atata

## Feedback

Any feedback, issues and feature requests are welcome.

If you faced an issue please report it to [Atata.Xunit.v3 Issues](https://github.com/atata-framework/atata-xunit-v3/issues),
[ask a question on Stack Overflow](https://stackoverflow.com/questions/ask?tags=atata+csharp) using [atata](https://stackoverflow.com/questions/tagged/atata) tag
or use another [Atata Contact](https://atata.io/contact/) way.

## Contact author

Contact me if you need a help in test automation using Atata Framework, or if you are looking for a quality test automation implementation for your project.

- LinkedIn: https://www.linkedin.com/in/yevgeniy-shunevych
- Email: yevgeniy.shunevych@gmail.com
- Consulting: https://atata.io/consulting/

## Contributing

Check out [Contributing Guidelines](CONTRIBUTING.md) for details.

## SemVer

Atata Framework tries to follow [Semantic Versioning 2.0](https://semver.org/) when possible.
Sometimes Selenium.WebDriver dependency package can contain breaking changes in minor version releases,
so those changes can break Atata as well.
But Atata manages its sources according to SemVer.
Thus backward compatibility is mostly followed and updates within the same major version
(e.g. from 2.1 to 2.2) should not require code changes.

## License

Atata is an open source software, licensed under the Apache License 2.0.
See [LICENSE](LICENSE) for details.
