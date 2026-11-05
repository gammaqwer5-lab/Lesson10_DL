namespace DeadlockLab;


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
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));

        // TODO 5
        if (ReferenceEquals(from, to)) throw new ArgumentException("счета должны отличаться");


        lock (from.SyncRoot)
        {
            lock (to.SyncRoot)
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

    public void TransferSafe(
        BankAccount from,
        BankAccount to,
        decimal amount)
    {
        if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount));
        if (ReferenceEquals(from, to)) throw new ArgumentException("счета должны отличаться");

        BankAccount first;
        BankAccount second;
        if (from.Id < to.Id)
        {
            first = from;
            second = to;
        }
        else
        {
            first = to;
            second = from;
        }

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
