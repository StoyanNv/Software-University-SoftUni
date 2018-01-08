USE DIABLO
--Problem 1. Number of Users for Email Provider
  SELECT SUBSTRING(Email, CHARINDEX('@', EMAIL) + 1, LEN(Email) - CHARINDEX('@', EMAIL))
      AS [Email Provider],
  	     COUNT(SUBSTRING(Email, CHARINDEX('@', EMAIL) + 1,LEN(Email) - CHARINDEX('@', EMAIL))) 
      AS [Number Of Users] 
    FROM Users
GROUP BY SUBSTRING(Email, CHARINDEX('@', EMAIL) + 1, LEN(Email) - CHARINDEX('@', EMAIL))
ORDER BY COUNT(SUBSTRING(Email, CHARINDEX('@', EMAIL) + 1,LEN(Email)- CHARINDEX('@', EMAIL))) DESC, [Email Provider]
--Problem 2. All User in Games
  SELECT G.Name,
         GT.Name,
         U.Username,
         UG.Level,
         UG.Cash,
         C.Name
    FROM Games AS G
    JOIN GameTypes AS GT ON GT.Id = G.GameTypeId
    JOIN UsersGames AS UG ON UG.GameId = G.Id
    JOIN Users AS U ON U.Id = UG.UserId
    JOIN Characters AS C ON C.Id = UG.CharacterId
ORDER BY UG.Level DESC,
         U.Username,
         G.Name

--Problem 3. Users in Games with Their Items
  SELECT U.Username,
         G.Name,
         COUNT(I.ItemTypeId) AS [Items Count],
         SUM(I.Price) AS [Items Price]
    FROM Games AS G
    JOIN UsersGames AS UG ON UG.GameId = G.Id
    JOIN UserGameItems AS UGI ON UGI.UserGameId = UG.GameId
    JOIN Users AS U ON U.Id = UG.UserId
    JOIN Items AS I ON I.Id =UGI.ItemId
GROUP BY U.Username,
         G.Name
  HAVING COUNT(I.Id) >= 10
ORDER BY [Items Count] DESC

--Problem 3. Users in Games with Their Items
  SELECT U.Username,
         G.Name,
         COUNT(UGI.ItemId) AS [Items Count],
         SUM(I.Price) AS [Items Price]
    FROM Users AS U
    JOIN UsersGames AS UG ON UG.UserId = U.Id
    JOIN Games AS G ON G.Id = UG.GameId
    JOIN UserGameItems AS UGI ON UGI.UserGameId = UG.Id
    JOIN Items AS I ON I.Id =UGI.ItemId
GROUP BY U.Username,
         G.Name
  HAVING COUNT(UGI.ItemId) >= 10
ORDER BY COUNT(UGI.ItemId) DESC , SUM(I.Price) DESC

--Problem 5. All Items with Greater than Average Statistics
SELECT Name,
       Price,
       MinLevel,
       Strength,
       S.Defence,
       S.Speed,
       S.Luck,
       S.Mind
  FROM Items AS I
  JOIN [Statistics] AS S ON S.Id = I.StatisticId
 WHERE S.Mind > (SELECT AVG(S.Mind)
				   FROM [Statistics] AS S)
   AND S.Luck > (SELECT AVG(S.Luck)
 	 	           FROM [Statistics] AS S)
   AND S.Speed > (SELECT AVG(S.Speed)
                    FROM [Statistics] AS S)
 ORDER BY Name

--Problem 6. Display All Items with Information about Forbidden Game Type
   SELECT I.Name,
          I.Price,
          I.MinLevel,
          GT.Name AS [Forbidden Game Type]
     FROM Items AS I
LEFT JOIN GameTypeForbiddenItems AS IT ON IT.ItemId = I.Id
LEFT JOIN GameTypes AS GT ON GT.Id = IT.GameTypeId
 ORDER BY GT.Name DESC,
          I.Name

--Problem 7. Buy Items for User in Game
 BEGIN TRANSACTION
UPDATE UsersGames
   SET Cash = Cash - (SELECT Price FROM Items WHERE Name = 'Blackguard')
 WHERE UserId = (SELECT Id FROM Users WHERE Username = 'Alex') AND GameId = (SELECT Id FROM Games WHERE Name = 'Edinburgh')
INSERT INTO UserGameItems VALUES
(
	(SELECT Id FROM Items WHERE Name = 'Blackguard'),
	(SELECT Id FROM UsersGames WHERE  GameId = (SELECT Id FROM Games WHERE Name = 'Edinburgh') 
	AND UserId = (SELECT Id FROM Users WHERE Username = 'Alex') )
)
UPDATE UsersGames
   SET Cash = Cash - (SELECT Price FROM Items WHERE Name = 'Bottomless Potion of Amplification')
 WHERE UserId = (SELECT Id FROM Users WHERE Username = 'Alex')
INSERT INTO UserGameItems VALUES
(
	(SELECT Id FROM Items WHERE Name = 'Bottomless Potion of Amplification'),
	(SELECT Id FROM UsersGames WHERE  GameId = (SELECT Id FROM Games WHERE Name = 'Edinburgh')
    AND UserId = (SELECT Id FROM Users WHERE Username = 'Alex') )
)
UPDATE UsersGames
   SET Cash = Cash - (SELECT Price FROM Items WHERE Name = 'Eye of Etlich (Diablo III)')
 WHERE UserId = (SELECT Id FROM Users WHERE Username = 'Alex')
INSERT INTO UserGameItems VALUES
(
	(SELECT Id FROM Items WHERE Name = 'Eye of Etlich (Diablo III)'),
	(SELECT Id FROM UsersGames WHERE  GameId = (SELECT Id FROM Games WHERE Name = 'Edinburgh')
     AND UserId = (SELECT Id FROM Users WHERE Username = 'Alex') )
)
UPDATE UsersGames
   SET Cash = Cash - (SELECT Price FROM Items WHERE Name = 'Gem of Efficacious Toxin')
 WHERE UserId = (SELECT Id FROM Users WHERE Username = 'Alex')
INSERT INTO UserGameItems VALUES
(
	(SELECT Id FROM Items WHERE Name = 'Gem of Efficacious Toxin'),
	(SELECT Id FROM UsersGames WHERE  GameId = (SELECT Id FROM Games WHERE Name = 'Edinburgh') 
	AND UserId = (SELECT Id FROM Users WHERE Username = 'Alex') )
)
UPDATE UsersGames
   SET Cash = Cash - (SELECT Price FROM Items WHERE Name = 'Golden Gorget of Leoric')
 WHERE UserId = (SELECT Id FROM Users WHERE Username = 'Alex')
INSERT INTO UserGameItems VALUES
(
	(SELECT Id FROM Items WHERE Name = 'Golden Gorget of Leoric'),
	(SELECT Id FROM UsersGames WHERE  GameId = (SELECT Id FROM Games WHERE Name = 'Edinburgh') 
	AND UserId = (SELECT Id FROM Users WHERE Username = 'Alex') )
)
UPDATE UsersGames
   SET Cash = Cash - (SELECT Price FROM Items WHERE Name = 'Hellfire Amulet')
 WHERE UserId = (SELECT Id FROM Users WHERE Username = 'Alex')
INSERT INTO UserGameItems VALUES
(
	(SELECT Id FROM Items WHERE Name = 'Hellfire Amulet'),
	(SELECT Id FROM UsersGames WHERE  GameId = (SELECT Id FROM Games WHERE Name = 'Edinburgh')
     AND UserId = (SELECT Id FROM Users WHERE Username = 'Alex') )
)

COMMIT

SELECT U.Username,
       G.Name,
       UG.Cash,
       I.Name
  FROM Users AS U
  JOIN UsersGames AS UG ON UG.UserId = U.Id
  JOIN Games AS G ON G.Id = UG.GameId
  JOIN UserGameItems AS UGI ON UGI.UserGameId = UG.Id
  JOIN Items AS I ON I.Id = UGI.ItemId
 WHERE G.Name = 'Edinburgh'
 ORDER BY I.Name


USE Geography
--Problem 8. Peaks and Mountains
  SELECT P.PeakName, 
         M.MountainRange,
         P.Elevation 
    FROM Peaks AS P 
    JOIN Mountains AS M ON M.Id = P.MountainId
ORDER BY P.Elevation DESC,P.PeakName 

--Problem 9. Peaks with Their Mountain, Country and Continent
  SELECT P.PeakName,
	     M.MountainRange,
	     C.CountryName,
	     CONT.ContinentName 
    FROM Peaks AS P 
    JOIN Mountains AS M ON M.Id = P.MountainId
    JOIN MountainsCountries AS MC ON MC.MountainId = M.Id
    JOIN Countries AS C ON C.CountryCode = MC.CountryCode
    JOIN Continents AS CONT ON CONT.ContinentCode = C.ContinentCode
ORDER BY P.PeakName , C.CountryName

--Problem 10. Rivers by Country
   SELECT C.CountryName,
    	  CONT.ContinentName,
    	  COUNT(R.Id),
    	  ISNULL(SUM(R.Length),0)
     FROM Countries AS C
LEFT JOIN Continents AS CONT ON CONT.ContinentCode = C.ContinentCode
LEFT JOIN CountriesRivers AS CR ON CR.CountryCode = C.CountryCode
LEFT JOIN Rivers AS R ON R.Id = CR.RiverId
 GROUP BY CONT.ContinentName ,C.CountryName
 ORDER BY COUNT(R.Id) DESC,ISNULL(SUM(R.Length),0) DESC,C.CountryName

--Problem 11. Count of Countries by Currency
  SELECT C.CurrencyCode,
         C.Description AS Currency,
         COUNT(C.Description)  AS NumberOfCountries
    FROM Currencies AS C
    JOIN Countries AS CO ON CO.CurrencyCode = C.CurrencyCode
GROUP BY C.CurrencyCode,C.Description
ORDER BY COUNT(CO.CountryCode) DESC,C.Description,C.CurrencyCode

--Problem 12. Population and Area by Continent
   SELECT CO.ContinentName,
          SUM(C.AreaInSqKm) AS CountriesArea,
          SUM(CAST(C.Population AS BIGINT)) AS CountriesPopulation
     FROM Countries AS C
LEFT JOIN Continents AS CO ON CO.ContinentCode= C.ContinentCode
 GROUP BY C.ContinentCode,CO.ContinentName
 ORDER BY SUM(CAST(C.Population AS BIGINT)) DESC

--Problem 13. Monasteries by Country
CREATE TABLE Monasteries
(
	Id INT PRIMARY KEY IDENTITY,
	Name VARCHAR(max),
	CountryCode CHAR(2),
	FOREIGN KEY (CountryCode) REFERENCES Countries(CountryCode)
) 
INSERT INTO Monasteries(Name, CountryCode) VALUES
('Rila Monastery “St. Ivan of Rila”', 'BG'), 
('Bachkovo Monastery “Virgin Mary”', 'BG'),
('Troyan Monastery “Holy Mother''s Assumption”', 'BG'),
('Kopan Monastery', 'NP'),
('Thrangu Tashi Yangtse Monastery', 'NP'),
('Shechen Tennyi Dargyeling Monastery', 'NP'),
('Benchen Monastery', 'NP'),
('Southern Shaolin Monastery', 'CN'),
('Dabei Monastery', 'CN'),
('Wa Sau Toi', 'CN'),
('Lhunshigyia Monastery', 'CN'),
('Rakya Monastery', 'CN'),
('Monasteries of Meteora', 'GR'),
('The Holy Monastery of Stavronikita', 'GR'),
('Taung Kalat Monastery', 'MM'),
('Pa-Auk Forest Monastery', 'MM'),
('Taktsang Palphug Monastery', 'BT'),
('Sümela Monastery', 'TR')

ALTER TABLE Countries 
        ADD IsDeleted BIT
ALTER TABLE Countries 
DROP COLUMN IsDeleted;

UPDATE Countries 
   SET IsDeleted = 1
 WHERE CountryCode IN (SELECT CountryCode
                         FROM CountriesRivers 
				     GROUP BY CountryCode 
					   HAVING COUNT(RiverId) > 3)

  SELECT Name,
         C.CountryName 
    FROM Monasteries AS M
    JOIN Countries AS C ON C.CountryCode = M.CountryCode
   WHERE NOT(C.IsDeleted = 1)
ORDER BY Name