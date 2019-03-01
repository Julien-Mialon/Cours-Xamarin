using Common.Core.CQRS;
using Common.Core.Models;
using ServiceStack.DataAnnotations;

namespace TD.Api.Models
{
    public class User : BaseEntity, ICommandUser
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        
        [Index]
        [StringLength(200)]
        public string Email { get; set; }
        
        [StringLength(StringLengthAttribute.MaxText)]
        public string FirstName { get; set; }
        
        [StringLength(StringLengthAttribute.MaxText)]
        public string LastName { get; set; }
        
        [StringLength(200)]
        public string Password { get; set; }
        
        [References(typeof(ImageModel))]
        public int? ImageId { get; set; }
    }
}