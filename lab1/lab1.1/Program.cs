using L2O___D09;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Intrinsics.X86;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;
namespace lab1._1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var products = ListGenerators.ProductList;
            //1.Find all products that are out of stock.
            var res1 = ListGenerators.ProductList.Where(p => p.UnitsInStock == 0);
            Console.WriteLine(res1);
            Console.WriteLine("---------------------------------------------------");
            foreach (var unit in res1)
            {
                Console.WriteLine(unit);
            }
            //2.Find all products that are in stock and cost more than 3.00 per unit.
            var res2 = ListGenerators.ProductList.Where(p => p.UnitsInStock > 0 && p.UnitPrice > 3).ToList();
            Console.WriteLine("---------------------------------------------------");
            foreach (var unit in res2)
            {
                Console.WriteLine(unit);
            }
            //3. Returns digits whose name is shorter than their value.
            var arrCount = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
            string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var res3 = Arr.Where((a, i) => a.Length < arrCount[i]);
            Console.WriteLine("---------------------------------------------------");
            foreach (var unit in res3)
            {
                Console.WriteLine(unit);
            }
            /*
             * LINQ - Element Operators
                Use ListGenerators.cs & Customers.xml
             */
            //1. Get first Product out of Stock
            var res4 = products.FirstOrDefault(p => p.UnitsInStock == 0);

            //2. Return the first product whose Price > 1000, unless there is no match, in which case null is returned.
            var res5 = products.FirstOrDefault(p => p.UnitPrice > 1000);

            //3. Retrieve the second number greater than 5 
            int[] Arr1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res6 = Arr1.Where(p => p > 5).ToList()[1];
            /****************************************************************************/
            /*
             
             * LINQ - Set Operators *
                Use ListGenerators.cs & Customers.xml
            

            */

            //1. Find the unique Category names from Product List
            var res7 = ListGenerators.ProductList
                                      .Select(p => p.Category)
                                      .Distinct()
                                      .ToList();
            //2. Produce a Sequence containing the unique first letter from both product and customer names.
            var productFirstLetters = ListGenerators.ProductList.Select(p => p.ProductName[0]).Distinct();


            var customerFirstLetters = ListGenerators.CustomerList.Select(c => c.CompanyName[0]).Distinct();


            var uniqueFirstLetters = productFirstLetters.Union(customerFirstLetters)
                                                        .OrderBy(c => c)
                                                        .ToList();
            //*************
            //3. Create one sequence that contains the common first letter from both product and customer names.
            var productFirstLetters1 = ListGenerators.ProductList
                                                     .Select(p => p.ProductName[0]);

            var customerFirstLetters1 = ListGenerators.CustomerList
                                                      .Select(c => c.CompanyName[0]);

            var commonFirstLetters = productFirstLetters
                                      .Intersect(customerFirstLetters)
                                      .Distinct()
                                      .OrderBy(c => c)
                                      .ToList();
            //********************************
            //4. Create one sequence that contains the first letters of product names that are not also first letters of customer names.
            var productFirstLetters2 = ListGenerators.ProductList
                                        .Select(p => p.ProductName[0]);

            var customerFirstLetters2 = ListGenerators.CustomerList
                                                     .Select(c => c.CompanyName[0]);

            var productOnlyLetters = productFirstLetters
                                      .Except(customerFirstLetters)
                                      .Distinct()
                                      .OrderBy(c => c)
                                      .ToList();
            //*********************************************
            //5. Create one sequence that contains the last Three Characters in each names of all customers and products, including any duplicates

            var productEndings = ListGenerators.ProductList
                                   .Select(p => p.ProductName.Length >= 3 ? p.ProductName[^3..] : p.ProductName);

            var customerEndings = ListGenerators.CustomerList
                                  .Select(c => c.CompanyName.Length >= 3 ? c.CompanyName[^3..] : c.CompanyName);

            var allEndings = productEndings
                             .Concat(customerEndings)
                             .ToList();
            /*****************************************************************************/
            /*LINQ - Aggregate Operators
*/
            /* 1. Uses Count to get the number of odd numbers in the array
            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };*/
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res8 = Arr2.Count(x => x % 2 != 0);

            //2.Return a list of customers and how many orders each has.
            ListGenerators.CustomerList.Select(c => c.Orders.Length);

            //3. Return a list of categories and how many products each has
            var res9 = ListGenerators.ProductList
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                ProductCount = g.Count()
            })
            .ToList();
            Console.WriteLine("---------------------------------------------------");
            foreach (var unit in res9)
            {
                Console.WriteLine(unit);
            }

            /*4. Get the total of the numbers in an array.
            int[] Arr = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };*/
            int[] Arr3 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            var Result = Arr3.Count();

            Console.WriteLine(Result);

            //6. Get the total units in stock for each product category.
            var res10 = ListGenerators.ProductList.Where(p => p.UnitsInStock > 0)
            .GroupBy(p => p.Category)
            .Select(g => new
            {
                Category = g.Key,
                ProductCount = g.Count()
            })
            .ToList();

            foreach (var item in res10)
            {
                Console.WriteLine(item);
            }

            //7. Get the length of the shortest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First).
            string filePath = @"dictionary_english.txt";
            string[] lines = File.ReadAllLines(filePath);

            var res11 = lines.Min(L => L.Length);

            Console.WriteLine(Result);

            //8. Get the cheapest price among each category's products
            var cheapestProductsByCategory = ListGenerators.ProductList
                       .GroupBy(p => p.Category)
                       .Select(g => new
                       {
                           Category = g.Key,
                          cheapestProducts = g.Where(p => p.UnitPrice == g.Min(p2 => p2.UnitPrice)).ToList()
                       })
                       .ToList();

            //9. Get the products with the cheapest price in each category (Use Let)
            var cheapestProductsByCategoryUsingLet =
            from p in ListGenerators.ProductList
            group p by p.Category into g
            let minPrice = g.Min(x => x.UnitPrice)
            from p in g
            where p.UnitPrice == minPrice
            select p;

            //10. Get the length of the longest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First).
            string filePath2 = @"dictionary_english.txt";
            string[] lines2 = File.ReadAllLines(filePath2);

            var res12 = lines.Max(L => L.Length);

            Console.WriteLine(Result);

            //11. Get the most expensive price among each category's products.
            var res13 = ListGenerators.ProductList.GroupBy(p => p.Category)
                        .Select(c => new { mostExPro = c.Max(z => z.UnitPrice) });

            //12. Get the products with the most expensive price in each category.
            var mostExpProductsByCategory = ListGenerators.ProductList
                      .GroupBy(p => p.Category)
                      .Select(g => new
                      {
                          Category = g.Key,
                          mostProducts = g.Where(p => p.UnitPrice == g.Max(p2 => p2.UnitPrice)).ToList()
                      })
                      .ToList();

            //13. Get the average length of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First).
            string filePath3 = @"dictionary_english.txt";
            string[] lines3 = File.ReadAllLines(filePath2);

            var res14 = lines.Average(L => L.Length);

            Console.WriteLine(Result);

            //14. Get the average price of each category's products.
            var avgExpProductsByCategory = ListGenerators.ProductList
                      .GroupBy(p => p.Category)
                      .Select(g => new
                      {
                          Category = g.Key,
                          avg = g.Average(c => c.UnitPrice)
                      })
                      .ToList();
            foreach(var i in avgExpProductsByCategory)
            {
                Console.WriteLine(i);
            }

            /*************************************************************************/
            /*LINQ - Ordering Operators

            Use ListGenerators.cs & Customers.xml
           
          */
            // 1. Sort a list of products by name
            var res15 = ListGenerators.ProductList.OrderBy(p => p.ProductName);

            //2. Uses a custom comparer to do a case-insensitive sort of the words in an array.
            string[] Arr5 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var res16 = Arr5.OrderBy(P => P);
            foreach (var item in res16)
            {
                Console.WriteLine(item);
            }

            //3. Sort a list of products by units in stock from highest to lowest.
            var res17 = ListGenerators.ProductList.OrderByDescending(P => P.UnitsInStock);

            foreach (var item in res17)
            {
                Console.WriteLine(item);
            }

            /* 4. Sort a list of digits, first by length of their name, and then alphabetically by the name itself.
            string[] Arr = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            */
            string[] Arr4 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var res18 = Arr.OrderBy(P => P.Length).ThenBy(P => P);

            foreach (var item in res18)
            {
                Console.WriteLine(item);
            }

            //5. Sort first by word length and then by a case-insensitive sort of the words in an array.
            string[] words = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var res19 = words.OrderBy(P => P.Length).ThenBy(P => P, StringComparer.OrdinalIgnoreCase);

            foreach (var item in res19)
            {
                Console.WriteLine(item);
            }

            /*
           */

            //6. Sort a list of products, first by category, and then by unit price, from highest to lowest.
            var res20 = ListGenerators.ProductList.OrderByDescending(P => P.Category).ThenByDescending(p => p.UnitPrice);

            foreach (var item in res20)
            {
                Console.WriteLine(item);
            }
            // 7. Sort first by word length and then by a case-insensitive descending sort of the words in an array.
            string[] Arr6 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };
            var res21 = Arr6.OrderByDescending(P => P.Length).ThenByDescending(P => P, StringComparer.OrdinalIgnoreCase);

            foreach (var item in res21)
            {
                Console.WriteLine(item);
            }

            // 8. Create a list of all digits in the array whose second letter is 'i' that is reversed from the order in the original array.
            string[] Arr7 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };
            var res22 = Arr7.Where(w => w[1] == 'i').Reverse();

            foreach (var item in res22)
            {
                Console.WriteLine(item);
            }

            /******************************************************************************/
            /*
             LINQ - Partitioning Operators
             */

            //1. Get the first 3 orders from customers in Washington
            var res23 = ListGenerators.CustomerList.Where(c => c.Address == "Washington").Take(3).Select(c => c.Orders);

            foreach (var item in res23)
            {
                Console.WriteLine(item);
            }

            //2. Get all but the first 2 orders from customers in Washington.
            var res24 = ListGenerators.CustomerList.Where(c => c.Address == "Washington").Skip(2).Select(c => c.Orders);


            foreach (var item in res24)
            {
                Console.WriteLine(item);
            }

            //3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
            int[] numbers = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res25 = numbers.TakeWhile((e, i) => e >= i);

            //4. Get the elements of the array starting from the first element divisible by 3.
            int[] numbers1 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res26 = numbers1.SkipWhile(c => c % 3 != 0);

            //5. Get the elements of the array starting from the first element less than its position.
            int[] numbers2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var res27 = numbers1.SkipWhile((c, i) => c >= i);

            /*********************************************************************************************/
            /*
             LINQ - Projection Operators 

             */

            //1. Return a sequence of just the names of a list of products.
            var res28 = ListGenerators.ProductList.Select(p => p.ProductName);

            //2. Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).
            string[] words2 = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            var result = words2.Select(w => new
            {
                Upper = w.ToUpper(),
                Lower = w.ToLower()
            });

            //3. Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.
            var res29 = ListGenerators.ProductList.Select(p => new
            {
                p.ProductName,
                p.Category,
                Price = p.UnitPrice 
            });

            // 4. Determine if the value of ints in an array match their position in the array.
            int[] Arr8 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            var matches = Arr8.Select((value, index) => new { value, index }).Where(x => x.value == x.index);

            // 5. Returns all pairs of numbers from both arrays such that the number from numbersA is less than the number from numbersB.
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };
            var res30 = numbersA.SelectMany(a => numbersB.Where(b => a < b), (a, b) => new { A = a, B = b });
            
            // 6. Select all orders where the order total is less than 500.00.
            var res31 = ListGenerators.CustomerList
                        .SelectMany(c => c.Orders)
                        .Where(o => o.Total < 500.00M);

            //7. Select all orders where the order was made in 1998 or later.
            var res32 = ListGenerators.CustomerList.SelectMany(c => c.Orders)
                                                   .Where(o => o.OrderDate >= new DateTime(1998, 1, 1));

            /**********************************************************************/
            /*
             LINQ - Quantifiers


                Use ListGenerators.cs & Customers.xml
                
               

             */
            // 1.Determine if any of the words in dictionary_english.txt(Read dictionary_english.txt into Array of String First) contain the substring 'ei'.
            string[] dictionary = File.ReadAllLines("dictionary_english.txt");

            bool res34 = dictionary.Any(word => word.Contains("ei"));

            //2. Return a grouped a list of products only for categories that have at least one product that is out of stock.
            var res35 = ListGenerators.ProductList
                                        .GroupBy(p => p.Category)
                                        .Where(g => g.Any(p => p.UnitsInStock == 0));

            //3.Return a grouped a list of products only for categories that have all of their products in stock.
            var result3 = ListGenerators.ProductList
                                        .GroupBy(p => p.Category)
                                        .Where(g => g.All(p => p.UnitsInStock > 0));

            /******************************************************************************************/
            /*
             LINQ - Grouping Operators

                

                Result
                ...
                from 
                form 
                ...
                salt
                last 
                ...
                earn 
                near


             */

            //1. Use group by to partition a list of numbers by their remainder when divided by 5
            int[] numbers3 = Enumerable.Range(0, 15).ToArray();

            var res36 = numbers.GroupBy(n => n % 5);

            //2. Uses group by to partition a list of words by their first letter.Use dictionary_english.txt for Input
            string[] dictionary2 = File.ReadAllLines("dictionary_english.txt");

            var res37 = dictionary2
                .Where(w => !string.IsNullOrWhiteSpace(w))
                .GroupBy(w => char.ToUpper(w[0]));

            //3.Consider this Array as an Input,Use Group By with a custom comparer that matches words that are consists of the same Characters Together
            string[] Arr9 = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };
            var groupedAnagrams = Arr9.Select(w => w.Trim().ToLower())
                                     .GroupBy(w => System.String.Concat(w.OrderBy(c => c)));

        }
    }
}
