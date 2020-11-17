using System;

namespace Airport_Simulator
{
    public interface IPlaneMaker
    {
        /// <summary>
        /// Set the timer that pushes airplanes
        /// </summary>
        /// <param name="millTime">The time between each event trigger in milliseconds</param>
        void ConfigureTimer(double intervalMills);
        /// <summary>
        /// Set the timer that pushes airplanes
        /// </summary>
        /// <param name="intervalTime">The time between each event trigger</param>
        void ConfigureTimer(TimeSpan intervalTime);
        /// <summary>
        /// Pushes a plane into the airport
        /// </summary>
        void PushPlane();
        /// <summary>
        /// Start the timer that pushes planes
        /// </summary>
        void StartTimer();
        /// <summary>
        /// Stop the timer that pushes planes
        /// </summary>
        void StopTimer();
    }
}