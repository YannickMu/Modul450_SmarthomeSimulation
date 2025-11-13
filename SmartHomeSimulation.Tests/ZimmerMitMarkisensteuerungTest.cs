using System;
using System.IO;
using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests;

[TestClass]
[TestSubject(typeof(ZimmerMitMarkisensteuerung))]
public class ZimmerMitMarkisensteuerungTest
{
    Wohnzimmer testZimmer = new Wohnzimmer();

    [TestMethod]
    public void TestVerarbeiteWetterdaten_AussentemperaturGrösserTemperaturvorgabeMarkiseOffenUndRegen_MarkiseKannNichtGeschlossenWerden()
    {
        ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer);
        testObj.Temperaturvorgabe = 20;
        testObj.PersonenImZimmer = true;
        
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 19;
        wetterdaten.Regen = true;
        wetterdaten.Windgeschwindigkeit = 0;
        
        StringWriter output = new StringWriter();
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsTrue(testObj.MarkiseOffen);
        wetterdaten.Aussentemperatur = 25;
        Console.SetOut(output);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsTrue(testObj.MarkiseOffen);
        Assert.AreEqual($"{testObj.Name}: Markise kann nicht geschlossen werden weils regnet.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", output.ToString());
    }

    [TestMethod]
    public void TestVerarbeiteWetterdaten_AussentemperaturGrösserTemperaturvorgabeMarkiseOffen_MarkiseSchliessen()
    {
        ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer);
        testObj.Temperaturvorgabe = 20;
        testObj.PersonenImZimmer = true;
        
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 19;
        wetterdaten.Regen = true;
        wetterdaten.Windgeschwindigkeit = 0;
        
        StringWriter output = new StringWriter();
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsTrue(testObj.MarkiseOffen);
        wetterdaten.Aussentemperatur = 25;
        wetterdaten.Regen = false;
        Console.SetOut(output);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsFalse(testObj.MarkiseOffen);
        Assert.AreEqual($"{testObj.Name}: Markise wird geschlossen.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", output.ToString());
    }

    [TestMethod]
    public void TestVerarbeiteWetterdaten_AussentemperaturKleinerTemperaturvorgabeMarkiseGeschlossenUndRegen_MarkiseWirdGeoeffnet()
    {
        ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer);
        testObj.Temperaturvorgabe = 20;
        testObj.PersonenImZimmer = true;
        
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 19;
        wetterdaten.Regen = true;
        wetterdaten.Windgeschwindigkeit = 0;
        
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsTrue(testObj.MarkiseOffen);
        Assert.AreEqual($"{testObj.Name}: Markise wird geöffnet.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", output.ToString());
    }

    [TestMethod]
    public void TestVerarbeiteWetterdaten_AussentemperaturGroesserTemperaturvorgabeMarkiseGeschlossenUndRegen_MarkiseWirdGeoeffnet()
    {
        ZimmerMitMarkisensteuerung testObj = new ZimmerMitMarkisensteuerung(testZimmer);
        testObj.Temperaturvorgabe = 20;
        testObj.PersonenImZimmer = true;
        
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 25;
        wetterdaten.Regen = true;
        wetterdaten.Windgeschwindigkeit = 0;
        
        StringWriter output = new StringWriter();
        Console.SetOut(output);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsTrue(testObj.MarkiseOffen);
        Assert.AreEqual($"{testObj.Name}: Markise wird geöffnet weils regnet.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", output.ToString());
    }
}