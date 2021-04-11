using AutoMapper;
using MetricsAgent.DTO;
using MetricsAgent.Models;
using MetricsAgent.Requests;
using MetricsAgent.Responses;
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
            CreateMap<RamMetric, RamMetricsGetLastResponse>();
            CreateMap<HddMetric, HddMetricsGetLastResponse>();

            CreateMap<CpuMetricCreateRequest, CpuMetric>();
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>();
            CreateMap<HddMetricCreateRequest, HddMetric>();
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>();
            CreateMap<RamMetricCreateRequest, RamMetric>();
        }

    }
}
