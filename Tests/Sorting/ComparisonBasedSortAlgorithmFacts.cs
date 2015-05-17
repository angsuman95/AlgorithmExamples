﻿using System;
using System.Collections.Generic;
using System.Linq;
using AlgorithmExamples.Sorting;
using Tests.Sorting.Input;
using Xunit;


namespace Tests.Sorting
{
    public class IntegerBasedSortAlgorithmFacts :
        ComparisonBasedSortAlgorithmFacts<IntegerSortDataSource, int>
    {
    }

    public class StringBasedSortAlgorithmFacts :
        ComparisonBasedSortAlgorithmFacts<StringSortDataSource, string>
    {
    }

    public abstract class ComparisonBasedSortAlgorithmFacts<TDataSourceType, TDataType>
        where TDataSourceType : SortDataSource<TDataType>, new()
        where TDataType : IComparable<TDataType>
    {
        [Theory, MemberData("GetData")]
        public void Should_Sort_Data_Correctly_Ascending(
            IComparisonBasedSortAlgorithm<TDataType> sortingAlgorithm,
            IReadOnlyList<TDataType> input)
        {
            TestSorting(sortingAlgorithm, input, SortDirection.Ascending);
        }

        [Theory, MemberData("GetData")]
        public void Should_Sort_Data_Correctly_Descending(
            IComparisonBasedSortAlgorithm<TDataType> sortingAlgorithm,
            IReadOnlyList<TDataType> input)
        {
            TestSorting(sortingAlgorithm, input, SortDirection.Descending);
        }


        protected void TestSorting(
            IComparisonBasedSortAlgorithm<TDataType> sortingAlgorithm,
            IReadOnlyList<TDataType> input,
            SortDirection direction)
        {
            // assume C# implementation is correct.
            var expectedOutput = direction == SortDirection.Ascending
                ? input.OrderBy(x => x)
                : input.OrderByDescending(x => x);

            var actualOutput = sortingAlgorithm.Sort(input, direction);

            // xUnit actually recognizes that we are dealing with collections,
            // thus verifies that both sequences are equal!
            Assert.Equal(expectedOutput, actualOutput);
        }

        public static IEnumerable<object[]> GetData()
        {
            return new TDataSourceType();
        }
    }
}