namespace DeadlockLab;
/*
// TODO 1. Проверить неправильную реализацию.
// Вместо TransferSafe вызовите TransferUnsafe.
// Запустите два параллельных перевода.
// Объясните, почему программа может зависнуть.
// Важно: для проверки `TransferUnsafe` используйте отдельный демонстрационный запуск, а не запускайте после него `TransferSafe` в том же процессе. Заблокированные потоки могут остаться работать.

// TODO 2. Изменить количество переводов.
// Увеличьте количество операций до 10 000.
// Проверьте, что общий баланс остаётся неизменным.

// TODO 3. Добавить проверку недостатка средств.
// Попробуйте перевести сумму, превышающую баланс.
// Убедитесь, что ни один из счетов не изменился.

// TODO 4. Добавить третий счёт.
// Создайте третий банковский счёт.
// Запустите параллельные переводы между всеми тремя счетами.
// Во всех операциях должен соблюдаться единый порядок захвата.

// TODO 5. Проверить граничный сценарий.
// Попробуйте перевести деньги с одного счёта на него же.
// Реализуйте защиту от такой операции.
public class TransferDemo
{
    public static async Task RunSafeAsync()
    {
        Console.WriteLine("=== SAFE TRANSFER DEMO ===");

        BankAccount accountA = new(1000);
        BankAccount accountB = new(2000);

        BankTransferService service = new();

        const int operations = 1000;
        const decimal amount = 1;

        Task[] tasks = new Task[2];

        tasks[0] = Task.Run(() =>
        {
            for (int i = 0; i < operations; i++)
            {
                service.TransferSafe(
                    accountA,
                    accountB,
                    amount
                );
            }
        });

        tasks[1] = Task.Run(() =>
        {
            for (int i = 0; i < operations; i++)
            {
                service.TransferSafe(
                    accountB,
                    accountA,
                    amount
                );
            }
        });

        await Task.WhenAll(tasks);

        Console.WriteLine(
            $"Баланс A: {accountA.Balance}"
        );

        Console.WriteLine(
            $"Баланс B: {accountB.Balance}"
        );

        Console.WriteLine(
            $"Общий баланс: {accountA.Balance + accountB.Balance}"
        );
    }
}
*/