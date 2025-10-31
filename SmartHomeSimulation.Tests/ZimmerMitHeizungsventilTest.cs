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
    public void TestTemperaturvorgabe()
    {
        int temperaturvorgabe = 23;
        
        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        
        testObj.Temperaturvorgabe = temperaturvorgabe;
        
        Assert.AreEqual(temperaturvorgabe, testObj.Temperaturvorgabe);
    }

    [TestMethod]
    public void TestPersonenImZimmer()
    {
        bool personenImZimmer = true;
        
        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        
        testObj.PersonenImZimmer = personenImZimmer;
        
        Assert.AreEqual(personenImZimmer,  testObj.PersonenImZimmer);
    }

    [TestMethod]
    public void TestVerarbeitetWetterdaten()
    {
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 23;
        wetterdaten.Regen = false;
        wetterdaten.Windgeschwindigkeit = 30;

        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        testObj.Temperaturvorgabe = 25;
        testObj.PersonenImZimmer = true;
        
        StringWriter stringWriter = new StringWriter();
        StringWriter stringWriterClose = new StringWriter();
        Console.SetOut(stringWriter);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.AreEqual($"{testObj.Name}: Heizungsventil wird geöffnet.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", stringWriter.ToString());
        Assert.IsTrue(testObj.HeizungsventilOffen);

        testObj.Temperaturvorgabe = 20;
        Console.SetOut(stringWriterClose);
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.AreEqual($"{testObj.Name}: Heizungsventil wird geschlossen.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", stringWriterClose.ToString());
    }
}