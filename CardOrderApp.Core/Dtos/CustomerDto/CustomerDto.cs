namespace CardOrderApp.Core.Dtos.Customer
{
    public class CustomerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public DateTime BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        public List<CustomerDocumentDto> Documents { get; set; } = new List<CustomerDocumentDto>();

        public override string? ToString()
        {
            return $"Id={Id}:FirstName={FirstName}:LastName={LastName}:MiddleName={MiddleName}:BirthDate={BirthDate}:" +
                $"PhoneNumber={PhoneNumber}:Email={Email} {string.Join(", ", Documents.Select(d => d.ToString()))}";
        }
    }
}
