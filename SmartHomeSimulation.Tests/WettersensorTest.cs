using JetBrains.Annotations;
using M320_SmartHome;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SmartHomeSimulation.Tests;

[TestClass]
[TestSubject(typeof(Wettersensor))]
public class WettersensorTest
{

    [TestMethod]
    public void TestGetWetterdaten()
    {
        // Arrange
        Wettersensor wettersensor = new Wettersensor();
        
        // Act
        Wetterdaten wetterdaten = wettersensor.GetWetterdaten();
        
        // Assert
        Assert.IsInstanceOfType<Wetterdaten>(wetterdaten);
    }
}