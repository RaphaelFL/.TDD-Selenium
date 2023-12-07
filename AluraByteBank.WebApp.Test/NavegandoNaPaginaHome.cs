
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.IO;
using System.Reflection;
using Xunit;

namespace Alura.ByteBank.WebApp.Testes
{
    public class NavegandoNaPaginaHome 
    {
        private readonly IWebDriver driver;

        public NavegandoNaPaginaHome( )
        {
            //Arrange
            driver = new ChromeDriver(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
        }

        [Fact]
        public void CarregaPaginaHomeEVerificaTituloDaPagina()
        {
            //Act
            driver.Navigate().GoToUrl("https://localhost:44309");
            //Assert
            Assert.Contains("WebApp", driver.Title);

        }
        [Fact]
        public void CarregarPaginaHomeVerificarExistenciaDeLinkLoginHome()
        {

            driver.Navigate().GoToUrl("https://localhost:44309");

            Assert.Contains("Login", driver.PageSource);
            Assert.Contains("Home", driver.PageSource);
        }
        [Fact]
        public void TestLogin()
        {
            driver.Navigate().GoToUrl("https://localhost:44309/");
            driver.Manage().Window.Size = new System.Drawing.Size(1486, 816);
            driver.FindElement(By.LinkText("Login")).Click();
            driver.FindElement(By.Id("Email")).Click();
            driver.FindElement(By.Id("Email")).SendKeys("andre@email.com");
            driver.FindElement(By.Id("Senha")).Click();
            driver.FindElement(By.Id("Senha")).SendKeys("senha01");
            driver.FindElement(By.Id("btn-logar")).Click();
        }

        [Fact]
        public void ValidarLoginNaHome()
        {
            //Arrange
            driver.Navigate().GoToUrl("https://localhost:44309/");
            var linkLogin = driver.FindElement(By.LinkText("Login"));
            //Act
            linkLogin.Click();
            //Assert
            Assert.Contains("img", driver.PageSource);
        }
        [Fact]
        public void AcessaPaginaSemEstarLogado()
        {
            //Arrange
            //Act 
            driver.Navigate().GoToUrl("https://localhost:44309/Agencia/Index");

            //Assert
            Assert.Contains("https://localhost:44309/Agencia/Index", driver.Url);
            Assert.Contains("401", driver.PageSource);

        }
    }
}