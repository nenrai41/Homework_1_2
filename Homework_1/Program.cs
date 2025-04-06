//ВАРИАНТ 2, ГРУППА М2О-208БВ-23, ГАРИФУЛЛИН РОБЕРТ ЭДУАРДОВИЧ
//ДЗ№1. МЕТОД ИСКЛЮЧЕНИЯ ГАУССА
/*
 * СЛАУ:
 * x1 + x2 + 20x3 = 2
 * 8x1 + 20x2 + x3 = 8
 * 20x1 + x2 + x3 = 2
 */
{
    float[,] system = new float[3, 4] { { 1, 1, 20, 2 }, { 8, 20, 1, 8 }, { 20, 1, 1, 2 } };

    Console.WriteLine("Матрица системы");

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            Console.Write(system[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

    //ПРЯМОЙ ХОД ГАУССА
    float[,] rightTriangleMatrix = system;

    //КОЭФФИЦИЕНТЫ ДЛЯ ОБНУЛЕНИЯ ЭЛЕМЕНТОВ ПОД ДИАГОНАЛЬЮ 
    float nu_21 = system[1, 0] / system[0, 0];
    float nu_31 = system[2, 0] / system[0, 0];
    float nu_32 = 0;


    for (int i = 1; i <= 3; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            if (i == 1)
                rightTriangleMatrix[i, j] = system[i, j] - (system[i - 1, j]) * nu_21;
            else if (i == 2)
                rightTriangleMatrix[i, j] = system[i, j] - (system[i - 2, j]) * nu_31;
        }
        if (i == 3)
        {
            nu_32 = rightTriangleMatrix[2, 1] / rightTriangleMatrix[1, 1];
            for (int k = 0; k < 4; k++)
                rightTriangleMatrix[i - 1, k] = rightTriangleMatrix[i - 1, k] - (rightTriangleMatrix[i - 2, k]) * nu_32;
        }
    }

    Console.WriteLine("Верхнетреугольная матрица");

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            Console.Write(rightTriangleMatrix[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

    //ОПРЕДЕЛИТЕЛЬ МАТРИЦЫ ПО МЕТОДУ ГАУССА
    float determenant = 1;
    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 4; j++)
        {
            if (i == j)
                determenant *= rightTriangleMatrix[i, j];
        }
    }

    Console.WriteLine("Определитель матрицы: " + determenant);
    Console.WriteLine();

    //ОБРАТНЫЙ ХОД 

    /*
    float x1, x2, x3 = 0;
    x3 = rightTriangleMatrix[2, 3] / rightTriangleMatrix[2, 2];
    x2 = (rightTriangleMatrix[1, 3] - rightTriangleMatrix[1, 2] * x3) / rightTriangleMatrix[1, 1];
    x1 = (rightTriangleMatrix[0, 3] - rightTriangleMatrix[0, 2] * x3 - rightTriangleMatrix[0, 1] * x2) / rightTriangleMatrix[0, 0];
    Console.WriteLine("Значения неизвестных:");
    Console.WriteLine("x1 = " + x1);
    Console.WriteLine("x2 = " + x2);
    Console.WriteLine("x3 = " + x3);
    */

    float[] results = new float[3];

    results[2] = rightTriangleMatrix[2, 3] / rightTriangleMatrix[2, 2]; //x3
    results[1] = (rightTriangleMatrix[1, 3] - rightTriangleMatrix[1, 2] * results[2]) / rightTriangleMatrix[1, 1]; //x2
    results[0] = (rightTriangleMatrix[0, 3] - rightTriangleMatrix[0, 2] * results[2] - rightTriangleMatrix[0, 1] * results[1]) / rightTriangleMatrix[0, 0]; //x1

    //РЕЗУЛЬТАТЫ

    Console.WriteLine("Значения неизвестных:");

    for (int i = 0; i < 3; i++)
    {
        Console.WriteLine($"X{i + 1} равно: " + results[i]);
    }
    Console.WriteLine();
    
}

//ПОИСК ОБРАТНОЙ МАТРИЦЫ МЕТОДОМ ГАУССА-ЖОРДАНА
{
    float[,] system = new float[3, 6] { { 1, 1, 20, 1, 0, 0 }, { 8, 20, 1, 0, 1, 0 }, { 20, 1, 1, 0, 0, 1 } };

    Console.WriteLine("Матрица системы, объедененная с единичной");

    for (int i = 0; i < 3; i++)
    {
        for (int j = 0; j < 6; j++)
        {
            Console.Write(system[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

    //ПРЯМОЙ ХОД ГАУССА
    float[,] rightTriangleMatrix = system;

    float nu_21 = system[1, 0] / system[0, 0];
    float nu_31 = system[2, 0] / system[0, 0];
    float nu_32 = 0;

    //ПРИВОДИМ К ПРАВОТРЕУГОЛЬНОЙ МАТРИЦЕ
    for (int i = 1; i <= 3; i++)
    {
        for (int j = 0; j < 6; j++)
        {
            if (i == 1)
                rightTriangleMatrix[i, j] = system[i, j] - (system[i - 1, j]) * nu_21;
            else if (i == 2)
                rightTriangleMatrix[i, j] = system[i, j] - (system[i - 2, j]) * nu_31;
        }
        if (i == 3)
        {
            nu_32 = rightTriangleMatrix[2, 1] / rightTriangleMatrix[1, 1];
            for (int k = 0; k < 6; k++)
                rightTriangleMatrix[i - 1, k] = rightTriangleMatrix[i - 1, k] - (rightTriangleMatrix[i - 2, k]) * nu_32;
        }
    }

    //ПРИВОДИМ К ДИАГОНАЛЬНОЙ МАТРИЦЕ, ИЗБАВЛЯЯСЬ ОТ ЭЛЕМЕТНОВ ЛЕЖАЩИХ НАД ГЛАВНОЙ ДИАГОНАЛЬЮ

    bool hasThePassageOnThe2ndStrokeBeenCompleted = false;
    bool hasThePassageOnThe1ndStrokeAnd2ndColumnBeenCompleted = false;

    //КОЭФФИЦИЕНТЫ ДЛЯ ОБНУЛЕНИЯ ЭЛЕМЕНТОВ ПОД ГЛАВНОЙ ДИАГОНАЛЬЮ 
    float nu_12 = 0;
    float nu_13 = 0;
    float nu_23 = rightTriangleMatrix[1, 2] / rightTriangleMatrix[2, 2];
    for (int i = 3; i > 0; i--)
    {
        if (!hasThePassageOnThe2ndStrokeBeenCompleted)
        {
            for (int k = 0; k < 6; k++)
                rightTriangleMatrix[i - 2, k] = rightTriangleMatrix[i - 2, k] - (rightTriangleMatrix[i - 1, k]) * nu_23;
            nu_12 = rightTriangleMatrix[0, 1] / rightTriangleMatrix[1, 1];
            hasThePassageOnThe2ndStrokeBeenCompleted = true;
        }
        else if (!hasThePassageOnThe1ndStrokeAnd2ndColumnBeenCompleted)
        {
            for (int k = 0; k < 6; k++)
                rightTriangleMatrix[i - 2, k] = rightTriangleMatrix[i - 2, k] - (rightTriangleMatrix[i - 1, k]) * nu_12;
            nu_13 = rightTriangleMatrix[0, 2] / rightTriangleMatrix[2, 2];
            hasThePassageOnThe1ndStrokeAnd2ndColumnBeenCompleted = true;
        }
        else
            for (int k = 0; k < 6; k++)
                rightTriangleMatrix[i - 1, k] = rightTriangleMatrix[i - 1, k] - (rightTriangleMatrix[i + 1, k]) * nu_13;
    }

    //КОЭФФИЦИЕНТЫ ДЛЯ ОБНУЛЕНИЯ ЭЛЕМЕНТОВ НАД ГЛАВНОЙ ДИАГОНАЛЬЮ 
    float nu_11 = rightTriangleMatrix[0, 0];
    float nu_22 = rightTriangleMatrix[1, 1];
    float nu_33 = rightTriangleMatrix[2, 2];

    for (int j = 0; j < 6; j++)
        rightTriangleMatrix[0, j] = rightTriangleMatrix[0, j] / nu_11;
    for (int k = 0; k < 6; k++)
        rightTriangleMatrix[1, k] = rightTriangleMatrix[1, k] / nu_22;
    for (int n = 0; n < 6; n++)
        rightTriangleMatrix[2, n] = rightTriangleMatrix[2, n] / nu_33;


    Console.WriteLine("Обратная матрица");

    for (int i = 0; i < 3; i++)
    {
        for (int j = 3; j < 6; j++)
        {
            Console.Write(rightTriangleMatrix[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

}

//ДЗ№2. МЕТОД ПРОГОНКИ
/*ВАРИАНТ 2 (6)
 * 6x1 - 5x2 = -58
 * -6x1 + 16x2 +9x3 = 161
 * 9x2 - 17x3 - 3x4 = -114
 * 8x3 +22x4 - 8x5 = -90
 * 6x4 - 13x5 = -55
 */
{
    Console.WriteLine("Матрица системы");
    float[,] system = new float[5, 6] { { 6, -5, 0, 0, 0, -58 }, { -6, 16, 9, 0, 0, 161 }, { 0, 9, -17, -3, 0, -114 }, { 0, 0, 8, 22, -8, -90 }, { 0, 0, 0, 6, -13, -55 } };
    for (int i = 0; i < 5; i++)
    {
        for (int j = 0; j < 6; j++)
        {
            Console.Write(system[i, j] + "\t");
        }
        Console.WriteLine();
    }
    Console.WriteLine();

    //ПОИСК ALFA И BETA
    float[,] arrayOfAlfaBeta = new float[4, 2];
    arrayOfAlfaBeta[0, 0] = -system[0, 1] / system[0, 0]; //alfa_2
    arrayOfAlfaBeta[0, 1] = system[0, 5] / system[0, 0]; //beta_2

    bool isPassage = false;
    int stroke = 1;
    int column = 0;

    float a = 0;
    float b = 0;
    float c = 0;
    for (int i = 1; i < 4; i++)
    {
        for (int j = 0; j < 1; j++)
        {
            //Цикл коэффициентов
            while (!isPassage)
            {
                if (system[stroke, column] != 0 && !isPassage)
                {
                    a = system[stroke, column];
                    b = system[stroke, column + 1];
                    c = system[stroke, column + 2];
                    isPassage = true;
                }
                column++;
            }
            arrayOfAlfaBeta[i, j] = -(c / (b + a * arrayOfAlfaBeta[i - 1, j])); //alfa_i
            arrayOfAlfaBeta[i, j + 1] = ((system[stroke, 5] - (a * arrayOfAlfaBeta[i - 1, j + 1])) / (b + a * arrayOfAlfaBeta[i - 1, j])); //beta_i

            stroke++;
            column = 0;
            isPassage = false;
        }
    }

    Console.WriteLine("Коэффициенты alfa и beta:");
    for (int i = 0; i < 4; i++)
    {
        for (int j = 0; j < 1; j++)
        {
            Console.WriteLine($"alfa{i+2}: " + arrayOfAlfaBeta[i,j] + "\t" + $"beta{i + 2}: "+ arrayOfAlfaBeta[i, j+1]);
        }
    }
    Console.WriteLine();

    //ОБРАТНЫЙ ХОД
    float[] results = new float[5];
    results[4] = ((system[4, 5] - (arrayOfAlfaBeta[3, 1] * system[4, 3])) / (system[4, 4] + (arrayOfAlfaBeta[3, 0] * system[4, 3])));

    for (int i = 3; i >= 0; i--)
        results[i] = ((arrayOfAlfaBeta[i, 0] * results[i + 1]) + arrayOfAlfaBeta[i, 1]);

    //РЕЗУЛЬТАТЫ 
    Console.WriteLine("Значения неизвестных:");
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"X{i+1} равно: " + results[i]);
    }
    Console.ReadKey();
}

