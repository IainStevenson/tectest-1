namespace tectest1.Api.Domain.Tests
{
    public abstract class TestBase<T>
    {
        protected T Unit;
        [SetUp]
        public virtual void Setup()
        {
            Unit = Activator.CreateInstance<T>();
        }

        [TearDown]
        public virtual void Teardown()
        {
            (Unit as IDisposable)?.Dispose();
        }
    }
}