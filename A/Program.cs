using StackExchange.Redis;
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

public class Program
{
    private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    private static IDatabase db = redis.GetDatabase();

    public static void Main()
    {
        // Cria uma lista de objetos Person
        List<Person> persons = new List<Person>
        {
            new Person { Name = "Alice", Age = 30, City = "New York" },
            new Person { Name = "Bob", Age = 25, City = "Los Angeles" },
            new Person { Name = "Charlie", Age = 35, City = "Chicago" }
        };

        // Converte a lista para JSON
        string jsonData = JsonConvert.SerializeObject(persons);

        // Armazena os dados no Redis
        db.StringSet("PersonList", jsonData);

        Console.WriteLine("Lista de pessoas armazenada no Redis com sucesso!");
    }
}
