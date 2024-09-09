CREATE TABLE RoomType (
                          RoomTypeId SERIAL PRIMARY KEY,
                          Name VARCHAR(100) NOT NULL,
                          DefaultRate DECIMAL(10, 2) NOT NULL,
                          Capacity INT NOT NULL,
                          Amenities TEXT,
                          Description TEXT,
                          Image VARCHAR(255),
                          BedSize VARCHAR(50)
);
CREATE TABLE Room (
                      RoomId SERIAL PRIMARY KEY,
                      RoomNumber VARCHAR(50) NOT NULL,
                      RoomTypeId INT NOT NULL,
                      IsAvailable BOOLEAN DEFAULT TRUE,
                      FOREIGN KEY (RoomTypeId) REFERENCES RoomType(RoomTypeId) ON DELETE CASCADE
);
CREATE TABLE Customer (
                          CustomerId SERIAL PRIMARY KEY,
                          FirstName VARCHAR(100) NOT NULL,
                          LastName VARCHAR(100) NOT NULL,
                          Address TEXT,
                          ZipCode VARCHAR(10),
                          Email VARCHAR(100) NOT NULL,
                          PhoneNumber VARCHAR(20)
);
CREATE TYPE BookingStatus AS ENUM ('Pending', 'Confirmed', 'Cancelled', 'Completed');
CREATE TABLE Booking (
                         BookingId SERIAL PRIMARY KEY,
                         CustomerId INT NOT NULL,
                         RoomId INT NOT NULL,
                         CheckInDate TIMESTAMP NOT NULL,
                         CheckOutDate TIMESTAMP NOT NULL,
                         Status BookingStatus NOT NULL DEFAULT 'Pending',
                         CreatedAt TIMESTAMP NOT NULL DEFAULT NOW(),
                         PaymentId INT,
                         FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId) ON DELETE CASCADE,
                         FOREIGN KEY (RoomId) REFERENCES Room(RoomId) ON DELETE CASCADE
);
CREATE TABLE Payment (
                         PaymentId SERIAL PRIMARY KEY,
                         BookingId INT NOT NULL,
                         Amount DECIMAL(10, 2) NOT NULL,
                         TotalPrice DECIMAL(10, 2) NOT NULL,
                         SalesTax DECIMAL(10, 2),
                         Fees DECIMAL(10, 2),
                         PaymentDate TIMESTAMP NOT NULL,
                         TransactionLink VARCHAR(255),
                         FOREIGN KEY (BookingId) REFERENCES Booking(BookingId) ON DELETE CASCADE
);
CREATE TABLE RoomRate (
                          RoomRateId SERIAL PRIMARY KEY,
                          RoomId INT NOT NULL,
                          Rate DECIMAL(10, 2) NOT NULL,
                          StartDateForRate TIMESTAMP NOT NULL,
                          EndDateForRate TIMESTAMP NOT NULL,
                          FOREIGN KEY (RoomId) REFERENCES Room(RoomId) ON DELETE CASCADE
);