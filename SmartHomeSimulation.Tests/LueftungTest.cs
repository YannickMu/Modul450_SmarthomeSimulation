using Microsoft.VisualStudio.TestTools.UnitTesting;
using M320_SmartHome;

namespace SmartHomeSimulation.Tests
{
    [TestClass]
    public class LueftungTest
    {
        [TestMethod]
        public void TestLueftungAusEsRegnet()
        {
            // Arrange
            var wetterdaten = new Wetterdaten { Aussentemperatur = 20, Regen = true };
            var zimmer = new Schlafzimmer
            {
                Temperaturvorgabe = 25,
                PersonenImZimmer = true,
                Wetter = wetterdaten
            };

            // Act
            zimmer.PruefeLueftung();

            // Assert
            Assert.IsFalse(zimmer.LueftungAn);
        }

        [TestMethod]
        public void TestLueftungAusKeinePersonImZimmer()
        {
            // Arrange
            var wetterdaten = new Wetterdaten { Aussentemperatur = 20, Regen = false };
            var zimmer = new Wohnzimmer
            {
                Temperaturvorgabe = 25,
                PersonenImZimmer = false,
                Wetter = wetterdaten
            };

            // Act
            zimmer.PruefeLueftung();

            // Assert
            Assert.IsFalse(zimmer.LueftungAn);
        }

        [TestMethod]
        public void TestLueftungAusAussentemperaturHoeher()
        {
            // Arrange
            var wetterdaten = new Wetterdaten { Aussentemperatur = 25, Regen = false };
            var zimmer = new BadWC
            {
                Temperaturvorgabe = 20,
                PersonenImZimmer = true,
                Wetter = wetterdaten
            };

            // Act
            zimmer.PruefeLueftung();

            // Assert
            Assert.IsFalse(zimmer.LueftungAn);
        }

        [TestMethod]
        public void TestLueftungEin()
        {
            // Arrange
            var wetterdaten = new Wetterdaten { Aussentemperatur = 20, Regen = false };
            var zimmer = new Schlafzimmer
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
