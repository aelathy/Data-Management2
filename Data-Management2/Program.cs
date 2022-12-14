﻿// Data Management Project - 2
#nullable disable
using static System.IComparable;
using System.Text.Json;
Console.Clear();

// List<User> users = new List<User>();
// users.Add(new User(---------, —-------));

// Read user-data file
string jsonString = File.ReadAllText("user-data.json");

// Convert Back -- > Data
List<User> users = JsonSerializer.Deserialize<List<User>>(jsonString);


// List of users
users.Add(new User("aly", "hi"));
users.Add(new User("Mr. Veldkamp", "CS"));
users.Add(new User("Bob", "Password"));

// List of products (including ones not in shopping list)
List<Product> products = new List<Product>();

// All Products
products.Add(new Product("Laptop", "Acer", 800));
products.Add(new Product("Phone", "Samsung", 1000));
products.Add(new Product("Phone", "Apple", 1000));
products.Add(new Product("Monitor", "Asus", 300));
products.Add(new Product("Telivision", "Samsung", 700));
products.Add(new Product("Tablet", "Samsung", 100));
products.Add(new Product("Laptop", "Apple", 2000));
// Temp Shopping List
List<Product> ShoppingList = new List<Product>();

//User Login
bool logInLoop = true;
while (logInLoop)
{
    Console.Write("Username: ");
    string usernameInput = Console.ReadLine();
    Console.Write("Password: ");
    string passwordInput = Console.ReadLine();
    // var shopList = getShopList(usernameInput);
    if (findUser(usernameInput, passwordInput))
    {
        logInLoop = false;
        bool loop = true;
        while (loop)
        {
            var shopList = getShopList(usernameInput);

            // Console.WriteLine("Please log in with your password and username");
            // Console.Write("Username: ");
            // string name = Console.ReadLine();
            // Console.Write("Password");
            // string password = Console.ReadLine();
            // if (name & password equal a user.Username & user.Password) {
            //  In
            // } else {
            //  Not In
            // }
            // Main Menu Loop
            Console.WriteLine("\n Main Menu");
            Console.WriteLine("1. Display All Products");
            Console.WriteLine("2. Look for Product");
            Console.WriteLine("3. Sort & Show Lowest to Highest");
            Console.WriteLine("4. Add Product to Shopping Cart");
            Console.WriteLine("5. Remove Product from Shopping Cart");
            Console.WriteLine("6. Display Shopping Cart");
            Console.WriteLine("7. Exit");
            string menuOption = Console.ReadLine().ToLower();
            Console.WriteLine();


            if (menuOption == "1")
            {
                for (int i = 0; i < products.Count; i++)
                {
                    Console.WriteLine($"{products[i].Type} {products[i].Brand} ${products[i].Price}");
                }
            }
            else if (menuOption == "2")
            {
                bool result = false;
                // Implement Search Program
                Console.Write("Search by Brand: ");
                string brandSearch = Console.ReadLine().ToLower();
                // First letters of the product brand match the letters type (left to right)
                for (int i = 0; i < products.Count; i++)
                {
                    if (brandSearch == products[i].Brand.ToLower())
                    {
                        result = true;
                        Console.WriteLine($"{products[i].Type} {products[i].Brand} ${products[i].Price}");
                    }
                }
                if (!result)
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else if (menuOption == "3")
            {
                // Do Some Type of Sort to Organize Products by Lowest to Highest Price
                for (int i = 0; i < products.Count; i++)
                {
                    for (int j = 0; j < products.Count - (i + 1); j++)
                    {
                        int compare = products[j].Price.CompareTo(products[j + 1].Price);
                        if (compare == 1)
                        {
                            int chng = products[j + 1].Price;
                            products[j + 1].Price = products[j].Price;
                            products[j].Price = chng;
                        }
                    }
                }

            }
            else if (menuOption == "4")
            {
                bool result = false;
                // Add Product to Shopping List
                Console.WriteLine("Enter the type & brand of the product you want to ADD:");
                Console.Write("Type: ");
                string addType = Console.ReadLine().ToLower();
                Console.Write("Brand: ");
                string addBrand = Console.ReadLine().ToLower();
                for (int i = 0; i < products.Count; i++)
                {
                    if (addType == products[i].Type.ToLower() && addBrand == products[i].Brand.ToLower())
                    {
                        // Product Found
                        result = true;
                        if (ShoppingList.Count > 0)
                        {
                            foreach (Product item in shopList)
                            {
                                if (addType == item.Type.ToLower() && addBrand == item.Brand.ToLower())
                                {
                                    Console.WriteLine("Item already in shopping cart.");
                                }
                                else
                                {
                                    shopList.Add(products[i]);
                                    Console.WriteLine("Item added to shopping cart.");
                                    break;
                                }
                            }
                        }
                        else
                        {
                            shopList.Add(products[i]);
                            Console.WriteLine("Item addded to shopping cart.");
                        }
                    }
                }
                if (!result)
                {
                    Console.WriteLine("Product not found.");
                }
            }
            else if (menuOption == "5")
            {
                // Remove Product from Shopping List
                if (shopList.Count > 0)
                {
                    bool result = false;
                    Console.WriteLine("Enter the type & brand of the product you want to REMOVE:");
                    Console.Write("Type: ");
                    string addType = Console.ReadLine().ToLower();
                    Console.Write("Brand: ");
                    string addBrand = Console.ReadLine().ToLower();
                    for (int j = 0; j < shopList.Count; j++)
                    {
                        if (addType == shopList[j].Type.ToLower() && addBrand == shopList[j].Brand.ToLower())
                        {
                            result = true;
                            shopList.Remove(shopList[j]);
                            Console.WriteLine("Item removed from shopping cart.");
                        }
                    }
                    if (!result)
                    {
                        Console.WriteLine("Item not found in shopping cart.");
                    }
                }
                else
                {
                    Console.WriteLine("Shopping cart is already empty.");
                }
            }
            else if (menuOption == "6")
            {
                // Display Shopping Cart
                if (shopList.Count > 0)
                {
                    for (int i = 0; i < shopList.Count; i++)
                    {
                        Console.WriteLine($"{shopList[i].Type} {shopList[i].Brand} ${shopList[i].Price}");
                    }
                }
                else
                {
                    Console.WriteLine("Shopping cart empty.");
                }
            }
            else if (menuOption == "7")
            {
                break;
            }
        }
    }
}

bool findUser(string username, string password)
{
    foreach (User user in users)
    {
        if (user.Username == username && user.Password == password)
        {
            Console.WriteLine($"Welcome, {username}");
            return true;
        }
    }
    Console.WriteLine("Username and/or password not found.");
    return false;
}

List<Product> getShopList(string username)
{
    foreach (User user in users)
    {
        if (user.Username == username)
        {
            var shopList = user.ShopList;
            return user.ShopList;
        }
    }

    return null;
}

// void logIn(string username, string password)
// {
//     if (findUser(username, password))
//     {
//         Console.WriteLine($"Welcome, {username}");
//         Start();
//     }
// }

// Convert --> JSON string
jsonString = JsonSerializer.Serialize(users);

// Store in user-data file
File.WriteAllText("user-data.json", jsonString);


class Product
{
    public string Type { get; set; }
    public string Brand { get; set; }
    public int Price { get; set; }

    public Product(string type, string brand, int price)
    {
        this.Type = type;
        this.Brand = brand;
        this.Price = price;
    }
}

class User
{
    public string Username { get; set; }
    public string Password { get; set; }
    public List<Product> ShopList { get; set; }

    public User(string username, string password)
    {
        this.Username = username;
        this.Password = password;
        this.ShopList = new List<Product>();
    }

    public override string ToString()
    {
        return $"({this.Username},{this.Password}, {this.ShopList})";
    }
}