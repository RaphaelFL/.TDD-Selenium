
namespace AluraByteBank.WebApp.Test.Util
{
    public class Fixture : IDisposable
    {
        public IWebDriver Driver { get; set; }
        public Fixture()
        {
            Driver = new ChromeDriver(Helper.CaminhoDriverNavegador());
        }
        public void Dispose()
        {
            Driver.Quit();
        }
    }
}
