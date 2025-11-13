using System;
using System.IO;
using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests
{
    [TestClass]
    [TestSubject(typeof(ZimmerMitMarkisensteuerung))]
    public class ZimmerMitMarkisensteuerungTest
    {
        Wohnzimmer testZimmer = new Wohnzimmer();

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturGrösserTemperaturvorgabeMarkiseOffenUndRegen_MarkiseKannNichtGeschlossenWerden()
        {
            ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer)
            {
                Temperaturvorgabe = 20,
                PersonenImZimmer = true
            };

            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 19,
                Regen = true,
                Windgeschwindigkeit = 0
            };

            testObj.VerarbeiteWetterdaten(wetterdaten);
            Assert.IsTrue(testObj.MarkiseOffen);

            wetterdaten.Aussentemperatur = 25;
            StringWriter output = new StringWriter();
            Console.SetOut(output);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsTrue(testObj.MarkiseOffen);
            string expected = $"{testObj.Name}: Markise kann nicht geschlossen werden weils regnet.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturGrösserTemperaturvorgabeMarkiseOffen_MarkiseSchliessen()
        {
            ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer)
            {
                Temperaturvorgabe = 20,
                PersonenImZimmer = true
            };

            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 19,
                Regen = true,
                Windgeschwindigkeit = 0
            };

            testObj.VerarbeiteWetterdaten(wetterdaten);
            Assert.IsTrue(testObj.MarkiseOffen);

            wetterdaten.Aussentemperatur = 25;
            wetterdaten.Regen = false;
            StringWriter output = new StringWriter();
            Console.SetOut(output);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsFalse(testObj.MarkiseOffen);
            string expected = $"{testObj.Name}: Markise wird geschlossen.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturKleinerTemperaturvorgabeMarkiseGeschlossenUndRegen_MarkiseWirdGeoeffnet()
        {
            ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer)
            {
                Temperaturvorgabe = 20,
                PersonenImZimmer = true
            };

            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 19,
                Regen = true,
                Windgeschwindigkeit = 0
            };

            StringWriter output = new StringWriter();
            Console.SetOut(output);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsTrue(testObj.MarkiseOffen);
            string expected = $"{testObj.Name}: Markise wird geöffnet.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, output.ToString());
        }

        [TestMethod]
        public void TestVerarbeiteWetterdaten_AussentemperaturGrösserTemperaturvorgabeMarkiseGeschlossenUndRegen_MarkiseWirdGeoeffnet()
        {
            ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer)
            {
                Temperaturvorgabe = 20,
                PersonenImZimmer = true
            };

            Wetterdaten wetterdaten = new Wetterdaten
            {
                Aussentemperatur = 25,
                Regen = true,
                Windgeschwindigkeit = 0
            };

            StringWriter output = new StringWriter();
            Console.SetOut(output);

            testObj.VerarbeiteWetterdaten(wetterdaten);

            Assert.IsTrue(testObj.MarkiseOffen);
            string expected = $"{testObj.Name}: Markise wird geöffnet weils regnet.{Environment.NewLine}" +
                              $"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, " +
                              $"Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.{Environment.NewLine}";
            Assert.AreEqual(expected, output.ToString());
        }
    }
}
