using System;
using Xunit;

namespace GradeBook.Tests
{
    public class BookTests
    {
        [Fact]
        public void Book_calulate_an_avgGrade()
        {
            // arrange
            var book = new InMemoryBook("");
            book.AddGrade(10);
            book.AddGrade(20);
            book.AddGrade(30);
            book.AddGrade(40);
            book.AddGrade(50);

            // act
            var res = book.getStatistics();

            // assert 
            Assert.Equal(50, res.Hi, 1);
            Assert.Equal('F', res.Letter);
        }
    }
}
