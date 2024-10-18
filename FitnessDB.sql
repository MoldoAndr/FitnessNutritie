IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'FitnessNutritieDB')
BEGIN
    CREATE DATABASE FitnessNutritieDB;
END
GO

USE FitnessNutritieDB;
GO

DROP TABLE IF EXISTS Exercitii;
DROP TABLE IF EXISTS Retete;
DROP TABLE IF EXISTS Utilizatori;

CREATE TABLE Exercitii (
    ID INT PRIMARY KEY IDENTITY(1,1),
    DenumireExercitiu NVARCHAR(100),
    Repetari INT,
    GrupaMusculara NVARCHAR(100),
    Seturi INT,
    Descriere NVARCHAR(100),
    TimpEstimareExecutie INT
);
GO

CREATE TABLE Retete (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Calorii INT NOT NULL,
    Carbohidrati DECIMAL(5,2) NOT NULL,
    Proteine DECIMAL(5,2) NOT NULL,
    Grasimi DECIMAL(5,2) NOT NULL,
    Ingrediente NVARCHAR(MAX) NOT NULL,
	TipMasa NVARCHAR(20) NOT NULL CHECK (TipMasa IN ('Mic Dejun', 'Pranz', 'Cina', 'Gustare'))
);
GO

CREATE TABLE Utilizatori (
    ID INT PRIMARY KEY IDENTITY(1,1),
    Name NVARCHAR(100) NOT NULL,
    HashedPassword NVARCHAR(255) NOT NULL,
    Sex NVARCHAR(10) CHECK (Sex IN ('Masculin', 'Feminin')),
    Height DECIMAL(5,2),
    Weight DECIMAL(5,2),
    PhysicalCondition NVARCHAR(50),
    UserType NVARCHAR(20) CHECK (UserType IN ('Utilizator', 'Nutritionist', 'Administrator')) NOT NULL
);

INSERT INTO Exercitii (DenumireExercitiu, Repetari, GrupaMusculara, Seturi, Descriere, TimpEstimareExecutie)
VALUES
('Flotari', 12, 'push', 4, 'Exercitiu pentru piept si triceps', 8),
('Impins la piept cu bara', 8, 'push', 4, 'Exercitiu clasic pentru dezvoltarea pieptului', 10),
('Impins deasupra capului cu gantere', 10, 'push', 3, 'Exercitiu pentru umeri si triceps', 9),
('Flotari la paralele', 12, 'push', 3, 'Exercitiu pentru triceps si piept', 7),
('Tractiuni la bara', 10, 'pull', 3, 'Exercitiu principal pentru spate si biceps', 10),
('Ramat cu gantere', 12, 'pull', 3, 'Exercitiu pentru dezvoltarea spatelui', 9),
('Indreptari cu bara', 8, 'pull', 4, 'Exercitiu pentru spate si hamstrings', 12),
('Flexii pentru biceps cu gantere', 12, 'pull', 3, 'Exercitiu pentru biceps si antebrat', 6),
('Genuflexiuni cu greutati', 15, 'legs', 4, 'Exercitiu complet pentru coapse si fesieri', 10),
('Fandari cu gantere', 12, 'legs', 4, 'Exercitiu pentru picioare si fesieri', 8),
('Presa picioare', 15, 'legs', 4, 'Exercitiu pentru dezvoltarea muschilor picioarelor', 10),
('Indreptari romanesti', 10, 'legs', 3, 'Exercitiu pentru hamstrings si fesieri', 9),
('Curl cu bara', 10, 'biceps', 4, 'Exercitiu pentru dezvoltarea bicepsului brahial', 8),
('Curl cu gantere pe banca inclinate', 12, 'biceps', 3, 'Exercitiu pentru bicepsul brahial', 7),
('Hammer curl', 12, 'biceps', 3, 'Exercitiu pentru biceps si antebrat', 6),
('Concentration curl', 10, 'biceps', 3, 'Exercitiu izolat pentru biceps', 5),
('Extensii pentru triceps cu gantera', 12, 'triceps', 4, 'Exercitiu izolat pentru triceps', 8),
('Flotari la paralele', 10, 'triceps', 4, 'Exercitiu pentru triceps si piept', 7),
('Pushdown la cablu pentru triceps', 12, 'triceps', 3, 'Exercitiu pentru triceps la cablu', 6),
('Extensii cu bara pentru triceps', 8, 'triceps', 4, 'Exercitiu pentru triceps la orizontal', 9),
('Ridicari laterale cu gantere', 12, 'umeri', 4, 'Exercitiu pentru deltoidul lateral', 7),
('Ridicari frontale cu gantere', 10, 'umeri', 3, 'Exercitiu pentru deltoidul anterior', 6),
('Presă militara cu gantere', 8, 'umeri', 4, 'Exercitiu pentru dezvoltarea umerilor', 9),
('Aripioare cu gantere', 12, 'umeri', 3, 'Exercitiu pentru deltoidul posterior', 7),
('Flotari pe o mana', 8, 'piept', 3, 'Exercitiu pentru piept, triceps si stabilitate', 7),
('Impingere cu gantere pe banca inclinata', 10, 'piept', 4, 'Exercitiu pentru piept superior', 8),
('Flotari la aparat', 12, 'piept', 3, 'Exercitiu pentru piept cu suport', 6),
('Dips pentru piept', 10, 'piept', 3, 'Exercitiu pentru piept si triceps', 9),
('Ramat cu bara', 10, 'spate', 4, 'Exercitiu pentru dezvoltarea spatelui', 9),
('Deadlift', 8, 'spate', 4, 'Exercitiu compus pentru intregul spate', 12),
('Pull-up cu priza larga', 8, 'spate', 3, 'Exercitiu pentru spate si biceps', 10),
('Ramat la aparat', 12, 'spate', 3, 'Exercitiu pentru spate cu suport', 8),
('Abdomene', 15, 'abdomen', 4, 'Exercitiu clasic pentru abdomen', 6),
('Plank', 30, 'abdomen', 4, 'Exercitiu pentru stabilitatea abdomenului', 10),
('Crunch-uri inversate', 15, 'abdomen', 3, 'Exercitiu pentru partea inferioara a abdomenului', 5),
('Rotatii rusești', 20, 'abdomen', 4, 'Exercitiu pentru oblici', 8),
('Genuflexiuni cu bara', 12, 'cvadriceps', 4, 'Exercitiu principal pentru cvadriceps', 10),
('Presa picioare', 15, 'cvadriceps', 4, 'Exercitiu pentru dezvoltarea cvadricepsului', 9),
('Fandari inainte', 12, 'cvadriceps', 3, 'Exercitiu pentru picioare si fesieri', 8),
('Genuflexiuni bulgare', 10, 'cvadriceps', 3, 'Exercitiu pentru un picior pentru cvadriceps', 9),
('Indreptari romane', 10, 'biceps femural', 4, 'Exercitiu pentru biceps femural si fesieri', 12),
('Flexii la aparat', 12, 'biceps femural', 3, 'Exercitiu izolat pentru biceps femural', 9),
('Deadlift cu picioarele drepte', 8, 'biceps femural', 4, 'Exercitiu pentru intregul posterior al piciorului', 11),
('Fandari inversate', 10, 'biceps femural', 3, 'Exercitiu pentru biceps femural si fesieri', 8),
('Extensii pentru picioare', 12, 'coapse', 4, 'Exercitiu izolat pentru cvadriceps', 8),
('Fandari laterale', 10, 'coapse', 4, 'Exercitiu pentru coapse si fesieri', 9),
('Abduse cu greutate', 12, 'coapse', 3, 'Exercitiu pentru muschii laterali ai coapselor', 7),
('Presa picioare cu un picior', 10, 'coapse', 3, 'Exercitiu pentru dezvoltarea fortei pe un picior', 10),
('Ridicari de umeri cu gantere', 12, 'trapez', 4, 'Exercitiu pentru dezvoltarea trapezului', 8),
('Shrugs cu bara', 10, 'trapez', 4, 'Exercitiu pentru trapez', 9),
('Ramat cu priza ingusta', 10, 'trapez', 3, 'Exercitiu pentru trapez si spate', 11),
('Extensii de spate', 12, 'trapez', 3, 'Exercitiu pentru intarirea spatelui superior', 10),
('Ridicari pe varfuri cu greutate', 15, 'gambe', 4, 'Exercitiu pentru dezvoltarea gambelor', 8),
('Ridicari pe varfuri la aparat', 12, 'gambe', 3, 'Exercitiu pentru gambe efectuat la aparat', 7),
('Ridicari pe varfuri pe un picior', 10, 'gambe', 3, 'Exercitiu izolat pentru gambe pe un picior', 9),
('Fandari cu ridicari pe varfuri', 12, 'gambe', 4, 'Exercitiu pentru gambe si coapse', 10);

GO


INSERT INTO Retete (Calorii, Carbohidrati, Proteine, Grasimi, Ingrediente, TipMasa)
VALUES
    (350, 50.00, 20.00, 5.00, 'Fulgi de ovaz (50g), Lapte (200ml), Fructe de padure (100g)', 'Mic Dejun'),
    (400, 60.00, 15.00, 8.00, 'Omleta (3 oua), Spanac (50g), Rosii (100g)', 'Mic Dejun'),
    (300, 40.00, 25.00, 7.00, 'Iaurt grecesc (200g), Miere (10g), Nuci (30g)', 'Mic Dejun'),
    (450, 55.00, 30.00, 10.00, 'Smoothie (Banana, 200ml lapte, 20g proteine)', 'Mic Dejun'),
    (500, 70.00, 20.00, 12.00, 'Paine integrala (2 felii), Avocado (100g), Oua (2 buc)', 'Mic Dejun'),
    (400, 50.00, 25.00, 9.00, 'Briose de ovaz (2 buc), Fructe (100g)', 'Mic Dejun'),
    (450, 65.00, 10.00, 7.00, 'Cereale integrale (50g), Lapte (200ml)', 'Mic Dejun'),
    (350, 45.00, 20.00, 8.00, 'Toast cu unt de arahide (2 felii), Banane (1)', 'Mic Dejun'),
    (400, 50.00, 25.00, 9.00, 'Clatite din faina integrala (2 buc), Sirop de artar', 'Mic Dejun'),
    (300, 40.00, 15.00, 5.00, 'Batoane de granola (2 buc)', 'Mic Dejun'),

    (500, 60.00, 35.00, 15.00, 'Piept de pui (150g), Orez (150g), Broccoli (100g)', 'Pranz'),
    (600, 80.00, 40.00, 20.00, 'Salata cu ton (100g), Quinoa (100g), Legume', 'Pranz'),
    (700, 90.00, 50.00, 25.00, 'Paste integrale (100g), Sos de rosii (100g), Carne macinata (100g)', 'Pranz'),
    (550, 75.00, 30.00, 12.00, 'Burgers din curcan (150g), Chifle integrale (1)', 'Pranz'),
    (650, 85.00, 40.00, 18.00, 'Supa de legume (300ml), Paine integrala (50g)', 'Pranz'),
    (500, 70.00, 30.00, 15.00, 'Taco cu carne (150g), Salata (50g)', 'Pranz'),
    (600, 75.00, 35.00, 25.00, 'Pizza cu blat integral (1 felie), Salata', 'Pranz'),
    (700, 90.00, 45.00, 20.00, 'Friptura de vita (150g), Piure de cartofi (150g)', 'Pranz'),
    (600, 70.00, 50.00, 15.00, 'Wrap cu pui (150g), Salata verde (100g)', 'Pranz'),
    (800, 100.00, 55.00, 25.00, 'Bol de orez cu legume și tofu (300g)', 'Pranz'),

    (400, 45.00, 25.00, 15.00, 'Somon la cuptor (100g), Legume la abur (150g)', 'Cina'),
    (600, 80.00, 40.00, 25.00, 'Tocana de legume (200g), Paine integrala (50g)', 'Cina'),
    (700, 90.00, 50.00, 20.00, 'Pui cu curry (150g), Orez (150g)', 'Cina'),
    (500, 60.00, 35.00, 15.00, 'Chili con carne (200g)', 'Cina'),
    (550, 75.00, 30.00, 20.00, 'Peste (150g), Quinoa (100g)', 'Cina'),
    (650, 80.00, 40.00, 25.00, 'Pasta cu creveti (200g)', 'Cina'),
    (600, 70.00, 50.00, 18.00, 'Salata calda cu naut (200g)', 'Cina'),
    (800, 100.00, 60.00, 30.00, 'Friptura de porc (150g), Cartofi la cuptor (150g)', 'Cina'),
    (700, 85.00, 50.00, 20.00, 'Cuscus cu legume (200g)', 'Cina'),
    (900, 110.00, 65.00, 25.00, 'Pui pe gratar (200g), Salata verde (100g)', 'Cina'),

    (200, 30.00, 10.00, 5.00, 'Mar (1), Iaurt (100g)', 'Gustare'),
    (250, 20.00, 15.00, 6.00, 'Batoane de cereale (2 buc)', 'Gustare'),
    (150, 20.00, 10.00, 3.00, 'Fructe de padure (150g)', 'Gustare'),
    (300, 40.00, 15.00, 10.00, 'Migdale (30g)', 'Gustare'),
    (200, 25.00, 10.00, 8.00, 'Popcorn (50g)', 'Gustare'),
    (250, 30.00, 10.00, 5.00, 'Iaurt grecesc (100g), Miere (10g)', 'Gustare'),
    (180, 20.00, 8.00, 6.00, 'Branza cottage (100g)', 'Gustare'),
    (220, 25.00, 12.00, 7.00, 'Banana (1)', 'Gustare'),
    (280, 30.00, 10.00, 5.00, 'Nuci (30g)', 'Gustare'),
    (150, 15.00, 5.00, 2.00, 'Biscuiti integrali (2 buc)', 'Gustare'),

    (420, 55.00, 25.00, 10.00, 'Briose cu banane (2 buc), Iaurt (100g)', 'Mic Dejun'),
    (390, 45.00, 20.00, 12.00, 'Toast cu avocado (1 felie), Iaurt (100g)', 'Mic Dejun'),
    (300, 35.00, 18.00, 9.00, 'Cereale integrale (50g), Lapte (200ml)', 'Mic Dejun'),
    (480, 65.00, 30.00, 10.00, 'Smoothie bol (Banana, fructe, granola)', 'Mic Dejun'),
    (520, 70.00, 22.00, 15.00, 'Batoane de ovaz (2 buc), Migdale (20g)', 'Mic Dejun'),
    (410, 58.00, 26.00, 9.00, 'Clatite de ovaz (3 buc), Sirop de arțar', 'Mic Dejun'),
    (450, 55.00, 20.00, 10.00, 'Paine-prajita cu unt de arahide (2 felii)', 'Mic Dejun'),
    (440, 60.00, 25.00, 8.00, 'Fructe proaspete (200g), Iaurt (150g)', 'Mic Dejun'),
    (500, 65.00, 28.00, 12.00, 'Sandwich cu ou fiert (2 ouă), Salata verde', 'Mic Dejun'),
    (390, 40.00, 20.00, 10.00, 'Granola (50g), Lapte (200ml)', 'Mic Dejun'),

    (750, 100.00, 45.00, 20.00, 'Bol de quinoa cu legume (300g)', 'Pranz'),
    (650, 90.00, 50.00, 15.00, 'Friptura de pui (200g), Legume la gratar (200g)', 'Pranz'),
    (800, 110.00, 60.00, 25.00, 'Taco cu carne de vita (3 buc), Salata (100g)', 'Pranz'),
    (700, 80.00, 45.00, 22.00, 'Lasagna vegetariană (200g)', 'Pranz'),
    (600, 70.00, 40.00, 20.00, 'Risotto cu ciuperci (250g)', 'Pranz'),
    (550, 60.00, 30.00, 15.00, 'Bowl de orez cu tofu (200g)', 'Pranz'),
    (500, 65.00, 25.00, 10.00, 'Pasta cu pesto (200g)', 'Pranz'),
    (700, 100.00, 55.00, 20.00, 'Bol de paste cu legume si branza (250g)', 'Pranz'),
    (800, 120.00, 65.00, 25.00, 'Friptura de porc (200g), Cartofi prajiti (150g)', 'Pranz'),
    (750, 90.00, 40.00, 22.00, 'Pasta cu ton (200g)', 'Pranz'),

    (450, 50.00, 30.00, 20.00, 'Peste la cuptor (150g), Sparanghel (100g)', 'Cina'),
    (600, 75.00, 40.00, 25.00, 'Friptura de curcan (200g), Legume la abur (150g)', 'Cina'),
    (700, 90.00, 45.00, 30.00, 'Pizza vegetariana (1 felie), Salata (100g)', 'Cina'),
    (650, 80.00, 35.00, 20.00, 'Curry de naut (200g), Orez (150g)', 'Cina'),
    (500, 60.00, 30.00, 15.00, 'Tocana de vita (200g), Legume (150g)', 'Cina'),
    (550, 70.00, 40.00, 20.00, 'Frittata cu legume (2 ouă)', 'Cina'),
    (700, 85.00, 50.00, 25.00, 'Fasole cu carnati (200g)', 'Cina'),
    (600, 75.00, 40.00, 20.00, 'Pasta cu carne de vita (200g)', 'Cina'),
    (800, 110.00, 60.00, 30.00, 'Ciorba de burta (300ml)', 'Cina'),
    (750, 90.00, 45.00, 25.00, 'Salata de ton (200g)', 'Cina'),

    (150, 20.00, 8.00, 5.00, 'Baton proteic (1 buc)', 'Gustare'),
    (180, 25.00, 10.00, 4.00, 'Iaurt (100g), Fructe (100g)', 'Gustare'),
    (220, 30.00, 12.00, 5.00, 'Castravete (100g), Hummus (50g)', 'Gustare'),
    (170, 25.00, 8.00, 4.00, 'Pere (1)', 'Gustare'),
    (200, 30.00, 10.00, 6.00, 'Smoothie de fructe (200ml)', 'Gustare'),
    (250, 35.00, 10.00, 7.00, 'Migdale (30g), Fructe uscate (20g)', 'Gustare'),
    (180, 20.00, 8.00, 3.00, 'Biscuiti de ovaz (3 buc)', 'Gustare'),
    (160, 15.00, 5.00, 4.00, 'Iaurt cu cereale (150g)', 'Gustare'),
    (230, 25.00, 10.00, 8.00, 'Batoane energizante (2 buc)', 'Gustare'),
    (190, 20.00, 9.00, 5.00, 'Cheesecake (1 felie)', 'Gustare');
GO
