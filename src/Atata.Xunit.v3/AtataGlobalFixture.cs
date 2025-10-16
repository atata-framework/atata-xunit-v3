namespace Atata.Xunit;

/// <summary>
/// Represents a base class for Atata Xunit global fixture,
/// providing configuration and initialization for the global <see cref="AtataContext"/>,
/// base configuration of <see cref="AtataContext"/>,
/// configuration of global properties of <see cref="AtataContext"/>.
/// </summary>
public abstract class AtataGlobalFixture : AtataFixture
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AtataGlobalFixture"/> class
    /// with the <see cref="AtataContextScope.Global"/> context scope.
    /// </summary>
    protected AtataGlobalFixture()
        : base(AtataContextScope.Global)
    {
    }

    /// <summary>
    /// Configures the <see cref="AtataContext.GlobalProperties"/>;
    /// configures the <see cref="AtataContext.BaseConfiguration"/>;
    /// configures and builds the global <see cref="AtataContext"/>.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/> object.</returns>
    public override async ValueTask InitializeAsync()
    {
        OnBeforeGlobalSetup();

        AtataContext.GlobalProperties.ModeOfCurrent = AtataContextModeOfCurrent.AsyncLocalBoxed;
        AtataContext.GlobalProperties.RootNamespace = GetType().Namespace;

        ConfigureAtataContextGlobalProperties(AtataContext.GlobalProperties);

        AtataContext.BaseConfiguration.UseAssertionExceptionFactory(XunitAssertionExceptionFactory.Instance);
        AtataContext.BaseConfiguration.UseAggregateAssertionExceptionFactory(XunitAggregateAssertionExceptionFactory.Instance);

        ConfigureAtataContextBaseConfiguration(AtataContext.BaseConfiguration);

        await base.InitializeAsync().ConfigureAwait(false);

        OnAfterGlobalSetup();
    }

    public override async ValueTask DisposeAsync()
    {
        OnBeforeGlobalTeardown();

        await base.DisposeAsync().ConfigureAwait(false);

        OnAfterGlobalTeardown();
    }

    /// <summary>
    /// Configures the global properties of the <see cref="AtataContext"/>.
    /// The method can be overridden to provide custom configuration.
    /// </summary>
    /// <param name="globalProperties">The global properties to configure.</param>
    protected virtual void ConfigureAtataContextGlobalProperties(AtataContextGlobalProperties globalProperties)
    {
    }

    /// <summary>
    /// Configures the base configuration of the <see cref="AtataContext"/>.
    /// The method can be overridden to provide custom configuration.
    /// </summary>
    /// <param name="builder">The builder for the base configuration.</param>
    protected virtual void ConfigureAtataContextBaseConfiguration(AtataContextBuilder builder)
    {
    }

    private protected sealed override void ConfigureAtataContext(AtataContextBuilder builder)
    {
        builder.UseTestSuiteType(GetType());

        ConfigureGlobalAtataContext(builder);
    }

    /// <summary>
    /// Configures the global <see cref="AtataContext"/>.
    /// The method can be overridden to provide custom configuration.
    /// </summary>
    /// <param name="builder">The <see cref="AtataContextBuilder"/> used to configure the context.</param>
    protected virtual void ConfigureGlobalAtataContext(AtataContextBuilder builder)
    {
    }

    /// <summary>
    /// Called before the global setup of <see cref="AtataContext"/>.
    /// The method can be overridden to provide custom logic before global setup.
    /// </summary>
    protected virtual void OnBeforeGlobalSetup()
    {
    }

    /// <summary>
    /// Called after the global setup of <see cref="AtataContext"/>.
    /// The method can be overridden to provide custom logic after global setup.
    /// </summary>
    protected virtual void OnAfterGlobalSetup()
    {
    }

    /// <summary>
    /// Called before the global teardown of <see cref="AtataContext"/>.
    /// The method can be overridden to provide custom logic before global teardown.
    /// </summary>
    protected virtual void OnBeforeGlobalTeardown()
    {
    }

    /// <summary>
    /// Called after the global teardown of <see cref="AtataContext"/>.
    /// The method can be overridden to provide custom logic after global teardown.
    /// </summary>
    protected virtual void OnAfterGlobalTeardown()
    {
    }
}
