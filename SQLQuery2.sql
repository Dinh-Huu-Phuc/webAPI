use BookAPIStoreDb

INSERT INTO Publishers (Name)
VALUES
('Penguin Random House'),
('HarperCollins'),
('Simon & Schuster'),
('Macmillan Publishers'),
('Hachette Livre');

INSERT INTO Authors (FullName)
VALUES
('J.K. Rowling'),
('Stephen King'),
('Agatha Christie'),
('George R.R. Martin'),
('Haruki Murakami');

INSERT INTO Books (Title, Description, isRead, DateRead, Rate, Genre, CoverUrl, DateAdded, PublisherID)
VALUES
('Harry Potter and the Sorcerer''s Stone', 'The first book in the Harry Potter series.', 1, '2023-01-15', 5, 'Fantasy', 'http://example.com/cover1.jpg', '2023-01-01', 1),
('The Shining', 'A psychological horror novel.', 1, '2023-02-20', 4, 'Horror', 'http://example.com/cover2.jpg', '2023-02-10', 2),
('And Then There Were None', 'A classic mystery novel.', 1, '2023-03-25', 5, 'Mystery', 'http://example.com/cover3.jpg', '2023-03-15', 3),
('A Game of Thrones', 'The first novel in A Song of Ice and Fire series.', 0, NULL, NULL, 'Fantasy', 'http://example.com/cover4.jpg', '2023-04-05', 4),
('Norwegian Wood', 'A coming-of-age novel.', 1, '2023-05-30', 4, 'Fiction', 'http://example.com/cover5.jpg', '2023-05-20', 5);

INSERT INTO Book_Authors (BookId, AuthorId)
VALUES
(1, 1),  
(2, 2),  
(3, 3),  
(4, 4),  
(5, 5);

--ALTER TABLE Books ALTER COLUMN Price DECIMAL(10,2) NULL;

--SELECT COLUMN_NAME FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = 'Books';


--SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = 'Books_Authors';

--SELECT * FROM dbo.Book_Authors;

EXEC sp_rename 'dbo.Books_Authors', 'Book_Authors';

