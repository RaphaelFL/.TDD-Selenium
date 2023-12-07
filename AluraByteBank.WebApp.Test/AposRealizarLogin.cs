

using AluraByteBank.WebApp.Test.PageObjects;
using System.Security.Cryptography;
using Xunit.Abstractions;

namespace AluraByteBank.WebApp.Test
{
    public class AposRealizarLogin
    {
        private readonly IWebDriver driver;
        private readonly ITestOutputHelper outputHelper;

        public AposRealizarLogin(ITestOutputHelper _outputHelper)
        {
            //Arrange
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            outputHelper = _outputHelper;
        }
        [Fact]
        public void AposRealizarLoginVerificarOpcoes()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var login = driver.FindElement(By.Id("Email"));
            var senha = driver.FindElement(By.Id("Senha"));
            var btnLogar = driver.FindElement(By.Id("btn-logar"));

            login.SendKeys("andre@email.com");
            senha.SendKeys("senha01");

            //Act
            btnLogar.Click();

            //Assert
            Assert.Contains("Agencia", driver.PageSource);
        }

        [Fact]
        public void tentaRealizarLoginSemPreencher()
        {
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            var logi = driver.FindElement(By.Id("Email"));
            var senha = driver.FindElement(By.Id("Senha"));
            var bntLogar = driver.FindElement(By.Id("btn-logar"));

            bntLogar.Click();

            Assert.Contains("The Email field is required.", driver.PageSource);
            Assert.Contains("The Senha field is required.", driver.PageSource);
        }

        [Fact]
        public void RealizarLoginAcessaListaDeContas()
        {
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");
            
            driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            driver.FindElement(By.Id("Senha")).SendKeys("senha01");
            driver.FindElement(By.Id("btn-logar")).Click();

            driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elemente = driver.FindElements(By.TagName("a"));

            foreach( IWebElement e in elemente)
            {
                outputHelper.WriteLine(e.Text);
            }
            //Assert
            Assert.True(elemente.Count == 36);

        }
        [Fact]
        public void OlhaSeTemLoginAcessaListaDeContasBotãoVoltar()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/UsuarioApps/Login");

            driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            driver.FindElement(By.Id("Senha")).SendKeys("senha01");
            driver.FindElement(By.Id("btn-logar")).Click();

            driver.FindElement(By.Id("contacorrente")).Click();

            IReadOnlyCollection<IWebElement> elements = driver.FindElements(By.TagName("a"));

            var elemente = (from WebElement in elements
                            where WebElement.Text.Contains("Detalhes")
                            select WebElement).First();
            //ACT
            elemente.Click();
            //Assert
            Assert.Contains("Voltar", driver.PageSource);

        }


        [Fact]
        public void AposRealizarLoginVerificarExistenciaPageObjet()
        {
            //Arrange
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            //Act
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.Logar();

            //Assert
            Assert.Contains("Agencia", driver.PageSource);

        }
        [Fact]
        public void RealizarLoginAcessaListaDeContasPageObjet()
        {
            var loginPO = new LoginPO(driver);
            loginPO.Navegar("https://localhost:44309/UsuarioApps/Login");
            loginPO.PreencherCampos("andre@email.com", "senha01");
            loginPO.Logar();
            var homePO = new HomePO(driver);

            homePO.LinkContaCorrenteClick();

            Assert.Contains("Adicionar Agência", driver.PageSource);

        }
    }
}
