using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardOrderApp.DAL.Queries.CustomerQuerys
{
    public class CustomerQuery
    {
        public const string GetAllCustomers =
            """
            SELECT customer_id, first_name,
            last_name,middle_name,
            birth_date, phone_number, 
            email
            FROM customer.customer
            where is_active=true;
            """;

        public const string GetCustomerById =
            """
            SELECT customer_id, first_name, last_name,
            middle_name, birth_date, phone_number, 
            email
            FROM customer.customer
            where customer_id=@Id;
            """;
        public const string UpdateCustomerById =
            """
            UPDATE customer.customer
            SET first_name = @FirstName, last_name= @LastName, middle_name= @MiddleName, 
            birth_date= @BirthDate, phone_number= @PhoneNumber, email= @Email
            WHERE customer_id = @Id;
            """;
        public const string UnActiveCustomerById =
            """
            UPDATE customer.customer
            SET  is_active=false
            WHERE customer_id =@id;
            """;

        public const string GetCustomerAndDocumensById =
            """
            SELECT c.customer_id,first_name,last_name,middle_name,birth_date,phone_number,email,
            document_id,cd.customer_id,cd.document_type_id,series,number,issue_date,department_code
            FROM customer.customer c
            join customer.customer_document cd
            on c.customer_id =cd.customer_id
            where c.customer_id=@Id
            
            """;


        public const string CreateCustomer =
           """
            INSERT INTO customer.customer 
            (first_name, last_name, middle_name, birth_date, phone_number, email, is_active) 
            VALUES (@FirstName, @LastName, @MiddleName, @BirthDate, @PhoneNumber, @Email, @IsActive) 
            RETURNING customer_id
            
            """;

    }
}
