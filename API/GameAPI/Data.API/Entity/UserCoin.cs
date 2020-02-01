/**
 * Project: Data.API 
 * FileName: UserCoin.cs 
 * EF Version: 6.1.0 - FR: 4.5
 * Description: Short description.
 * Last update: 2020-1-31
 * Author: NGUYỄN ANH QUỐC (ASUS)
 * Email: naquoc1204@gmail.com
 * Phone: 093.123.6699
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.API.Entity
{
    public partial class UserCoin
    {
        public int ID { get; set; }
        public double Coin { get; set; }

        public string UserId { get; set; }
    }
}
