using ClassLibrary;
using System;
using System.Linq;
using Xunit;

namespace TestProjectIControl
{
    /// <summary>
    /// Тесты для класса TextPipeline с проверкой пограничных значений
    /// и контрактов (Pre/Post conditions)
    /// </summary>
    public class UnitTestTextPipeline
    {
        #region Тесты для RemoveExtraSpaces

        /// <summary>
        /// Проверяет корректное удаление лишних пробелов из строки с множественными пробелами
        /// и пробелами в начале и конце строки
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_ValidInput_ShouldRemoveExtraSpaces()
        {
            string input = "  hello    world  ";
            string expected = "hello world";

            string result = TextPipeline.RemoveExtraSpaces(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет, что строка с нормальными пробелами остается без изменений
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_SingleSpace_ShouldReturnSame()
        {
            string input = "hello world";
            string expected = "hello world";

            string result = TextPipeline.RemoveExtraSpaces(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет, что строка состоящая только из пробелов вызывает исключение
        /// согласно предусловию контракта
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_OnlySpaces_ShouldThrowException()
        {
            string input = "   ";

            Assert.Throws<ArgumentException>(() => TextPipeline.RemoveExtraSpaces(input));
        }

        /// <summary>
        /// Проверяет, что пустая строка вызывает исключение согласно предусловию контракта
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_EmptyString_ShouldThrowException()
        {
            string input = "";

            Assert.Throws<ArgumentException>(() => TextPipeline.RemoveExtraSpaces(input));
        }

        /// <summary>
        /// Проверяет, что null вызывает исключение согласно предусловию контракта
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_NullInput_ShouldThrowException()
        {
            string input = null;

            Assert.Throws<ArgumentNullException>(() => TextPipeline.RemoveExtraSpaces(input));
        }

        /// <summary>
        /// Проверяет, что строка состоящая только из whitespace символов вызывает исключение
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_WhitespaceOnly_ShouldThrowException()
        {
            string input = "   \t\n  ";

            Assert.Throws<ArgumentException>(() => TextPipeline.RemoveExtraSpaces(input));
        }

        /// <summary>
        /// Проверяет нормализацию табов и переносов строк в обычные пробелы
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_TabsAndNewlines_ShouldNormalize()
        {
            string input = "hello\t\tworld\n\n";
            string expected = "hello world";

            string result = TextPipeline.RemoveExtraSpaces(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет обработку одиночного символа без пробелов
        /// </summary>
        [Fact]
        public void RemoveExtraSpaces_SingleCharacter_ShouldReturnSame()
        {
            string input = "a";
            string expected = "a";

            string result = TextPipeline.RemoveExtraSpaces(input);

            Assert.Equal(expected, result);
        }

        #endregion

        #region Тесты для ToLowerCase

        /// <summary>
        /// Проверяет корректное преобразование строки с заглавными буквами в нижний регистр
        /// </summary>
        [Fact]
        public void ToLowerCase_ValidInput_ShouldConvertToLower()
        {
            string input = "Hello WORLD";
            string expected = "hello world";

            string result = TextPipeline.ToLowerCase(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет, что строка уже в нижнем регистре остается без изменений
        /// </summary>
        [Fact]
        public void ToLowerCase_AlreadyLowerCase_ShouldReturnSame()
        {
            string input = "hello world";
            string expected = "hello world";

            string result = TextPipeline.ToLowerCase(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет, что пустая строка вызывает исключение согласно предусловию контракта
        /// </summary>
        [Fact]
        public void ToLowerCase_EmptyString_ShouldThrowException()
        {
            string input = "";

            Assert.Throws<ArgumentException>(() => TextPipeline.ToLowerCase(input));
        }

        /// <summary>
        /// Проверяет, что null вызывает исключение согласно предусловию контракта
        /// </summary>
        [Fact]
        public void ToLowerCase_NullInput_ShouldThrowException()
        {
            string input = null;

            Assert.Throws<ArgumentNullException>(() => TextPipeline.ToLowerCase(input));
        }

        /// <summary>
        /// Проверяет, что строка состоящая только из пробелов вызывает исключение
        /// </summary>
        [Fact]
        public void ToLowerCase_WhitespaceOnly_ShouldThrowException()
        {
            string input = "   ";

            Assert.Throws<ArgumentException>(() => TextPipeline.ToLowerCase(input));
        }

        /// <summary>
        /// Проверяет корректное преобразование строки со специальными символами
        /// </summary>
        [Fact]
        public void ToLowerCase_SpecialCharacters_ShouldConvertCorrectly()
        {
            string input = "HELLO! @#$%";
            string expected = "hello! @#$%";

            string result = TextPipeline.ToLowerCase(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет преобразование одиночного символа в нижний регистр
        /// </summary>
        [Fact]
        public void ToLowerCase_SingleCharacter_ShouldConvert()
        {
            string input = "A";
            string expected = "a";

            string result = TextPipeline.ToLowerCase(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет преобразование строки со смешанным регистром
        /// </summary>
        [Fact]
        public void ToLowerCase_MixedCase_ShouldConvertAll()
        {
            string input = "HeLLo WoRLd";
            string expected = "hello world";

            string result = TextPipeline.ToLowerCase(input);

            Assert.Equal(expected, result);
        }

        #endregion

        #region Тесты для RemoveEmptyLines

        /// <summary>
        /// Проверяет корректное удаление пустых строк из многострочного текста
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_ValidInput_ShouldRemoveEmptyLines()
        {
            string input = "line1\n\nline2\n\nline3";
            string expected = "line1\r\nline2\r\nline3";

            string result = TextPipeline.RemoveEmptyLines(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет, что текст без пустых строк остается без изменений
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_NoEmptyLines_ShouldReturnSame()
        {
            string input = "line1\nline2\nline3";
            string expected = "line1\r\nline2\r\nline3";

            string result = TextPipeline.RemoveEmptyLines(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет, что пустая строка вызывает исключение согласно предусловию контракта
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_EmptyString_ShouldThrowException()
        {
            string input = "";

            Assert.Throws<ArgumentException>(() => TextPipeline.RemoveEmptyLines(input));
        }

        /// <summary>
        /// Проверяет, что null вызывает исключение согласно предусловию контракта
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_NullInput_ShouldThrowException()
        {
            string input = null;

            Assert.Throws<ArgumentNullException>(() => TextPipeline.RemoveEmptyLines(input));
        }

        /// <summary>
        /// Проверяет, что строка состоящая только из пробелов вызывает исключение
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_WhitespaceOnly_ShouldThrowException()
        {
            string input = "   ";

            Assert.Throws<ArgumentException>(() => TextPipeline.RemoveEmptyLines(input));
        }

        /// <summary>
        /// Проверяет обработку текста состоящего только из пустых строк
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_OnlyEmptyLines_ShouldReturnEmpty()
        {
            string input = "\n\n\n";

            Assert.Throws<ArgumentException>(() => TextPipeline.RemoveEmptyLines(input));
        }

        /// <summary>
        /// Проверяет обработку одиночной строки без переносов
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_SingleLine_ShouldReturnSame()
        {
            string input = "single line";
            string expected = "single line";

            string result = TextPipeline.RemoveEmptyLines(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет корректную обработку смешанных окончаний строк (Windows и Unix)
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_MixedLineEndings_ShouldHandleCorrectly()
        {
            string input = "line1\r\n\r\nline2\n\nline3";
            string expected = "line1\r\nline2\r\nline3";

            string result = TextPipeline.RemoveEmptyLines(input);

            Assert.Equal(expected, result);
        }

        /// <summary>
        /// Проверяет удаление пустых строк в начале и конце текста
        /// </summary>
        [Fact]
        public void RemoveEmptyLines_LeadingAndTrailingEmptyLines_ShouldRemove()
        {
            string input = "\n\nline1\nline2\n\n";
            string expected = "line1\r\nline2";

            string result = TextPipeline.RemoveEmptyLines(input);

            Assert.Equal(expected, result);
        }

        #endregion
    }
}