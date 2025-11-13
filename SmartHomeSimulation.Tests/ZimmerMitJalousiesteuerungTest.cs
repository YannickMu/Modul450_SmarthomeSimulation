using System;
using System.IO;
using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests
{
    [TestClass]
    [TestSubject(typeof(ZimmerMitJalousiesteuerung))]
    public class ZimmerMitJalousiesteuerungTest
    {
        private Schlafzimmer testZimmer = new Schlafzimmer();

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturGroesserTemperaturvorgabeUndPersonInZimmer_JalousieLassen()
        {
            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 25,
                Regen = false,
                Windgeschwindigkeit = 0
            };

            ZimmerMitJalousiesteuerung testObj = new ZimmerMitJalousiesteuerung(testZimmer)
            {
                PersonenImZimmer = true,
                Temperaturvorgabe = 23
            };

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsFalse(testObj.JalousieHeruntergefahren);
            string expected = $"{testObj.Name}: Jalousie kann nicht geschlossen werden weil Personen im Zimmer sind.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturGroesserTemperaturvorgabeOhnePersonInZimmer_JalousieSchliessen()
        {
            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 25,
                Regen = false,
                Windgeschwindigkeit = 0
            };

            ZimmerMitJalousiesteuerung testObj = new ZimmerMitJalousiesteuerung(testZimmer)
            {
                PersonenImZimmer = false,
                Temperaturvorgabe = 23
            };

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsTrue(testObj.JalousieHeruntergefahren);
            string expected = $"{testObj.Name}: Jalousie wird geschlossen.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturKleinerTemperaturvorgabeJalousieGeschlossen_JalousieOeffnen()
        {
            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 23,
                Regen = false,
                Windgeschwindigkeit = 0
            };

            ZimmerMitJalousiesteuerung testObj = new ZimmerMitJalousiesteuerung(testZimmer)
            {
                PersonenImZimmer = false,
                Temperaturvorgabe = 22
            };

            testObj.VerarbeiteWetterdaten(wetterdaten);
            Assert.IsTrue(testObj.JalousieHeruntergefahren);

            testObj.Temperaturvorgabe = 24;

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);
            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsFalse(testObj.JalousieHeruntergefahren);
            string expected = $"{testObj.Name}: Jalousie wird geöffnet.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, stringWriter.ToString());
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturKleinerTemperaturvorgabeJalousieOffen_JalousieLassen()
        {
            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 23,
                Regen = false,
                Windgeschwindigkeit = 0
            };

            ZimmerMitJalousiesteuerung testObj = new ZimmerMitJalousiesteuerung(testZimmer)
            {
                PersonenImZimmer = false,
                Temperaturvorgabe = 24
            };

            StringWriter stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsFalse(testObj.JalousieHeruntergefahren);
            string expected = $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, stringWriter.ToString());
        }
    }
}
