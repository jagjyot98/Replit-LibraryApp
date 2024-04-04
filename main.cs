using System;
using System.Collections.Generic;

class Book{
	public string title;
	public string author;
	public int BookID;

	public void addNewBoook(){
		Random random = new Random();
		BookID = new Random().Next(100,500);
		Console.WriteLine("Enter the title of the book");
		title = Console.ReadLine();
		Console.WriteLine("Enter the author of the book");
		author = Console.ReadLine();
	}

	public void displayBook(){
		Console.WriteLine("Book Id: {0}", BookID);
		Console.WriteLine("Title: " + title);
		Console.WriteLine("Author: " + author);
	}
}

class Library
{
	List<Book> booksList = new List<Book>();

	public int booksCount(){
		return booksList.Count;
	}

	public void addBook(Book Book){
		// Book Book = new Book();
		// Book.addNewBoook();
		booksList.Add(Book);

		Console.WriteLine();
		Console.ForegroundColor = ConsoleColor.Green;
		Console.WriteLine("Book Added Successfully !");
		Console.ResetColor();
	}

	public void deleteBook(String title){
		Boolean found = false;
		// Console.Write("Enter book Title: ");
		
		foreach(Book book in booksList)
		{	
			if(book.title == title)//Console.ReadLine())
			{	
				Boolean response = booksList.Remove(book);
				Console.WriteLine();
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("Book Removed Successfully !");
					Console.ResetColor();
					found = true;
					break;
			}
		}
		if(!found)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("Book not found !");
			Console.ResetColor();
		}
	}

	public void displayALLBooks(){
		if(booksList.Count != 0)
		{
			for(int i=0;i<booksList.Count;i++)
			{	
				Console.WriteLine(i+1);
				booksList[i].displayBook();
				Console.WriteLine("-------------------");
			}
		}else{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("No books found in the system !!");
			Console.ResetColor();
		}
	}

	public void searchBook(String title){
		Boolean found = false;

		Console.WriteLine();
		foreach(Book book in booksList)
		{
			if(book.title == title)
			{
				found = true;
				Console.ForegroundColor = ConsoleColor.Green;
				Console.WriteLine("Match Found :");
				Console.ResetColor();
				book.displayBook();
				Console.WriteLine();
			}
		}
		if(!found)
		{
			Console.WriteLine();
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("No match found !");
			Console.ResetColor();
		}
	}
}

class Program {
  public static void Main (string[] args) {
    
		Library library = new Library();
		
		
		while(true){
			Console.ForegroundColor = ConsoleColor.Blue;
			Console.WriteLine ("	--------Library System--------");
			Console.ForegroundColor = ConsoleColor.Red;
			Console.WriteLine("\nBooks Count in System: {0}", library.booksCount());
			Console.ResetColor();
			
			Console.WriteLine();
			Console.WriteLine("1. Add Book");
			Console.WriteLine("2. Display ALL Books");
			Console.WriteLine("3. Search a Book with title");
			Console.WriteLine("4. Delete a Book with title");
			Console.Write("Select an operation to perform: ");
			
			switch(Console.ReadLine()){
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