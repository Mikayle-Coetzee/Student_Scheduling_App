#region Documentation Header
// File: IUserRepository.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description:...
#endregion

using System.Collections.Generic;
using System.Net;

namespace _10023767_P2.Model
{
    public interface IUserRepository
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="credential"></param>
        /// <returns></returns>
        bool AuthenticateUser(NetworkCredential credential);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        bool Edit(UserModel userModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userModel"></param>
        /// <returns></returns>
        bool Add(UserModel userModel);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool Delete(int id);
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        UserModel GetById(int id);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        UserModel GetByUsername(string username);

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        IEnumerable<UserModel> GetAll();

    }
}
