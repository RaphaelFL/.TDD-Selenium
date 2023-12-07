
namespace AluraByteBank.WebApp.Test.PageObjects
{
    public class LoginPO
    {
        private IWebDriver driver;
        private By CampoEmail;
        private By CampoSenha;
        private By btnLogar;

        public LoginPO(IWebDriver driver)
        {
            this.driver = driver;
            CampoEmail = By.Id("Email");
            CampoSenha = By.Id("Senha");
            btnLogar = By.Id("btn-logar");
        }

        public void Navegar(string url)
        {
            driver.Navigate().GoToUrl(url);
        }

        public void PreencherCampos(string email, string senha)
        {
            driver.FindElement(CampoSenha).SendKeys(senha);
            driver.FindElement(CampoEmail).SendKeys(email);
        }
        public void Logar()
        {
            driver.FindElement(btnLogar).Click();
        }
    }
}
