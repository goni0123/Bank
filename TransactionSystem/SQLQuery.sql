-- Create database transactionss
--use transactionss
CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Salt VARBINARY(16) NOT NULL,
    Balance DECIMAL(18,2) DEFAULT (500),
    Transaction_count INT DEFAULT (0),
    Discount FLOAT DEFAULT (0),
    Role NVARCHAR(50) NOT NULL DEFAULT ('User')
);
CREATE TABLE Transactions (
    TransactionId INT PRIMARY KEY identity(1,1),
    Sender INT,
    Receiver INT,
    Amount DECIMAL(18, 2),
    Fee DECIMAL(18, 2),
	TransactionDate DATETIME,
    FOREIGN KEY (Sender) REFERENCES Users (Id),
    FOREIGN KEY (Receiver) REFERENCES Users (Id)
);
Create Table Bills(
	BillId int Primary key identity(1,1),
	Name varchar(100),
	Amount DECIMAL(18, 2),
	Status bit,
	UserId int,
	FOREIGN KEY (UserId) REFERENCES Users (Id)
);

-- Trigger to update transaction count and apply discount after each transaction
CREATE TRIGGER UpdateTransactionCountAndDiscount
ON Transactions
AFTER INSERT
AS
BEGIN
    -- Update the transaction count for the sender account
    UPDATE Users
    SET Transaction_count = Transaction_count + 1
    WHERE Id = (SELECT Sender FROM inserted);

    -- Check if the transaction count is a multiple of 5
    IF (SELECT Transaction_count FROM Users WHERE Id = (SELECT Sender FROM inserted)) % 5 = 0
    BEGIN
        -- Update the discount for the sender account by adding 0.10
        UPDATE Users
        SET Discount = Discount + 0.10
        WHERE Id = (SELECT Sender FROM inserted);
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
    @Id INT,
    @Amount DECIMAL(18, 2),
    @Fee DECIMAL(18, 2),
    @Discount FLOAT
AS
BEGIN
    UPDATE Users
    SET Balance = Balance - (@Amount + (@Fee - (@Fee * @Discount)))
    WHERE Id = @Id
END;
CREATE PROCEDURE UpdateAccountBalanceNoDiscount
    @Id INT,
    @Amount DECIMAL(18, 2),
    @Fee DECIMAL(18, 2),
    @Discount FLOAT
AS
BEGIN
    UPDATE Users 
	SET Balance = Balance - (@Amount+@Fee) WHERE Id = @Id
END;
CREATE PROCEDURE UpdateBill
    @BillId INT,
	@UserId int
AS
BEGIN
    UPDATE Bills 
	SET Status = 1 WHERE BillId = @BillId and UserId=@UserId 
END;
