USE BANK
GO
--Problem 1. Create Table Logs
CREATE TABLE Logs
(
	LogId INT NOT NULL PRIMARY KEY IDENTITY,
	AccountId INT FOREIGN KEY(AccountId) REFERENCES Accounts(Id) NOT NULL,
	OldSum MONEY NOT NULL,
	NewSum MONEY NOT NULL,
)
GO

-- For Judge - only the trigger creation
CREATE TRIGGER tr_Accounts_Logs_After_Update ON Accounts
FOR UPDATE
AS
BEGIN
	INSERT INTO Logs VALUES   
	(
		(
			 SELECT d.Id
             FROM deleted as d
		),
		
		(
			 SELECT d.Balance
             FROM deleted as d
		),
		(
			 SELECT i.Balance
             FROM inserted as i
		)
	)
END
GO
--Problem 2. Create Table Emails
CREATE TABLE NotificationEmails 
(
	Id INT NOT NULL PRIMARY KEY IDENTITY,
	Recipient INT NOT NULL,
	Subject VARCHAR(MAX),
	Body VARCHAR(MAX)
)
GO

-- For Judge - only the trigger creation
CREATE TRIGGER tr_email_Whenever_New_Record_Is_Inserted_In_Logs_Table ON Logs
FOR INSERT
AS	
BEGIN
INSERT INTO NotificationEmails VALUES   
(
	(SELECT d.AccountId FROM inserted AS d),
	CONCAT('Balance change for account: ',(SELECT AccountId FROM inserted)),
	CONCAT('On ', FORMAT(GETDATE(), 'dd-MM-yyyy HH:mm'), ' your balance was changed from ',
               (SELECT OldSum FROM Logs), ' to ',(SELECT NewSum FROM Logs), '.')
)
END
GO

--Problem 3. Deposit Money
CREATE PROCEDURE usp_DepositMoney
(
	@accountId   INT,
	@moneyAmount MONEY
)
AS
BEGIN
    IF(@moneyAmount < 0)
        BEGIN
            RAISERROR('Cannot deposit negative value', 16, 1);
    END;
        ELSE
        BEGIN
            IF(@accountId IS NULL
               OR @moneyAmount IS NULL)
                BEGIN
                    RAISERROR('Missing value', 16, 1);
            END;
    END;
    BEGIN TRANSACTION;
    UPDATE Accounts
      SET
          Balance+=@moneyAmount
    WHERE Id = @accountId;
    IF(@@ROWCOUNT < 1)
        BEGIN
            ROLLBACK;
            RAISERROR('Account doesn''t exists', 16, 1);
    END;
    COMMIT;
END;
--Problem 4. Withdraw Money
CREATE PROC usp_WithdrawMoney (@AccountId INT, @MoneyAmount MONEY) 
AS
   BEGIN
         IF(@moneyAmount < 0)
             BEGIN
                 RAISERROR('Withdraw deposit negative value', 16, 1);
			 END;
         ELSE
             BEGIN
                 IF(@accountId IS NULL
                    OR @moneyAmount IS NULL)
                 BEGIN
                         RAISERROR('Missing value', 16, 1);
                 END;
			  END;
         BEGIN TRANSACTION;
         UPDATE Accounts
           SET
               Balance-=@moneyAmount
         WHERE Id = @accountId;
         IF(@@ROWCOUNT < 1)
             BEGIN
                 ROLLBACK;
                 RAISERROR('Account doesn''t exists', 16, 1);
         END;
         COMMIT;
     END;
GO

--Problem 5. Money Transfer
CREATE PROC usp_TransferMoney(@SenderId INT, @ReceiverId INT, @Amount MONEY)
AS
BEGIN
	IF @Amount < 0
		BEGIN
			RAISERROR('Cannot transfer negative amount', 16, 1);
		END
    ELSE
		BEGIN
			IF(@senderId IS NULL
				OR @receiverId IS NULL
			    OR @amount IS NULL)
					 BEGIN
						 RAISERROR('Missing value', 16, 1);
					 END;
		END;
    BEGIN TRANSACTION;
    UPDATE Accounts
      SET
          Balance-=@amount
    WHERE Id = @senderId;
    IF(@@ROWCOUNT < 1)
        BEGIN
            ROLLBACK;
            RAISERROR('Sender''s account doesn''t exists', 16, 1);
    END;

    IF(0 >
      (
          SELECT Balance
          FROM Accounts
          WHERE ID = @senderId
      ))
        BEGIN
            ROLLBACK;
            RAISERROR('Not enough funds', 16, 1);
    END;

    UPDATE Accounts
      SET
          Balance+=@amount
    WHERE ID = @receiverId;
    IF(@@ROWCOUNT < 1)
        BEGIN
            ROLLBACK;
            RAISERROR('Receiver''s account doesn''t exists', 16, 1);
    END;
    COMMIT;
END;
GO
USE	Diablo
GO

--Problem 6. Trigger
DECLARE @userId INT  = (SELECT Id FROM Users WHERE Username = 'Stamat')
DECLARE @gameId INT = (SELECT Id FROM Games WHERE Name = 'Safflower')
DECLARE @userGameId INT = (SELECT Id FROM UsersGames WHERE UserId = @userId AND GameId = @gameId)
BEGIN TRY 
BEGIN TRANSACTION 
	UPDATE UsersGames 
	SET Cash -= (SELECT SUM(Price) FROM Items WHERE MinLevel IN (11,12))
	WHERE Id = @userGameId

	DECLARE @userBalance DECIMAL(15,4) = (SELECT Cash FROM UsersGames WHERE Id = @userGameId)
	IF @userBalance <0 
	BEGIN
		ROLLBACK
		RETURN
	END

	INSERT INTO UserGameItems
SELECT Id , @userGameId FROM Items WHERE MinLevel IN (11,12)
COMMIT
END TRY 
BEGIN CATCH
	ROLLBACK
END CATCH
BEGIN TRY 
BEGIN TRANSACTION 
	UPDATE UsersGames 
	SET Cash -= (SELECT SUM(Price) FROM Items WHERE MinLevel BETWEEN 19 AND 21)
	WHERE Id = @userGameId

	SET @userBalance = (SELECT Cash FROM UsersGames WHERE Id = @userGameId)
	IF @userBalance <0 
	BEGIN
		ROLLBACK
		RETURN
	END

	INSERT INTO UserGameItems
	SELECT Id, @userGameId  FROM Items WHERE MinLevel BETWEEN 19 AND 21
COMMIT
END TRY 
BEGIN CATCH
	ROLLBACK
END CATCH

SELECT I.Name FROM Items AS I 
JOIN UserGameItems AS UGI ON I.Id = UGI.ItemId
WHERE UGI.UserGameId = @userGameId
ORDER BY I.Name
GO

USE SoftUni
GO

--Problem 8. Employees with Three Projects
CREATE PROCEDURE usp_AssignProject(@emloyeeId INT, @projectID INT)
AS
BEGIN
BEGIN TRANSACTION
	INSERT INTO EmployeesProjects VALUES
	(
		@emloyeeId , @projectID
	)
	DECLARE @projectCount INT = (SELECT COUNT(*) FROM EmployeesProjects WHERE EmployeeID = @emloyeeId) 
	IF @projectCount > 3
	BEGIN
		ROLLBACK
		RAISERROR ('The employee has too many projects!',16,1) 
		RETURN
	END
	COMMIT
END

--Problem 9. Delete Employees
CREATE TABLE  Deleted_Employees
(
	EmployeeId INT PRIMARY KEY IDENTITY,
	FirstName VARCHAR(50) NOT NULL,
	LastName VARCHAR(50) NOT NULL,
	MiddleName VARCHAR(10), 
	JobTitle VARCHAR(50), 
	DeparmentId INT FOREIGN KEY REFERENCES  Departments(DepartmentID),
	Salary DECIMAL (15,4)
)
GO
CREATE TRIGGER t_FireEmployee ON Employees AFTER DELETE AS
BEGIN 
	INSERT INTO Deleted_Employees
	SELECT D.FirstName,D.LastName,D.MiddleName,D.JobTitle,D.DepartmentID,D.Salary
    FROM deleted AS D
END