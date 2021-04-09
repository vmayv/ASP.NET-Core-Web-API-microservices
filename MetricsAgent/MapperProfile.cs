using AutoMapper;
using MetricsAgent.DTO;
using MetricsAgent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsAgent
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetric, CpuMetricDto>();
            CreateMap<DotNetMetric, DotNetMetricDto>();
            CreateMap<HddMetric, HddMetricDto>();
            CreateMap<NetworkMetric, NetworkMetricDto>();
            CreateMap<RamMetric, RamMetricDto>();

            CreateMap<CpuMetricDto, CpuMetric>();
            CreateMap<DotNetMetricDto, DotNetMetric>();
            CreateMap<HddMetricDto, HddMetric>();
            CreateMap<NetworkMetricDto, NetworkMetric>();
            CreateMap<RamMetricDto, RamMetric>();
        }

    }
}
