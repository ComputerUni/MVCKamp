using BusinessLayer.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using DataAccessLayer.Concrete;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Abstract;

namespace BusinessLayer.Concrete
{
    public class StatisticManager : IStatisticService
    {

        ICategoryDal _categoryDal;
        IWriterDal _writerDal;
        IHeadingDal _headingDal;

        public StatisticManager(ICategoryDal categoryDal, IWriterDal writerDal, IHeadingDal headingDal)
        {
            _categoryDal = categoryDal;
            _writerDal = writerDal;
            _headingDal = headingDal;
        }

        public int ActiveCategoriesCount()
        {
            return _categoryDal.List(x => x.CategoryStatus == true).Count();
        }

        public string GetCategoryNameWithMostHeadings()
        {
            return _headingDal.List().GroupBy(x => x.Category.CategoryName).OrderByDescending(z => z.Count()).Select(y => y.Key).FirstOrDefault();
        }

        public int GetCategoryStatusDifference()
        {
            int active = _categoryDal.List(x => x.CategoryStatus == true).Count();
            int inactive = _categoryDal.List(x => x.CategoryStatus == false).Count();
            return (active - inactive);
        }

        public int GetSoftwareCategoryHeadingCount()
        {
            return _headingDal.List(x => x.Category.CategoryName == "Yazılım").Count();
        }

        public int GetTotalCategoryCount()
        {
            return _categoryDal.List().Count();
        }

        public int GetWriterCountWithA()
        {
            return _writerDal.List(x => x.WriterName.Contains("a") || x.WriterName.Contains("A")).Count();
        }

        public int InActiveCategoriesCount()
        {
            return _categoryDal.List(x => x.CategoryStatus == false).Count();
        }
    }

}
