using System.Runtime.InteropServices;

namespace WordFinder.Tests
{
    public class TestWordFinder
    {
        [Fact]
        public void TestMatrixWithOneWordFoundHorizontally()
        {
            var matrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "abc", "afkp", "isjkl" };

            var result = wordFinder.Find(wordStream);

            Assert.Contains("abc", result);
            Assert.DoesNotContain("afkp", result);
            Assert.DoesNotContain("isjkl", result);
        }

        [Fact]
        public void TestMatrixWithOneWordFoundVertically()
        {
            var matrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "aeim", "afkp", "isjk" };

            var result = wordFinder.Find(wordStream);

            Assert.Contains("aeim", result);
            Assert.DoesNotContain("afkp", result);
            Assert.DoesNotContain("isjk", result);
        }

        [Fact]
        public void TestMatrixWithNoWordsFound()
        {
            var matrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "xyz", "pqr", "tuv" };

            var result = wordFinder.Find(wordStream);

            Assert.Empty(result);
        }

        [Fact]
        public void TestMatrixWithMultipleWordsFound()
        {
            var matrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "abcd", "mnop", "ijkl" };

            var result = wordFinder.Find(wordStream);

            Assert.Contains("abcd", result);
            Assert.Contains("ijkl", result);
            Assert.Contains("mnop", result);
        }

        [Fact]
        public void TestMatrixWithSingleWordFoundTwice()
        {
            var matrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "abcd", "abcd" };

            var result = wordFinder.Find(wordStream);

            Assert.Single(result);
            Assert.Contains("abcd", result);
        }

        [Fact]
        public void TestMatrixCorrectResultOrderOnMultipleFind()
        {
            var matrix = new List<string>
        {
            "abca",
            "abcb",
            "efgc",
            "efgh",
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "abc", "efg", "h" };

            var result = wordFinder.Find(wordStream).ToArray();
            var expectedResults = new string[] { "abc", "efg", "h" };

            for (int i = 0; i < result.Length; i++)
            {
                Assert.Equal(expectedResults[i], result[i]);
            }
        }

        [Fact]
        public void TestMatrixMaxTenResults()
        {
            var matrix = new List<string>
        {
            "abcd", "efgh", "ijkl", "mnop", "qrst", "uvwx", "yzab", "cdef", "ghij", "klmn", "opqr"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "abcd", "efgh", "ijkl", "mnop", "qrst", "uvwx", "yzab", "cdef", "ghij", "klmn", "opqr" };

            var result = wordFinder.Find(wordStream).ToArray();
            var expectedResults = new string[] { "abcd", "efgh", "ijkl", "mnop", "qrst", "uvwx", "yzab", "cdef", "ghij", "klmn" };

            Assert.Equal(10, result.Count());

            foreach (var word in expectedResults)
            {
                Assert.Contains(word, result);
            }
        }

        [Fact]
        public void TestMatrixWithLongerWordThanMatrix()
        {
            var matrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "abcdefgh", "mnop" };

            var result = wordFinder.Find(wordStream);

            Assert.DoesNotContain("abcdefgh", result);
            Assert.Contains("mnop", result);
        }

        [Fact]
        public void TestMatrixWithEmptyWordStream()
        {
            var matrix = new List<string>
        {
            "abcd",
            "efgh",
            "ijkl",
            "mnop"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string>();

            var result = wordFinder.Find(wordStream);

            Assert.Empty(result);
        }

        [Fact]
        public void TestMatrixWithLongMatrixAndShortWordStream()
        {
            var matrix = new List<string>
        {
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz"
        };
            var wordFinder = new WordFinder.Services.WordFinder(matrix);
            var wordStream = new List<string> { "abcd", "efgh", "ijkl" };

            var result = wordFinder.Find(wordStream);

            Assert.Contains("abcd", result);
            Assert.Contains("efgh", result);
            Assert.Contains("ijkl", result);
        }

        [Fact]
        public void TestMatrixCreationWithEmptyMatrix()
        {
            var matrix = new List<string>();
            var exception = Assert.Throws<ArgumentNullException>(() => new WordFinder.Services.WordFinder(matrix));
        }

        [Fact]
        public void TestMatrixCreationWithEmptyRow()
        {
            var matrix = new List<string>
        {
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
            ""
        };

            var exception = Assert.Throws<InvalidDataException>(() => new WordFinder.Services.WordFinder(matrix));
        }

        [Fact]
        public void TestMatrixCreationWithWhitespaceRow()
        {
            var matrix = new List<string>
        {
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
            "   "
        };

            var exception = Assert.Throws<InvalidDataException>(() => new WordFinder.Services.WordFinder(matrix));
        }

        [Fact]
        public void TestMatrixCreationWithDifferentLengthRows()
        {
            var matrix = new List<string>
        {
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyz",
            "abcdefghijklmnopqrstuvwxyz"
        };

            var exception = Assert.Throws<InvalidDataException>(() => new WordFinder.Services.WordFinder(matrix));
        }

        [Fact]
        public void TestMatrixCreationWithMoreThan64Columns()
        {
            var matrix = new List<string>
        {
            "abcdefghijklmnopqrstuvwxyzabcdefghijklmnopqrstuvwxyzabcdefghijklm",
        };

            var exception = Assert.Throws<InvalidDataException>(() => new WordFinder.Services.WordFinder(matrix));
        }

        [Fact]
        public void TestMatrixCreationWithMoreThan64Rows()
        {
            var matrix = new List<string>();
            for (int i = 0; i < 65; i++)
            {
                matrix.Add("abcd");
            }

            var exception = Assert.Throws<InvalidDataException>(() => new WordFinder.Services.WordFinder(matrix));
        }
    }
}