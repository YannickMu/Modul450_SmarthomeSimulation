using System;
using System.IO;
using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests;

[TestClass]
[TestSubject(typeof(ZimmerMitJalousiesteuerung))]
public class ZimmerMitJalousiesteuerungTest
{
    private Schlafzimmer testZimmer = new Schlafzimmer();

    [TestMethod]
    public void TestVerarbeiteWetterdaten_AussentemperaturGroesserTeperaturvorgabeUndPersonInZimmer_JalousieLassen()
    {
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 25;
        wetterdaten.Regen = false;
        wetterdaten.Windgeschwindigkeit = 0;
        
        ZimmerMitJalousiesteuerung testObj = new ZimmerMitJalousiesteuerung(testZimmer);
        testObj.PersonenImZimmer = true;
        testObj.Temperaturvorgabe = 23;
        
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsFalse(testObj.JalousieHeruntergefahren);
        Assert.AreEqual($"{testObj.Name}: Jalousie kann nicht geschlossen werden weil Personen im Zimmer sind.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", stringWriter.ToString());
    }
    
    [TestMethod]
    public void TestVerarbeiteWetterdaten_AussentemperaturGroesserTeperaturvorgabeOhnePersonInZimmer_JalousieLassen()
    {
        Wetterdaten wetterdaten = new Wetterdaten();
        wetterdaten.Aussentemperatur = 25;
        wetterdaten.Regen = false;
        wetterdaten.Windgeschwindigkeit = 0;
        
        ZimmerMitJalousiesteuerung testObj = new ZimmerMitJalousiesteuerung(testZimmer);
        testObj.PersonenImZimmer = false;
        testObj.Temperaturvorgabe = 23;
        
        StringWriter stringWriter = new StringWriter();
        Console.SetOut(stringWriter);
        
        testObj.VerarbeiteWetterdaten(wetterdaten);
        
        Assert.IsTrue(testObj.JalousieHeruntergefahren);
        Assert.AreEqual($"{testObj.Name}: Jalousie wird geschlossen.\nWetterdaten für {testObj.Name} verarbeitet: Temperaturvorgabe: {testObj.Temperaturvorgabe}°C, Personen im Zimmer: {(testObj.PersonenImZimmer ? "ja" : "nein")}.\n", stringWriter.ToString());
    }
}