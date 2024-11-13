using StackExchange.Redis;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

public class Program
{
    private static ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("localhost");
    private static IDatabase db = redis.GetDatabase();

    [STAThread]
    public static void Main()
    {
        try
        {
            // Tenta recuperar os dados do Redis
            string jsonData = db.StringGet("PersonList");

            if (!string.IsNullOrEmpty(jsonData))
            {
                // Converte o JSON de volta para uma lista de objetos Person
                List<Person> persons = JsonConvert.DeserializeObject<List<Person>>(jsonData);

                // Exibe os dados em um MessageBox
                string message = "Pessoas recuperadas do Redis:\n";
                foreach (var person in persons)
                {
                    message += $"Nome: {person.Name}, Idade: {person.Age}, Cidade: {person.City}\n";
                }

                MessageBox.Show(message, "Dados do Redis");
            }
            else
            {
                MessageBox.Show("Nenhum dado encontrado no Redis.", "Erro");
            }
			
			// Limpa todas as chaves do banco de dados atual
			db.Execute("FLUSHDB");

			Console.WriteLine("Todos os dados do banco de dados Redis atual foram limpos.");
		
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erro ao acessar o Redis: {ex.Message}", "Erro de Conexão");
        }
    }
}