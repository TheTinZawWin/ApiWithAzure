
using ApiWithAzure.APIModels;
using ApiWithAzure.Data.Model;
using ApiWithAzure.Data.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiWithAzure.Controllers
{
    /// <summary>
    /// The controller in charge of all the operation of the Member
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MemberController : ControllerBase
    {
        #region Read Only Feilds

       

        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        #endregion

        #region Constructor

        /// <summary>
        /// Defautl constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="userManager"></param>
        public MemberController(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        #endregion

        /// <summary>
        /// Register a new member
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("CreateUser")]
        public async Task<ActionResult<APIResponse<MemberAPIModel.Response>>> CreateUser(MemberAPIModel.Request requestModel)
        {
            // Get the current logged in user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Initiate the response model
            var responseModel = new APIResponse<MemberAPIModel.Response>();

           
            var email = requestModel.Email.ToLower();
            // Check for the duplication in member name
            if (await _context.Members.FirstOrDefaultAsync(member => member.Email == email) != null)
            {
                responseModel.AddError("This member is already exists");

                return responseModel;
            }

            // Create new member
            var member = new Member()
            {
                Name = requestModel.Name,
                Email=requestModel.Email,
                PhoneNo=requestModel.PhoneNo,
                Age=requestModel.Age
            };


            // Add the member the database and save changes
            await _context.Members.AddAsync(member);
            await _context.SaveChangesAsync();


            // Get all the member
            responseModel.Response = await _context.Members
                .Where(member => member.Name == requestModel.Name && member.Email==requestModel.Email)
                // Assign it to our response
                .Select(member =>
                    // Our response
                    new MemberAPIModel.Response()
                    {
                        // Assign the id
                        Id = member.Id.ToString(),
                        Name = member.Name,
                        Email = member.Email,
                        PhoneNo = member.PhoneNo,
                        Age = member.Age
                    }
                // Return the member
                ).FirstOrDefaultAsync();

           

            // Return the response
            return Ok(responseModel);

        }

        /// <summary>
        /// End point to get the details of a member by Id
        /// </summary>
        /// <param name="id">The id of the member</param>
        /// <returns></returns>
        [HttpGet("getMemberById/{id}")]
        public async Task<ActionResult<APIResponse<MemberAPIModel.Response>>> GetMemberById(int id)
        {
            // Get the current logged in user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Initiate the response model
            var responseModel = new APIResponse<MemberAPIModel.Response>();


            // Get all the member
            responseModel.Response = await _context.Members
                .Where(member => member.Id == id )
                // Assign it to our response
                .Select(member =>
                    // Our response
                    new MemberAPIModel.Response()
                    {
                        // Assign the id
                        Id = member.Id.ToString(),
                        Name = member.Name,
                        Email = member.Email,
                        PhoneNo = member.PhoneNo,
                        Age = member.Age
                    }
               
                ).FirstOrDefaultAsync();


            // Returning the response
            return Ok(responseModel);
        }

        /// <summary>
        /// End point to search for a members 
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("FindMember")]
        public async Task<ActionResult<APIResponse<List<SearchMemberAPIModel.Response>>>> GetMemberByUserName(SearchMemberAPIModel.Request requestModel)
        {
            // Get the current logged in user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var responseModel = new APIResponse<List<SearchMemberAPIModel.Response>>();

           
            var email = requestModel.Email;
            var phono = requestModel.PhoneNo;


            responseModel.Response = (await _context.Members
                .ToListAsync())
                .Select(member => new SearchMemberAPIModel.Response()
                {
                    Id = member.Id.ToString(),
                    Name = member.Name,
                    Email = member.Email,
                    PhoneNo = member.PhoneNo,
                    Age = member.Age,
                   
                }).ToList();

            if (!String.IsNullOrEmpty(email))
            {
                responseModel.Response = responseModel.Response.Where(user => user.Email.Contains(email)).OrderBy(x=>x.Email).ToList();
            }
            if (!String.IsNullOrEmpty(phono))
            {
                responseModel.Response = responseModel.Response.Where(user => user.PhoneNo.Contains(phono)).OrderBy(x => x.PhoneNo).ToList();
            }
            // Returning the response
            return Ok(responseModel);
        }

        /// <summary>
        /// End point to update the member
        /// </summary>
        /// <param name="requestModel"></param>
        /// <returns></returns>
        [HttpPost("Update")]
        public async Task<ActionResult<APIResponse>> UpdateChore(UpdateMemberAPIModel.Request requestModel)
        {
            // Get the current logged in user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var responseModel = new APIResponse<UpdateMemberAPIModel.Response>();

            int id = int.Parse(requestModel.Id);


            // Get all the member
            var member = await _context.Members
                 .Where(member => member.Id == id)
                 // Assign it to our response
                 .Select(member =>
                     // Our response
                     new  MemberAPIModel.Response
                     {
                        // Assign the id
                        Id = member.Id.ToString(),
                         Name = member.Name,
                         Email = member.Email,
                         PhoneNo = member.PhoneNo,
                         Age = member.Age
                     }
                 // Return the member
                 ).FirstOrDefaultAsync();

            // Checck if the user is a member  exist
            if (member == null)
            {
                responseModel.AddError("Invlid member Id");

                // Return the response
                return responseModel;
            }
           

            var updatemember = new Member();
            updatemember.Id = id;
            updatemember.Name = requestModel.Name;
            updatemember.Email = requestModel.Email;
            updatemember.PhoneNo = requestModel.PhoneNo;
            updatemember.Age = requestModel.Age;
            // Update the chore
            _context.Members.Update(updatemember);
            // Save the changes
            await _context.SaveChangesAsync();

            return Ok(responseModel);
        }

        /// <summary>
        /// End point to get the details of a member by Id
        /// </summary>
        /// <param name="id">The id of the member</param>
        /// <returns></returns>
        [HttpDelete("DeleteMember/{id}")]
        public async Task<ActionResult<APIResponse<MemberAPIModel.Response>>> DeleteMember(int id)
        {
            // Get the current logged in user
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            // Initiate the response model
            var responseModel = new APIResponse<MemberAPIModel.Response>();


            // Get all the member
            var member = await _context.Members
                .Where(member => member.Id == id)
                // Assign it to our response
                .Select(member =>
                    // Our response
                    new Member
                    {
                        // Assign the id
                        Id = member.Id,
                        Name = member.Name,
                        Email = member.Email,
                        PhoneNo = member.PhoneNo,
                        Age = member.Age
                    }
               
                ).FirstOrDefaultAsync();

             _context.Members.Remove(member);
          await  _context.SaveChangesAsync();
            // Returning the response
            return Ok(responseModel);
        }
    }
}
