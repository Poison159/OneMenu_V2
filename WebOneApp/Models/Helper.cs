using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebOneApp.Models
{
    public class Helper
    {
        public static List<string> getCategoryNames(List<Category> categories) {
            List<string> categoryList = new List<string>();
            foreach (var item in categories){
                categoryList.Add(item.name);
            }
            return categoryList;
        }

        internal static void SortMeals(List<Meal> meals, Dictionary<string, List<Meal>> catAndMeal)
        {
            List<string> categories = getDistinctCategories(meals);
            foreach (var item in categories)
            {
                var listOfMeals = meals.Where(x => x.category == item).ToList();
                catAndMeal.Add(item,listOfMeals);
            }
        }

        private static List<string> getDistinctCategories(List<Meal> list)
        {
            var strLst = new List<string>();
            foreach (var item in list){
                strLst.Add(item.category);
            }
            return strLst.Distinct().ToList();
        }
    }
}