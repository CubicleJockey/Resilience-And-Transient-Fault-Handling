namespace Polly.Services.Tests
{
    public abstract class TestBase
    {
        protected SomeService service;

        protected TestBase()
        {
            service = new SomeService("http://localhost");
        }
    }
}
