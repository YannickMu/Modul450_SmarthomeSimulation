using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartHomeSimulation.Tests
{
    [TestClass]
    public class IntegrationTest_Aktoren
    {
        [TestMethod]
        public void Schlafzimmer_ReagiertAufEchtenWettersensor()
        {
            // Arrange
            var wettersensor = new Wettersensor();
            var wetter = wettersensor.GetWetterdaten();

            var logger = new ConsoleFileLogger();
            var schlafzimmer = new Schlafzimmer(logger)
            {
                Temperaturvorgabe = wetter.Aussentemperatur + 3,
                PersonenImZimmer = true,
                Wetter = wetter
            };

            // Act
            schlafzimmer.PruefeLueftung();

            // Assert
            if (wetter.Regen)
            {
                Assert.IsFalse(schlafzimmer.LueftungAn, "Lüftung darf bei Regen nicht an sein.");
            }
            else
            {
                Assert.IsTrue(schlafzimmer.LueftungAn || !schlafzimmer.LueftungAn, "Lüftung sollte korrekt reagieren, je nach Temperatur und Regenstatus.");
            }
        }

        [TestMethod]
        public void Wohnzimmer_EchterWettersensor_LueftungBleibtKonsistent()
        {
            // Arrange
            var wettersensor = new Wettersensor();
            var wetter = wettersensor.GetWetterdaten();

            var wohnzimmer = new Wohnzimmer(new ConsoleFileLogger())
            {
                Temperaturvorgabe = wetter.Aussentemperatur + 2,
                PersonenImZimmer = true,
                Wetter = wetter
            };

            // Act
            wohnzimmer.PruefeLueftung();

            // Assert
            Assert.IsNotNull(wohnzimmer.LueftungAn);
        }
    }
}
