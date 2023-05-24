--Create Database Transactions
--use Transactions-- Create the Accounts table
CREATE TABLE Accounts (
    AccountId INT PRIMARY KEY identity(1,1),
    AccountName varchar(50) not null,
	Transaction_count int DEFAULT 0,
	Balance DECIMAL(18, 2) DEFAULT 0,
    Discount float DEFAULT 0 -- Automatically set the discount to 0 for each account
);

-- Create the Transactions table
CREATE TABLE Transactions (
    TransactionId INT PRIMARY KEY identity(1,1),
    Sender INT,
    Receiver INT,
    Amount DECIMAL(18, 2),
    Fee DECIMAL(18, 2),
	TransactionDate DATETIME,
    FOREIGN KEY (Sender) REFERENCES Accounts (AccountId),
    FOREIGN KEY (Receiver) REFERENCES Accounts (AccountId)
);

-- Trigger to update transaction count and apply discount after each transaction
CREATE TRIGGER UpdateTransactionCountAndDiscount
ON Transactions
AFTER INSERT
AS
BEGIN
    -- Update the transaction count for the sender account
    UPDATE Accounts
    SET Transaction_count = Transaction_count + 1
    WHERE AccountId = (SELECT Sender FROM inserted);

    -- Check if the transaction count is a multiple of 5
    IF (SELECT Transaction_count FROM Accounts WHERE AccountId = (SELECT Sender FROM inserted)) % 5 = 0
    BEGIN
        -- Update the discount for the sender account by adding 0.10
        UPDATE Accounts
        SET Discount = Discount + 0.10
        WHERE AccountId = (SELECT Sender FROM inserted);
    END
END;
CREATE PROCEDURE InsertTransaction
    @Sender INT,
    @Receiver INT,
    @Amount DECIMAL(18, 2),
    @Fee DECIMAL(18, 2),
    @TransactionDate DATETIME
AS
BEGIN
    INSERT INTO Transactions (Sender, Receiver, Amount, Fee, TransactionDate)
    VALUES (@Sender, @Receiver, @Amount, @Fee, @TransactionDate);
END;
CREATE PROCEDURE UpdateAccountBalance
    @AccountId INT,
    @Amount DECIMAL(18, 2),
    @Fee DECIMAL(18, 2),
    @Discount FLOAT
AS
BEGIN
    UPDATE Accounts
    SET Balance = Balance - (@Amount + (@Fee - (@Fee * @Discount)))
    WHERE AccountId = @AccountId
END
CREATE PROCEDURE UpdateAccountBalanceNoDiscount
    @AccountId INT,
    @Amount DECIMAL(18, 2),
    @Fee DECIMAL(18, 2),
    @Discount FLOAT
AS
BEGIN
    UPDATE Accounts 
	SET Balance = Balance - (@Amount+@Fee) WHERE AccountId = @AccountId
END
