using Dapper;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMS.Dapper.Infrastructure;
using TMS.Dapper.Interface;
using TMS.DataEntities.Users;

namespace TMS.Dapper.Services
{
   public class UserService : IUser
    {
        //IConnectionFactory _connectionFactory;
        //public UserService(IConnectionFactory connectionFactory)
        //{
        //    _connectionFactory = connectionFactory;
        //}
        private readonly IOptions<TMSConfiguration> _configs;

        public UserService(IOptions<TMSConfiguration> Configs)
        {
            _configs = Configs;
        }

        /// <summary>
        /// User Authenticate method for validate username and password
        /// </summary>
        /// <param name="username">username</param>
        /// <param name="password">password</param>
        /// <returns>object of userdetails </returns>
        public async Task<User> Authenticate(string username, string password)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configs.Value.DbConnectionString))
                {
                    var query = "USP_VALIDATEUSER";
                    var param = new DynamicParameters();
                    param.Add("@USERNAME", username);
                    param.Add("@PASSWORD", password);

                    User userDetails = await con.QueryFirstOrDefaultAsync<User>(query, param, commandType: CommandType.StoredProcedure);
                    //User userDetails = await SqlMapper.QueryFirstOrDefaultAsync<User>(_connectionFactory.GetConnection, );
                    if (userDetails == null)
                        return null;

                    return userDetails;
                }
            }catch(Exception ex)
            {
                throw ex;
            }
            
           
         
           
        }

        public async Task<int> RegisterUser(UserRegistration objUser)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(_configs.Value.DbConnectionString))
                {
                    var query = "USP_REGISTERUSER";
                    var param = new DynamicParameters();
                    param.Add("@USRNAME", objUser.UserName);
                    param.Add("@PASSWORD", objUser.Password);
                    param.Add("@FIRSTNAME", objUser.FirstName);
                    param.Add("@LASTNAME", objUser.LastName);
                    param.Add("@EMAILADDRESS", objUser.EmailAddress);
                    param.Add("@ORGNAME", objUser.OrgName);
                    param.Add("@ADDRESS", objUser.OrgAddress);
                    param.Add("@CONTACTNUMBER", objUser.OrgContactNumber);

                    int Result= await con.ExecuteScalarAsync<int>(query, param, commandType: CommandType.StoredProcedure);
                    //User userDetails = await SqlMapper.QueryFirstOrDefaultAsync<User>(_connectionFactory.GetConnection, );
                    return Result;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
