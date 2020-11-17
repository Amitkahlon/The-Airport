using Airport_Common.Routes;
using Airport_Logic;
using Airport_Logic.Logic_Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Airport_Test.Mock;
using System.Threading.Tasks;
using System.Threading;
using Airport_Common.Models;
using Airport_Simulator;

namespace Airport_Test
{
    [TestClass]
    public class AirportTests
    {
        [TestMethod]
        public void InitializeAndConfigure_LandingRoute_Successfull()
        {
            var airport = new Airport(builder =>
            {
                //Add Stations
                builder.AddStation("Station1", TimeSpan.FromSeconds(15));
                builder.AddStation("Station2", TimeSpan.FromSeconds(10));
                builder.AddStation("Station3", TimeSpan.FromSeconds(30));
                builder.AddStation("Station4", TimeSpan.FromSeconds(5));
                builder.AddStation("Station5", TimeSpan.FromSeconds(10));
                builder.AddStation("Station6", TimeSpan.FromSeconds(15));
                builder.AddStation("Station7", TimeSpan.FromSeconds(15));
                builder.AddStation("Station8", TimeSpan.FromSeconds(40));

                //Add Routes
                builder.AddRoute(new LandingRoute());
            });

            // Arrange

            var entries = airport.EntryManager.GetEntryStations("Landing");
            var secondLayer = entries[0].ConnectedStations;
            var thirdLayer = secondLayer[0].ConnectedStations;
            var fouthLayer = thirdLayer[0].ConnectedStations;
            var fithLayer = fouthLayer[0].ConnectedStations;


            //Act

            //entry layer
            bool firstLayerContain8 = entries.Any(station => station.StationNumber == 8);
            bool firstLayerContainOnly8 = !(entries.Any(station => station.StationNumber != 8));

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

        [TestMethod]
        public void PushPlanes_DefualtAirport_1Plane()
        {
            //Arrange
            var airport = new Airport(builder =>
            {
                builder.AddDefualtStations();
                builder.AddDefualtRoute();
            });

            IPlaneMaker MockSimulator = new PlaneMakerMock(airport);


            var station8 = airport.EntryManager.GetEntryStations("Landing").First();
            var station7 = station8.ConnectedStations.First();
            var station4 = station7.ConnectedStations.First();
            var station2 = station4.ConnectedStations.First();

            //Act 
            MockSimulator.PushPlane();

            Thread.Sleep(100);
            Assert.IsTrue(station8.CurrentPlane.FlightNumber == "0"); //entry point

            Thread.Sleep(15005); // wait 15 seconds
            Assert.IsTrue(station8.CurrentPlane == null);
            Assert.IsTrue(station7.CurrentPlane.FlightNumber == "0");

            Thread.Sleep(20005); // Wait 20 seconds
            Assert.IsTrue(station7.CurrentPlane == null);
            Assert.IsTrue(station4.CurrentPlane.FlightNumber == "0");

            Thread.Sleep(5005); // Wait 5 seconds
            Assert.IsTrue(station4.CurrentPlane == null);
            Assert.IsTrue(station2.CurrentPlane.FlightNumber == "0");

            Thread.Sleep(10005); // Wait 10 seconds
            Assert.IsTrue(station2.CurrentPlane == null);
        }

        [TestMethod]
        public void PushPlanes_DefualtAirport_1Plane_CheckIfDisposePlaneBeforeTime()
        {
            //Arrange
            var airport = new Airport(builder =>
            {
                builder.AddDefualtStations();
                builder.AddDefualtRoute();
            });

            IPlaneMaker MockSimulator = new PlaneMakerMock(airport);

            var station8 = airport.EntryManager.GetEntryStation("Landing", 8);
            var station7 = station8.GetLogicStationByNumber(7);
            var station4 = station7.GetLogicStationByNumber(4);
            var station2 = station4.GetLogicStationByNumber(2);



            //Act 
            MockSimulator.PushPlane();

            Thread.Sleep(100);
            Assert.IsTrue(station8.CurrentPlane.FlightNumber == "0"); // station 1

            Thread.Sleep(15005); // wait 15 seconds
            Assert.IsTrue(station8.CurrentPlane == null);
            Assert.IsTrue(station7.CurrentPlane.FlightNumber == "0"); // station 7

            Thread.Sleep(20005); // Wait 20 seconds
            Assert.IsTrue(station7.CurrentPlane == null);
            Assert.IsTrue(station4.CurrentPlane.FlightNumber == "0"); // station 4

            Thread.Sleep(5005); // Wait 5 seconds
            Assert.IsTrue(station4.CurrentPlane == null);
            Assert.IsTrue(station2.CurrentPlane.FlightNumber == "0"); // station 2

            Thread.Sleep(5000); // Wait 5 seconds **wrong
            Assert.IsFalse(station2.CurrentPlane == null);
        }

        [TestMethod]
        public void PushPlane_DefualtAirport_3Planes_3Layers()
        {
            //Arrange
            var airport = new Airport(builder =>
            {
                builder.AddDefualtStations();
                builder.AddDefualtRoute();
            });

            IPlaneMaker MockSimulator = new PlaneMakerMock(airport);

            var station8 = airport.EntryManager["Landing"].First();
            var station7 = station8.GetLogicStationByNumber(7);
            var station6 = station8.GetLogicStationByNumber(6);
            var station4 = station7.GetLogicStationByNumber(4);


            //Act 
            MockSimulator.PushPlane();

            Thread.Sleep(100);

            MockSimulator.PushPlane();

            Thread.Sleep(100);

            MockSimulator.PushPlane();

            Thread.Sleep(100);


            void FirstStage()
            {
                Assert.IsTrue(station8.CurrentPlane.FlightNumber == "0");
                int id = 1;
                foreach (var plane in station8.WaitingLine)
                {
                    Assert.IsTrue(plane.FlightNumber == id.ToString());
                    id++;
                }
            }
            FirstStage();

            Thread.Sleep(TimeSpan.FromSeconds(15.3));

            void SecondStage()
            {
                Assert.IsTrue(station8.CurrentPlane.FlightNumber == "1");

                station8.WaitingLine.TryPeek(out Plane waitngPlane8);
                Assert.IsTrue(waitngPlane8.FlightNumber == "2");

                Assert.IsTrue(station7.CurrentPlane.FlightNumber == "0");
            }
            SecondStage();

            Thread.Sleep(TimeSpan.FromSeconds(20.1));

            void ThirdStage()
            {
                Assert.IsTrue(station8.CurrentPlane.FlightNumber == "2");
                Assert.IsTrue(station8.WaitingLine.IsEmpty);

                Assert.IsTrue(station6.CurrentPlane.FlightNumber == "1"); //station 6 actually
                Assert.IsTrue(station6.WaitingLine.IsEmpty);

                Assert.IsTrue(station4.CurrentPlane.FlightNumber == "0");
                Assert.IsTrue(station4.WaitingLine.IsEmpty);
            }
            ThirdStage();
        }
    }
}
