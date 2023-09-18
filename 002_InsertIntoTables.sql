DELETE FROM amountconsumed;
DELETE FROM person;
DELETE FROM drink;
DELETE FROM brand;

INSERT INTO brand (name, deleted)
VALUES ('Brand 1', 0), ('Brand 2', 0), ('Brand 3', 1);

INSERT INTO person (name, surname, username)
VALUES ('John', 'Doe', 'johndoe'), ('Jane', 'Smith', 'janesmith'), ('Michael', 'Johnson', 'michaeljohnson');

INSERT INTO drink (name, type, brand, deleted)
VALUES ('Drink 1', 0, (SELECT id FROM Brand WHERE name = 'Brand 1'), 0),
       ('Drink 2', 1, (SELECT id FROM Brand WHERE name = 'Brand 2'), 0),
       ('Drink 3', 0, (SELECT id FROM Brand WHERE name = 'Brand 1'), 1);

INSERT INTO amountconsumed (person, drink, timeofconsumation, amount)
VALUES ((SELECT id FROM Person WHERE username = 'johndoe'),
        (SELECT id FROM Drink WHERE name = 'Drink 1'),
        NOW(),
        2),
       ((SELECT id FROM Person WHERE username = 'janesmith'),
        (SELECT id FROM Drink WHERE name = 'Drink 2'),
        NOW(),
        3),
       ((SELECT id FROM Person WHERE username = 'michaeljohnson'),
        (SELECT id FROM Drink WHERE name = 'Drink 1'),
        NOW(),
        1);
