using System;
using System.IO;
using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests;

[TestClass]
[TestSubject(typeof(ZimmerMitHeizungsventil))]
public class ZimmerMitHeizungsventilTest
{

    private Wohnzimmer testZimmer = new Wohnzimmer();

    [TestMethod]
    public void TestVerarbeitetWetterdaten_TemperaturvorgabeGrösserIstTemperatur_OeffneHeizventil()
    {
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 23;
        wetterdaten.Regen = false;
        wetterdaten.Windgeschwindigkeit = 30;

        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        testObj.Temperaturvorgabe = 25;
        testObj.PersonenImZimmer = true;
        
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.AreEqual($"{testObj.Name}: Heizungsventil wird geöffnet.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", stringWriter.ToString());
        Assert.IsTrue(testObj.HeizungsventilOffen);
    }
    
    [TestMethod]
    public void TestVerarbeitWetterdaten_TemperaturvorgabeKleinerIstTempeatur_SchliesseHeizventil()
    {
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 23;
        wetterdaten.Regen = false;
        wetterdaten.Windgeschwindigkeit = 30;
        
        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        testObj.Temperaturvorgabe = 25;
        testObj.PersonenImZimmer = true;
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        Assert.IsTrue(testObj.HeizungsventilOffen);
        
        testObj.Temperaturvorgabe = 20;
        
        StringWriter stringWriter = new StringWriter();

        Console.SetOut(stringWriter);
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.AreEqual($"{testObj.Name}: Heizungsventil wird geschlossen.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", stringWriter.ToString());
        Assert.IsFalse(testObj.HeizungsventilOffen);
    }

    [TestMethod]
    public void TestVerarbeitetWetterdaten_TemperaturvorgabeGleichIstTemperatur_Verarbeitet()
    {
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 23;
        wetterdaten.Regen = false;
        wetterdaten.Windgeschwindigkeit = 30;
        
        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        testObj.Temperaturvorgabe = 22;
        testObj.PersonenImZimmer = true;
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        Assert.IsFalse(testObj.HeizungsventilOffen);
        
        testObj.Temperaturvorgabe = 23;
        
        StringWriter stringWriter = new StringWriter();

        Console.SetOut(stringWriter);
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.AreEqual($"Wetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", stringWriter.ToString());
        Assert.IsFalse(testObj.HeizungsventilOffen);
    }
}