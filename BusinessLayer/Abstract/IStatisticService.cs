using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Abstract
{
    public interface IStatisticService
    {
        int GetTotalCategoryCount();
        int GetSoftwareCategoryHeadingCount();
        int GetWriterCountWithA();
        string GetCategoryNameWithMostHeadings();
        int GetCategoryStatusDifference();
        int ActiveCategoriesCount();
        int InActiveCategoriesCount();

    }
}
