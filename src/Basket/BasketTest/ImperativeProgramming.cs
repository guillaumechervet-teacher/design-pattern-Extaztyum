﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Basket;
using Newtonsoft.Json;

namespace BasketTest
{
    public class ImperativeProgramming
    {
        public static int CalculBasketAmount(List<BasketLineArticle> basketTestBasketLineArticles)
        {
            var amountTotal = 0;
            foreach (var basketLineArticle in basketTestBasketLineArticles)
            {
                // Retrive article from database
                var codeBase = Assembly.GetExecutingAssembly().CodeBase;
                var uri = new UriBuilder(codeBase);
                var path = Uri.UnescapeDataString(uri.Path);
                var assemblyDirectory = Path.GetDirectoryName(path);
                var jsonPath = Path.Combine(assemblyDirectory, "article-database.json");
                IList<ArticleDatabase> articleDatabases =
                    JsonConvert.DeserializeObject<List<ArticleDatabase>>(File.ReadAllText(jsonPath));
                var article = articleDatabases.First(articleDatabase =>
                    articleDatabase.Id == basketLineArticle.Id);
                // Calculate amount
                var amount = 0;
                switch (article.Category)
                {
                    case "food":
                        amount += article.Price * 100 + article.Price * 12;
                        break;
                    case "electronic":
                        amount += article.Price * 100 + article.Price * 20 + 4;
                        break;
                    case "desktop":
                        amount += article.Price * 100 + article.Price * 20;
                        break;
                }

                amountTotal += amount * basketLineArticle.Number;
            }

            return amountTotal;
        }
    }
}