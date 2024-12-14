using Snake;

Coordinates gridDimensions = new Coordinates(50, 20);
Coordinates snakeStart = new Coordinates(10, 1);

Random rand = new Random();
Coordinates applePos = new Coordinates(rand.Next(1, gridDimensions.X - 1), rand.Next(1, gridDimensions.Y - 1));

int FrameRate = 100;

Direction currentDirection = Direction.Down;

List<Coordinates> snakePosHistory = new List<Coordinates>();
int tailLength = 1;

int score = 0;

while (true)
{
    Console.Clear();
    Console.WriteLine($"Score: {score}");
    snakeStart.ApplyMovementDirection(currentDirection);

    for (int y = 0; y < gridDimensions.Y; y++)
    {
        for (int x = 0; x < gridDimensions.X; x++)
        {
            Coordinates currentCoordinates = new Coordinates(x, y);

            if (snakeStart.Equals(currentCoordinates) || snakePosHistory.Contains(currentCoordinates))
            {
                Console.Write("■");
            }

            else if (applePos.Equals(currentCoordinates))
            {
                Console.Write("@");
            }

            else if (x == 0 || x == gridDimensions.X - 1 || y == 0 || y == gridDimensions.Y - 1)
            {
                Console.Write("#");
            }

            else
            {
                Console.Write(" ");
            }
        }
        Console.WriteLine();
    }

    if (snakeStart.Equals(applePos))
    {
        tailLength++;
        score++;
        applePos = new Coordinates(rand.Next(1, gridDimensions.X - 1), rand.Next(1, gridDimensions.Y - 1));
    }
    else if(snakeStart.X == 0 || snakeStart.X == gridDimensions.X - 1 || snakeStart.Y == 0 || snakeStart.Y == gridDimensions.Y - 1 || snakePosHistory.Contains(snakeStart))
    {
        score = 0;
        tailLength = 1;
        snakePosHistory.Clear();
        snakeStart = new Coordinates(10, 1);
        currentDirection = Direction.Down;
        continue;
    }

    snakePosHistory.Add(new Coordinates(snakeStart.X, snakeStart.Y));

    if(snakePosHistory.Count > tailLength)
    {
        snakePosHistory.RemoveAt(0);
    }

    DateTime time = DateTime.Now;

    while ((DateTime.Now - time).Milliseconds < FrameRate)
    {
        if (Console.KeyAvailable)
        {
            ConsoleKey key = Console.ReadKey().Key;
            switch (key)
            {
                case ConsoleKey.UpArrow:
                    currentDirection = Direction.Up;
                    break;
                case ConsoleKey.DownArrow:
                    currentDirection = Direction.Down;
                    break;
                case ConsoleKey.LeftArrow:
                    currentDirection = Direction.Left;
                    break;
                case ConsoleKey.RightArrow:
                    currentDirection = Direction.Right;
                    break;
            }
        }
    }
}
