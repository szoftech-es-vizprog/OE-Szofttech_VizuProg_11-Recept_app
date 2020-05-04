SELECT * FROM [Receptek];

INSERT INTO [Receptek]
([Nev], [ElkeszitesIdo], [Elkeszites])
VALUES('Delux saláta', 15.0, '...');

DELETE [Receptek]
WHERE [Id] = 5;

UPDATE [Receptek]
SET [ElkeszitesIdo] = 20
WHERE [Id] = 4 ;

SELECT [Hozzavalok].Nev, 
[ReceptekHozzavalok].Mertekegyseg,
[ReceptekHozzavalok].Mennyiseg 
FROM [Hozzavalok]
INNER JOIN [ReceptekHozzavalok] ON [Hozzavalok].Id = [ReceptekHozzavalok].HozzavaloId WHERE [ReceptekHozzavalok].ReceptId = 1 ;