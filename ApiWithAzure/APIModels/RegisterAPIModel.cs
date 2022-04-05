using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiWithAzure.APIModels
{
    /// <summary>
    /// API model for registering a new user
    /// </summary>
    public class RegisterAPIModel
    {
        /// <summary>
        /// The request model
        /// </summary>
        public class Request
        {
            /// <summary>
            /// The user name for the new user, must be unique
            /// </summary>
            /// <example>Test</example>
            [Required]
            [MaxLength(50)]
            public string FirstName { get; set; }

            /// <summary>
            /// The user name for the new user, must be unique
            /// </summary>
            /// <example>User</example>
            [Required]
            [MaxLength(50)]
            public string LastName { get; set; }

            /// <summary>
            /// The email of the new user, must be unique
            /// </summary>
            /// <example>testuser@gmail.com</example>
            [EmailAddress]
            [MaxLength(50)]
            public string Email { get; set; }

            /// <summary>
            /// The passward for the new user
            /// Must be more than 8 digits
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Password { get; set; }
        }

        /// <summary>
        /// The response model
        /// </summary>
        public class Response 
        {
            /// <summary>
            /// The userName of the created user
            /// </summary>
            public string UserName { get; set; }

            /// <summary>
            /// The userName of the created user
            /// </summary>
            public string FirstName { get; set; }

            /// <summary>
            /// The userName of the created user
            /// </summary>
            public string LastName { get; set; }

            /// <summary>
            /// The email of the created user
            /// </summary>
            public string Email { get; set; }
        }
    }
}
