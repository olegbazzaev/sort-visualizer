using System.Numerics; // предоставляет классы для работы с числами с большей точностью, чем стандартные типы данных
using System.Threading; // предоставляет классы для работы с потоками
using Raylib_cs; // отрисовка графики

class Program 
{
    static int[] elements = new int[198]; // Создаёт статический целочисленный массив elements размером 198 для
                                          // хранения элементов, которые будут сортироваться.
    static int barWidth = 5; // Задает статическую переменную barWidth шириной столбца в 5 пикселей, который будет
                             // использоваться для визуального представления элементов массива.
    static Random rand = new Random(); // Создаёт статический объект rand класса Random для генерации случайных чисел.
    static int numberOfAsync = 1; // Задает статическую переменную numberOfAsync равной 1,
                                  // которая определяет количество асинхронных потоков сортировки, запущенных программой.


    static bool sorted = false; // Создаёт статическую переменную sorted типа bool с начальным значением false,
                                // которая будет использоваться для определения, отсортирован ли массив.
    static string algo = ""; //  Создаёт статическую строковую переменную algo с пустым начальным значением,
                             //  которая будет хранить название используемого алгоритма сортировки.

    public static void Main(string[] args) // 
    {
        Raylib.InitWindow(1000, 300, "Sort Visualizer"); // Инициализирует окно для визуализации сортировки
                                                         // размером 1000x300 пикселей с названием "Sort Visualizer",
                                                         // используя библиотеку Raylib.
        Raylib.SetTargetFPS(60); //  Устанавливает целевую частоту кадров (FPS) для отрисовки окна на 144 кадра в секунду.

        Task.Run(() => // Запускает асинхронную задачу с использованием лямбда-функции.
        {
            for (int i = 0; i < elements.Length; i++) //  Цикл for, который заполняет массив elements случайными числами от
                                                      //  0 до 250 с помощью объекта rand.
            {
                elements[i] = rand.Next(0, 251); 
                Thread.Sleep(5); // Внутри цикла добавляется пауза в 5 миллисекунд, для визуального эффекта заполнения массива.

            }
            
            for (int i = 0; i < numberOfAsync; i++) // Начало алгоритма
            {
            
                Task.Run(() => GnomeSort()); // чтобы изменить алгоритм сортировки, нужно измнить значение после =>
            }
        });

        while (!Raylib.WindowShouldClose()) // Главный цикл программы, который выполняется, пока окно приложения не должно быть закрыто.
        {
            Raylib.BeginDrawing(); // Начинает отрисовку нового кадра.
            Raylib.ClearBackground(Color.BLACK); // Очищает фон окна чёрным цветом. 

            Raylib.DrawText($"{algo}", 10, 12, 20, Color.WHITE); // Отображает название используемого алгоритма сортировки (algo)
                                                                 // в левом верхнем углу окна белым цветом.
            if (sorted){
                Raylib.DrawText("SORTED", 10, 35, 20, Color.GREEN); // Если массив отсортирован (sorted = true), выводит надпись "SORTED"
                                                                    // зелёным цветом.
            }

            
            int currentX = 10; // метод для отрисовки каждого столбца. Эта переменная будет отслеживать положение по оси X для каждого столбца,
                               // который мы будем рисовать.
            foreach (int number in elements) // Цикл foreach проходит по каждому элементу в массиве elements
            {
                Raylib.DrawRectangle(currentX, 300, -barWidth, -number, Color.WHITE);// Внутри цикла эта строка кода рисует прямоугольник
                currentX += barWidth;// После отрисовки каждого столбца значение currentX увеличивается на barWidth.
                                     //Это гарантирует, что следующий столбец будет отрисован с правильным отступом по оси X.
            }

            Raylib.EndDrawing();// Эта строка кода завершает процесс отрисовки кадра.
        }

        Raylib.CloseWindow();// Эта строка кода закрывает окно приложения Raylib.

    }

    // Алгоритмы сортировок
    static void BubbleSort()
    {
        algo = "Bubble Sort";
        int iterations = 5000; // максимальное количество проходов цикла сортировки.
        while (true)
        {
            bool swapped = false; // Переменная swapped используется для отслеживания, были ли сделаны какие-либо обмены
                                  // элементов в текущем проходе цикла.
            for (int i = 0; i < iterations; i++)
            {
                for (int j = 0; j < elements.Length - 1; j++)
                {
                    if (elements[j] > elements[j + 1]){
                        int temp = elements[j + 1];
                        elements[j + 1] = elements[j];
                        elements[j] = temp;
                        swapped = true;
                        Thread.Sleep(1);
                    }
                }
            }
            if (!swapped)
            {
                break;
            }
        }
        sorted = true;
    }

    static void InsertionSort(){
        algo = "Insertion Sort";
        int currentIndex = 0;
        for (int i = 1; i < elements.Length; i++)
        {
            currentIndex = i;
            while (elements[currentIndex] < elements[currentIndex - 1])
            {
                    int temp = elements[currentIndex - 1];
                    elements[currentIndex - 1] = elements[currentIndex];
                    elements[currentIndex] = temp;
                    if (currentIndex > 1){
                        currentIndex -= 1;
                    }
                    Thread.Sleep(1);
            }
        }
        sorted = true;
    }

    static void StalinSort() //Алгоритм сортировки Сталина — проходим по массиву и проверяем, по порядку ли стоят элементы.
                             //Каждый элемент, который нарушает порядок, удаляем.
                             //На выходе получаем массив, где все элементы стоят по порядку.
    {
        algo = "Stalin Sort";
        int bigger = 0;
        for (int i = 0; i < elements.Length; i++)
        {
            if (elements[i] >= bigger)
            {
                bigger = elements[i];
            }
            else
            {
                elements[i] = 0;
            }
            System.Threading.Thread.Sleep(1);
        }
        sorted = true;
    }

    static void GnomeSort()
    { // Гномья сортировка  — алгоритм сортировки, похожий на сортировку вставками, но в отличие от последней перед вставкой на нужное место
      // происходит серия обменов, как в сортировке пузырьком. Название происходит от предполагаемого поведения садовых гномов при сортировке
      // линии садовых горшков.
        algo = "Gnome Sort";

        int i = 1;
        while (i < elements.Length)
        {
            if (elements[i] < elements[i - 1])
            {
                int temp = elements[i - 1];
                elements[i - 1] = elements[i];
                elements[i] = temp;
                Thread.Sleep(1);
                if (i > 1)
                {
                    i--;
                }
            }
            else{
                i++;
            }
        }
        sorted = true;
    }

}