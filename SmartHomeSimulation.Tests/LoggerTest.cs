using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.IO;

namespace SmartHomeSimulation.Tests
{
    [TestClass]
    public class LoggerTest
    {
        [TestMethod]
        public void TestLueftungEinMitLogger()
        {
            // Arrange
            var logger = new SmartHomeSimulation.ConsoleFileLogger();
            var wetterdaten = new Wetterdaten { Aussentemperatur = 20, Regen = false };
            Schlafzimmer zimmer = new Schlafzimmer(logger)
            {
                Temperaturvorgabe = 25,
                PersonenImZimmer = true,
                Wetter = wetterdaten
            };

            // Act
            zimmer.PruefeLueftung();

            // Assert
            Assert.IsTrue(zimmer.LueftungAn);
        }
    }
}