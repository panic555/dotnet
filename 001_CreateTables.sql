DROP TABLE IF EXISTS amountconsumed;
DROP TABLE IF EXISTS drink;
DROP TABLE IF EXISTS brand;
DROP TABLE IF EXISTS person;

CREATE TABLE person (
                       id INT PRIMARY KEY AUTO_INCREMENT,
                       name VARCHAR(255),
                       surname VARCHAR(255),
                       username VARCHAR(255)
);

CREATE TABLE brand (
                       id INT PRIMARY KEY AUTO_INCREMENT,
                       name VARCHAR(255),
                       deleted BIT
);

CREATE TABLE drink (
                      id INT PRIMARY KEY AUTO_INCREMENT,
                      name VARCHAR(255),
                      type VARCHAR(255),
                      brand INT,
                      deleted BIT,
                      FOREIGN KEY (brand) REFERENCES Brand(id)
);

CREATE TABLE amountconsumed (
                       id INT PRIMARY KEY AUTO_INCREMENT,
                       person INT,
                       drink INT,
                       timeofconsumation DATETIME,
                       amount DECIMAL(10, 2),
                       FOREIGN KEY (person) REFERENCES Person(id),
                       FOREIGN KEY (drink) REFERENCES Drink(id)
);
