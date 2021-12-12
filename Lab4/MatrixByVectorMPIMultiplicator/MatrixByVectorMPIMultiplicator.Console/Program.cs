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

MPI.Environment.Run(ref args, comm =>
{
    var n = comm.Size;
    
    var matrix = new int[n,n];
    var vector = new int[n];

    if (comm.Rank is 0)
    {
        FillInMatrix(matrix, n);
        FillInVector(vector, n);
    }

    comm.Broadcast<int[,]>(ref matrix, 0);
    comm.Broadcast<int[]>(ref vector, 0);
  
    var multiplyResult = MultiplyMatrixRowOnVector(matrix, vector, comm.Rank, n);
    Console.WriteLine($"Processor with rank {comm.Rank} counted MultiplyMatrixRowOnVector value {multiplyResult}");
        
    var resultingVector = new int[n];
    
    comm.Gather(multiplyResult, 0, ref resultingVector);
    if (comm.Rank is 0)
    {
        Console.WriteLine($"Resulting vector: [{string.Join(",", resultingVector)}]");
    }
});