using System;
using System.Diagnostics;
using System.Text.RegularExpressions;
using CommunityToolkit.Diagnostics;

namespace ClassLibrary
{
    /// <summary>
    /// Статический класс, содержащий операции текстового конвейера
    /// Реализует бизнес-логику с проверкой контрактов (Pre/Post conditions)
    /// </summary>
    public static class TextPipeline
    {
        // ОПЕРАЦИЯ 1: Удаление лишних пробелов
        public static string RemoveExtraSpaces(string input)
        {
            // PRECONDITION: Проверка условий ДО выполнения операции
            Guard.IsNotNullOrWhiteSpace(input);

            // ЛОГИКА ОПЕРАЦИИ: Основная бизнес-логика
            // Заменяем множественные пробелы на один и обрезаем края
            string result = Regex.Replace(input, @"\s+", " ").Trim();

            // POSTCONDITION: Проверка условий ПОСЛЕ выполнения операции
            Debug.Assert(result != null, "Результат не должен быть null");
            Debug.Assert(!result.Contains("  "), "Не должно быть двойных пробелов");
            Debug.Assert(result == result.Trim(), "Не должно быть пробелов по краям");

            return result;
        }

        // ОПЕРАЦИЯ 2: Приведение к нижнему регистру
        public static string ToLowerCase(string input)
        {
            // PRECONDITION
            Guard.IsNotNullOrWhiteSpace(input);

            // ЛОГИКА ОПЕРАЦИИ: Преобразование всех символов к нижнему регистру
            string result = input.ToLower();

            // POSTCONDITION
            Debug.Assert(result != null, "Результат не должен быть null");
            Debug.Assert(result == input.ToLower(), "Все символы должны быть в нижнем регистре");

            return result;
        }

        // ОПЕРАЦИЯ 3: Удаление пустых строк
        public static string RemoveEmptyLines(string input)
        {
            // PRECONDITION
            Guard.IsNotNullOrWhiteSpace(input);

            // ЛОГИКА ОПЕРАЦИИ: 
            // 1. Разбиваем текст на строки
            // 2. Удаляем пустые строки
            // 3. Собираем обратно в текст
            string[] lines = input.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries);
            string result = string.Join(Environment.NewLine, lines);

            // POSTCONDITION
            Debug.Assert(result != null, "Результат не должен быть null");
            Debug.Assert(!result.Contains(Environment.NewLine + Environment.NewLine), "Не должно быть пустых строк");

            return result;
        }
    }
}