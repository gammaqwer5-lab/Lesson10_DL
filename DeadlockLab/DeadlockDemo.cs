namespace DeadlockLab;

// TODO 1. Изменить порядок захвата.
// Измените порядок захвата блокировок в потоке B.
// Оба потока должны сначала захватывать lockA, а затем lockB.
// Проверьте, что deadlock больше не воспроизводится.

// TODO 2. Убрать Barrier.
// Удалите Barrier и SignalAndWait().
// Запустите программу несколько раз.
// Объясните в комментарияъ, почему deadlock теперь возникает не при каждом запуске.

// Без барьера потоки стартуют рассинхронизированно

// TODO 3. Изменить тайм-аут диагностики.
// Измените Join(2000) на Join(5000).
// Объясните, почему увеличение времени ожидания не исправляет взаимную блокировку.

// Join ждёт завершения потока максимум указанное время. Поэтому увеличение задержки не является способом устранения взаимной блокировки

// TODO 4. Добавить третий ресурс.
// Создайте lockC.
// Организуйте три потока:
// A -> B;
// B -> C;
// C -> A.
public class DeadlockDemo
{
    public static void Run()
    {
        Console.WriteLine("=== DEADLOCK DEMO ===");

        // Два независимых ресурса. Каждый объект используется как отдельная блокировка.
        object lockA = new();
        object lockB = new();
        object lockC = new();
         
        using Barrier barrier = new(3);

        Thread threadA = new(() =>
        {
            Console.WriteLine("Поток A: ожидает ресурс A");

            // Первый поток. Сначала захватывает A, затем пытается получить B.
            lock (lockA)
            {
                Console.WriteLine("Поток A: захватил ресурс A");

                // Синхронизация начала ожидания. Оба потока должны дойти до этой строки. 
                // Только после этого они продолжат выполнение и попытаются захватить вторую блокировку. 
                // Это делает воспроизведение deadlock практически детерминированным.
                barrier.SignalAndWait();

                Console.WriteLine("Поток A: ожидает ресурс B");

                lock (lockB)
                {
                    Console.WriteLine("Поток A: получил оба ресурса");
                }
            }
        });

        Thread threadB = new(() =>
        {
            Console.WriteLine("Поток B: ожидает ресурс B");

            // Второй поток. Сначала захватывает B, затем пытается получить A.
            lock (lockB)
            {
                Console.WriteLine("Поток B: захватил ресурс B");

                // Синхронизация начала ожидания. Оба потока должны дойти до этой строки. 
                // Только после этого они продолжат выполнение и попытаются захватить вторую блокировку. 
                // Это делает воспроизведение deadlock практически детерминированным.
                barrier.SignalAndWait();

                Console.WriteLine("Поток B: ожидает ресурс C");

                lock (lockC)
                {
                    Console.WriteLine("Поток B: получил оба ресурса");
                }
            }
        });

        Thread threadC = new(() =>
        {
            Console.WriteLine("Поток C: ожидает ресурс B");

            lock (lockC)
            {
                Console.WriteLine("Поток C: захватил ресурс C");
                barrier.SignalAndWait();

                Console.WriteLine("Поток C: ожидает ресурс A");

                lock (lockA)
                {
                    Console.WriteLine("Поток C: получил оба ресурса");
                }
            }
        });

        // Фоновые потоки позволят завершить демонстрационное
        // приложение, даже если потоки останутся заблокированными.
        threadA.IsBackground = true;
        threadB.IsBackground = true;
        threadC.IsBackground = true;

        threadA.Start();
        threadB.Start();
        threadC.Start();

        // Обнаружение зависания
        // `Join(2000)` ждёт завершения потока максимум две секунды.
        // Если поток не завершился, метод возвращает `false`.
        // Обратите внимание: `Join` здесь используется для диагностики, а не для устранения deadlock. 
        // После обнаружения зависания потоки остаются заблокированными. 
        // Поэтому это отдельный демонстрационный запуск.
        bool completedA = threadA.Join(5000);
        bool completedB = threadB.Join(5000);
        bool completedC = threadB.Join(5000);

        Console.WriteLine();

        if (!completedA || !completedB || !completedC)
        {
            Console.WriteLine("DEADLOCK ОБНАРУЖЕН!");
            Console.WriteLine("Потоки не завершились за 5 секунды.");
        }
        else
        {
            Console.WriteLine("3 потока завершились.");
        }
    }
}
