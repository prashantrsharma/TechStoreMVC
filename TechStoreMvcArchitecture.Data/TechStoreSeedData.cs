using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Text;
using TechStoreMvcArchitecture.Model;

namespace TechStoreMvcArchitecture.Data
{
    public class TechStoreSeedData :DropCreateDatabaseIfModelChanges<TechStoreContext>
    {
        protected override void Seed(TechStoreContext context)
        {
            GetProducts().ForEach(p=>context.Products.Add(p));
            GetSpecifications().ForEach(spec=>context.Specifications.Add(spec));
            context.Commit();
        }

        private static List<Product> GetProducts()
        {
            return new List<Product>
            {
                new Product
                {
                    Name = "Google Pixel 4G LTE2",
                    Description = "Bring the power of Google directly to your fingertips with the Google Pixel. A large 32GB of storage keep data secure, while unlimited cloud storage transfers data as needed, and it's completely automatic. The large 5-inch screen is protected by Corning Gorilla Glass 4 to ensure the Google Pixel remains scratch-free.",
                    Sku = "5656023",
                    NumberInStock = 6
                },
                new Product
                {
                    Name = "Apple MacBook Pro",
                    Description = "It's faster and more powerful than before, yet remarkably thinner and lighter.It has the brightest,most colorful Mac notebook display ever. And it introduces the Touch Bar — a Multi-Touch enabled strip of glass built into the keyboard for instant access to the tools you want,right when you want them. The new MacBook Pro is built on groundbreaking ideas. And it's ready for yours.",
                    Sku = "8532557",
                    NumberInStock = 26
                },
                new Product
                {
                    Name = "Huawei - Smartwatch 42mm Stainless Steel - Silver Leather",
                    Description = "With its Bluetooth interface, this smartwatch easily pairs with your compatible Apple® iOS or Android device and delivers call, text and app notifications to keep you informed. Just say 'OK Google' to control functions using spoken commands.Plus, stay on top of fitness goals with a built-in activity tracker with heart rate monitor.",
                    Sku = "4457500",
                    NumberInStock = 7
                },
                new Product
                {
                    Name = "Apple Iphone XS Max",
                    Description = "The iPhone is a smartphone made by Apple that combines a computer, iPod, digital camera and cellular phone into one device with a touchscreen interface.",
                    Sku = "4459500",
                    NumberInStock = 100
                },
                new Product
                {
                    Name = "Apple iPad",
                    Description = "A new handheld tablet computing device from Apple Inc. that first launched in January 2010. The iPad is designed for consumers who want a mobile device that is bigger than a smartphone but smaller than a laptop for entertainment multimedia. The iPad device is roughly the size of a sheet of paper and weighs 1.5 pounds.",
                    Sku = "12389891",
                    NumberInStock = 75
                }
            };
        }

        private static List<Specification> GetSpecifications()
        {
            return new List<Specification>()
            {
                new Specification
                {
                    Name = "Carrier",
                    Value = "Verizon",
                    ProductId = 1,
                },
                new Specification
                {
                    Name = "Wireless Technology",
                    Value = "4G LTE, CDMA, GSM, WCDMA",
                    ProductId = 1,
                },
                new Specification
                {
                    Name = "Hard Drive Capacity",
                    Value = "128 GB",
                    ProductId = 2,
                },
                new Specification
                {
                    Name = "Hard Drive Type",
                    Value = "Other",
                    ProductId = 2,
                },
                new Specification
                {
                    Name = "Operating System",
                    Value = "Android",
                    ProductId = 3,
                },
                new Specification
                {
                    Name = "Water Resistant",
                    Value = "Yes",
                    ProductId = 3,
                },
                new Specification
                {
                    Name = "Operating System",
                    Value = "iOS",
                    ProductId = 4,
                },
                new Specification
                {
                    Name = "Internal Memory",
                    Value = "8GB",
                    ProductId = 4,
                },
                new Specification
                {
                    Name = "Operating System",
                    Value = "iOS",
                    ProductId = 5,
                },
                new Specification
                {
                    Name = "Internal Memory",
                    Value = "8GB",
                    ProductId = 5,
                }
            };
        }
    }
}
