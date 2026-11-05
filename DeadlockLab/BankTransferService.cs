namespace DeadlockLab;

/*
public class BankTransferService
{
    // У каждого счёта есть уникальный идентификатор.
    // Независимо от направления перевода блокировки всегда захватываются в одном порядке: сначала счёт с меньшим `Id`, потом с большим.
    // восстановить
    // Обратите внимание: порядок определяется не направлением перевода, а идентификаторами счетов.
    // создайте на основе этого методаTransferSafe
    // оставил подсказки(вернуть  переменные from и to при lock'ах)
    public void TransferUnsafe(
        BankAccount from,
        BankAccount to,
        decimal amount)
    {
        // TODO 3

        // TODO 5


        lock (first.SyncRoot)
        {
            lock (second.SyncRoot)
            {
                if (from.GetBalanceUnsafe() < amount)
                    throw new InvalidOperationException(
                        "Недостаточно средств."
                    );

                from.SetBalanceUnsafe(
                    from.GetBalanceUnsafe() - amount
                );

                to.SetBalanceUnsafe(
                    to.GetBalanceUnsafe() + amount
                );
            }
        }
    }
}
*/