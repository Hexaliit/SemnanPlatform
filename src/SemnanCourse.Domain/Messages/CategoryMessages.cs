using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace SemnanCourse.Domain.Messages
{
    public static class CategoryMessages
    {
        public const string NotFound = "Category with the given ID not found.";
        public const string InvalidParent = "Parent ID is invalid or is not null.";
        public const string CategoryNameExists = "Category name is already exists.";
        public const string DependencyExists = "Dependency Exists for this category.";
    }
}
