namespace ConsoleAppPatternBuilder;

internal class Program
{
/*
Объяснение кода:
    1) Класс Computer: Это класс, который представляет продукт, который мы хотим создать. 
       В данном случае это компьютер с различными компонентами.

    2) Интерфейс IComputerBuilder: Определяет методы для установки различных компонентов компьютера 
       и метод для получения готового продукта.

    3) Классы GamingComputerBuilder и OfficeComputerBuilder: Это конкретные строители, 
       которые реализуют интерфейс IComputerBuilder. 
       Они определяют, как именно будут устанавливаться компоненты для игрового и офисного компьютеров соответственно.

    4) Класс ComputerDirector: Это директор, который управляет процессом строительства. 
       Он использует строителя для пошагового создания компьютера.

    5) Пример использования: В методе Main создаются два разных компьютера (игровой и офисный) 
       с помощью соответствующих строителей и директора. 
       Затем информация о каждом компьютере выводится на экран.
*/

    static void Main()
    {
        Console.WriteLine("Паттерн \"Строитель\" (Builder) используется для пошагового создания сложных объектов. \n" +
            "Он позволяет отделить процесс конструирования объекта от его представления, \n" +
            "что делает код более гибким и читаемым.\n\n");


        Console.WriteLine("Рассмотрим пример реализации паттерна \"Строитель\" на C# для создания объекта \"Компьютер\". \n" +
            "(Лабораторная по ООП \"Простой класс\")\n" +
            "Компьютер может иметь различные компоненты, такие как процессор, оперативная память, жесткий диск и видеокарта.\n\n");

        // Создаем строителя для игрового компьютера
        IComputerBuilder gamingBuilder = new GamingComputerBuilder();
        ComputerDirector director = new ComputerDirector(gamingBuilder);

        // Строим игровой компьютер
        director.ConstructComputer();
        Computer gamingComputer = director.GetComputer();

        // Выводим информацию о игровом компьютере
        Console.WriteLine("Строим игровой компьютер");
        Console.WriteLine("Игровой компьютер:");
        gamingComputer.Display();

        Console.WriteLine();

        // Создаем строителя для офисного компьютера
        IComputerBuilder officeBuilder = new OfficeComputerBuilder();
        director = new ComputerDirector(officeBuilder);

        // Строим офисный компьютер
        director.ConstructComputer();
        Computer officeComputer = director.GetComputer();

        // Выводим информацию о офисном компьютере
        Console.WriteLine("Строим офисный компьютер");
        Console.WriteLine("Офисный компьютер:");
        officeComputer.Display();
    }
}


/// <summary>
/// Класс, представляющий продукт (Компьютер)
/// </summary>
public class Computer
{
    /// <summary>
    /// Процессор
    /// </summary>
    public string CPU { get; set; } = "";

    /// <summary>
    /// Оперативная память
    /// </summary>
    public string RAM { get; set; } = "";

    /// <summary>
    /// Жесткий диск
    /// </summary>
    public string HDD { get; set; } = "";

    /// <summary>
    /// Графический процессор
    /// </summary>
    public string GPU { get; set; } = "";

    /// <summary>
    /// Метод для вывода всех характеристик
    /// </summary>
    public void Display()
    {
        Console.WriteLine($"CPU: {CPU}");
        Console.WriteLine($"RAM: {RAM}");
        Console.WriteLine($"HDD: {HDD}");
        Console.WriteLine($"GPU: {GPU}");
    }
}

/// <summary>
/// Интерфейс строителя
/// </summary>
public interface IComputerBuilder
{
    /// <summary>
    /// Задать значение Процессора
    /// </summary>
    void SetCPU();

    /// <summary>
    /// Задать значение Оперативной памяти
    /// </summary>
    void SetRAM();

    /// <summary>
    /// Задать значение Жксткого диска
    /// </summary>
    void SetHDD();

    /// <summary>
    /// Задать значение Видеокарты, Графического процессора
    /// </summary>
    void SetGPU();

    /// <summary>
    /// Получить компьютер
    /// </summary>
    /// <returns></returns>
    Computer GetComputer();
}

/// <summary>
/// Конкретный строитель для сборки игрового компьютера
/// </summary>
public class GamingComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();

    public void SetCPU()
    {
        _computer.CPU = "Intel Core i9";
    }

    public void SetRAM()
    {
        _computer.RAM = "32GB DDR4";
    }

    public void SetHDD()
    {
        _computer.HDD = "2TB SSD";
    }

    public void SetGPU()
    {
        _computer.GPU = "NVIDIA RTX 3090";
    }

    public Computer GetComputer()
    {
        return _computer;
    }
}

/// <summary>
/// Конкретный строитель для сборки офисного компьютера
/// </summary>
public class OfficeComputerBuilder : IComputerBuilder
{
    private Computer _computer = new Computer();

    public void SetCPU()
    {
        _computer.CPU = "Intel Core i5";
    }

    public void SetRAM()
    {
        _computer.RAM = "8GB DDR4";
    }

    public void SetHDD()
    {
        _computer.HDD = "500GB HDD";
    }

    public void SetGPU()
    {
        _computer.GPU = "Integrated Graphics";
    }

    public Computer GetComputer()
    {
        return _computer;
    }
}

/// <summary>
/// Класс, который управляет процессом строительства (Директор)
/// </summary>
public class ComputerDirector
{
    private IComputerBuilder _computerBuilder;

    public ComputerDirector(IComputerBuilder computerBuilder)
    {
        _computerBuilder = computerBuilder;
    }

    /// <summary>
    /// Метод сборки компьютера
    /// </summary>
    public void ConstructComputer()
    {
        _computerBuilder.SetCPU();
        _computerBuilder.SetRAM();
        _computerBuilder.SetHDD();
        _computerBuilder.SetGPU();
    }

    /// <summary>
    /// получить компьютер
    /// </summary>
    /// <returns></returns>
    public Computer GetComputer()
    {
        return _computerBuilder.GetComputer();
    }
}