using System;
using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests;

[TestClass]
[TestSubject(typeof(ZimmerMitAktor))]
public class ZimmerMitAktorTest
{
    ZimmerMitAktor testZimmerMitAktor;
    
    [TestMethod]
    public void TestGetZimmerMitAktor()
    {
        testZimmerMitAktor = new ZimmerMitMarkisensteuerung(new Wohnzimmer());
        ZimmerMitAktor testZimmerMitHeizung = new ZimmerMitHeizungsventil(testZimmerMitAktor);
        
        Assert.AreEqual(testZimmerMitAktor, testZimmerMitHeizung.GetZimmerMitAktor<ZimmerMitMarkisensteuerung>());
        Assert.IsNull(testZimmerMitHeizung.GetZimmerMitAktor<Schlafzimmer>());
    }
}