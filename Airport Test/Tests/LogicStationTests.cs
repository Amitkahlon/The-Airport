using Airport_Common.Interfaces;
using Airport_Common.Models;
using Airport_Logic.Logic_Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Airport_Test.Tests
{
    [TestClass]
    public class LogicStationTests
    {
        [TestMethod]
        public void InitializeLogicStation()
        {
            var station = new LogicStation();
        }

        [TestMethod]
        public void AddStation()
        {
            //Arrange
            var station1 = new LogicStation();
            var station2 = new LogicStation();

            //Act
            station1.AddStation(station2);

            //Assert
            Assert.IsTrue(station1.ConnectedStations[0].Equals(station2));
        }

        [TestMethod]
        public void GetStation()
        {
            //Arrange
            var station1 = new LogicStation();
            var station2 = new LogicStation()
            {
                StationNumber = 2
            };

            //Act
            station1.AddStation(station2);

            //Assert
            Assert.IsTrue(station1.GetLogicStationByNumber(2).Equals(station2));
        }

        [TestMethod]
        public void EnterStationTest()
        {
            //Arrange
            var station1 = new LogicStation();
            station1.WaitingTime = TimeSpan.FromSeconds(2);
            var plane = new Plane()
            {
                PlaneRoute = new StationTest1()
            };

            //Act
            station1.EnterStation(plane);

            //Assert
            Thread.Sleep(10);
            Assert.IsTrue(station1.CurrentPlane.Equals(plane));

            Thread.Sleep(TimeSpan.FromSeconds(2.01));
            Assert.IsTrue(station1.CurrentPlane == null);
        }

        public class StationTest1 : IRoute
        {
            public string Name => "Test1";

            public IEnumerable<int> GetNextAvailableRoute(int stationNumber)
            {
                switch (stationNumber)
                {
                    case 1:
                        yield return 0;
                        break;
                }
            }
        }
    }
}
