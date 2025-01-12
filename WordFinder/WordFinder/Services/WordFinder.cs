namespace WordFinder.Services
{
    using Microsoft.VisualBasic;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class WordFinder
    {
        private readonly char[][] _matrix;

        //we make a result count tracker to sort and limit after searching and initialize it
        private readonly Dictionary<string, int> _resultTracker = [];

        //we store matrix data for conviniency
        private readonly int _matrixRows;
        private readonly int _matrixColumns;

        //The constructor validates and generates the matrix
        public WordFinder(IEnumerable<string> matrix)
        {
            if (matrix == null || !matrix.Any())
            {
                throw new ArgumentNullException();
            }

            int length = matrix.First().Length;
            if (matrix.Count() > 64 || matrix.Any(f => string.IsNullOrWhiteSpace(f) || f.Length > 64 || f.Length != length))
            {
                throw new InvalidDataException();
            }

            _matrix = matrix.Select(f => f.ToCharArray()).ToArray();


            //asuming all words are equal length we can get rows and columns
            _matrixRows = _matrix.Length;
            _matrixColumns = _matrix[0].Length;
        }


        public IEnumerable<string> Find(IEnumerable<string> wordstream)
        {
            //hashset rids the list of duplicates
            var wordSet = new HashSet<string>(wordstream);

            //first we preprocess rows and columns to strings to gain efficiency by avoiding repeatedly
            //accesing the matrix for each word, avoiding repeated character lookups and making searches more efficient
            var rows = PreprocessRows();
            var columns = PreprocessColumns();

            //iterate the list and find words in the matrix
            foreach (var word in wordSet)
            {
                CheckWord(word, rows, columns);
            }

            return _resultTracker.OrderByDescending(f => f.Value)
                            .Take(10)
                            .Select(f => f.Key);
        }
        private string[] PreprocessRows()
        {
            var rows = new string[_matrixRows];
            for (int row = 0; row < _matrixRows; row++)
            {
                rows[row] = new string(_matrix[row]);
            }
            return rows;
        }

        private string[] PreprocessColumns()
        {
            var columns = new string[_matrixColumns];
            for (int column = 0; column < _matrixColumns; column++)
            {
                //as columns cannot be accessed directly as strings, we must build them
                var builder = new StringBuilder();
                for (int row = 0; row < _matrixRows; row++)
                {
                    builder.Append(_matrix[row][column]);
                }
                columns[column] = builder.ToString();
            }
            return columns;
        }

        private void CheckWord(string word, string[] rows, string[] columns)
        {
            //first we evaluate rows
            for (int i = 0; i < _matrixRows; i++)
            {
                if (rows[i].Contains(word))
                {
                    AddMatchResult(word);
                }
            }

            //then columns
            for (int j = 0; j < _matrixColumns; j++)
            {
                if (columns[j].Contains(word))
                {
                    AddMatchResult(word);
                }
            }
        }

        private void AddMatchResult(string word)
        {
            if (_resultTracker.ContainsKey(word))
            {
                _resultTracker[word]++;
            }
            else
            {
                _resultTracker.Add(word, 1);
            }
        }
    }

}
