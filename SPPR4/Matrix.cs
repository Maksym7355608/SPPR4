using System.Linq;
namespace SPPR4;

public class Matrix
{
    public static IEnumerable<List<IdNamePriority>> GetAllGroups(List<IdNamePriority> values)
    {
        for (int i = 0; i < values.Count; i++)
        {
            for (int j = i; j < values.Count; j++)
            {
                if(i != j)
                    for (int k = j; k < values.Count; k++)
                    {
                        if (i != k && j != k)
                            yield return new List<IdNamePriority> { values[i], values[j], values[k] };
                    }
            }
        }
    }

    public static List<List<int>> CreateMatrix(List<IdNamePriority> values)
    {
        var matrix = new List<List<int>>(values.Count);
        matrix.AddRange(values
            .Select(item => values
                .Select(val => item.Priority.CompareTo(val.Priority))
                .ToList()));

        return matrix;
    }

    public static void PrintMatrix(List<List<int>> matrix, List<IdNamePriority> values)
    {
        var i = 0;
        Console.Write('\t');
        values.ForEach(x => Console.Write(x.Name + '\t'));
        if (matrix[0].Count > values.Count)
            Console.Write("Sum");
        Console.WriteLine();
        foreach (var row in matrix)
        {
            if(i < values.Count)
                Console.Write(values[i].Name + '\t');
            foreach (var value in row)
            {
                Console.Write(value.ToString() + '\t');
            }
            Console.WriteLine();
            i++;
        }
    }

    public static List<List<int>> AddSummaMatrix(List<List<int>> matrix)
    {
        matrix.ForEach(row =>
        {
            row.Add(row.Sum());
        });
        return matrix;
    }

    public static List<NameValue> GetRanking(List<List<int>> matrix, List<IdNamePriority> values)
    {
        var result = new List<NameValue>();
        foreach (var val in values)
        {
            result.Add(new NameValue()
            {
                Name = val.Name,
                Value = matrix[values.IndexOf(val)].Last()
            });
        }
        return result.OrderByDescending(x => x.Value).ToList();
    }
    
    public static (bool transitive, bool nontransitive) CheckMatrixProperties(int[][] matrix)
    {
        int n = matrix.GetLength(0); // Розмір матриці (кількість об'єктів)

        // Перевірка рефлексивності
        bool isReflexive = true;
        for (int i = 0; i < n; i++)
        {
            if (matrix[i][i] != 1)
            {
                isReflexive = false;
                break;
            }
        }

        // Перевірка антирефлексивності
        bool isAntireflexive = true;
        for (int i = 0; i < n; i++)
        {
            if (matrix[i][i] != 0)
            {
                isAntireflexive = false;
                break;
            }
        }

        // Перевірка симетричності
        bool isSymmetric = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = i + 1; j < n; j++)
            {
                if (matrix[i][j] != matrix[j][i])
                {
                    isSymmetric = false;
                    break;
                }
            }

            if (!isSymmetric)
                break;
        }

        // Перевірка ачиметричності
        bool isAsymmetric = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == 1 && matrix[j][i] == 1 && i != j)
                {
                    isAsymmetric = false;
                    break;
                }
            }

            if (!isAsymmetric)
                break;
        }

        // Перевірка антисиметричності
        bool isAntisymmetric = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == 1 && matrix[j][i] == 1 && i != j)
                {
                    isAntisymmetric = false;
                    break;
                }
            }

            if (!isAntisymmetric)
                break;
        }

        // Перевірка транзитивності
        bool isTransitive = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] == 1)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (matrix[j][k] == 1 && matrix[i][k] != 1)
                        {
                            isTransitive = false;
                            break;
                        }
                    }
                }

                if (!isTransitive)
                    break;
            }

            if (!isTransitive)
                break;
        }

        // Перевірка нетранзитивності
        bool isNontransitive = true;
        for (int i = 0; i < n; i++)
        {
            for (int j = 0; j < n; j++)
            {
                if (matrix[i][j] != 1)
                {
                    for (int k = 0; k < n; k++)
                    {
                        if (matrix[j][k] == 1 && matrix[i][k] == 0)
                        {
                            isNontransitive = false;
                            break;
                        }
                    }
                }

                if (!isNontransitive)
                    break;
            }

            if (!isNontransitive)
                break;
        }

        return (isTransitive, isNontransitive);
    }

    public static void PrintRanking(List<NameValue> list)
    {
        list.ForEach(item => Console.WriteLine($"{item.Name}:\t{item.Value}"));
    }
}