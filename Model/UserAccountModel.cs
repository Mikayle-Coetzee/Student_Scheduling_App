#region Documentation Header
// File: UserAccountModel.cs
// Author: Mikayle Coetzee (ST10023767)
// Course: PROG6212 POE 2023
// Part: 2
// Description: ...
#endregion

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _10023767_P2.Model
{
    public class UserAccountModel
    {
        /// <summary>
        /// 
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public byte[] ProfilePicture { get; set; }
    }
}
