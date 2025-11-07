using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests;

[TestClass]
[TestSubject(typeof(Zimmer))]
public class ZimmerTest
{
    Zimmer testZimmer = new Wohnzimmer();

    [TestMethod]
    public void TestTemperaturvorgabeGetterSetter()
    {
        int temperaturvorgabe = 23;
        
        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        
        testObj.Temperaturvorgabe = temperaturvorgabe;
        
        Assert.AreEqual(temperaturvorgabe, testObj.Temperaturvorgabe);
    }

    [TestMethod]
    public void TestPersonenImZimmerGetterSetter()
    {
        bool personenImZimmer = true;
        
        ZimmerMitHeizungsventil testObj = new ZimmerMitHeizungsventil(testZimmer);
        
        testObj.PersonenImZimmer = personenImZimmer;
        
        Assert.AreEqual(personenImZimmer,  testObj.PersonenImZimmer);
    }

}