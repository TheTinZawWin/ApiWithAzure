using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ApiWithAzure.APIModels
{
    /// <summary>
    /// API model for creating member
    /// </summary>
    public class UpdateMemberAPIModel
    {
        /// <summary>
        /// The request model
        /// </summary>
        public class Request
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
