// -----------------------------------------------------------------------
// <copyright file="AutoMapperExtensions.cs" company="Microsoft">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace WebApp.Common.Mappers
{
    using AutoMapper;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public static class AutoMapperExtensions
    {
        public static TOutput MapFrom<TInput, TOutput>(TInput input)
        {
            return Mapper.Map<TInput, TOutput>(input);
        }

        public static void MapInto<TInput, TOutput>(TInput input, TOutput output)
        {
            Mapper.Map(input, output);
        }

        public static TOutput MapFrom<TInput1, TInput2, TOutput>(TInput1 input1, TInput2 input2)
        {
            var result = Mapper.Map<TInput1, TOutput>(input1);
            Mapper.Map(input2, result);

            return result;
        }

        public static void MapInto<TInput1, TInput2, TOutput>(TInput1 input1, TInput2 input2, TOutput output)
        {
            var result = Mapper.Map<TInput1, TOutput>(input1);
            Mapper.Map(result, output);
        }
    }
}
