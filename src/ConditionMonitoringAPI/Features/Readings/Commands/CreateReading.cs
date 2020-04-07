using AutoMapper;
using ConditionMonitoringAPI.Dtos;
using ConditionMonitoringCore;
using Domain.Interfaces;
using Domain.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ConditionMonitoringAPI.Features.Readings.Commands
{
    public class CreateReading : IRequest<ISensorReading>
    {
        public RawSensorReadingDto RawSensorReadingDto { get; }
        public Board Board { get; set; }

        public CreateReading(RawSensorReadingDto rawSensorReadingDto, Board board)
        {
            RawSensorReadingDto = rawSensorReadingDto;
            Board = board;
        }
    }
}
