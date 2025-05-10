using System.ComponentModel.DataAnnotations;
using CustomAth.Models;
namespace TestProject2;

[TestClass]
public class UnitTest1
{
    [TestClass]
    public class UserAccountTests
    {
        private List<ValidationResult> ValidateModel(UserAccount model)
        {
            var results = new List<ValidationResult>();
            var context = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, context, results, true);
            return results;
        }

        [TestMethod]
        public void UserAccount_Should_Be_Valid_When_All_Fields_Are_Correct()
        {
            var user = new UserAccount
            {
                FirstName = "Yassine",
                LastName = "Hammi",
                UserName = "admin",
                Email = "admin@example.com",
                Password = "StrongPass123",
                RoleId = 1,
                timeStamp = DateTime.Now,
                Role = new Role { Id = 1, Name = "Admin" }
            };

            var results = ValidateModel(user);

            Assert.AreEqual(0, results.Count);
        }

        [TestMethod]
        public void UserAccount_Should_Fail_When_Required_Field_Missing()
        {
            var user = new UserAccount(); 

            var results = ValidateModel(user);

            Assert.IsTrue(results.Any(v => v.ErrorMessage.Contains("First name is required")));
            Assert.IsTrue(results.Any(v => v.ErrorMessage.Contains("Last name is required")));
            Assert.IsTrue(results.Any(v => v.ErrorMessage.Contains("User name is required")));
            Assert.IsTrue(results.Any(v => v.ErrorMessage.Contains("Email name is required")));
            Assert.IsTrue(results.Any(v => v.ErrorMessage.Contains("Password is required")));
        }

        [TestMethod]
        public void UserAccount_Should_Fail_When_Fields_Too_Long()
        {
            var user = new UserAccount
            {
                FirstName = new string('A', 51),
                LastName = new string('B', 51),
                UserName = new string('C', 51),
                Email = new string('D', 51),
                Password = new string('E', 51),
                RoleId = 1
            };

            var results = ValidateModel(user);

            Assert.IsTrue(results.Count > 0);
        }
    }
}