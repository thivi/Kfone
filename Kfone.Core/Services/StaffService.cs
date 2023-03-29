using Kfone.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Kfone.Core.Services
{
    public static class StaffService
    {
        public static List<Staff> staffs = new List<Staff>(new Staff[]
        {
            new Staff{name="John Doe", address="20, PG, Colombo-03", contactno="74373876847", dob=DateTime.Parse("03/03/1995"), joinedDate=DateTime.Parse("03/29/2023"), profilePic="https://th.bing.com/th/id/R.569493641bff31b6ee9a484586487b10?rik=KsmvdhoyrlKC7g&pid=ImgRaw&r=0", roles=Roles.Marketing}
        });

        public static List<Staff> GetStaffs()
        {
            return staffs;
        }

        public static void SetStaff(Staff staff)
        {
            staffs.Add(staff);
        }

        public static void DeleteStaff(Staff staff)
        {
            staffs.Remove(staff);
        }
    }
}
