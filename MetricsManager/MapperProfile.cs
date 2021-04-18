using AutoMapper;
using MetricsManager.DAL.Models;
using MetricsManager.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MetricsManager
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<CpuMetricApi, CpuMetricApiDto>();
            CreateMap<GcHeapSizeMetricApi, GcHeapSizeMetricApiDto>();
            CreateMap<HddMetricApi, HddMetricApiDto>();
            CreateMap<NetworkMetricApi, NetworkMetricApiDto>();
            CreateMap<RamMetricApi, RamMetricApiDto>();
            /*CreateMap<RamMetric, RamMetricsGetLastResponse>();
            CreateMap<HddMetric, HddMetricsGetLastResponse>();

            CreateMap<CpuMetricCreateRequest, CpuMetric>();
            CreateMap<DotNetMetricCreateRequest, DotNetMetric>();
            CreateMap<HddMetricCreateRequest, HddMetric>();
            CreateMap<NetworkMetricCreateRequest, NetworkMetric>();
            CreateMap<RamMetricCreateRequest, RamMetric>();*/
        }
    }
}
