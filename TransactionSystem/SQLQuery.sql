-- Create the Users table
CREATE TABLE Users
(
    Id INT PRIMARY KEY IDENTITY(1,1),
    Username NVARCHAR(50) NOT NULL,
    Password NVARCHAR(100) NOT NULL,
    Salt VARBINARY(16) NOT NULL,
    Balance DECIMAL(18,2) DEFAULT (200),
    Transaction_count INT,
    Discount FLOAT DEFAULT (0),
    Role NVARCHAR(50) NOT NULL DEFAULT ('User')
);
