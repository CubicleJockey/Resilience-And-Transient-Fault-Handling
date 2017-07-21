namespace Polly.Services.Tests
{
    public class TestBase
    {
        protected SomeService service;

        public TestBase()
        {
            service = new SomeService("http://localhost");
        }
    }
}
