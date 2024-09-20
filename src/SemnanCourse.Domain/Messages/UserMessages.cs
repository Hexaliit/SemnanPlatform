using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Messages
{
    public static class UserMessages
    {
        public const string EmailExists = "This E-mail already registered.";
        public const string NotFound = "User with this E-mail not found.";
        public const string UnAuthorized = "Unauthorized User.";
        public const string InsuficentBalance = "Your Balance is Insuficient to have this course.";
        public const string PurchaseNeeded = "You need to buy the course to watch this video.";
    }
}
