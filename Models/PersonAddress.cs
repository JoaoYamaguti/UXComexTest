
using System.ComponentModel.DataAnnotations;

namespace UxcomexTest.Models;

public class PersonAddress
{
    public required Person person;
    public required Address address;
}