using System;
using AutoMapper;
using BenchmarkDotNet.Attributes;
using TypeCaster.TestClasses;

namespace TypeCaster.Benchmark
{
    [MarkdownExporterAttribute.GitHub]
    public class TypeCastBenchmark
    {
        private readonly TestClassA _testClass = TestClassA.RandomClass();

        private readonly IMapper _mapper;

        public TypeCastBenchmark()
        {
            _mapper = ConfigMapper().CreateMapper();
        }

        [Benchmark]
        public TestClassB TypeCaster() => CastToClass<TestClassB>.From(_testClass);

        [Benchmark]
        public TestClassB ManualMap()
        {
            return new()
            {
                Age = _testClass.Age,
                Animal = _testClass.Animal.ToString(),
                Id = _testClass.Id,
                Name = _testClass.Name,
                Vehicle = ParseEnum(_testClass.Vehicle),
                Year = (int)_testClass.Year,
                IsCorrect = _testClass.IsCorrect ? 1 : 0,
                IsIncorrect = _testClass.IsCorrect
            };
        }

        [Benchmark]
        public TestClassB AutoMap() => _mapper.Map<TestClassB>(_testClass);

        private TestEnumB ParseEnum(string enumName)
        {
            return enumName switch
            {
                "Bicycle" => TestEnumB.Bicycle,
                "Motorcycle" => TestEnumB.Motorcycle,
                "Car" => TestEnumB.Car,
                "Truck" => TestEnumB.Truck,
                _ => throw new ArgumentOutOfRangeException(nameof(enumName), enumName, null)
            };
        }

        private MapperConfiguration ConfigMapper()
        {
            return new MapperConfiguration(cfg =>
                cfg.CreateMap<TestClassA, TestClassB>()
                    .ForMember(
                        dest => dest.Vehicle,
                        opt => opt.MapFrom((src) => ParseEnum(src.Vehicle)))
                    .ForMember(
                        dest => dest.Animal,
                        opt => opt.MapFrom((src) => src.Animal.ToString()))
                    .ForMember(
                        dest => dest.IsCorrect,
                        opt => opt.MapFrom((src) => src.IsCorrect)));
        }
    }
}