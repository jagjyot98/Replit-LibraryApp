using System;
using System.Data;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

    class Book
    {
        public string title;
        public string author;
        public int BookID;

        private static Random random = new Random();

        // By creating a single static instance of Random in the Book class, you ensure that the random BookID generation is more reliable.

        public void addNewBoook()
        {
            BookID = new Random().Next(100, 500);
            Console.WriteLine("Enter the title of the book");
            title = Console.ReadLine();
            Console.WriteLine("Enter the author of the book");
            author = Console.ReadLine();
        }

        public void displayBook()
        {
            Console.WriteLine("Book Id: {0}", BookID);
            Console.WriteLine("Title: " + title);
            Console.WriteLine("Author: " + author);
        }
    }

    class Library
    {
        List<Book> booksList = new List<Book>();    //System data collection

        string connectionString = "Server=127.0.0.1;Database=LibrarySys;Uid=root;Pwd=;";

        public int booksCount()
        {
            return booksList.Count;
        }

        public void updateBooks()
        {
            booksList.Clear();      //clearing previous data collected before updation

            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM books";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.title = reader.GetString(1);
                        book.author = reader.GetString(2);
                        book.BookID = reader.GetInt32(0);
                        booksList.Add(book);
                        //Console.WriteLine(reader["BookID"] + " " + reader["Title"]+" " + reader["Author"]); // Replace with your column names

                    }
                }
                connection.Close();
            }
        }

        public void addBook(Book Book)      //Adding new data 
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "INSERT INTO books (BookID, Title, Author) VALUES (@BookID, @Title, @Author)";
            
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@BookID", Book.BookID);
                command.Parameters.AddWithValue("@Title", Book.title);
                command.Parameters.AddWithValue("@Author", Book.author);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Data Inserted successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to insert data.");
                    Console.ResetColor();
                }
                connection.Close();
            }

        updateBooks();      //updating system collection with updated data

        /*booksList.Add(Book);

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book Added Successfully !");
            Console.ResetColor();*/
        }

        public void deleteBook(String title)    //Deleting data 
        {
            Boolean found = false;
            foreach (Book book in booksList)    //checking for data in system collection
            {
                if (book.title == title)//Console.ReadLine())
                {
                    found = true;
                    using (MySqlConnection connection = new MySqlConnection(connectionString))
                    {
                        connection.Open();
                        string query = "DELETE FROM books WHERE Title = @Title ";

                        MySqlCommand command = new MySqlCommand(query, connection);
                        command.Parameters.AddWithValue("@Title", title);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("Data Deleted successfully.");
                            Console.ResetColor();
                        }
                        else
                        {
                            Console.WriteLine();
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Failed to delete data.");
                            Console.ResetColor();
                        }
                        connection.Close();
                    }
                    updateBooks();      //updating system collection with updated data
                    break;
                }
            }
            if (!found)     //if data not found in system collection
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Book not found !");
                Console.ResetColor();
            }
            
            /*using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "DELETE FROM books WHERE Title = @Title ";

                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@Title", title);

                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Data Deleted successfully.");
                    Console.ResetColor();
                }
                else
                {
                    Console.WriteLine();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Failed to delete data.");
                    Console.ResetColor();
                }
                connection.Close();
            }
            updateBooks();      //updating system collection with updated data
            */
    }

    public void displayALLBooks()       //Displaying data in system collection
        {
            /*using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Books";
                MySqlCommand command = new MySqlCommand(query, connection);
                using (MySqlDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Book book = new Book();
                        book.title = reader.GetString(2);
                        book.author = reader.GetString(3);
                        book.BookID = reader.GetInt32(1);
                        booksList.Add(book);
                        //Console.WriteLine(reader["BookID"] + " " + reader["Title"]+" " + reader["Author"]); // Replace with your column names
                        
                    }
                }
            connection.Close();
            }*/
            if (booksList.Count != 0)
            {
                for (int i = 0; i < booksList.Count; i++)
                {
                    Console.WriteLine(i + 1);
                    booksList[i].displayBook();
                    Console.WriteLine("-------------------");
                }
            }
            else
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No books found in the system !!");
                Console.ResetColor();
            }
        }

        public void searchBook(String title)        //Searching specific data in system collection
    {
            Boolean found = false;

            Console.WriteLine();
            foreach (Book book in booksList)
            {
                if (book.title == title)
                {
                    found = true;
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Match Found :");
                    Console.ResetColor();
                    book.displayBook();
                    Console.WriteLine();
                }
            }
            if (!found)
            {
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No match found !");
                Console.ResetColor();
            }
        }
    }

    class Program
    {
        public static void Main(string[] args)
        {

            Library library = new Library();
            library.updateBooks();      //Fetching data from database into system collection

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("	--------Library System--------");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nBooks Count in System: {0}", library.booksCount());
                Console.ResetColor();

                Console.WriteLine();
                Console.WriteLine("1. Add Book");
                Console.WriteLine("2. Display ALL Books");
                Console.WriteLine("3. Search a Book with title");
                Console.WriteLine("4. Delete a Book with title");
                Console.Write("Select an operation to perform: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("System Response ------ ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Book book = new Book();
                        book.addNewBoook();
                        library.addBook(book);
                        Console.WriteLine();
                        break;
                    case "2":
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("System Response ------ ");
                        Console.ResetColor();
                        Console.WriteLine();
                        library.displayALLBooks();
                        Console.WriteLine();
                        break;
                    case "3":
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("System Response ------ ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.Write("Enter book title: ");
                        library.searchBook(Console.ReadLine());
                        Console.WriteLine();
                        break;
                    case "4":
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("System Response ------ ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.Write("Enter book title: ");
                        library.deleteBook(Console.ReadLine());
                        Console.WriteLine();
                        break;
                    default:
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("System Response ------ ");
                        Console.ResetColor();
                        Console.WriteLine();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Invalid Operation !");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                }
            }
        }
    }
