﻿using System;
using Moq;
using PulsePI.DataContracts;
using FluentAssertions;
using PulsePI.Service.ServiceInterfaces;
using Xunit;

namespace PulseTest.ServiceTests
{
    public class HeartRateRecordServiceTests
    {
        private Mock<IHeartRateRecordService> mockHeartRateRecordService = new Mock<IHeartRateRecordService>();

        [Fact]
        public void HeartRateRecordService_RecordHeartRate_Test()
        {
            //arrange 
            var heartRateRecordData = new HeartRateRecordData()
            {

                username = "Username",
                type = "Exercise",
                startTime = 2147483647,
                endTime = 22 / 12 / 2222,
                bpmLow = 50.0,
                bpmHigh = 150.0,
                bpmAvg = 100.0
            };

            //act 
            var response = mockHeartRateRecordService.Object.RecordHeartRate(heartRateRecordData);

            //assert
            Assert.True(response.IsCompletedSuccessfully);
            mockHeartRateRecordService.Verify();
        }

    }

}
