void FillInVector(int[] vector, int n)
{
    var random = new Random(321);
    
    for (var i = 0; i < n; i++)
        vector[i] = random.Next(1, 10);
}

void FillInMatrix(int[,] matrix, int n)
{
    var random = new Random(123);
    
    for (var i = 0; i < n; i++)
        for (var j = 0; j < n; j++)
            matrix[i, j] = random.Next(1, 10);
}

int MultiplyMatrixRowOnVector(int[,] matrix, int[] vector, int row, int n)
{
    var sum = 0;

    for (var j = 0; j < n; j++)
        sum += matrix[row, j] * vector[j];

    return sum;
}

var n = 10000;
    
var matrix = new int[n,n];
var vector = new int[n];

FillInMatrix(matrix, n);
FillInVector(vector, n);

var resultingVector = new int[n];
for (int i = 0; i < n; i++)
{
    resultingVector[i] = MultiplyMatrixRowOnVector(matrix, vector, i, n);
}

Console.WriteLine($"Resulting vector: [{string.Join(",", resultingVector)}]");