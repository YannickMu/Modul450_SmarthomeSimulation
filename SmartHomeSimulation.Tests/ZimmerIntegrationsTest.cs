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
    public class IntegrationTest_Zimmer
    {
        [TestMethod]
        public void MehrereZimmer_VerarbeitenWetterdaten_Korrekt()
        {
            // Arrange
            var wetter = new Wetterdaten { Aussentemperatur = 15, Regen = false };
            var logger = new ConsoleFileLogger();

            var zimmerListe = new List<ILueftung>
            {
                new Schlafzimmer(logger) { Temperaturvorgabe = 22, PersonenImZimmer = true, Wetter = wetter },
                new Wohnzimmer(logger) { Temperaturvorgabe = 21, PersonenImZimmer = false, Wetter = wetter },
                new BadWC(logger) { Temperaturvorgabe = 23, PersonenImZimmer = true, Wetter = wetter }
            };

            // Act
            foreach (var zimmer in zimmerListe)
                zimmer.PruefeLueftung();

            // Assert
            Assert.IsTrue(((Schlafzimmer)zimmerListe[0]).LueftungAn, "Schlafzimmer sollte lüften");
            Assert.IsFalse(((Wohnzimmer)zimmerListe[1]).LueftungAn, "Wohnzimmer sollte nicht lüften (keine Person)");
            Assert.IsTrue(((BadWC)zimmerListe[2]).LueftungAn, "BadWC sollte lüften");
        }
    }
}
