using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace UserRegistraion_Project
{
    internal class Program
    {
        const int maxlength = 3;
        static int currentattempt = 0;
        static void Main(string[] args)
        {
           


            System.Console.WriteLine("User Signup Console App");
           
                User newUser = GetUserDetails();

                User_data dbManager = new User_data();

           

            dbManager.AddUser(newUser);
                Console.WriteLine("\nUser Login Console App");
            bool isAuthenticated = false;


            for ( currentattempt = 0; currentattempt < maxlength; currentattempt++)
            {

                Console.Write("Enter username: ");
                string loginUsername = Console.ReadLine();

                Console.Write("Enter password: ");
                string loginPassword = Console.ReadLine();


                Console.WriteLine("Enter Email: ");
                string loginEmail = Console.ReadLine();

               // if(dbManager.IsUserExist(loginUsername, loginPassword))
                {

                }

                    // if islocked(username)
                    // checkpassword() 
                    // if passward check failed, getnumberofattempts, 
                    // if(attemtps>3) locked
                    // else updateuserattempts(attempts++, username)
                    //

                isAuthenticated = dbManager.LoginUser(loginUsername, loginPassword);

                //bool isAuthenticated = false;
                // for (int currentattempt = 0; currentattempt < maxlength; currentattempt++)
                //{
                //isAuthenticated = dbManager.LoginUser(loginUsername, loginPassword, loginEmail);

                if (isAuthenticated)
                {
                    Console.WriteLine("Login successful!");
                    break;
                }
                else
                {
                    dbManager.Attempts(loginUsername);
                    Console.WriteLine("Invalid username or password. Login failed. Attempts left: {0}", maxlength - currentattempt - 1);
                    // Console.WriteLine("Invalid username or password. Login failed. Attempts left: {0}", maxlength - currentattempt - 1);
                }
               
                Console.ReadLine();
            }
            if (!isAuthenticated)
            {
                Console.WriteLine("You have reached maxim try " +
                    "our account has been locked");
            }
            Console.ReadLine();
            Environment.Exit(0);

            

        }
       
        
         static User GetUserDetails()
         {

                    System.Console.Write("Enter Id: ");
                    string id = System.Console.ReadLine();

                    System.Console.Write("Enter username: ");
                    string username = System.Console.ReadLine();

                    System.Console.Write("Enter password: ");
                    string password = System.Console.ReadLine();

                    System.Console.Write("Enter Email: ");
                    string email = System.Console.ReadLine();

                    // Hash the password before storing it in the database (for a real-world scenario, use a secure hashing algorithm)
                    // For simplicity, we'll just store the plain text password in this example.

                    return new User { Username = username, UserPassword = password, id = id, Email = email };
         }
    }

}

