using System;
using Xunit;

namespace GradeBook.Tests
{
    public delegate string WriteLogDelegate(string log_msg);
    public class TypeTests
    {
        private int cnt = 0;
        [Fact]
        public void delegateCanPointToMethod()
        {
            WriteLogDelegate log = return_msg;
            log += return_msg;
            log += return_msg;

            log += return_msg2;
            log += return_msg2;

            var result = log("Hi");
            // Assert
            Assert.Equal(5, cnt);
        }

        string return_msg2(string msg)
        {
            cnt++; return msg;
        }
        string return_msg(string msg)
        {
            cnt++; return msg;
        }

        [Fact]
        public void test_passingByValue()
        {
            var x = getInt();
            changeInt(out x);
            Assert.Equal(5, x);
        }

        [Fact]
        public void createBook_returns_diffObjects()
        {
            // arrange
            var book1 = createBook("book1");
            var book2 = createBook("book2");

            // assert
            Assert.Equal("book1", book1.Name);
            Assert.Equal("book2", book2.Name);
        }

        [Fact]
        public void two_vars_reference_sameObject()
        {
            // arrange
            var book1 = createBook("book1");
            var book2 = book1;

            // assert
            // Assert.Equal("book1", book1.Name);
            // Assert.Equal("book1", book2.Name);
            Assert.Same(book1, book2);
            Assert.True(Object.ReferenceEquals(book1, book2));
        }

        [Fact]
        public void can_setName_reference()
        {
            // arrange
            var book1 = createBook("book1");
            setName(book1, "new");

            // assert
            Assert.Equal("new", book1.Name);
        }
        [Fact]
        public void csharp_canPass_by_ref()
        {
            // arrange
            var book1 = createBook("book1");
            createBook_setName(out book1, "new");

            // assert
            Assert.Equal("new", book1.Name);
        }

        [Fact]
        public void string_behaviour()
        {
            string m = "muhammed";
            changeName(m);
            // strings are immutable
            Assert.Equal("muhammed", m);
        }

        private void changeInt(out int x) => x = 5;
        private int getInt() => 3;
        private void changeName(string parameter) => parameter.ToUpper();
        private void createBook_setName(out InMemoryBook book, string name) => book = new InMemoryBook(name);
        private void setName(InMemoryBook book, string name) => book.Name = name;
        private InMemoryBook createBook(string name) => new InMemoryBook(name);
    }
}
