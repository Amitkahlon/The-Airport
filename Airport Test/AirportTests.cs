using Airport_Logic;
using Airport_Logic.Logic_Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Airport_Test
{
    [TestClass]
    public class AirportTests
    {
        [TestMethod]
        public void InitializeAndConfigure_LandingRoute_Successfull()
        {
            var airport = new AirportStatus(builder =>
            {
                //Add Stations
                builder.AddStation(1, "Station1", TimeSpan.FromSeconds(15));
                builder.AddStation(2, "Station2", TimeSpan.FromSeconds(10));
                builder.AddStation(3, "Station3", TimeSpan.FromSeconds(30));
                builder.AddStation(4, "Station4", TimeSpan.FromSeconds(5));
                builder.AddStation(5, "Station5", TimeSpan.FromSeconds(10));
                builder.AddStation(6, "Station6", TimeSpan.FromSeconds(15));
                builder.AddStation(7, "Station7", TimeSpan.FromSeconds(15));
                builder.AddStation(8, "Station8", TimeSpan.FromSeconds(40));

                //Add Routes
                builder.RegisterRoute(new LandingRoute(), "Landing");
            });

            // Arrange
            var entryPoint = airport.EntryPoints["Landing"];
            var secondLayer = entryPoint[0].ConnectedStations;
            var thirdLayer = secondLayer[0].ConnectedStations;
            var fouthLayer = thirdLayer[0].ConnectedStations;
            var fithLayer = fouthLayer[0].ConnectedStations;


            //Act
            
            //entry layer
            bool firstLayerContain8 = entryPoint.Any(station => station.StationNumber == 8);
            bool firstLayerContainOnly8 = !(entryPoint.Any(station => station.StationNumber != 8));

            //second layer
            bool secondLayerContain7 = secondLayer.Any(station => station.StationNumber == 7);
            bool secondLayerContain6 = secondLayer.Any(station => station.StationNumber == 6);
            bool secondLayerContain5 = secondLayer.Any(station => station.StationNumber == 5);
            bool secondLayerContainOnly = !(secondLayer.Any(station => station.StationNumber != 7
            && station.StationNumber != 6
            && station.StationNumber != 5));

            //third layer
            bool thirdLayerContain4 = thirdLayer.Any(station => station.StationNumber == 4);
            bool thirdLayerContain3 = thirdLayer.Any(station => station.StationNumber == 3);
            bool thirdLayerContainOnly = !(thirdLayer.Any(station => station.StationNumber != 4 && station.StationNumber != 3));

            //fourth layer
            bool fouthLayerContain2 = fouthLayer.Any(station => station.StationNumber == 2);
            bool fouthLayerContain1 = fouthLayer.Any(station => station.StationNumber == 1);
            bool fouthLayerContainOnly = !(fouthLayer.Any(station => station.StationNumber != 2 && station.StationNumber != 1));

            //fith layer
            bool fithLayerEmpty = !fithLayer.Any();

            //Assert

            Assert.IsTrue(firstLayerContain8);
            Assert.IsTrue(firstLayerContainOnly8);

            Assert.IsTrue(secondLayerContain7);
            Assert.IsTrue(secondLayerContain6);
            Assert.IsTrue(secondLayerContain5);
            Assert.IsTrue(secondLayerContainOnly);

            Assert.IsTrue(thirdLayerContain4);
            Assert.IsTrue(thirdLayerContain3);
            Assert.IsTrue(thirdLayerContainOnly);

            Assert.IsTrue(fouthLayerContain2);
            Assert.IsTrue(fouthLayerContain1);
            Assert.IsTrue(fouthLayerContainOnly);

            Assert.IsTrue(fithLayerEmpty);

        }
    }
}
