using System;
using Common.Core.Models;
using ServiceStack.DataAnnotations;

namespace TD.Api.Models
{
    public class AuthenticationToken : BaseEntity
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        
        [References(typeof(User))]
        public int UserId { get; set; }
        
        [Index]
        [StringLength(200)]
        public string AccessToken { get; set; }
        
        [Index]
        [StringLength(200)]
        public string RefreshToken { get; set; }
        
        public DateTime ExpirationDate { get; set; }
    }
}