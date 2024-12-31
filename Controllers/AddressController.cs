using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
using UxcomexTest.Models;

namespace UxcomexTest.Controllers
{
    public class AddressController(IConfiguration configuration) : Controller
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection");

        // POST: Address/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(int Id, [Bind("Name,City,State,CEP")] Address address)
        {
            // address.CEP = new string(address.CEP.Where(char.IsDigit).ToArray());

            var connection = new SqlConnection(_connectionString);

            var sql = "INSERT INTO Address (Name, City, State, CEP, PersonId) OUTPUT INSERTED.Id VALUES (@Name, @City, @State, @CEP, @Id)";

            var respose = await connection.QuerySingleAsync<int>(sql, new { address.Name, address.City, address.State, address.CEP, Id });

            return RedirectToAction(nameof(Edit), nameof(Person), new { id = Id });
        }

        // GET: Address/Edit/5
        public IActionResult Edit(int id)
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Address WHERE Address.Id = @Id ";


            try
            {
                var address = connection.QuerySingle<Address>(sql, new { id });

                return View(address);
            }
            catch (System.Exception)
            {

                throw;
            }


        }

        // POST: Address/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([Bind("Name,City,State,CEP")] Address address, int id)
        {
            // address.CEP = new string(address.CEP.Where(char.IsDigit).ToArray());

            var connection = new SqlConnection(_connectionString);

            var sql = "UPDATE Address SET Name = @Name, City = @City, State = @State, CEP = @CEP OUTPUT INSERTED.PersonId WHERE Address.Id = @Id";

            var PersonId = await connection.QuerySingleAsync<int>(sql, new { address.Name, address.City, address.State, address.CEP, id });

            return RedirectToAction(nameof(Edit), nameof(Person), new { id = PersonId });
        }

        // GET: Address/Delete/5
        public IActionResult Delete(int id)
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "SELECT * FROM Address WHERE Address.Id = @Id";

            try
            {
                var address = connection.QuerySingle<Address>(sql, new { id });

                return View(address);
            }
            catch (System.Exception)
            {
                return NotFound();
                throw;
            }
        }

        // POST: Address/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var connection = new SqlConnection(_connectionString);

            var sql = "DELETE FROM Address OUTPUT DELETED.PersonId WHERE Address.Id = @Id;";

            var PersonId = await connection.QuerySingleAsync<int>(sql, new { id });

            return RedirectToAction(nameof(Edit), nameof(Person), new { id = PersonId });
        }
    }
}
