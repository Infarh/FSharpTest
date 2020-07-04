using System;
using System.Data.SqlClient;
using System.Threading.Tasks;
using FSharpTest.Models;

namespace FSharpLib
{
    internal static class PersonService
    {
        private const string __SQLCommand = "SELECt * FROM Persons";
        private const string __ConnectionString = "qwe";
        private static readonly Lazy<Task<Person>> __LazyPersonReceiverCurrentContext =
            new Lazy<Task<Person>>(
                async () =>
                {
                    await using var connection = new SqlConnection(__ConnectionString);
                    await using var cmd = new SqlCommand(__SQLCommand, connection);
                    await using var reader = await cmd.ExecuteReaderAsync();

                    if (!await reader.ReadAsync())
                        throw new InvalidOperationException("Ошибка загрузки данных");

                    var surname = reader["surname"].ToString();
                    var name = reader["name"].ToString();
                    return new Person(surname, name);
                }
            );

        private static readonly Lazy<Task<Person>> __LazyPersonReceiverThreadPool =
            new Lazy<Task<Person>>(
                Task.Run(
                    async () =>
                    {
                        await using var connection = new SqlConnection(__ConnectionString);
                        await using var cmd = new SqlCommand(__SQLCommand, connection);
                        await using var reader = await cmd.ExecuteReaderAsync();

                        if (!await reader.ReadAsync())
                            throw new InvalidOperationException("Ошибка загрузки данных");

                        var surname = reader["surname"].ToString();
                        var name = reader["name"].ToString();
                        return new Person(surname, name);
                    }
                )
            );

        public static async Task Run()
        {
            var context_person = await __LazyPersonReceiverCurrentContext.Value;
            var thread_pool_person = await __LazyPersonReceiverThreadPool.Value;
        }
    }
}