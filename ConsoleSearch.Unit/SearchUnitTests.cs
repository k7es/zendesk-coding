using System;
using Xunit;
using ConsoleSearch;
using ConsoleSearch.Unit.Tests.Models;
using System.Collections.Generic;

namespace ConsoleSearch.UnitTests
{
    public class SearchUnitTests
    {
        private readonly Search<TestSearchItem> _search;
        private const string TestString = "Test";

        public SearchUnitTests()
        {
            var list = new List<TestSearchItem> { new TestSearchItem
            {
                Id = "1",
                Name = TestString,
                Tags = new List<string> {TestString}
            }};
            _search = new Search<TestSearchItem>(list);
        }

        [Fact]
        public void IsValidProperty_ValidProperty_ReturnsTrue()
        {
            Assert.True(_search.IsValidProperty("Name"));
        }

        [Fact]
        public void IsValidProperty_InvalidProperty_ReturnsFalse()
        {
            Assert.False(_search.IsValidProperty("Type"));
        }
        [Fact]
        public void IsValidProperty_EmptyString_ReturnsFalse()
        {
            Assert.False(_search.IsValidProperty(String.Empty));
        }
        [Fact]
        public void GetMatches_ValidInput_ReturnsOneMatch()
        {                  
            var matches = _search.GetMatches("Name", TestString);

            Assert.NotEmpty(matches);
            Assert.Equal(1, matches.Count);           
        }
        [Fact]
        public void GetMatches_ValidInput_ReturnsNoMatch()
        {          
            var matches = _search.GetMatches("Name", "NotValid");

            Assert.Empty(matches);
        }
        [Fact]
        public void GetMatches_InvalidInput_ReturnsNoMatch()
        {          
            var matches = _search.GetMatches(TestString, TestString);

            Assert.Empty(matches);
        }
        [Fact]
        public void GetMatchById_ValidInput_ReturnsMatch()
        {           
            var matches = _search.GetMatchById("1");

            Assert.NotEmpty(matches);
            Assert.Equal(1, matches.Count);
        }
        [Fact]
        public void GetMatchById_InvalidInput_ReturnsNoMatch()
        {         
            var matches = _search.GetMatchById("2");

            Assert.Empty(matches);
        }
        [Fact]
        public void GetMatchesByTags_ValidInput_ReturnsMatches()
        {         
            var matches = _search.GetMatchesByTags(TestString);

            Assert.Equal(1, matches.Count);
        }
        [Fact]
        public void GetMatchesByTags_InvalidInput_ReturnNoMatches()
        {           
            var matches = _search.GetMatchesByTags("Testing");

            Assert.Empty(matches);
        }
    }
}
