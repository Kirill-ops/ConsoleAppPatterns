namespace ConsoleAppPatternStrategy;

internal class Program
{
    /*

Объяснение кода:
    1) ISortStrategy: Интерфейс, который определяет метод Sort. 
       Все стратегии должны реализовывать этот интерфейс.

    2) BubbleSortStrategy: Конкретная стратегия, реализующая алгоритм сортировки пузырьком.

    3) QuickSortStrategy:Конкретная стратегия, реализующая алгоритм быстрой сортировки.

    4) Sorter: Контекст, который использует стратегию для выполнения сортировки. 
       Он содержит ссылку на объект стратегии и вызывает её метод Sort.

    5) Program: В методе Main создается массив данных и объект Sorter. 
       Сначала используется стратегия сортировки пузырьком, затем — быстрая сортировка.
*/

    static void Main()
    {
        Console.WriteLine("Паттерн \"Стратегия\" (Strategy) — это поведенческий паттерн проектирования, \n" +
            "который позволяет определять семейство алгоритмов, \n" +
            "инкапсулировать каждый из них и делать их взаимозаменяемыми. \n" +
            "Это позволяет выбирать алгоритм в зависимости от контекста использования.\n\n");

        Console.WriteLine("Реализация показана на примере лаобраторной САОД на тему \"Виды сортировки\"\n");

        int[] dataOne = { 5, 2, 8, 1, 9, 3 };

        Console.WriteLine("Используем стратегию сортировки пузырьком:");
        var sorter = new Sorter(new BubbleSortStrategy());
        sorter.Sort(dataOne);
        Console.WriteLine("Отсортированный массив: " + string.Join(", ", dataOne));

        Console.WriteLine();

        Console.WriteLine("Используем стратегию быстрой сортировки:");
        int[] datatwo = { 5, 2, 8, 1, 9, 3 };
        sorter = new Sorter(new QuickSortStrategy());
        sorter.Sort(datatwo);
        Console.WriteLine("Отсортированный массив: " + string.Join(", ", datatwo));
    }
}

/// <summary>
/// Интерфейс стратегии сортировки
/// </summary>
public interface ISortStrategy
{
    void Sort(int[] data);
}

/// <summary>
/// Конкретная стратегия: сортировка пузырьком
/// </summary>
public class BubbleSortStrategy : ISortStrategy
{
    public void Sort(int[] data)
    {
        for (int i = 0; i < data.Length - 1; i++)
        {
            for (int j = 0; j < data.Length - i - 1; j++)
            {
                if (data[j] > data[j + 1])
                {
                    // Меняем элементы местами
                    int temp = data[j];
                    data[j] = data[j + 1];
                    data[j + 1] = temp;
                }
            }
        }
    }
}

/// <summary>
/// Конкретная стратегия: быстрая сортировка
/// </summary>
public class QuickSortStrategy : ISortStrategy
{
    public void Sort(int[] data)
    {
        QuickSort(data, 0, data.Length - 1);
    }

    private void QuickSort(int[] data, int low, int high)
    {
        if (low < high)
        {
            int pi = Partition(data, low, high);
            QuickSort(data, low, pi - 1);
            QuickSort(data, pi + 1, high);
        }
    }

    private int Partition(int[] data, int low, int high)
    {
        int pivot = data[high];
        int i = low - 1;

        for (int j = low; j < high; j++)
        {
            if (data[j] < pivot)
            {
                i++;
                int temp = data[i];
                data[i] = data[j];
                data[j] = temp;
            }
        }

        (data[high], data[i + 1]) = (data[i + 1], data[high]);
        return i + 1;
    }
}

/// <summary>
/// Контекст, который использует стратегию
/// </summary>
public class Sorter
{
    private ISortStrategy _sortStrategy;

    /// <summary>
    /// Устанавливаем стратегию через конструктор
    /// </summary>
    /// <param name="sortStrategy">Стратегия сортировки</param>
    public Sorter(ISortStrategy sortStrategy)
    {
        _sortStrategy = sortStrategy;
    }

    /// <summary>
    /// Метод для выполнения сортировки
    /// </summary>
    /// <param name="data">Массив, который нужно отсортировать</param>
    public void Sort(int[] data)
    {
        _sortStrategy.Sort(data);
    }
}