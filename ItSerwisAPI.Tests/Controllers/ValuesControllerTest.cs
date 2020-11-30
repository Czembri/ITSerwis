using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ItSerwisAPI;
using ItSerwisAPI.Controllers;

namespace ItSerwisAPI.Tests.Controllers
{
    [TestClass]
    public class ValuesControllerTest
    {
        [TestMethod]
        public void Get()
        {
            // Przygotowanie
            ValuesController controller = new ValuesController();

            // Wykonanie
            IEnumerable<string> result = controller.Get();

            // Sprawdzenie
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }

        [TestMethod]
        public void GetById()
        {
            // Przygotowanie
            ValuesController controller = new ValuesController();

            // Wykonanie
            string result = controller.Get(5);

            // Sprawdzenie
            Assert.AreEqual("value", result);
        }

        [TestMethod]
        public void Post()
        {
            // Przygotowanie
            ValuesController controller = new ValuesController();

            // Wykonanie
            controller.Post("value");

            // Sprawdzenie
        }

        [TestMethod]
        public void Put()
        {
            // Przygotowanie
            ValuesController controller = new ValuesController();

            // Wykonanie
            controller.Put(5, "value");

            // Sprawdzenie
        }

        [TestMethod]
        public void Delete()
        {
            // Przygotowanie
            ValuesController controller = new ValuesController();

            // Wykonanie
            controller.Delete(5);

            // Sprawdzenie
        }
    }
}
