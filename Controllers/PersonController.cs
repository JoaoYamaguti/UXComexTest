using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using UxcomexTest.Models;
using System.ComponentModel;

namespace UxcomexTest.Controllers
{
    public class PersonController(IConfiguration configuration) : Controller
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        // GET: Person/Index
        public IActionResult Index()
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Person";

            var persons = connection.Query<Person>(sql).ToList();

            return View(persons);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // // POST: Person/Create
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,PhoneNumber,CPF")] Person person)
        {
            // person.PhoneNumber = new string(person.PhoneNumber.Where(char.IsDigit).ToArray());
            // person.CPF = new string(person.CPF.Where(char.IsDigit).ToArray());

            var connection = new SqlConnection(_connectionString);

            var sql = "INSERT INTO Person OUTPUT INSERTED.Id VALUES (@Name, @PhoneNumber, @CPF)";

            var newId = await connection.QuerySingleAsync<int>(sql, new { person.Name, person.PhoneNumber, person.CPF });

            return RedirectToAction(nameof(Edit), new { id = newId });
        }

        // GET: Person/Edit/id
        public async Task<IActionResult> Edit(int id)
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Person LEFT JOIN Address ON Person.Id = Address.PersonId WHERE Person.Id = @Id;";

            var personDictionary = new Dictionary<int, Person>();

            var result = await connection.QueryAsync<Person, Address, Person>(sql,
                (person, address) =>
                {
                    // Se a pessoa ainda não foi registrada no dicionário, cria uma nova entrada
                    if (!personDictionary.TryGetValue(person.Id, out var personEntry))
                    {
                        personEntry = person;
                        personEntry.Addresses = new List<Address>(); // Inicializa a lista de endereços
                        personDictionary.Add(personEntry.Id, personEntry);
                    }

                    // Se o endereço não for nulo, adiciona à lista de endereços da pessoa
                    if (address != null)
                    {
                        personEntry.Addresses.Add(address);
                    }

                    return personEntry;
                },
                new { id },
                splitOn: "Id" // Indica onde a divisão entre Person e Address ocorre
            );

            if (result == null)
            {
                return RedirectToAction(nameof(Index), nameof(Person));
            }

            var personAddress = new PersonAddress
            {
                person = result.FirstOrDefault(),
                address = new Address { Id = 0, Name = "", City = "", State = "", CEP = "", PersonId = 0 }
            };

            return View(personAddress);
        }

        // // POST: Person/Edit/id
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,PhoneNumber,CPF")] Person person, int id)
        {
            // person.PhoneNumber = new string(person.PhoneNumber.Where(char.IsDigit).ToArray());
            // person.CPF = new string(person.CPF.Where(char.IsDigit).ToArray());

            var connection = new SqlConnection(_connectionString);

            var sql = "UPDATE Person SET Name = @Name, PhoneNumber = @PhoneNumber, CPF = @CPF OUTPUT INSERTED.Id WHERE Person.Id = @Id";

            var newInfos = await connection.QuerySingleAsync<Person>(sql, new { person.Name, person.PhoneNumber, person.CPF, id });

            return RedirectToAction(nameof(Index));
        }

        // GET: Person/Delete/5
        public IActionResult Delete(int id)
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Person WHERE Person.Id = @Id";

            try
            {
                var person = connection.QuerySingle<Person>(sql, new { id });

                return View(person);

            }
            catch (System.Exception)
            {
                return NotFound();
                throw;
            }

        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "DELETE FROM Person WHERE Person.Id = @Id;";

            await connection.QueryAsync(sql, new { id });

            return RedirectToAction(nameof(Index));
        }
    }
}
