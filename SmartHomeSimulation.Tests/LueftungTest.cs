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
            Wetterdaten wetterdaten = new Wetterdaten { Aussentemperatur = 20, Regen = true };
            Schlafzimmer zimmer = new Schlafzimmer
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
            Wetterdaten wetterdaten = new Wetterdaten { Aussentemperatur = 20, Regen = false };
            Wohnzimmer zimmer = new Wohnzimmer
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
            Wetterdaten wetterdaten = new Wetterdaten { Aussentemperatur = 25, Regen = false };
            BadWC zimmer = new BadWC
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
            Wetterdaten wetterdaten = new Wetterdaten { Aussentemperatur = 20, Regen = false };
            Schlafzimmer zimmer = new Schlafzimmer
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
