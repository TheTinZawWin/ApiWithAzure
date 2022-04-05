using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithAzure.APIModels
{
    /// <summary>
    /// API model for registering a new user
    /// </summary>
    public class MemberAPIModel
    {
       
       
            /// <summary>
            /// The request model
            /// </summary>
            public class Request
            {
            /// <summary>
            /// request of name
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Name { get; set; }


            /// <summary>
            /// request of email
            /// </summary>
            [Required]
            [MaxLength(50)]
            public string Email { get; set; }


            /// <summary>
            /// request of phno
            /// </summary>
            [MaxLength(12)]
            public string PhoneNo { get; set; }

            /// <summary>
            /// request of age
            /// </summary>
            [MaxLength(2)]
            public string Age { get; set; }
        }

            /// <summary>
            /// The response model
            /// </summary>
            public class Response
            {

            /// <summary>
            ///  user id
            /// </summary>
            public string Id { get; set; }
            /// <summary>
            ///  userName
            /// </summary>
            public string Name { get; set; }

                
                /// <summary>
                /// user email 
                /// </summary>
                public string Email { get; set; }

            /// <summary>
            ///user of phone no
            /// </summary>
            public string PhoneNo { get; set; }

            /// <summary>
            /// user age
            /// </summary>
            public string Age { get; set; }
            }
        }
}
