using System;
using NUnit.Framework;

/// <summary>
/// This class contains simple test cases
/// for the bank account Program class.
/// Each test checks one clear behaviour.
/// </summary>
[TestFixture] // Tells NUnit that this class contains tests
public class UnitTest
{
    /// <summary>
    /// Test that a valid deposit amount
    /// correctly increases the account balance.
    /// </summary>
    [Test] // Marks this method as a test
    public void Test_Deposit_ValidAmount()
    {
        // Start with an account that has 100 as initial balance
        Program account = new Program(100m);

        // Deposit 50 into the account
        account.Deposit(50m);

        // After deposit, balance should be 150
        decimal expectedBalance = 150m;

        // Only one Assert: check expected vs actual
        Assert.AreEqual(expectedBalance, account.Balance);
    }

    /// <summary>
    /// Test that depositing a negative amount
    /// throws an exception with the correct message.
    /// </summary>
    [Test]
    public void Test_Deposit_NegativeAmount()
    {
        // Start with any initial balance, for example 100
        Program account = new Program(100m);

        // One Assert that also checks the exception message
        Assert.That(
            () => account.Deposit(-50m),
            Throws.Exception.With.Message.EqualTo("Deposit amount cannot be negative")
        );
    }

    /// <summary>
    /// Test that a valid withdrawal amount
    /// correctly decreases the account balance.
    /// </summary>
    [Test]
    public void Test_Withdraw_ValidAmount()
    {
        // Start with an account that has 200 as initial balance
        Program account = new Program(200m);

        // Withdraw 80 from the account
        account.Withdraw(80m);

        // After withdrawal, balance should be 120
        decimal expectedBalance = 120m;

        // Only one Assert: check expected vs actual
        Assert.AreEqual(expectedBalance, account.Balance);
    }

    /// <summary>
    /// Test that withdrawing more than the balance
    /// throws an exception with the "Insufficient funds." message.
    /// </summary>
    [Test]
    public void Test_Withdraw_InsufficientFunds()
    {
        // Start with an account that has 100 as initial balance
        Program account = new Program(100m);

        // Try to withdraw 200, which is more than the balance
        // One Assert that also checks the exception message
        Assert.That(
            () => account.Withdraw(200m),
            Throws.Exception.With.Message.EqualTo("Insufficient funds.")
        );
    }
}

