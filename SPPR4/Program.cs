using SPPR4;

var listItems = new List<IdNamePriority>()
{
    new() { Id = 1, Name = "a", Priority = 1 },
    new() { Id = 2, Name = "b", Priority = 1 },
    new() { Id = 3, Name = "c", Priority = 4 },
    new() { Id = 4, Name = "d", Priority = 1 },
    new() { Id = 5, Name = "e", Priority = 4 },
};

var matrix = Matrix.CreateMatrix(listItems);
Matrix.PrintMatrix(matrix, listItems);
Console.WriteLine();
matrix = Matrix.AddSummaMatrix(matrix);
Matrix.PrintMatrix(matrix, listItems);
Console.WriteLine();
var ranking = Matrix.GetRanking(matrix, listItems);
Matrix.PrintRanking(ranking);
Console.WriteLine();
var groups = Matrix.GetAllGroups(listItems);

foreach (var group in groups)
{
    var groupMatrix = Matrix.CreateMatrix(group);
    var (transitive, nonTransitive) = Matrix.CheckMatrixProperties(groupMatrix.Select(x => x.ToArray()).ToArray());
    var result = transitive ? "Transitive" : nonTransitive ? "No transitive" : "Transitivity cannot be checked";
    Console.WriteLine($"{string.Join(", ", group.Select(x => x.Name))}: {result}");
}

//Matrix.CheckMatrixProperties(matrix.Select(x => x.ToArray()).ToArray());

