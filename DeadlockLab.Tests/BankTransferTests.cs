using DeadlockLab;

namespace DeadlockLab.Tests;

public class BankTransferTests
{
    [Fact]
    public void TransferSafe_ShouldPreserveTotalBalance()
    {
        var accountA = new BankAccount(1000);
        var accountB = new BankAccount(2000);

        var service = new BankTransferService();

        service.TransferSafe(accountA, accountB, 500);

        Assert.Equal(500, accountA.Balance);
        Assert.Equal(2500, accountB.Balance);
        Assert.Equal(3000, accountA.Balance + accountB.Balance);
    }

    [Fact]
    public void TransferSafe_ShouldRejectInsufficientFunds()
    {
        var accountA = new BankAccount(100);
        var accountB = new BankAccount(200);

        var service = new BankTransferService();

        Assert.Throws<InvalidOperationException>(() =>
            service.TransferSafe(accountA, accountB, 500)
        );

        Assert.Equal(100, accountA.Balance);
        Assert.Equal(200, accountB.Balance);
    }

    [Fact]
    public void TransferSafe_ShouldRejectSameAccount()
    {
        var account = new BankAccount(1000);

        var service = new BankTransferService();

        Assert.Throws<ArgumentException>(() =>
            service.TransferSafe(account, account, 100)
        );

        Assert.Equal(1000, account.Balance);
    }

    [Fact]
    public async Task TransferSafe_ShouldPreserveBalanceUnderConcurrency()
    {
        var accountA = new BankAccount(10000);
        var accountB = new BankAccount(10000);

        var service = new BankTransferService();

        Task taskA = Task.Run(() =>
        {
            for (int i = 0; i < 1000; i++)
                service.TransferSafe(accountA, accountB, 1);
        });

        Task taskB = Task.Run(() =>
        {
            for (int i = 0; i < 1000; i++)
                service.TransferSafe(accountB, accountA, 1);
        });

        await Task.WhenAll(taskA, taskB).WaitAsync(
            TimeSpan.FromSeconds(5)
        );

        Assert.Equal(20000, accountA.Balance + accountB.Balance);
    }
}
