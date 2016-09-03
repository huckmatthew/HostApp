using HostApp.Core.DTO;
using MongoDB.Bson.Serialization.Attributes;

namespace PersonService.DTO
{
    public class PersonDTO : EntityMongoBase
    {

        ///<summary>
        /// Primary key for Person records.
        ///</summary>
        public int businessEntityId { get; set; } // BusinessEntityID (Primary key)

        ///<summary>
        /// Primary type of person: SC = Store Contact, IN = Individual (retail) customer, SP = Sales person, EM = Employee (non-sales), VC = Vendor contact, GC = General contact
        ///</summary>
        /// 
        [BsonElement("PersonType")]
        public string PersonType { get; set; } // PersonType (length: 2)

        ///<summary>
        /// 0 = The data in FirstName and LastName are stored in western style (first name, last name) order.  1 = Eastern style (last name, first name) order.
        ///</summary>
        [BsonElement("NameStyle")]
        public bool NameStyle { get; set; } // NameStyle

        ///<summary>
        /// A courtesy title. For example, Mr. or Ms.
        ///</summary>
        [BsonElement("Title")]
        public string Title { get; set; } // Title (length: 8)

        ///<summary>
        /// First name of the person.
        ///</summary>
        [BsonElement("FirstName")]
        public string FirstName { get; set; } // FirstName (length: 50)

        ///<summary>
        /// Middle name or middle initial of the person.
        ///</summary>
        [BsonElement("MiddleName")]
        public string MiddleName { get; set; } // MiddleName (length: 50)

        ///<summary>
        /// Last name of the person.
        ///</summary>
        [BsonElement("lastName")]
        public string lastName { get; set; } // LastName (length: 50)

        ///<summary>
        /// Surname suffix. For example, Sr. or Jr.
        ///</summary>
        [BsonElement("Suffix")]
        public string Suffix { get; set; } // Suffix (length: 10)

        ///<summary>
        /// 0 = Contact does not wish to receive e-mail promotions, 1 = Contact does wish to receive e-mail promotions from AdventureWorks, 2 = Contact does wish to receive e-mail promotions from AdventureWorks and selected partners.
        ///</summary>
        [BsonElement("EmailPromotion")]
        public int EmailPromotion { get; set; } // EmailPromotion

        ///<summary>
        /// Additional contact information about the person stored in xml format.
        ///</summary>
        [BsonElement("AdditionalContactInfo")]
        public string AdditionalContactInfo { get; set; } // AdditionalContactInfo

        ///<summary>
        /// Personal information such as hobbies, and income collected from online shoppers. Used for sales analysis.
        ///</summary>
        [BsonElement("Demographics")]
        public string Demographics { get; set; } // Demographics

        ///<summary>
        /// ROWGUIDCOL number uniquely identifying the record. Used to support a merge replication sample.
        ///</summary>
        [BsonElement("Rowguid")]
        public System.Guid Rowguid { get; set; } // rowguid

        ///<summary>
        /// Date and time the record was last updated.
        ///</summary>
        [BsonElement("ModifiedDate")]
        public System.DateTime ModifiedDate { get; set; } // ModifiedDate

    }
}
