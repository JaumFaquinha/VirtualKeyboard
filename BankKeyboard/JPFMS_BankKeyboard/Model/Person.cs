using MongoDB.Bson;

namespace JPFMS_BankKeyboard.Model
{
    public class Person
    {
        public ObjectId Id { get; set; }
        public string? Name { get; set; }
        public string? Password { get; set; }
        public string? CPF { get; set; }

        public int Attempts { get; set; }

    }
}
