using System;
using System.IO;
using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests
{
    [TestClass]
    [TestSubject(typeof(ZimmerMitHeizungsventil))]
    public class ZimmerMitHeizungsventilTest
    {
        private Wohnzimmer testZimmer = new Wohnzimmer();

        [TestMethod]
        public void TestVerarbeiteWetterdaten_TemperaturvorgabeGrösserIstTemperatur_OeffneHeizventil()
        {
            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 23,
                Regen = false,
                Windgeschwindigkeit = 30
            };

            ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer)
            {
                Temperaturvorgabe = 25,
                PersonenImZimmer = true
            };

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            string expected = $"{testObj.Name}: Heizungsventil wird geöffnet.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";

            Assert.AreEqual(expected, stringWriter.ToString());
            Assert.IsTrue(testObj.HeizungsventilOffen);
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_TemperaturvorgabeKleinerIstTemperatur_SchliesseHeizventil()
        {
            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 23,
                Regen = false,
                Windgeschwindigkeit = 30
            };

            ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer)
            {
                Temperaturvorgabe = 25,
                PersonenImZimmer = true
            };

            testObj.VerarbeiteWetterdaten(wetterdaten);
            Assert.IsTrue(testObj.HeizungsventilOffen);

            testObj.Temperaturvorgabe = 20;

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            testObj.VerarbeiteWetterdaten(wetterdaten);

            string expected = $"{testObj.Name}: Heizungsventil wird geschlossen.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";

            Assert.AreEqual(expected, stringWriter.ToString());
            Assert.IsFalse(testObj.HeizungsventilOffen);
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_TemperaturvorgabeGleichIstTemperatur_Verarbeitet()
        {
            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 23,
                Regen = false,
                Windgeschwindigkeit = 30
            };

            ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer)
            {
                Temperaturvorgabe = 22,
                PersonenImZimmer = true
            };

            testObj.VerarbeiteWetterdaten(wetterdaten);
            Assert.IsFalse(testObj.HeizungsventilOffen);

            testObj.Temperaturvorgabe = 23;

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            testObj.VerarbeiteWetterdaten(wetterdaten);

            string expected = $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";

            Assert.AreEqual(expected, stringWriter.ToString());
            Assert.IsFalse(testObj.HeizungsventilOffen);
        }
    }
}
