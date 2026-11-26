using System.Globalization;

class Program
{
    // Верните, сколько студентов в группе имеют высокий балл:
    // есть хотя бы одна оценка и средний балл >= minAverage.
    static int CountHigh(Student[] group, double minAverage)
    {
        int count = 0;
        foreach (Student student in group)
        {
            if (student.HasScores && student.Average >= minAverage)
                count++;
        }
        return count;
    }

    // Верните НОВЫЙ массив только из студентов с высоким баллом (правило то же, что в CountHigh),
    // в том же порядке, что и в group. Длина массива — ровно число таких студентов.
    // Новые объекты Student не создавайте: кладите в массив те же ссылки из group.
    static Student[] FindHigh(Student[] group, double minAverage)
    {
        Student[] high = new Student[CountHigh(group, minAverage)];
        int index = 0;
        foreach (Student student in group)
        {
            if (student.HasScores && student.Average >= minAverage)
                high[index++] = student;
        }
        return high;
    }

    static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        CultureInfo.CurrentCulture = new CultureInfo("ru-RU"); // дробная часть через запятую

        Student[] group = SampleData.Group();

        Check("CountHigh(group, 85)", CountHigh(group, 85), 4);
        Check("CountHigh(group, 90)", CountHigh(group, 90), 3);
        Check("CountHigh(group, 0)", CountHigh(group, 0), 7);

        Check("FindHigh(group, 85)", Names(FindHigh(group, 85)),
              "Ахметова Айгерим, Ким Анна, Жумабаев Нурлан, Денисова Ольга");
        Check("FindHigh(group, 95)", Names(FindHigh(group, 95)), "Ким Анна");
        Check("FindHigh(group, 0)", Names(FindHigh(group, 0)),
              "Сериков Дамир, Ахметова Айгерим, Ким Анна, Петровых Александр, Бекова Мадина, Жумабаев Нурлан, Денисова Ольга");

        Student[] high = FindHigh(group, 85);
        Check("FindHigh(group, 85)[1] — тот же объект, что group[2]",
              high.Length > 1 && high[1] == group[2] ? "да" : "нет", "да");

        Console.WriteLine();
        Console.WriteLine("Средний балл от 85:");
        foreach (Student s in high)
            Console.WriteLine(s);
    }

    // Имена через запятую; null в массиве показывается как "null".
    static string Names(Student[] a)
    {
        if (a.Length == 0) return "(пусто)";
        string result = a[0]?.Name ?? "null";
        for (int i = 1; i < a.Length; i++)
            result += ", " + (a[i]?.Name ?? "null");
        return result;
    }

    static void Check(string what, int result, int expected)
    {
        string status = result == expected ? "OK" : "ОШИБКА";
        Console.WriteLine($"{what}: получено {result}, ожидалось {expected} - {status}");
    }

    static void Check(string what, string result, string expected)
    {
        string status = result == expected ? "OK" : "ОШИБКА";
        Console.WriteLine($"{what}: получено {result}, ожидалось {expected} - {status}");
    }
}
