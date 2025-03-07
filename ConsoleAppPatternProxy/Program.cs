namespace ConsoleAppPatternProxy;

internal class Program
{
/*
Объяснение кода простого примера:
    1) ISubject: Интерфейс, который определяет метод Request(). 
       Этот интерфейс реализуется как реальным объектом, так и заместителем.

    2) RealSubject: Реальный объект, который выполняет основную работу. 
       В данном случае он просто выводит сообщение в консоль.

    3) Proxy: Заместитель, который контролирует доступ к реальному объекту. 
       Он содержит ссылку на реальный объект и инициализирует его только при первом вызове метода Request().

    4) Program: В методе Main создается объект заместителя, и вызывается метод Request(). 
       При первом вызове заместитель инициализирует реальный объект, 
       а при последующих вызовах просто передает запрос уже существующему объекту.
*/

    static void Main()
    {
        Console.WriteLine("Паттерн \"Заместитель\" (Proxy) — это структурный паттерн проектирования, \n" +
            "который позволяет подставлять вместо реальных объектов специальные объекты-заменители. ");
        Console.WriteLine("Эти объекты перехватывают вызовы к оригинальному объекту, \n" +
            "позволяя выполнить какие-либо действия до или после передачи вызова оригиналу.\n\n");

        Console.WriteLine("Простой пример: ");
        // Создаем заместителя
        var proxy = new Proxy();

        // Первый вызов: заместитель инициализирует реальный объект и передает ему запрос
        Console.WriteLine("Клиент: Отправка запроса заместителю.");
        proxy.Request();

        // Второй вызов: заместитель уже имеет ссылку на реальный объект и передает ему запрос
        Console.WriteLine("Клиент: Отправка другого запроса заместителю.");
        proxy.Request();

        Console.WriteLine();

        Console.WriteLine("Далее показан пример использования в лабораторной \"Шифр Цезаря\"");

        var proxyCaesarCipher = new ProxyCaesarCipher();
        var text = "Tugasha has a small duck";
        var shift = 10;

        Console.WriteLine($"Зашифруем строку: {text}");

        var encryptionText = proxyCaesarCipher.Encryption(text, shift);

        Console.WriteLine($"Зашифрованная строка: {encryptionText}");
        Console.WriteLine($"А теперь расшифруем её обратно");

        var dencryptionText = proxyCaesarCipher.Encryption(encryptionText, -shift);

        Console.WriteLine($"Расшифрованная строка: {dencryptionText}");
    }
}

/// <summary>
/// Интерфейс, который определяет общие методы для реального объекта и заместителя
/// </summary>
public interface ISubject
{
    void Request();
}

/// <summary>
/// Реальный объект, который выполняет основную работу
/// </summary>
public class RealSubject : ISubject
{
    public void Request()
    {
        Console.WriteLine("Реальный объект: Обработка запроса.");
    }
}

/// <summary>
/// Заместитель, который контролирует доступ к реальному объекту
/// </summary>
public class Proxy : ISubject
{
    private RealSubject? _realSubject;

    public void Request()
    {
        // Отложенная инициализация реального объекта
        if (_realSubject == null)
        {
            Console.WriteLine("Заместитель: Инициализация реального объекта.");
            _realSubject = new RealSubject();
        }

        // Передача запроса реальному объекту
        _realSubject.Request();
    }
}

////////////////////////////////////////////////////////////////////////////////////////

/// <summary>
/// Интерфейс, который определяет общие методы для реального объекта и заместителя
/// </summary>
public interface ICaesarCipher
{
    public string Encryption(string text, int shift);
}

public class ProxyCaesarCipher : ICaesarCipher
{
    private CaesarCipher? _caesarCipher;

    public string Encryption(string text, int shift)
    {
        // Отложенная инициализация реального объекта
        if (_caesarCipher == null)
        {
            _caesarCipher = new CaesarCipher();
        }

        // Передача запроса реальному объекту
        return _caesarCipher.Encryption(text, shift);
    }
}

/// <summary>
/// Шифр цезаря, только для английских
/// </summary>
public class CaesarCipher : ICaesarCipher
{

    /// <summary>
    /// Метод для шифрования строки
    /// </summary>
    /// <param name="plainText">Строка, которую надо зашифровать</param>
    /// <param name="shift">на сколько сдвинуть каждый символ</param>
    /// <returns>Зашифрованная строка</returns>
    public string Encryption(string plainText, int shift)
    {
        return Transform(plainText, shift);
    }

    /// <summary>
    /// Вспомогательный метод для преобразования текста
    /// </summary>
    /// <param name="text">Строка, в которой будут сдвигаться символы</param>
    /// <param name="shift">сдвиг</param>
    /// <returns>Измененная строка</returns>
    private string Transform(string text, int shift)
    {
        char[] buffer = text.ToCharArray();
        for (int i = 0; i < buffer.Length; i++)
        {
            char letter = buffer[i];

            // Шифруем только буквы
            if (char.IsLetter(letter))
            {
                // Определяем смещение для заглавных и строчных букв
                char offset = char.IsUpper(letter) ? 'A' : 'a';
                // Применяем сдвиг и учитываем зацикливание алфавита
                letter = (char)((letter + shift - offset + 26) % 26 + offset);
                buffer[i] = letter;
            }
        }
        return new string(buffer);
    }
}